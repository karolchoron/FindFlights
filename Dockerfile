# Build stage - u�ywamy obrazu SDK .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Ustawiamy katalog roboczy
WORKDIR /app

# Kopiujemy plik csproj i przywracamy zale�no�ci (nuget)
COPY *.csproj ./
RUN dotnet restore

# Kopiujemy reszt� plik�w
COPY . ./

# Budujemy aplikacj� Blazor i publikujemy do folderu /out
RUN dotnet publish -c Release -o /out

# Runtime stage - u�ywamy obrazu .NET ASP.NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Ustawiamy katalog roboczy
WORKDIR /app

# Kopiujemy pliki z etapu build (publikacja)
COPY --from=build /out .

# Otwieramy port 80
EXPOSE 80

# Uruchamiamy aplikacj�
ENTRYPOINT ["dotnet", "FindFlights.dll"]