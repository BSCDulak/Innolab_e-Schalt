using System.ComponentModel.DataAnnotations.Schema;
namespace eSchalt.Backend.Models

{
    public class Eschalttabledemo
    {
        [Column("stockwerk")]
        public string Stockwerk { get; set; }
        [Column("raum")]
        public string? Raum { get; set; }
        [Column("bemerkung")]
        public string? Bemerkung { get; set; }
        [Column("fi")]
        public string? Fi { get; set; }
        [Column("leiter")]
        public string? Leiter { get; set; }
        [Column("gruppe")]
        public string? Gruppe { get; set; }
        [Column("sicherung")]
        public string? Sicherung { get; set; }
        [Column("relais")]
        public string? Relais { get; set; }
        [Column("dimmer")]
        public string? Dimmer { get; set; }
        [Column("ausgang")]
        public string? Ausgang { get; set; }
        [Column("eingang")]
        public string? Eingang { get; set; }
        [Column("Kabel Info")]
        public string? KabelInfo { get; set; }  // Column: "Kabel Info"
        [Column("typ")]
        public string? Typ { get; set; }
        [Column("Info ")]
        public string? Info { get; set; }  // Column: "Info "
        [Column("Beschr. ")]
        public string? Beschr { get; set; }  // Column: "Beschr. "
        [Column("Stockwerk(kurz)")]
        public string? StockwerkKurz { get; set; }  // Column: "Stockwerk(kurz)"
        [Column("SPS Position im array")]
        public string? SpsPositionImArray { get; set; }  // Column: "SPS Position im array"
    }
}
