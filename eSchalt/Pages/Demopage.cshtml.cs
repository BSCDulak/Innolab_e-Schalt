using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSchalt.Backend;
using eSchalt.Backend.Models;

namespace eSchalt.Pages;

public class DemopageModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public string ComponentInfo { get; set; }

    public DemopageModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        // Initial page load (no component clicked)
        ComponentInfo = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync(string componentId)
    {
        if (string.IsNullOrEmpty(componentId))
        {
            ComponentInfo = "No component selected.";
            return Page();
        }

        // Query the database for the component based on its ID (example logic)
        var result = await _context.Eschalttabledemo
            .Where(e => e.ComponentId == componentId) // Assuming "ComponentId" exists in your table
            .FirstOrDefaultAsync();

        if (result != null)
        {
            // Display relevant info about the component
            ComponentInfo = $"Component ID: {result.ComponentId}\n" +
                            $"Stockwerk: {result.StockwerkKurz}\n" +
                            $"SPS Position: {result.SpsPositionImArray}\n" +
                            $"Ausgang: {result.Ausgang}";
        }
        else
        {
            ComponentInfo = "No matching component found.";
        }

        return Page();
    }
}