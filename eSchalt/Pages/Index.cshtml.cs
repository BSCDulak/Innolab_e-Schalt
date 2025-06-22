using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eSchalt.Backend;
using eSchalt.Backend.Models;

namespace eSchalt.Pages;

public class IndexModel : PageModel
{
    //private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public string AusgangResult { get; set; } = "";
    
    /*
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    */
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostGetAusgangAsync()
    {
        // Query the database
        var result = await _context.HardcodingDbTest
            .Where(e => e.SpsPositionImArray == "1")
            .FirstOrDefaultAsync(); // Get the first entry or null if no match

        // Check if a result was found
        if (result != null)
        {
            AusgangResult = result.Ausgang; // Set the Ausgang value
        }
        else
        {
            AusgangResult = "No matching entry found."; // If no match is found
        }

        return Page(); // Return the page with the updated model data
    }
}