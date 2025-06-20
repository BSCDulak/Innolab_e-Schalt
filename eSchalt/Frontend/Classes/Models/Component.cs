namespace eSchalt.Frontend.Classes.Models;

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Position on the photograph
    public int XPosTopLeft { get; set; }
    public int YPosTopLeft { get; set; }
    public int XPosBottomRight { get; set; }
    public int YPosBottomRight { get; set; }
    
    // Percentage for style attributes of the button
    public double ButtonTop { get; set; } = 0;
    public double ButtonLeft { get; set; } = 0;
    public double ButtonWidth { get; set; } = 0;
    public double ButtonHeight { get; set; } = 0;

    public List<Component> Connections { get; } = new();

    public Component(int id, string name, int xPos1, int yPos1, int xPos2, int yPos2)
    {
        Id = id;
        Name = name;
        UpdatePosition(xPos1, yPos1, xPos2, yPos2);
    }

    public void UpdatePosition(int xPos1, int yPos1, int xPos2, int yPos2)
    {
        XPosTopLeft = xPos1;
        YPosTopLeft = yPos1;
        XPosBottomRight = xPos2;
        YPosBottomRight = yPos2;
    }

    public void AddConnection(Component connection)
    {
        if (!Connections.Contains(connection))
            Connections.Add(connection);
        if (!connection.Connections.Contains(this))
            connection.Connections.Add(this);
    }
}