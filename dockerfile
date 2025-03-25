# Imagen base con .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo los archivos .csproj primero (para aprovechar la caché de Docker)
COPY *.sln ./
COPY */*.csproj ./
RUN dotnet nuget locals all --clear && dotnet restore --disable-parallel --verbosity detailed
RUN dotnet restore

# Copia el resto del código
COPY . .
RUN dotnet publish -c Release -o /app

# Imagen final con solo el runtime de .NET (más ligero)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "E-MOVEBCN-BACK.dll"]
