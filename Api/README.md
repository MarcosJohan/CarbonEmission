# Proyecto de Emisiones de Carbono


## Descripción

Esta API permite a las empresas registrar, actualizar y consultar sus emisiones de carbono. Proporciona varios endpoints para gestionar las emisiones y generar reportes.

## Endpoints

- **GET /emissions**: Retorna todas las emisiones registradas.
- **GET /emissions/{id}**: Retorna una emisión específica por su Id.
- **POST /emissions**: Registra una nueva emisión.
- **PUT /emissions/{id}**: Actualiza una emisión existente.
- **DELETE /emissions/{id}**: Elimina una emisión por su Id.
- **GET /emissions/company/{companyId}**: Retorna todas las emisiones de una empresa específica.

### Reportes

- **GET /report?type=**: Genera un reporte de emisiones por tipo.
- **GET /report/range?startDate=&endDate=**: Genera un reporte de emisiones en un rango de fecha.

### Autenticación

- **GET /auth/login**: Obtener JWT Bearer Token.

1. Configure el archivo `appsettings.json` con las variables necesarias:
    ```json
    {
      "Authentication": {
        "Key": "tu-clave-secreta"
      },
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=tu-base-de-datos;User=root;Password=tu-contraseña;"
      }
    }
    ```

2. Construya y ejecute el contenedor Docker para la base de datos MySQL:
    
    ```bash
    docker compose up -d
    ```

4. Restaura las dependencias y ejecuta la aplicación:
    ```bash
    dotnet restore
    dotnet run
    ```

## Uso

Accede a la documentación Swagger para probar los endpoints:
```
    https://localhost:44372/swagger/index.html
```

## Pruebas

Ejecuta las pruebas unitarias con xUnit:
```bash
dotnet test
