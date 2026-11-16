# Indumentaria — E-commerce en Blazor

Tienda online de indumentaria masculina, femenina y unisex. Trabajo Práctico
Integrador individual de la materia Desarrollo Web con Blazor.

La aplicación permite recorrer un catálogo con filtros, búsqueda y paginación,
ver el detalle de cada prenda, armar un carrito que se conserva al recargar la
página y administrar los productos desde un panel con alta, edición y baja.

> **Alcance de esta entrega.** La aplicación se ejecuta en forma local. El
> despliegue público y el pipeline de CI/CD (requisitos 14 y 16) quedaron fuera
> del alcance acordado y figuran como pendientes en
> [CUMPLIMIENTO_REQUISITOS_TP.md](CUMPLIMIENTO_REQUISITOS_TP.md).
> Tampoco incluye login, pasarela de pagos ni registro de pedidos.

## Documentación

| Documento | Contenido |
|---|---|
| [GUIA_TECNICA.md](GUIA_TECNICA.md) | Arquitectura y explicación de cada concepto de Blazor aplicado |
| [GUIA_USUARIO.md](GUIA_USUARIO.md) | Cómo usar la tienda y el panel de administración |
| [CUMPLIMIENTO_REQUISITOS_TP.md](CUMPLIMIENTO_REQUISITOS_TP.md) | Los 16 requisitos de la consigna con su evidencia en el código |
| [CHANGELOG.md](CHANGELOG.md) | Historial de cambios |
| [IMAGENES.md](IMAGENES.md) | Estado de las fotos del catálogo |

## Tecnologías

| Tecnología | Versión | Uso |
|---|---|---|
| .NET | 9 | Plataforma |
| Blazor Web App | .NET 9 | Interfaz, con render interactivo de servidor (Interactive Server) |
| Entity Framework Core | 9.0.19 | Acceso a datos y migraciones |
| SQLite | (proveedor `Microsoft.EntityFrameworkCore.Sqlite`) | Base de datos en un archivo local |
| Bootstrap | 5.3.3 | Grilla responsive y componentes visuales (incluido en `wwwroot/lib`) |
| JavaScript | Vanilla | Acceso a `localStorage` mediante JS Interop |
| Google Fonts | — | Tipografías Anton e IBM Plex Sans (requieren conexión a internet; sin ella se usan fuentes del sistema) |

No hay otras dependencias externas: ver
[IndumentariaEcommerce.csproj](IndumentariaEcommerce/IndumentariaEcommerce/IndumentariaEcommerce.csproj).

## Características

- **Portada** con productos destacados.
- **Catálogo** con filtro por género y por categoría, búsqueda por nombre y paginación (12 por página).
- **Detalle de producto** con selección de talle, aviso de stock bajo y de producto sin stock.
- **Carrito** con cantidades, subtotales y total; persiste en el navegador (`localStorage`) y el contador del menú se actualiza en todas las páginas.
- **Panel de administración** con listado paginado (10 por página), búsqueda, alta, edición y baja con confirmación.
- **Validaciones** en el formulario de productos, incluida una validación personalizada del formato de talles.
- **Datos de demostración:** 5 categorías y 30 productos cargados por las migraciones.

## Estructura del proyecto

```
Blazor/                                  raíz del repositorio
├── README.md y demás documentación
└── IndumentariaEcommerce/
    ├── IndumentariaEcommerce.sln        solución (abrir en Visual Studio)
    └── IndumentariaEcommerce/           proyecto web
        ├── Program.cs                   registro de servicios y arranque
        ├── Models/                      entidades, enum y modelos de transporte
        │   └── Validaciones/            atributos de validación propios
        ├── Data/                        AppDbContext y datos semilla
        ├── Migrations/                  migraciones de EF Core (generadas)
        ├── Services/                    ProductoService y CarritoService
        ├── Components/
        │   ├── Layout/                  MainLayout y NavMenu
        │   ├── Pages/                   componentes con ruta (@page)
        │   └── Shared/                  componentes reutilizables
        └── wwwroot/                     CSS, JS de interop, imágenes y Bootstrap
```

## Requisitos previos

- [SDK de .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0).
- Para usar Visual Studio: Visual Studio 2022 (17.12 o posterior) con la carga de trabajo **Desarrollo de ASP.NET y web**.
- Opcional, solo para crear migraciones nuevas: la herramienta `dotnet-ef` 9.x
  (`dotnet tool install --global dotnet-ef --version 9.0.19`).

No hace falta instalar SQLite ni crear la base a mano.

## Ejecución local

### Con Visual Studio

1. Abrir `IndumentariaEcommerce/IndumentariaEcommerce.sln`.
2. Elegir el perfil **https** (o **http**) en la barra de herramientas.
3. Ejecutar con **F5** (con depurador) o **Ctrl+F5** (sin depurador).

### Con la línea de comandos

```bash
cd IndumentariaEcommerce
dotnet build
dotnet run --project IndumentariaEcommerce --launch-profile https
```

Direcciones configuradas en
[launchSettings.json](IndumentariaEcommerce/IndumentariaEcommerce/Properties/launchSettings.json):

- `https://localhost:7220`
- `http://localhost:5067`

La primera ejecución crea la base de datos y carga los datos de demostración
automáticamente.

## Base de datos SQLite

- **Cadena de conexión:** `ConexionSqlite` en
  [appsettings.json](IndumentariaEcommerce/IndumentariaEcommerce/appsettings.json),
  con valor `Data Source=indumentaria.db`. El archivo se crea en la carpeta del
  proyecto web. No contiene usuarios ni contraseñas.
- **Creación automática:** al arrancar,
  [Program.cs](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs) ejecuta
  `Database.Migrate()`, que crea la base si no existe y aplica las migraciones
  pendientes.
- **Fuera del repositorio:** `*.db`, `*.db-shm` y `*.db-wal` están excluidos en
  [.gitignore](.gitignore). Cada clon genera su propia base a partir de las
  migraciones, así nunca se versionan datos locales.
- **Restaurar los datos de demostración:** con la aplicación detenida, borrar
  `indumentaria.db`, `indumentaria.db-shm` e `indumentaria.db-wal` de la
  carpeta del proyecto web y volver a ejecutar.

Comandos de EF Core (desde `IndumentariaEcommerce/`):

```bash
dotnet ef migrations add <NombreMigracion> --project IndumentariaEcommerce
dotnet ef database update --project IndumentariaEcommerce
```

## Capturas de pantalla

Todavía no hay capturas en el repositorio.

## Limitaciones conocidas

- 8 de los 30 productos no tienen foto real y muestran una imagen genérica (ver [IMAGENES.md](IMAGENES.md)).
- La búsqueda ignora mayúsculas pero no acentos: `algodon` no encuentra "Algodón" (limitación del `LIKE` de SQLite).
- El panel de administración no tiene autenticación: el login está fuera del alcance del trabajo.
- No hay pruebas automatizadas.

## Autor

Luciano Nitto — Trabajo Práctico Integrador, Desarrollo Web con Blazor.
