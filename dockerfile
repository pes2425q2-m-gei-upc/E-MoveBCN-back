# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo los archivos de solución y proyectos primero (para caché)
COPY *.sln ./
COPY */*.csproj ./

# Asegura que se copien los archivos correctamente antes de restaurar
RUN ls -l && dotnet nuget locals all --clear && dotnet restore

# Copia el resto de los archivos
COPY . .

# Compila el proyecto
RUN dotnet publish -c Release -o /app

# Etapa de runtime (para hacer la imagen más ligera)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "E-MOVEBCN-BACK.dll"]