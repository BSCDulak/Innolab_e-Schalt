FROM nginx:latest
COPY nginx.conf /etc/nginx/nginx.conf
COPY localhost.cer.pem /etc/ssl/certs/localhost.crt
COPY localhost.key.pem /etc/ssl/private/localhost.key