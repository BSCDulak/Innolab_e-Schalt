using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eSchalt.Backend.HelperClasses;

namespace eSchalt.Pages.Photo;

public class PhotoUploadModel : PageModel
{
    private const string TempPath = "wwwroot/images/uploads/temp/";

    private readonly AiComponentImportService _aiImportService;
    private readonly IHttpClientFactory _httpClientFactory;

    public PhotoUploadModel(
        AiComponentImportService aiImportService,
        IHttpClientFactory httpClientFactory)
    {
        _aiImportService = aiImportService;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(Request.Cookies["SwitchBoxId"]))
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync(IFormFile? photo)
    {
        if (photo is { Length: > 0 })
        {
            // Save photo in temp folder
            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
            string filePath = Path.Combine(TempPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Read SwitchBoxId from cookie
            if (!Request.Cookies.TryGetValue("SwitchBoxId", out var switchBoxIdStr) ||
                !int.TryParse(switchBoxIdStr, out var switchBoxId))
            {
                return RedirectToPage("/Error/NoSwitchBox");
            }

            try
            {
                // Call AI docker to get JSON for this image
                string aiJson = await CallAiDockerAsync(filePath);

                // Save JSON to file alongside the image (same filename with .json extension)
                // Pretty-print the JSON for easier reading
                string jsonFileName = Path.ChangeExtension(fileName, ".json");
                string jsonFilePath = Path.Combine(TempPath, jsonFileName);
                
                // Parse and reformat JSON with indentation for readability
                var jsonDoc = JsonDocument.Parse(aiJson);
                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                };
                string prettyJson = JsonSerializer.Serialize(jsonDoc, options);
                
                await System.IO.File.WriteAllTextAsync(jsonFilePath, prettyJson);
                Console.WriteLine($"[PhotoUpload] Saved pretty-printed AI JSON to: {jsonFilePath}");

                // Import into DB (throws if JSON invalid)
                await _aiImportService.ImportComponentsAsync(switchBoxId, aiJson);

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
}