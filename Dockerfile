# 1. IMAGEN DE CONSTRUCCIÓN (Cocinamos la app)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["FunerariaWeb.csproj", "./"]
RUN dotnet restore "FunerariaWeb.csproj"

COPY . .
RUN dotnet publish "FunerariaWeb.csproj" -c Release -o /app/publish

# 2. IMAGEN DE EJECUCIÓN CON NGINX (Servimos los platos)
# Usamos Nginx en lugar de .NET para correr la web
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copiamos SOLO la carpeta wwwroot (que es lo que ve el navegador)
COPY --from=build /app/publish/wwwroot .

# Creamos una configuración automática para que funcionen las rutas (como /clientes)
# Esto evita el error 404 al recargar la página
RUN echo 'server { listen 80; server_name localhost; location / { root /usr/share/nginx/html; index index.html; try_files $uri $uri/ /index.html; } }' > /etc/nginx/conf.d/default.conf

EXPOSE 80
