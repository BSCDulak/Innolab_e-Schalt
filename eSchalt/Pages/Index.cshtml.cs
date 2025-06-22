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

}