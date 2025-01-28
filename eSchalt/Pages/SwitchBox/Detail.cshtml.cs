using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eSchalt.Backend;
using eSchalt.Backend.Models;

namespace eSchalt.Pages.SwitchBox;

public class DetailpageModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public Eschalttabledemo? Component { get; private set; }

    public async Task<IActionResult> OnGetAsync(int componentId)
    {
        Component = new()
        {
            ComponentId = 6,
            Stockwerk = "EG",
            Gruppe = "E-DO15",
            Leiter = "L2",
            Sicherung = "S15"
        };
        
        return Page();
    }
}