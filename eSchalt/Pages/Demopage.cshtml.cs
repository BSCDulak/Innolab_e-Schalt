using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSchalt.Backend;
using eSchalt.Backend.Models;

namespace eSchalt.Pages;

public class DemopageModel : PageModel
{
    private readonly ApplicationDbContext _context;

    // Property to hold the result
    public string ComponentInfo { get; set; }

    public DemopageModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        // Initial page load, no component selected
        ComponentInfo = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync(int componentId)
    {
        if (componentId <= 0)
        {
            ComponentInfo = "No valid component selected.";
            return Page();
        }

        try
        {
            // Query the database to find the component
            var result = await _context.Eschalttabledemo
                .Where(e => e.ComponentId == componentId) 
                .FirstOrDefaultAsync();

            if (result != null)
            {
                // Format the component info for display
                ComponentInfo = $"Component ID: {result.ComponentId}<br/>" +
                                $"Stockwerk: {result.StockwerkKurz}<br/>" +
                                $"SPS Position: {result.SpsPositionImArray}<br/>" +
                                $"Ausgang: {result.Ausgang}<br/>" +
                                $"Gruppe: {result.Gruppe}<br/>" +
                                $"Leiter: {result.Leiter} <br/>" +
                                $"Sicherung: {result.Sicherung} <br/>" +
                                $"Relais: {result.Relais} <br/>";
            }
            else
            {
                ComponentInfo = $"No matching component found for componentId {componentId}.";
            }
        }
        catch (Exception ex)
        {
            // Log the exception and display an error
            Console.WriteLine($"Error: {ex.Message}");
            ComponentInfo = $"An error occurred while processing the request:{ex.Message}";
        }

        return Page();
    }
}