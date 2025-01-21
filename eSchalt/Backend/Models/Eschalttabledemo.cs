namespace eSchalt.Backend.Models
{
    public class Eschalttabledemo
    {
        public string Stockwerk { get; set; }
        public string Raum { get; set; }
        public string Bemerkung { get; set; }
        public string Fi { get; set; }
        public string Leiter { get; set; }
        public string Gruppe { get; set; }
        public string Sicherung { get; set; }
        public string Relais { get; set; }
        public string Dimmer { get; set; }
        public string Ausgang { get; set; }
        public string Eingang { get; set; }
        public string KabelInfo { get; set; }  // Column: "Kabel Info"
        public string Typ { get; set; }
        public string Info { get; set; }  // Column: "Info "
        public string Beschr { get; set; }  // Column: "Beschr. "
        public string StockwerkKurz { get; set; }  // Column: "Stockwerk(kurz)"
        public string SpsPositionImArray { get; set; }  // Column: "SPS Position im array"
    }
}
