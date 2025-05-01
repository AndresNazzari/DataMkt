# 📦 DataMkt - Gestión de Stock por Sucursal

DataMkt es una API RESTful desarrollada en .NET 7 y Entity Framework Core que permite administrar productos, sucursales y su stock correspondiente, manteniendo una estructura clara y escalable basada en arquitectura por capas.

---

## 🚀 Tecnologías usadas

- ASP.NET Core Web API
- Entity Framework Core + SQLite
- Swagger / OpenAPI
- Rider (JetBrains)
- Clean Architecture (Presentation, Application, Domain, Infrastructure)

---

## 📁 Estructura del Proyecto

```plaintext
DataMkt.sln
├── DataMkt.API             // Capa de presentación
├── DataMkt.Application     // Casos de uso, DTOs, validaciones
├── DataMkt.Domain          // Entidades y lógica de negocio
└── DataMkt.Infrastructure  // EF Core, DbContext, acceso a datos
```

## 🧪 Clonar y correr el proyecto localmente

- Clonar el repositorio:

```bash
git clone https://github.com/tu_usuario/DataMkt.git
```

```bash
cd DataMkt
```

- Restaurar dependencias:

```bash
dotnet restore
```

- Crear la base de datos y aplicar migraciones:

```bash
dotnet ef database update --project DataMkt.Infrastructure --startup-project DataMkt.API
```

- Ejecutar la aplicación:

```bash
dotnet run --project DataMkt.API
```

- La API estará disponible en:

http://localhost:5001/swagger/index.html

## 📌 Cómo probar los endpoints
### 📄 Documentación automática con Swagger

Una vez levantada la API, accedé a:

```bash 
https://localhost:5001/swagger
```

Desde allí podés:

Probar los endpoints GET, POST, PUT directamente.

Ver la documentación generada por los comentarios XML.

Enviar datos de prueba fácilmente.

## 🧪 Probar con Postman
Podés importar esta colección de endpoints manualmente:

```
POST   /api/sucursales
GET    /api/sucursales
POST   /api/productos
GET    /api/productos
PUT    /api/productos/stock
GET    /api/productos/con-stock
```

### 📥 Ejemplo para PUT /api/productos/stock
```json
{
"productoId": 1,
"sucursalId": 2,
"cantidad": 5
}
```

## 🗄️ Migraciones con Entity Framework Core
- Crear una nueva migración:
```bash
dotnet ef migrations add NombreMigracion --project DataMkt.Infrastructure --startup-project DataMkt.API
```
Aplicar todas las migraciones a la base de datos:

## 🗄️ Migraciones con Entity Framework Core

- Crear una nueva migración:

```bash
dotnet ef migrations add NombreMigracion --project DataMkt.Infrastructure --startup-project DataMkt.API
```
-Aplicar todas las migraciones a la base de datos:

```bash
dotnet ef database update --project DataMkt.Infrastructure --startup-project DataMkt.APIdotnet ef database update --project DataMkt.Infrastructure --startup-project DataMkt.API
```