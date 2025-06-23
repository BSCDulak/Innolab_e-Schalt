from ultralytics import YOLO
import os
import yaml

# Basisverzeichnis
base_dir = os.path.abspath(os.path.dirname(__file__))

# Arbeitsverzeichnis
os.chdir(base_dir)

# Pfad zur data.yaml
data_path = os.path.join("dataset", "data.yaml")

# Debugging, um zu überprüfen, ob die Datei existiert
print("Pfad zu data.yaml:", data_path)
print("Existiert data.yaml?", os.path.exists(data_path))

# Dynamisches Laden und Anpassen der data.yaml
with open(data_path, 'r') as file:
    data_config = yaml.safe_load(file)

# Alle Pfade in der data.yaml an das aktuelle Verzeichnis anpassen
data_config['train'] = os.path.join(base_dir, "dataset", "train", "images")
data_config['val'] = os.path.join(base_dir, "dataset", "valid", "images")
data_config['test'] = os.path.join(base_dir, "dataset", "test", "images")

# Temporäre data.yaml schreibne
temp_data_path = os.path.join(base_dir, "temp_data.yaml")
with open(temp_data_path, 'w') as file:
    yaml.safe_dump(data_config, file)

# Debugging -> neue yaml-Datei prüfen
print("Temporäre data.yaml erstellt:", temp_data_path)

# Vortrainiertes Modell laden
model = YOLO("yolov8s-seg.pt") #n steht für die nano-Version - haben es jetzt zu s geändert, da wir mehr Genauigkeit wollen

# Training starten
model.train(
    data=temp_data_path,      # Pfad zur temporären YAML
    epochs=60,                # Anzahl der Trainingsdurchläufe - für den letzten Durchlauf von 50 auf 60 erhöht
    imgsz=640,                
    batch=4,                 # Batch-Größe
    project="Training-Output", # Speicherort
    name="experiment1",       # Name des Ordners (Ziffer erhöht sich durch ultralytics-Logik)
)

# validierung
results = model.val()
print("Validierungsergebnisse:", results)

# Ergebnisse in Log-Datei speichern
with open("training-log.txt", "w") as logfile:
    logfile.write(str(results))

#Temporäre Datei löschen 
os.remove(temp_data_path)
