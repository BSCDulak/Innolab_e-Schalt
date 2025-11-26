# Wir nutzen ein kleines Linux-Basis-Image (ohne Extras)
FROM python:3.10-slim

# Systembibliotheken installieren, damit OpenCV funktioniert
#libgl1 ist Grafikdarstellung und libglib2.0-0 ist für Threads usw.
RUN apt-get update && apt-get install -y \
    libgl1 \
    libglib2.0-0 \
    && rm -rf /var/lib/apt/lists/*

# Arbeitsverzeichnis im Container festlegen (also alle Befehle laufen im Ordner /app
WORKDIR /app

# Kopiert unsere requirements vom Textfile in den Container und wir installieren diese dann
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Alle restlichen Sachen (zB best.pt, das Testbild usw.) werden auch in den Container kopiert
COPY . .

# Expose FastAPI port
EXPOSE 8000

# Wenn jemand das Image startet wird der Befehl automatisch ausgeführt (kann/muss man noch ändern)
# CMD ["python", "predict_single.py", "testBild.jpg"]
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]
