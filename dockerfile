# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo .csproj a la imagen
COPY plantilla.Web.csproj ./plantilla.Web.csproj

# Verifica que el archivo .csproj se haya copiado correctamente
RUN ls -l

# Restaura dependencias usando el archivo .csproj
RUN dotnet restore plantilla.Web.csproj

# Copia el resto del código
COPY . .

# Compila la aplicación
RUN dotnet publish -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "plantilla.Web.dll"]
