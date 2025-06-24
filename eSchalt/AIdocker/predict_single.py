# Importiert mal die ganzen Standardpakete
from ultralytics import YOLO
import sys
import os

# Das sys.argv nimmt den Dateipfad aus dem Befehl und dann wird gecheckt, ob das Bild existiert
image_path = sys.argv[1]
assert os.path.exists(image_path), f"{image_path} nicht gefunden."

# Das lädt unsere KI (best.pt ist unser trainiertes Modell)
model = YOLO("best.pt")

# Das führt die Inferenz (Vorhersage) aus. Es liest das Bild ein, es 
#werden die Masken und Labels erzeugt und speichert das Ergebnisbild
#sowie die Infos dazu (das in der.txt- Datei)
results = model.predict(source=image_path, save=True, save_txt=True)

# Zum Debuggen (gibt aus, welches Bild verarbeitet wurde und wie viele Masken erkannt wurden)
for result in results:
    print(f"Bild: {result.path}")
    print(f"Anzahl erkannter Masken: {len(result.masks.data)}")
