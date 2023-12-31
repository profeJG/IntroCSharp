# IntroCSharp
Repositorio para introducirnos en la programación en CSharp en Linux utilizando contenedores Docker.
## Contenidos

Ejemplo de ejecución de aplicación **donet** en un contenedor:
```bash
docker run --rm mcr.microsoft.com/dotnet/samples:dotnetapp
```
Creación del contenedor dónde crearemos y compilaremos el código del proyecto **.NET**:
```bash
docker run -it -w /usr/src/mypro -v ./src:/usr/src/mypro dotnet
```
## Creación de aplicaciones .NET
En la carpeta de trabajo, ejecute el comando siguiente para crear un proyecto en un subdirectorio llamado App:
```bash
dotnet new console -o App -n DotNet.Docker
```
Con el comando `dotnet` new se crea una carpeta denominada **App** y se genera una aplicación de consola "*Hola mundo"*. Cambie los directorios y vaya a la carpeta **App**, desde la sesión de Terminal. Use el comando `dotnet run` para iniciar la aplicación. La aplicación se ejecuta e imprime *Hello World!*.

Cualquier parámetro posterior a -- no se pasa al comando `dotnet run` y, en su lugar, se pasa a su aplicación.
Si pasa un número en la línea de comandos a la aplicación, solo se contará hasta esa cantidad y se cerrará. Inténtelo con `dotnet run -- 5` para contar hasta cinco.

## Publicación de una aplicación .NET

Antes de agregar la aplicación .NET a la imagen de Docker, primero debe publicarse. Es mejor que el contenedor ejecute la versión publicada de la aplicación. Ejecute el comando siguiente para publicar la aplicación:
CLI de .NET
```bash
dotnet publish -c Release
```
Con este comando se compila la aplicación en la carpeta publish. La ruta de acceso a la carpeta publish desde la carpeta de trabajo debería ser `.\App\bin\Release\net8.0\publish\.`

## Creación del archivo Dockerfile

El archivo *Dockerfile* lo usa el comando `docker build` para crear una imagen de contenedor. Este archivo es un archivo de texto denominado Dockerfile que no tiene ninguna extensión.

Cree un archivo denominado Dockerfile en el directorio que contiene el archivo `.csproj` y ábralo en un editor de texto. En este tutorial se usa la imagen del runtime de ASP.NET Core (que contiene la imagen de runtime de .NET) y corresponde a la aplicación de consola de .NET.

```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
```



## Referencias
- [.NET Docker Sample](https://github.com/dotnet/dotnet-docker/blob/main/samples/dotnetapp/README.md)
- [.NET en Docker](https://github.com/dotnet/dotnet-docker/tree/0f9bcba898466d5c00ac0fa57fdc2d40a9f29491)
- [Tutorial: Inclusión de una aplicación de .NET en un contenedor](https://learn.microsoft.com/es-es/dotnet/core/docker/build-container?tabs=linux&pivots=dotnet-8-0)