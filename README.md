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

Nachdem man den Schritten in der Anleitung gefolgt ist und das localhost.pfx in, wenn man das nicht abgeändert hat c:tmp hat kann man dort ein terminal öffnen und
folgende befehle ausführen:

openssl pkcs12 -in localhost.pfx -nocerts -out localhost.key.pem -nodes
openssl pkcs12 -in localhost.pfx -clcerts -nokeys -out localhost.cer.pem

und diese beiden Dateien dann in `/nginx` innerhalb des Projektes reinkopieren.


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

bei Änderungen schneller neubuilden und neustarten `docker-compose up -d --build`

## Backend

Die Struktur der tables ist in Backend/Models zu erkennen, der gemappte Name der table in Backend/DBContext.cs 
Die Datenbank hat standardmäßig keine tables, man kann sie z.B. über pg admin 4 in docker (username und password der db in appsettings.json zu finden) erstellen und mit einer csv Datei befüllen. (csv Datei gibt es im Backend folder) 
Verbindung mit pg admin 4 docker: 

Server registrieren:	 
Connection: "Host name/address" host.docker.internal, "Port" 5432, "Username" siehe appsettings.json 
Rest bleibt Standard. 