from ultralytics import YOLO
import os

# Basisverzeichnis bestimmen
base_dir = os.path.abspath(os.path.dirname(__file__))

#das trainierte Modell laden
model = YOLO("Training-Output/experiment1/weights/best.pt")

#Ordner der Testdaten
test_images_folder = os.path.join("dataset", "test", "images")

# Ergebnisse speichern
results = model.predict(
    source=test_images_folder,      # Ordner mit Testbildern
    save=True,
    show=True,
    save_txt=True,
    save_crop=False,                # Zuschneiden von Objekten (kann man aktivieren, falls es benötigt wird)
    project=os.path.join(base_dir, "runs"),  # Speicherordner
    name="predict"                  # Name des Unterordners
)

print("Predict-Funktion wurde ausgeführt.")

#Debugging-Hilfe
output_folder = os.path.join("runs", "predict")
print(f"Ergebnisse wurden im Ordner '{output_folder}[N]' gespeichert.")

for result in results:
    print("Bild:", result.path)
    print("Anzahl erkannter Masken:", result.masks.shape[0])
    for mask in result.masks.data:
        print("Maske erkannt:", mask.shape)

