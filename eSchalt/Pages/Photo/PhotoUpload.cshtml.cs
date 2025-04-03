using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eSchalt.Pages.Photo;

public class PhotoUploadModel : PageModel
{
    private const string TempPath = "wwwroot/images/uploads/temp/";
    
    public void OnGet() { }
    
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

            return RedirectToPage("/SwitchBox/Detail", new { fileName = fileName });
        }
        
        return Page();
    }
}