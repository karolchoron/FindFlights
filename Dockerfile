# --- Etap 1: Budowanie aplikacji Blazor WebAssembly ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopiowanie plików projektu i przywrócenie zależności
COPY . . 
RUN dotnet restore

# Budowanie aplikacji Blazor WebAssembly
RUN dotnet publish -c Release -o /out

# --- Etap 2: Serwer Nginx do hostowania Blazor WASM ---
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Usunięcie domyślnej strony Nginx
RUN rm -rf ./*

# Kopiowanie plików aplikacji z poprzedniego etapu
COPY --from=build /out/wwwroot .

# Konfiguracja Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port dla Render
EXPOSE 80

# Uruchomienie serwera
CMD ["nginx", "-g", "daemon off;"]
