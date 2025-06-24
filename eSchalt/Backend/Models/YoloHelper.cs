using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Backend.Models
{
    // Hilfsklasse zur Vorbereitung der KI-Integration
    public class YoloHelper
    {
        // Diese Methode soll später das hochgeladene Bild nehmen, an den Docker-Container schicken und mit Komponenten zurückkommen

        public async Task<string> AnalyzeImageAsync(IFormFile image)
        {
            // Hier wird Bild gespeichert und an Container übergeben
            // Befehl:
            // docker run --rm -v ${PWD}:/app yolo-infer python predict_single.py [bildpfad]

            // Hier: Ergebnisdateien einlesen (runs/segment/predict/labels)
            // Bounding Boxes extrahieren und Komponenten-ID zuordnen

            return "Analyseergebnis (Platzhalter)";
        }
    }
}
