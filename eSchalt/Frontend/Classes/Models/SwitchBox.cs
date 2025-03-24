namespace eSchalt.Frontend.Classes.Models;

public class SwitchBox
{
    public string Floor { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public List<Component> Components { get; set; } = new();
}