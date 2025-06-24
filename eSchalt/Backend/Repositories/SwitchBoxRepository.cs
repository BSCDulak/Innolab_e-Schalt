using eSchalt.Frontend.Classes.Models;
using eSchalt.Backend.Models;
using Microsoft.EntityFrameworkCore;
using BackendSwitchBox = eSchalt.Backend.Models.SwitchBox; // this could be removed but I´ll leave it here for reference and possible future use
using FrontendSwitchBox = eSchalt.Frontend.Classes.Models.SwitchBox;

namespace eSchalt.Backend.Repositories;

public class SwitchBoxRepository
{
    private readonly ApplicationDbContext _context;
    public SwitchBoxRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public FrontendSwitchBox? FindById(int id)
    {
        // Load SwitchBox with Components and their connections
        var dbSwitchBox = _context.SwitchBoxes
            .Include(sb => sb.Components)
            .FirstOrDefault(sb => sb.Id == id);
        if (dbSwitchBox == null)
            return null;

        // Load all connections for components in this switchbox
        var componentIds = dbSwitchBox.Components.Select(c => c.Id).ToList();
        var dbConnections = _context.ComponentConnections
            .Where(cc => componentIds.Contains(cc.FromComponentId) && componentIds.Contains(cc.ToComponentId))
            .ToList();

        // Map backend components to frontend components
        var frontendComponents = dbSwitchBox.Components
            .Select(c => new eSchalt.Frontend.Classes.Models.Component(
                c.Id, c.Name, c.XPosTopLeft, c.YPosTopLeft, c.XPosBottomRight, c.YPosBottomRight))
            .ToDictionary(c => c.Id, c => c);

        // Reconstruct connections in frontend model
        foreach (var conn in dbConnections)
        {
            if (frontendComponents.TryGetValue(conn.FromComponentId, out var from) &&
                frontendComponents.TryGetValue(conn.ToComponentId, out var to))
            {
                from.AddConnection(to);
            }
        }

        // Map SwitchBox
        var frontendSwitchBox = new FrontendSwitchBox
        {
            Floor = dbSwitchBox.Floor,
            Room = dbSwitchBox.Room,
            Group = dbSwitchBox.Group,
            Type = dbSwitchBox.Type,
            Components = frontendComponents.Values.ToList()
        };
        return frontendSwitchBox;
    }

    // Vorbereitung: KI-Bildanalyse
    // public async Task<string> AnalyzeSwitchBoxImageAsync(IFormFile imageFile)
    // {
        // var yoloHelper = new YoloHelper();

        // Übergabe des Bildes an die KI
        // var result = await yoloHelper.AnalyzeImageAsync(imageFile);

        // Was ich noch machen muss: Analyseergebnis weiterverarbeiten

        //return result;
    // }

}
