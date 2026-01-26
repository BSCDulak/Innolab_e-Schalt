using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eSchalt.Backend;
using eSchalt.Backend.Repositories;
using eSchalt.Backend.Models;
using eSchalt.Frontend.Classes.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using QRCoder;
using FrontendComponent = eSchalt.Frontend.Classes.Models.Component;

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
    public bool IsImageInTempFolder { get; private set; }

    public Frontend.Classes.Models.SwitchBox? SwitchBox { get; private set; }
    public FrontendComponent? SelectedComponent { get; private set; }
    public string? QrCodeImageBase64 { get; private set; }

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

                // Store the SwitchBoxId in a cookie for consistency and to use when uploading a new image under the assumption it is the same switchbox
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
                GenerateQrCode();
                return Page();
            }
        }

        // Fall back to cookie-based logic if no fileName provided, or if fileName wasn't found in QRLinks
        // This handles cases where:
        // - QR-Code just directs to the details page with no filename
        // - A new photo was uploaded (has fileName but not in QRLinks table)
        Initialize();

        if (SwitchBox == null)
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }

        GenerateQrCode();
        return Page();
    }

    private void UpdateImage()
    {
        // Default values
        ImagePath = DefaultImage;
        IsImageInTempFolder = false;

        if (!string.IsNullOrEmpty(FileName))
        {
            // First check Preset folder
            var presetPath = Path.Combine("wwwroot", PresetFolder, FileName);
            var tempPath = Path.Combine("wwwroot", TempFolder, FileName);

            if (System.IO.File.Exists(presetPath))
            {
                ImagePath = PresetFolder + FileName;
                IsImageInTempFolder = false;
            }
            else if (System.IO.File.Exists(tempPath))
            {
                ImagePath = TempFolder + FileName;
                IsImageInTempFolder = true;
            }
        }

        using (var image = Image.Load<Rgba32>("wwwroot/" + ImagePath))
        {
            ImageWidth = image.Width;
            ImageHeight = image.Height;
        }

        // Update percentages for the button for each component
        foreach (FrontendComponent component in SwitchBox?.Components ?? [])
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

    private void GenerateQrCode()
    {
        if (string.IsNullOrEmpty(FileName))
        {
            QrCodeImageBase64 = null;
            return;
        }

        // Generate QR code with relative path only so we are host agnostic (also the database QR-Code linking to Switchboxes works with the filename like that)
        var qrCodeUrl = $"/detail?fileName={Uri.EscapeDataString(FileName)}";

        using (var qrGenerator = new QRCodeGenerator())
        {
            var qrCodeData = qrGenerator.CreateQrCode(qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
            using (var qrCode = new PngByteQRCode(qrCodeData))
            {
                var qrCodeBytes = qrCode.GetGraphic(20);
                QrCodeImageBase64 = Convert.ToBase64String(qrCodeBytes);
            }
        }
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
        {
            GenerateQrCode();
            return Page();
        }
        
        if (int.TryParse(Request.Form["selectedComponent"], out int id))
            SelectedComponent = SwitchBox?.Components.FirstOrDefault(c => c.Id == id);

        GenerateQrCode();
        return Page();
    }

    public async Task<IActionResult> OnPostSaveSwitchBoxAsync(string? fileName, string? room, string? floor, string? group, string? type)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }

        // Get SwitchBoxId from cookie
        if (!Request.Cookies.TryGetValue("SwitchBoxId", out var switchBoxIdStr) ||
            !int.TryParse(switchBoxIdStr, out var switchBoxId))
        {
            return RedirectToPage("/Error/NoSwitchBox");
        }

        try
        {
            // Update SwitchBox properties if they were provided
            var dbSwitchBox = _context.SwitchBoxes.FirstOrDefault(sb => sb.Id == switchBoxId);
            if (dbSwitchBox != null)
            {
                dbSwitchBox.Room = room ?? string.Empty;
                dbSwitchBox.Floor = floor ?? string.Empty;
                dbSwitchBox.Group = group ?? string.Empty;
                dbSwitchBox.Type = type ?? string.Empty;
                _context.SwitchBoxes.Update(dbSwitchBox);
                Console.WriteLine($"[Detail] Updated SwitchBox {switchBoxId} properties: Room={room}, Floor={floor}, Group={group}, Type={type}");
            }

            var tempImagePath = Path.Combine("wwwroot", TempFolder, fileName);
            var presetImagePath = Path.Combine("wwwroot", PresetFolder, fileName);

            // Only move image if it exists in temp folder (for new uploads)
            if (System.IO.File.Exists(tempImagePath))
            {
                // Ensure presets directory exists
                var presetDir = Path.Combine("wwwroot", PresetFolder);
                if (!Directory.Exists(presetDir))
                {
                    Directory.CreateDirectory(presetDir);
                }

                // Move image from temp to presets folder
                System.IO.File.Move(tempImagePath, presetImagePath, overwrite: true);
                Console.WriteLine($"[Detail] Moved image from {tempImagePath} to {presetImagePath}");

                // Also move JSON file if it exists
                var tempJsonPath = Path.ChangeExtension(tempImagePath, ".json");
                var presetJsonPath = Path.ChangeExtension(presetImagePath, ".json");
                if (System.IO.File.Exists(tempJsonPath))
                {
                    System.IO.File.Move(tempJsonPath, presetJsonPath, overwrite: true);
                    Console.WriteLine($"[Detail] Moved JSON from {tempJsonPath} to {presetJsonPath}");
                }

                // Check if a SwitchBoxQRLink already exists for this filename
                var existingQRLink = _context.SwitchBoxQRLinks.FirstOrDefault(l => l.QRLink == fileName);

                if (existingQRLink != null)
                {
                    // Update existing QRLink to point to the current SwitchBoxId
                    if (existingQRLink.SwitchBoxId != switchBoxId)
                    {
                        Console.WriteLine($"[Detail] Updating existing QRLink {existingQRLink.Id} from SwitchBoxId {existingQRLink.SwitchBoxId} to {switchBoxId}");
                        existingQRLink.SwitchBoxId = switchBoxId;
                        _context.SwitchBoxQRLinks.Update(existingQRLink);
                    }
                    else
                    {
                        Console.WriteLine($"[Detail] QRLink already exists and points to correct SwitchBoxId {switchBoxId}");
                    }
                }
                else
                {
                    // Create new QRLink
                    var newQRLink = new SwitchBoxQRLink
                    {
                        SwitchBoxId = switchBoxId,
                        QRLink = fileName
                    };
                    _context.SwitchBoxQRLinks.Add(newQRLink);
                    Console.WriteLine($"[Detail] Created new QRLink for SwitchBoxId {switchBoxId} with QRLink {fileName}");
                }
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"[Detail] Successfully saved SwitchBox and QRLink for fileName {fileName}");

            // Redirect back to detail page with the fileName
            return RedirectToPage("/SwitchBox/Detail", new { fileName = fileName });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Detail] Error saving switchbox: {ex.Message}");
            Console.WriteLine($"[Detail] Stack trace: {ex.StackTrace}");
            return RedirectToPage("/Error/NoSwitchBox");
        }
    }
}