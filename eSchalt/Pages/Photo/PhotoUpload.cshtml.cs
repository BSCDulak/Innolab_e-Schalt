using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eSchalt.Backend;
using eSchalt.Backend.HelperClasses;
using eSchalt.Backend.Models;

namespace eSchalt.Pages.Photo;

public class PhotoUploadModel : PageModel
{
    private const string TempPath = "wwwroot/images/uploads/temp/";

    private readonly AiComponentImportService _aiImportService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApplicationDbContext _context;

    public PhotoUploadModel(
        AiComponentImportService aiImportService,
        IHttpClientFactory httpClientFactory,
        ApplicationDbContext context)
    {
        _aiImportService = aiImportService;
        _httpClientFactory = httpClientFactory;
        _context = context;
    }

    public IActionResult OnGet()
    {
        // Allow access even without a cookie (for creating new switchbox)
        // The update action will check for cookie in OnPostAsync
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(IFormFile? photo, string? action)
    {
        if (photo is { Length: > 0 })
        {
            // Save photo in temp folder
            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);

            int switchBoxId;
            string fileName;

            // Determine which action to take
            if (action == "new")
            {
                // Create a new SwitchBox, for some reaoson we gotta give the full path for the SwitchBox model otherwise it throws error: 'SwitchBox' is a namespace but is used like a type 
                // even though we have the using statement at the top, I gotta find where we use SwitchBox as a namespace
                var newSwitchBox = new eSchalt.Backend.Models.SwitchBox
                {
                    Floor = "",
                    Room = "",
                    Group = "",
                    Type = ""
                };

                _context.SwitchBoxes.Add(newSwitchBox);
                await _context.SaveChangesAsync();
                switchBoxId = newSwitchBox.Id;

                Console.WriteLine($"[PhotoUpload] Created new SwitchBox with Id: {switchBoxId}");

                Response.Cookies.Append("SwitchBoxId", switchBoxId.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                // Generate new filename for new switchbox
                fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
            }
            else
            {
                // Read SwitchBoxId from cookie
                if (!Request.Cookies.TryGetValue("SwitchBoxId", out var switchBoxIdStr) ||
                    !int.TryParse(switchBoxIdStr, out switchBoxId))
                {
                    return RedirectToPage("/Error/NoSwitchBox");
                }

                // Find existing QRLink for this switchbox to reuse the filename
                var existingQRLink = _context.SwitchBoxQRLinks.FirstOrDefault(l => l.SwitchBoxId == switchBoxId);
                if (existingQRLink != null)
                {
                    // Reuse existing filename to keep the same QR code
                    fileName = existingQRLink.QRLink;
                    Console.WriteLine($"[PhotoUpload] Reusing existing filename for override: {fileName}");
                }
                else
                {
                    // No existing link, generate new filename
                    fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                    Console.WriteLine($"[PhotoUpload] No existing QRLink found, generating new filename: {fileName}");
                }
            }

            string filePath = Path.Combine(TempPath, fileName);

            // Overwrite existing file if it exists (for override scenario)
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            try
            {
                // Call AI docker to get JSON for this image
                string aiJson = await CallAiDockerAsync(filePath);

                // Save JSON to file alongside the image (same filename with .json extension)
                // Pretty-print the JSON for easier reading
                string jsonFileName = Path.ChangeExtension(fileName, ".json");
                string jsonFilePath = Path.Combine(TempPath, jsonFileName);

                // Parse json
                var jsonDoc = JsonDocument.Parse(aiJson);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string prettyJson = JsonSerializer.Serialize(jsonDoc, options);

                //The raw json will be saved:
                string rawJsonFileName = Path.GetFileNameWithoutExtension(jsonFileName) + "_raw.json";
                string rawJsonPath = Path.Combine(TempPath, rawJsonFileName);
                await System.IO.File.WriteAllTextAsync(rawJsonPath, prettyJson);
                Console.WriteLine($"[PhotoUpload] Saved RAW AI JSON to: {rawJsonPath}");

                //save a copy of the jsonfile that we change afterwars
                string copyJsonFileName = Path.GetFileNameWithoutExtension(jsonFileName) + "_copy.json";
                string copyJsonPath = Path.Combine(TempPath, copyJsonFileName);
                await System.IO.File.WriteAllTextAsync(copyJsonPath, prettyJson);
                Console.WriteLine($"[PhotoUpload] Saved COPY AI JSON to: {copyJsonPath}");

                //load the pattern and correct the copy
                string patternPath = GetPatternPathForSwitchBox(switchBoxId);
                string correctedJson = PatternCorrector.Correct(aiJson, patternPath);
                await System.IO.File.WriteAllTextAsync(copyJsonPath, correctedJson);
                Console.WriteLine($"[PhotoUpload] Corrected COPY JSON via pattern: {copyJsonPath}");

                // Save the corrected JSON file (this is the one that will be moved to presets)
                await System.IO.File.WriteAllTextAsync(jsonFilePath, correctedJson);
                Console.WriteLine($"[PhotoUpload] Saved corrected JSON to: {jsonFilePath}");

                //Import the corrected JSON in DB
                await _aiImportService.ImportComponentsAsync(switchBoxId, correctedJson);

                // Show the result on the detail page
                return RedirectToPage("/SwitchBox/Detail", new { fileName = fileName });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing photo: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                return RedirectToPage("/Error/NoSwitchBox");
            }
        }
        
        return Page();
    }

    private async Task<string> CallAiDockerAsync(string filePath)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:8000");

        using var form = new MultipartFormDataContent();
        await using var fileStream = System.IO.File.OpenRead(filePath);

        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        form.Add(fileContent, "file", Path.GetFileName(filePath));

        var response = await client.PostAsync("/predict", form);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return json;
    }


    private string GetPatternPathForSwitchBox(int switchBoxId)
    {
        var patternFile = (switchBoxId == 1) ? "Cabinet1.json" : "Cabinet2.json";

        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(baseDir, "AIdocker", patternFile);
    }


}