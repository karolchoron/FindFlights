# U¿ywamy obrazu .NET SDK do budowania aplikacji Blazor WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Ustawiamy katalog roboczy
WORKDIR /src

# Kopiujemy pliki projektu Blazor do kontenera
COPY . .

# Przywracamy zale¿noœci NuGet
RUN dotnet restore

# Budujemy aplikacjê w trybie Release
RUN dotnet publish -c Release -o /app/publish

# Tworzymy nowy, mniejszy obraz do hostowania statycznych plików
FROM nginx:alpine AS final

# Kopiujemy statyczne pliki z etapu build
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Otwieramy port 80 dla aplikacji Blazor
EXPOSE 80

# Uruchamiamy Nginx do serwowania aplikacji Blazor
CMD ["nginx", "-g", "daemon off;"]
