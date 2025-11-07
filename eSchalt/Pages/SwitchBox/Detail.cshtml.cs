using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eSchalt.Backend;
using eSchalt.Backend.Repositories;
using eSchalt.Frontend.Classes.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace eSchalt.Pages.SwitchBox;

public class DetailpageModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private const string PresetFolder = "images/electrical_cabinets/presets/";
    private const string TempFolder = "images/uploads/temp/";
    private const string DefaultImage = "images/electrical_cabinets/default.jpg";
    private readonly SwitchBoxRepository _repository;
    
    public string ImagePath { get; private set; }
    public string? FileName { get; set; }
    public int ImageWidth { get; private set; }
    public int ImageHeight { get; private set; }

    public Frontend.Classes.Models.SwitchBox? SwitchBox { get; private set; }
    public Component? SelectedComponent { get; private set; }

    public DetailpageModel(ApplicationDbContext context)
    {
        _context = context;
        _repository = new SwitchBoxRepository(_context);
    }

    private void Initialize()
    {
        SwitchBox = null;
        if (string.IsNullOrEmpty(Request.Cookies["SwitchBoxId"]))
        {
            return;
        }
        if (int.TryParse(Request.Cookies["SwitchBoxId"], out int switchBoxId))
        {
            SwitchBox = _repository.FindById(switchBoxId);
            UpdateImage();
        }
    }

    public async Task<IActionResult> OnGetAsync(string? fileName)
    {
        FileName = fileName;

        // Try to find a SwitchBox via fileName from the QR code
        if (!string.IsNullOrEmpty(fileName))
        {
            var switchBox = _repository.FindByFileName(fileName);
            if (switchBox != null)
            {
                SwitchBox = switchBox;

                // Store the SwitchBoxId in a cookie for consistency
                var qrLink = _context.SwitchBoxQRLinks.FirstOrDefault(l => l.QRLink == fileName);
                if (qrLink != null)
                {
                    Response.Cookies.Append("SwitchBoxId", qrLink.SwitchBoxId.ToString(), new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                }

                UpdateImage();
                return Page();
            }
            else
            {
                // remove old cookie to prevent issues with old SwitchBox if the link couldn't be associated with a Switchbox in SwitchBoxQRLinks table
                if (Request.Cookies.ContainsKey("SwitchBoxId"))
                {
                    Response.Cookies.Delete("SwitchBoxId");
                }
                return RedirectToPage("/Error/NoSwitchBox");
            }
        }

        // Fall back to cookie-based logic if no fileName provided, e.g. if the QR-Code just directs to the details page with no filename provided.
        // This means that someone can use their last cookie, this can prolly be useful for redirect shenanigans but might cause issues if an old cookie
        // should have been removed instead.
        Initialize();

        if (SwitchBox == null)
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }

        return Page();
    }

    private void UpdateImage()
    {
        // Default values
        ImagePath = DefaultImage;

        if (!string.IsNullOrEmpty(FileName))
        {
            // First check Preset folder
            var presetPath = Path.Combine("wwwroot", PresetFolder, FileName);
            var tempPath = Path.Combine("wwwroot", TempFolder, FileName);

            if (System.IO.File.Exists(presetPath))
            {
                ImagePath = PresetFolder + FileName;
            }
            else if (System.IO.File.Exists(tempPath))
            {
                ImagePath = TempFolder + FileName;
            }
        }

        using (var image = Image.Load<Rgba32>("wwwroot/" + ImagePath))
        {
            ImageWidth = image.Width;
            ImageHeight = image.Height;
        }

        // Update percentages for the button for each component
        foreach (Component component in SwitchBox?.Components ?? [])
        {
            // Convert absolute pixel positions to percent to be responsive
            component.ButtonTop = (float)component.YPosTopLeft / ImageHeight * 100;
            component.ButtonLeft = (float)component.XPosTopLeft / ImageWidth * 100;
            component.ButtonHeight = (float)(component.YPosBottomRight - component.YPosTopLeft) / ImageHeight * 100;
            component.ButtonWidth  = (float)(component.XPosBottomRight - component.XPosTopLeft) / ImageWidth * 100;
        }
        // HttpContext.Session.SetInt32("ImageWidth", ImageWidth);
        // HttpContext.Session.SetInt32("ImageHeight", ImageHeight);
    }
    
    public async Task<IActionResult> OnPostAsync(string? fileName)
    {
        FileName = fileName;
        Initialize();
        // SwitchBox = SessionUtility.GetObject<Frontend.Classes.Models.SwitchBox>(HttpContext.Session, "SwitchBox");
        // ImageWidth = HttpContext.Session.GetInt32("ImageWidth") ?? 0;
        // ImageHeight = HttpContext.Session.GetInt32("ImageHeight") ?? 0;
        
        var selectedId = Request.Form["selectedComponent"];
        if (string.IsNullOrEmpty(selectedId))
            return Page();
        
        if (int.TryParse(Request.Form["selectedComponent"], out int id))
            SelectedComponent = SwitchBox?.Components.FirstOrDefault(c => c.Id == id);

        return Page();
    }
}