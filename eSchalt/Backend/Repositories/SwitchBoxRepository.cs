using eSchalt.Frontend.Classes.Models;

namespace eSchalt.Backend.Repositories;

public class SwitchBoxRepository
{
    public SwitchBox? FindById(int id)
    {
        var switchBox = new SwitchBox()
        {
            Floor = "EG",
            Group = "E-DO15"
        };
        var s1 = new Component(1, "S1", 101, 105, 163, 211);
        var s2 = new Component(2, "S2", 166, 111, 213, 215);
        var r1 = new Component(18, "R1", 126, 278, 168, 371);
        s1.AddConnection(s2);
        s1.AddConnection(r1);
        
        switchBox.Components.Add(s1);
        switchBox.Components.Add(s2);
        switchBox.Components.Add(new Component(3, "S3", 215, 105, 252, 215));
        switchBox.Components.Add(new Component(4, "S4", 329, 111, 351, 212));
        switchBox.Components.Add(new Component(5, "S5", 353, 111, 372, 211));
        switchBox.Components.Add(new Component(6, "S6", 374, 111, 393, 209));
        switchBox.Components.Add(new Component(7, "S7", 394, 112, 413, 212));
        switchBox.Components.Add(new Component(8, "S8", 415, 112, 434, 210));
        switchBox.Components.Add(new Component(9, "S9", 435, 104, 455, 210));
        switchBox.Components.Add(new Component(10, "S10", 456, 109, 474, 208));
        switchBox.Components.Add(new Component(11, "S11", 476, 107, 497, 211));
        switchBox.Components.Add(new Component(12, "S12", 548, 109, 583, 208));
        switchBox.Components.Add(new Component(13, "S13", 584, 108, 603, 209));
        switchBox.Components.Add(new Component(14, "S14", 604, 109, 624, 209));
        switchBox.Components.Add(new Component(15, "S15", 626, 110, 644, 208));
        switchBox.Components.Add(new Component(16, "S16", 645, 107, 664, 205));
        switchBox.Components.Add(new Component(17, "S17", 665, 104, 694, 207));
        switchBox.Components.Add(r1);
        switchBox.Components.Add(new Component(19, "R2", 169, 277, 207, 370));
        switchBox.Components.Add(new Component(20, "R3", 207, 275, 249, 370));
        switchBox.Components.Add(new Component(21, "R4", 249, 269, 300, 367));
        switchBox.Components.Add(new Component(22, "R5", 425, 274, 503, 367));
        switchBox.Components.Add(new Component(23, "R6", 504, 272, 579, 363));
        switchBox.Components.Add(new Component(24, "R7", 580, 272, 655, 361));
        switchBox.Components.Add(new Component(25, "R8", 656, 272, 735, 362));
        switchBox.Components.Add(new Component(26, "R9", 561, 411, 652, 508));
        switchBox.Components.Add(new Component(27, "R0", 652, 412, 729, 506));
        return switchBox;
    }
}