# Innolab_e-Schalt

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
Docker images builden: `docker-compose build`


## ASP.NET Applikation
### Starten
`docker-compose up -d`

*-d lässt die container im Hintergrund laufen*

Website ist lokal unter `localhost` bzw. `https://localhost/` aufrufbar

bei Änderungen schneller neubuilden und neustarten `docker-compose up -d --build`