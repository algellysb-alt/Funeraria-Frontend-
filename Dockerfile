# 1. IMAGEN DE CONSTRUCCIÓN
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# ¡OJO! Cambia 'FunerariaWeb.csproj' por el nombre REAL de tu archivo .csproj de la web
COPY ["FunerariaWeb.csproj", "./"]
RUN dotnet restore "FunerariaWeb.csproj"

# Copia todo lo demás
COPY . .

# Construye la Web
# Cambia 'FunerariaWeb.csproj' por tu nombre real
RUN dotnet publish "FunerariaWeb.csproj" -c Release -o /app/publish

# 2. IMAGEN DE EJECUCIÓN
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

# Cambia 'FunerariaWeb.dll' por el nombre real de tu DLL (suele ser el mismo del proyecto)
ENTRYPOINT ["dotnet", "FunerariaWeb.dll"]