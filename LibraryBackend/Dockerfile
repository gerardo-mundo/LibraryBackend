# Usar una imagen del SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar el archivo de proyecto y restaurar las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos y construir la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usar una imagen de tiempo de ejecución de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 80
# Establecer el punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "LibraryBackend.dll"]
