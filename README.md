# Innolab_e-Schalt

## Command Shortlist
### Docker
```shell
docker-compose up -d
docker exec -it eschalt bash
dotnet ef migrations add IrgendeinMigrationName
```

## Setup
### Prequesites
Nodejs (damit npm läuft) muss installiert sein
https://nodejs.org/en/download

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
Docker container starten : `docker-compose up`

## ASP.NET Applikation
### Starten
`docker-compose up -d`

*-d lässt die container im Hintergrund laufen*

Website ist lokal unter `https://localhost:5000` aufrufbar, nachdem man es in visual studio 2022 oder einer Alternative als http gestartet hat.


## Frontend
### SCSS / CSS
In `Frontend/Scss` kommen alle .scss Files hinein.
Diese werden bei einem image build compiled.

Ein Sass watcher wird automatisch mit dem Sass container ausgeführt.
Damit werden in der Dev Umgebung Änderungen in den scss Dateien sofort erkannt.

Die Files zu den Razor Pages (also Files im Ordner /Pages/...)
können nur CSS-Files sein, keine SASS-Files.


Das Compilen funktioniert nur in der Dev-Umgebung, in der Prod-Umgebung nicht.

## Backend
Es handelt sich um eine postgres Datenbank die in Docker läuft. Dafür muss Docker (Desktop) gestartet sein.
Die Struktur der tables ist in Backend/Models zu erkennen, der gemappte Name der table in Backend/DBContext.cs 
Die Datenbank hat standardmäßig die tables beim runnen von http über die migratinos bekommen und hat ein paar test Werte.
Änderungen im model muss man mit 
dotnet ef migrations add IrgendeinMigrationName 
registrieren sodass sie beim nächsten build/run über https automatisch an die Datenbank angewandt warden.
Dafür muss natürlich die DB auch mit docker-compose up schon laufen.
Wenn man die dotnet tools noch nicht hat, muss man die tools installieren damit der dotnet Befehl funktioniert:
dotnet tool install --global dotnet-ef
dies muss man ein mal pro PC machen und ist unabhängig vom Projekt.

User authentication wird mit ASP.NET Core Identity gemacht, siehe:
For setup and configuration information, see https://go.microsoft.com/fwlink/?linkid=2116645.


Um Inhalte (Nicht Struktur! Das macht man mit migrations) leichter zu verändern ist pgadmin4 sehr hilfreich.

Verbindung mit pgadmin4 im docker: 
docker run --name pgadmin_container -e PGADMIN_DEFAULT_EMAIL=irgendeinenusername -e PGADMIN_DEFAULT_PASSWORD=irgendeinpassword -p 5050:80 -d dpage/pgadmin4
username und pw verwendet man um sich im pgadmin4 container anzumelden als superuser und dann kann man
Server registrieren:	 
Connection: Host name/address: host.docker.internal, Port: 5432, Username und Passwort, siehe: appsettings.json bzw appsettings.Development.json je nach environment.
Rest bleibt Standard. 

## Dev
Bilder Koordinaten: https://www.image-map.net/

`dotnet run --launch-profile "https"`

