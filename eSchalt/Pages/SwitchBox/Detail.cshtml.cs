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
    private const string ImageFolder = "images/uploads/temp/";
    private const string DefaultImage = "images/electrical_cabinets/demopage.jpg";
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
        Initialize();

        if (SwitchBox == null)
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }
        
        // SessionUtility.SetObject(HttpContext.Session, "SwitchBox", SwitchBox);
        // HttpContext.Session.SetObject("SwitchBox", SwitchBox);
        return Page();
    }

    private void UpdateImage()
    {
        ImagePath = FileName != null ? ImageFolder + FileName : DefaultImage;
        if (!System.IO.File.Exists("wwwroot/" + ImagePath))
        {
            ImagePath = DefaultImage;
            FileName = null;
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