# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0.100 AS build
WORKDIR /src

# Elimina global.json si existe (Evita conflictos de versión)
RUN rm -f global.json

# Copia el archivo .csproj a la imagen
COPY E-MoveBCN-back.csproj ./E-MoveBCN-back.csproj

# Verifica que el archivo .csproj se haya copiado correctamente
RUN ls -l

# Restaura dependencias usando el archivo .csproj
RUN dotnet restore E-MoveBCN-back.csproj

# Copia el resto del código
COPY . .

# Compila la aplicación
RUN dotnet publish E-MoveBCN-back.csproj -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 8080

COPY --from=build /app .
ENTRYPOINT ["dotnet", "E-MoveBCN-back.dll"]

