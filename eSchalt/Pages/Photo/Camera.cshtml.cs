using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using eSchalt.Backend.HelperClasses;

namespace eSchalt.Pages.Photo;

public class Camera : PageModel
{
    private const string TempPath = "wwwroot/images/uploads/temp/";

    private readonly AiComponentImportService _aiImportService;
    private readonly IHttpClientFactory _httpClientFactory;

    public Camera(AiComponentImportService aiImportService, IHttpClientFactory httpClientFactory)
    {
        _aiImportService = aiImportService;
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Photo { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(Request.Cookies["SwitchBoxId"]))
            return RedirectToPage("/Error/NoSwitchBox");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Photo))
            return Page();

        // read SwitchBoxId from cookie
        if (!Request.Cookies.TryGetValue("SwitchBoxId", out var switchBoxIdStr) ||
            !int.TryParse(switchBoxIdStr, out var switchBoxId))
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }

        try
        {
            // save Base64 image to temp folder
            var (filePath, fileName) = await SaveBase64PhotoToTempAsync(Photo);

            // call ai docker to get JSON for this image
            string aiJson = await CallAiDockerAsync(filePath, fileName);

            // import into DB (throws if JSON is invalid)
            await _aiImportService.ImportComponentsAsync(switchBoxId, aiJson);

            // show the result on the detail page
            return RedirectToPage("/SwitchBox/Detail", new { fileName });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing camera photo: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return RedirectToPage("/Error/NoSwitchBox");
        }
    }

    private async Task<(string FilePath, string FileName)> SaveBase64PhotoToTempAsync(string base64Photo)
    {
        // Supports "data:image/png;base64,AAAA..." and plain "AAAA..."
        var commaIndex = base64Photo.IndexOf(',');
        var base64Data = commaIndex >= 0 ? base64Photo[(commaIndex + 1)..] : base64Photo;

        byte[] bytes = Convert.FromBase64String(base64Data);

        if (!Directory.Exists(TempPath))
            Directory.CreateDirectory(TempPath);

        string fileName = Guid.NewGuid() + ".png";
        string filePath = Path.Combine(TempPath, fileName);

        await System.IO.File.WriteAllBytesAsync(filePath, bytes);

        return (filePath, fileName);
    }

    private async Task<string> CallAiDockerAsync(string filePath, string fileName)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:8000");

        using var form = new MultipartFormDataContent();
        await using var fileStream = System.IO.File.OpenRead(filePath);

        var fileContent = new StreamContent(fileStream);

        // Camera saves PNG by default -> send as PNG
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        form.Add(fileContent, "file", fileName);

        var response = await client.PostAsync("/predict", form);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
