# Innolab_e-Schalt

## Command Shortlist
### Docker
```shell
docker-compose up -d
docker-compose up --build -d
docker exec -it eschalt bash
```

### Frontend
```shell
docker exec -it eschalt bash -c "npm run sass:watch"
```

## Setup
### Zertifikat

Das Projekt verwendet ein Setup, was einem Produktiv Setup sehr ähnlich ist.

Dafür muss jeder zu Beginn ein Zertifikat im Ordner `/nginx` haben.
Es braucht die Dateien `localhost.cer.pem` (Zertifikat) und `localhost.key.pem` (Schlüssel)

*darauf achten, dass das Zertifikat lang genug gültig ist, damit es nicht wieder erneuert werden muss*

**Anleitungen:**

[Windows & Linux](https://www.humankode.com/asp-net-core/develop-locally-with-https-self-signed-certificates-and-asp-net-core/)

[Mac](https://ryanparman.com/posts/2019/how-to-create-local-tls-certificates-for-development-on-macos/)
(beim Key Umwandeln stattdessen folgenden Command: `openssl pkcs12 -in localhost.key.p12 -nocerts -nodes -out localhost.key.pem -legacy`)

### Docker
Docker container starten + images builden: `docker-compose up --build -d`

## ASP.NET Applikation
### Starten
`docker-compose up -d`

*-d lässt die container im Hintergrund laufen*

Website ist lokal unter `localhost` bzw. `https://localhost/` aufrufbar

## Frontend
### SCSS / CSS
In `Frontend/Scss` kommen alle .scss Files hinein.
Diese werden bei einem image build compiled.
Sie können auch mit dem Command 
```shell
docker exec -it eschalt bash -c "npm run sass:watch"
```
gewatched werden (= es hört auf Änderungen und compiled automatisch)
oder die Files können einmalig compiled werden:
```shell
docker exec -it eschalt bash -c "npm run sass"
```

Die Files zu den Razor Pages (also Files im Ordner /Pages/...)
können nur CSS-Files sein, keine SASS-Files.

Das Compilen funktioniert nur in der Dev-Umgebung, in der Prod-Umgebung nicht.