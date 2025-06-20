using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eSchalt.Pages.Photo;

public class QRCode : PageModel
{
    public void OnGet() { }
    
    public async Task<IActionResult> OnPostAsync()
    {
        // todo save actual id of the scanned switchbox
        Response.Cookies.Append("SwitchBoxId", "1");
        return RedirectToPage("/Index");
    }
}