# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0.100 AS build
WORKDIR /src

# Copia el archivo .csproj y restaura dependencias
COPY src/E-MoveBCN-back.csproj ./E-MoveBCN-back.csproj
RUN dotnet restore E-MoveBCN-back.csproj

# Copia el resto del código fuente
COPY src/. .

# Publica la aplicación
RUN dotnet publish E-MoveBCN-back.csproj -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app .
ENTRYPOINT ["dotnet", "E-MoveBCN-back.dll"]