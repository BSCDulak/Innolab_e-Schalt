using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eSchalt.Pages.Photo;

public class Camera : PageModel
{
    private const string TempPath = "wwwroot/images/uploads/temp/";
    
    [BindProperty]
    public string Photo { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(Request.Cookies["SwitchBoxId"]))
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Photo))
        {
            var base64Data = Photo.Split(',')[1];
            var bytes = Convert.FromBase64String(base64Data);

            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);

            var fileName = Guid.NewGuid() + ".png";
            var filePath = Path.Combine(TempPath, fileName);

            await System.IO.File.WriteAllBytesAsync(filePath, bytes);

            return RedirectToPage("/SwitchBox/Detail", new { fileName = fileName });
        }
        
        return Page();
    }
}