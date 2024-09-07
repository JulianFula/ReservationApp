#ReservationApp

Este repositorio contiene el código fuente de una aplicación web completa con un backend en .NET Core y un frontend en Angular.

## Estructura del Repositorio

- `/backend` - Contiene el código del API REST desarrollado en .NET Core.
- `/frontend` - Contiene el código del frontend desarrollado en Angular.

## Requisitos Previos

- **Node.js** (versión 14 o superior) y **npm**: Para ejecutar el frontend de Angular.
- **.NET Core SDK** (versión 8.0 o superior): Para ejecutar el backend.
- **SQL Server** o **SQLite** (dependiendo de tu configuración del backend): Para la base de datos.
- **Docker** (opcional): Para ejecutar los contenedores de Docker.

## Instrucciones de Configuración

### Clonar el Repositorio

```bash
git clone https://github.com/JulianFula/ReservationsApp.git
cd ReservationApp
```

## Configuración del Backend

1. Navega a la carpeta del backend:
```bash
  cd backend
```
2. Restaura los paquetes de NuGet:
```bash
  dotnet restore
```
3. Configura la base de datos en el archivo appsettings.json, Modifica la cadena de conexión a la base de datos según tu entorno
4. Ejecuta las migraciones de la base de datos (si estás utilizando Entity Framework Core):
```bash
  dotnet ef database update
```
5. Compila y ejecuta el proyecto:
```bash
  dotnet run
```
6. La API estará disponible en http://localhost:7069

## Configuración del Frontend

1. Navega a la carpeta del frontend:
```bash
  cd ../frontend
```
2. Instala las dependencias de npm::
```bash
  npm install
```
3. Ejecuta el servidor de desarrollo de Angular:
```bash
  ng serve
```
4. El frontend estará disponible en http://localhost:4200.
