# Cumplimiento de requisitos — TP Integrador Blazor

Relaciona cada requisito de la consigna con su evidencia en el código. Los
enlaces son relativos a la raíz del repositorio. En GitHub, los que terminan en
`#Lnn` abren el archivo en esa línea; en Visual Studio abren el archivo.

Las líneas citadas corresponden al estado del código al 16/11/2026.

## Resumen

| Estado | Cantidad | Requisitos |
|---|---|---|
| CUMPLE | 14 | 1 a 13 y 15 |
| PENDIENTE (fuera del alcance de esta entrega) | 2 | 14 (CI/CD) y 16 (despliegue público) |

> El proyecto compila con 0 errores y 0 advertencias y fue probado
> manualmente en Visual Studio. No tiene pruebas automatizadas.

## Índice

| # | Unidad | Requisito | Estado |
|---|---|---|---|
| [1](#1-mínimo-6-componentes-razor-reutilizables-y-encapsulados) | 1 | Mínimo 6 componentes Razor reutilizables y encapsulados | CUMPLE |
| [2](#2-formularios-con-editform-y-validación-avanzada) | 1 | Formularios con EditForm y validación avanzada | CUMPLE |
| [3](#3-routing-con-parámetros-y-navegación-programática) | 1 | Routing con parámetros y navegación programática | CUMPLE |
| [4](#4-state-service-con-notificación-reactiva) | 1 | State Service con notificación reactiva | CUMPLE |
| [5](#5-html5-semántico-en-todos-los-layouts) | 2 | HTML5 semántico en todos los layouts | CUMPLE |
| [6](#6-css-isolation-en-al-menos-3-componentes) | 2 | CSS Isolation en al menos 3 componentes | CUMPLE |
| [7](#7-diseño-responsive-con-bootstrap-5-mobile-first) | 2 | Diseño responsive con Bootstrap 5 (mobile-first) | CUMPLE |
| [8](#8-js-interop) | 2 | JS Interop: al menos una funcionalidad con JavaScript | CUMPLE |
| [9](#9-ef-core-con-sqlite-y-dbcontextfactory) | 3 | EF Core con SQLite y DbContextFactory | CUMPLE |
| [10](#10-al-menos-2-entidades-relacionadas) | 3 | Al menos 2 entidades relacionadas | CUMPLE |
| [11](#11-migraciones-versionadas-y-datos-de-seed) | 3 | Migraciones versionadas y datos de seed | CUMPLE |
| [12](#12-crud-completo-con-paginación-y-búsqueda) | 3 | CRUD completo con paginación y búsqueda | CUMPLE |
| [13](#13-repositorio-github-con-historial-limpio-y-conventional-commits) | 4 | Repositorio GitHub con historial limpio y Conventional Commits | CUMPLE |
| [14](#14-cicd-con-github-actions) | 4 | CI/CD con GitHub Actions | PENDIENTE |
| [15](#15-readmemd-profesional) | 4 | README.md profesional | CUMPLE |
| [16](#16-aplicación-desplegada-en-url-pública) | 4 | Aplicación desplegada en URL pública | PENDIENTE |

Además: [componentes reutilizables](#componentes-razor-reutilizables) ·
[guion de exposición](#guion-de-exposición-10-minutos).

---

## Unidad 1 — Componentes Blazor

### 1. Mínimo 6 componentes Razor reutilizables y encapsulados

**Estado: CUMPLE.** Hay 6 componentes reutilizables en `Components/Shared/`,
más 6 páginas y 2 de layout.

**Cómo está hecho.** Ninguno de los seis tiene `@page`: reciben los datos por
`[Parameter]` y avisan lo que pasa por `EventCallback`. No conocen a su padre,
y por eso se reutilizan en varias pantallas. Ver la
[tabla de componentes](#componentes-razor-reutilizables).

**Qué mostrar.** `Paginacion.razor`: recibe dos números y devuelve uno, y se usa
igual en el catálogo
([Catalogo.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor))
y en el panel
([AdminProductos.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor)).

**Qué explicar.** `[EditorRequired]` hace que el compilador avise si falta un
parámetro obligatorio. `Cargando` reemplazó un bloque que estaba copiado en
cinco páginas.

### 2. Formularios con EditForm y validación avanzada

**Estado: CUMPLE.**

**Cómo está hecho.**
- El formulario de alta y edición usa `EditForm` con `OnValidSubmit`, que
  ejecuta el guardado solo si los datos son válidos.
- `DataAnnotationsValidator` aplica las reglas del modelo; `ValidationSummary`
  y `ValidationMessage` muestran los errores.
- La validación avanzada es un atributo propio, `[TallesValidos]`: rechaza
  talles vacíos, con espacios o repetidos.

**Evidencia.**
- [FormularioProducto.razor#L38-L40](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor#L38-L40): `EditForm`, `DataAnnotationsValidator` y `ValidationSummary`.
- [Producto.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `Required`, `StringLength` y `Range`.
  - [Línea 41](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs#L41): `[TallesValidos]`.
  - [Línea 48](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs#L48): `[Range(1, …)]`, que obliga a elegir una categoría.
- [TallesValidosAttribute.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Validaciones/TallesValidosAttribute.cs): hereda de `ValidationAttribute` y sobrescribe `IsValid`.

**Qué mostrar.** En Administración → Nuevo producto, escribir `S,M,S` en Talles
y tocar Guardar: aparece el error y no se guarda.

**Qué explicar.**
- Sin `DataAnnotationsValidator` el formulario acepta cualquier dato.
- El atributo personalizado no requiere migración, porque EF Core lo ignora.
- Los talles repetidos romperían `@key` en el detalle del producto.

### 3. Routing con parámetros y navegación programática

**Estado: CUMPLE.**

**Cómo está hecho.**
- Rutas con parámetros tipados: `/producto/{Id:int}` y `/admin/productos/{Id:int}/editar`.
- El formulario declara **dos** `@page`: con `Id` edita, sin `Id` da de alta.
- Después de guardar se navega por código con `NavigationManager`.

**Evidencia.**
- [DetalleProducto.razor#L3](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/DetalleProducto.razor#L3): ruta `/producto/{Id:int}`.
- [FormularioProducto.razor#L5-L6](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor#L5-L6): las dos rutas del formulario.
- [FormularioProducto.razor#L193](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor#L193): `NavigateTo`.
- [NavMenu.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor): `NavLink` con `Match="NavLinkMatch.All"`.

**Qué explicar.**
- La restricción `:int` hace que `/producto/abc` dé 404.
- Se usa `OnParametersSetAsync` porque Blazor reutiliza el componente al pasar
  de `/producto/1` a `/producto/2`, y `OnInitializedAsync` no volvería a correr.

### 4. State Service con notificación reactiva

**Estado: CUMPLE.**

**Cómo está hecho.**
- `CarritoService` guarda el carrito y avisa cada cambio con el evento
  `OnCambio`.
- `IconoCarrito` (el contador del menú) y la página `Carrito` se suscriben,
  redibujan con `InvokeAsync(StateHasChanged)` y se desuscriben en `Dispose`.

**Evidencia.**
- [CarritoService.cs#L43](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs#L43): `public event Action? OnCambio`.
- [Program.cs#L38](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs#L38): registrado como `Scoped`.
- [IconoCarrito.razor#L23](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor#L23) y [#L51](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor#L51): suscripción y desuscripción.
- [Carrito.razor#L133](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor#L133) y [#L144](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor#L144): ídem.

**Qué mostrar.** Agregar un producto desde el detalle: el contador del menú se
actualiza aunque `DetalleProducto` no conoce a `IconoCarrito`. Después, F5 en
`/carrito`: los productos siguen ahí.

**Qué explicar.**
- `Scoped` significa una instancia por circuito, es decir un carrito por
  usuario. Con `Singleton` todos compartirían el mismo carrito.
- Desuscribirse evita que el servicio quede apuntando a componentes destruidos.

---

## Unidad 2 — Interfaz visual

### 5. HTML5 semántico en todos los layouts

**Estado: CUMPLE.**

**Cómo está hecho.** El layout usa `aside` para la columna lateral, `header`
para la marca, `nav` para los enlaces, `main` para el contenido y `footer` como
pie, fuera de `main`.

**Evidencia.**
- [MainLayout.razor#L9](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor#L9) (`aside`), [#L14](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor#L14) (`main`) y [#L19](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor#L19) (`footer`).
- [NavMenu.razor#L3](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor#L3): `header`.
- En las páginas: `section` en [Home.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Home.razor), `nav aria-label` en la paginación y los breadcrumbs, y `form role="search"` en el panel.

**Qué mostrar.** F12 → Elements: la estructura `aside`, `main` y `footer`.

**Qué explicar.** El `div.columna` no tiene significado: solo agrupa `main` y
`footer` para mantener el diseño en escritorio.

### 6. CSS Isolation en al menos 3 componentes

**Estado: CUMPLE.** Hay 11 archivos `.razor.css`: 9 de componentes propios y 2
de layout.

**Cómo está hecho.** Cada `Componente.razor.css` se aplica solo a ese
componente. Blazor agrega un atributo único a los elementos y los reúne en
`IndumentariaEcommerce.styles.css`.

**Evidencia.**
- Archivos: [TarjetaProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/TarjetaProducto.razor.css), [Paginacion.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Paginacion.razor.css), [IconoCarrito.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor.css), [Home.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Home.razor.css), [Catalogo.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor.css), [DetalleProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/DetalleProducto.razor.css), [Carrito.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor.css), [AdminProductos.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor.css), [FormularioProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor.css), [MainLayout.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor.css) y [NavMenu.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor.css).
- [App.razor#L13](IndumentariaEcommerce/IndumentariaEcommerce/Components/App.razor#L13): enlace al bundle `IndumentariaEcommerce.styles.css`.

**Qué mostrar.** F12: un elemento de la tarjeta con su atributo `b-xxxxxxxx`.

**Qué explicar.** `::deep`
([NavMenu.razor.css#L79](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor.css#L79))
hace falta porque el `<a>` lo dibuja el componente hijo `NavLink`.

### 7. Diseño responsive con Bootstrap 5 (mobile-first)

**Estado: CUMPLE.** Usa Bootstrap 5.3.3, incluido en `wwwroot/lib/bootstrap`.

**Cómo está hecho.** Las clases sin prefijo valen para móvil y los prefijos
`sm`, `md`, `lg` y `xl` agregan columnas al crecer la pantalla. Las media
queries propias usan `min-width`, que también es mobile-first.

**Evidencia.**
- [Catalogo.razor#L31](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor#L31): `row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4`.
- [AdminProductos.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor): `table-responsive`.
- [FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor): `col-6 col-md-3`.
- [app.css](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/app.css): la paleta, que redefine variables `--bs-*`.

**Qué mostrar.** F12 → modo dispositivo: el catálogo pasa de 1 a 4 columnas y
el menú lateral se convierte en barra superior desplegable.

### 8. JS Interop

**Estado: CUMPLE.**

**Cómo está hecho.** El carrito se guarda en `localStorage` del navegador
mediante un módulo JavaScript propio, invocado desde C# con `IJSRuntime`.

**Evidencia.**
- [almacenamiento.js](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/js/almacenamiento.js): `guardar`, `leer` y `borrar`, cada una con `try/catch`.
- [App.razor#L21](IndumentariaEcommerce/IndumentariaEcommerce/Components/App.razor#L21): carga del script.
- [CarritoService.cs#L76](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs#L76) (leer), [#L99](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs#L99) (borrar) y [#L204](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs#L204) (guardar).
- [IconoCarrito.razor#L35](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor#L35): rehidratación en `OnAfterRenderAsync(firstRender)`.

**Qué mostrar.** F12 → Application → Local Storage → clave `carrito`.

**Qué explicar.** JS solo se puede llamar después del primer render, porque
durante el prerender no hay navegador.

---

## Unidad 3 — Persistencia

### 9. EF Core con SQLite y DbContextFactory

**Estado: CUMPLE.**

**Cómo está hecho.** Se registra una fábrica de contextos. Cada método de
`ProductoService` crea un contexto corto con
`await using var contexto = await _fabrica.CreateDbContextAsync()`.

**Evidencia.**
- [Program.cs#L27](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs#L27): `AddDbContextFactory<AppDbContext>` con `UseSqlite`.
- [ProductoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs): recibe `IDbContextFactory<AppDbContext>`.
- [appsettings.json](IndumentariaEcommerce/IndumentariaEcommerce/appsettings.json): cadena `ConexionSqlite`.
- [IndumentariaEcommerce.csproj](IndumentariaEcommerce/IndumentariaEcommerce/IndumentariaEcommerce.csproj): EF Core Sqlite 9.0.19.

**Qué explicar.** En Blazor Server un `DbContext` *scoped* viviría todo el
circuito, durante minutos u horas. Además no es thread-safe: dos eventos
solapados lo romperían. Es la pregunta más probable de la defensa.

### 10. Al menos 2 entidades relacionadas

**Estado: CUMPLE.** `Categoria` (1) se relaciona con `Producto` (N).

**Evidencia.**
- [Categoria.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Categoria.cs): `ICollection<Producto> Productos`.
- [Producto.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `CategoriaId` y `Categoria`.
- [AppDbContext.cs#L44-L47](IndumentariaEcommerce/IndumentariaEcommerce/Data/AppDbContext.cs#L44-L47): `HasOne` / `WithMany` / `HasForeignKey` / `OnDelete(Restrict)`.

**Qué explicar.**
- `Restrict` evita que borrar una categoría elimine sus productos en silencio.
- `Include(p => p.Categoria)` carga la relación en las consultas.

### 11. Migraciones versionadas y datos de seed

**Estado: CUMPLE.** Hay 2 migraciones, 5 categorías y 30 productos.

**Evidencia.**
- [DatosSemilla.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/DatosSemilla.cs): `HasData`, aplicado desde [AppDbContext.cs#L56](IndumentariaEcommerce/IndumentariaEcommerce/Data/AppDbContext.cs#L56).
- [20260830215822_MigracionInicial.cs](IndumentariaEcommerce/IndumentariaEcommerce/Migrations/20260830215822_MigracionInicial.cs): tablas, clave foránea, índice y seed.
- [20260831021009_ActualizarCatalogoConFotos.cs](IndumentariaEcommerce/IndumentariaEcommerce/Migrations/20260831021009_ActualizarCatalogoConFotos.cs): `UpdateData` del seed.
- [Program.cs#L50](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs#L50): `Database.Migrate()` al arrancar.

**Comprobación realizada.** `dotnet ef migrations has-pending-model-changes`
respondió que el modelo no tiene cambios sin migrar.

**Qué explicar.** La base (`*.db`) no se versiona
([.gitignore](.gitignore)): cada clon la regenera desde las migraciones.

### 12. CRUD completo con paginación y búsqueda

**Estado: CUMPLE.**

**Evidencia** en [ProductoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs):

| Operación | Método | Línea |
|---|---|---|
| Listar / buscar / paginar | `BuscarAsync` | [L30](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L30) |
| Leer | `ObtenerPorIdAsync` | [L90](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L90) |
| Crear | `CrearAsync` | [L138](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L138) |
| Actualizar | `ActualizarAsync` | [L156](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L156) |
| Eliminar | `EliminarAsync` | [L191](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L191) |

- La búsqueda usa `EF.Functions.Like` ([L70](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L70)) y el total se cuenta antes de paginar ([L76](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs#L76)).
- En el panel:
  - Formulario de búsqueda: [AdminProductos.razor#L17](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor#L17).
  - Paso del texto a `BuscarAsync`: [#L180](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor#L180).
  - Baja con `ConfirmacionEliminar`: [#L96](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor#L96).

**Qué mostrar.** En el panel, buscar `zapatillas` estando en la página 2: vuelve
a la página 1 con los resultados. Después crear, editar y eliminar un producto.

**Qué explicar.**
- Contar después de `Skip`/`Take` daría siempre una sola página.
- Al cambiar el criterio de búsqueda se vuelve a la página 1.

---

## Unidad 4 — Prácticas profesionales

### 13. Repositorio GitHub con historial limpio y Conventional Commits

**Estado: CUMPLE.**

**Evidencia verificada.**
- Repositorio público: `https://github.com/nittoluciano9-max/Indumentaria-Blazor`, rama `main`.
- Los 33 commits del historial siguen el formato `tipo(alcance): descripción`.
  Se usan los tipos `feat`, `fix`, `style`, `refactor`, `docs` y `chore`.
- Las bases de datos y los artefactos de compilación están excluidos en [.gitignore](.gitignore).

**Pendiente antes de entregar.** Los ajustes técnicos y la documentación
actuales todavía **no están commiteados ni subidos**. Hay que commitearlos
también con Conventional Commits y hacer push.

**Qué mostrar.** `git log --oneline` o la pestaña Git Changes / Git Repository
de Visual Studio.

### 14. CI/CD con GitHub Actions

**Estado: PENDIENTE — fuera del alcance de esta entrega.**

- La parte de **Git y GitHub** está cumplida (requisito 13).
- La parte de **CI/CD no está implementada.** La carpeta `.github/workflows/`
  está vacía y no hay ningún workflow versionado, así que GitHub no ejecuta
  build ni deploy automáticos.
- Se dejó fuera junto con el despliegue (requisito 16), por decisión acordada
  para esta entrega.

### 15. README.md profesional

**Estado: CUMPLE** (pendiente de commit).

[README.md](README.md) incluye descripción, tecnologías con versiones,
características, estructura, requisitos previos, ejecución con Visual Studio y
con `dotnet`, configuración de SQLite y limitaciones conocidas. Lo complementa
[GUIA_TECNICA.md](GUIA_TECNICA.md).

### 16. Aplicación desplegada en URL pública

**Estado: PENDIENTE — excluido por el docente para esta entrega.**

No hay despliegue ni URL pública. La aplicación se ejecuta localmente
(`https://localhost:7220`).

---

## Componentes Razor reutilizables

| # | Componente | Parámetros principales | Se usa en |
|---|---|---|---|
| 1 | [TarjetaProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/TarjetaProducto.razor) | `Producto` (requerido) | Home, Catálogo |
| 2 | [Paginacion.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Paginacion.razor) | `PaginaActual`, `TotalPaginas`, `EventCallback<int> OnCambioPagina` | Catálogo, AdminProductos |
| 3 | [FiltroProductos.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/FiltroProductos.razor) | `EventCallback<FiltrosCatalogo> OnCambio` | Catálogo |
| 4 | [IconoCarrito.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor) | Sin parámetros; usa `CarritoService` | NavMenu |
| 5 | [Cargando.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Cargando.razor) | `Texto` (opcional) | Home, Catálogo, Detalle, Admin, Formulario |
| 6 | [ConfirmacionEliminar.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/ConfirmacionEliminar.razor) | `Pregunta`, `EventCallback OnConfirmar`, `EventCallback OnCancelar` | AdminProductos |

---

## Guion de exposición (10 minutos)

| Tiempo | Tema | Qué hacer |
|---|---|---|
| 0:00–1:00 | Presentación | Qué es la tienda y su alcance: sin login, sin pagos, sin pedidos. Stack: Blazor Server .NET 9, EF Core y SQLite. |
| 1:00–2:00 | Arquitectura | En el Explorador de soluciones: `Models`, `Data`, `Services`, `Components`. Regla: los componentes no tocan la base. Mostrar los registros en `Program.cs`. |
| 2:00–3:30 | Persistencia (req. 9–11) | `AppDbContext`: relación con `Restrict`. Explicar **por qué `DbContextFactory`**. Mostrar `DatosSemilla` y la carpeta `Migrations`. |
| 3:30–5:00 | Catálogo en vivo (req. 1, 3, 7, 12) | Filtrar por género y categoría, buscar y paginar. Abrir un producto (`/producto/{Id:int}`). Achicar la ventana para mostrar el diseño responsive. |
| 5:00–6:30 | Carrito (req. 4, 8) | Elegir talle y agregar: el contador se actualiza solo. Cambiar cantidades en `/carrito`, presionar F5 y mostrar Local Storage. En código: `OnCambio`, `InvokeAsync(StateHasChanged)` y `Dispose`. |
| 6:30–8:00 | Administración (req. 2, 12) | Buscar, editar y probar `S,M,S` para mostrar la validación personalizada. Crear y eliminar con confirmación. Mostrar `TallesValidosAttribute`. |
| 8:00–9:00 | Interfaz (req. 5, 6) | F12: `aside`, `main` y `footer`, y el atributo `b-xxxx` del CSS aislado. Mostrar un `.razor.css` y la tabla de los 6 componentes reutilizables. |
| 9:00–10:00 | Unidad 4 y cierre | `git log` con Conventional Commits y el README. Aclarar que CI/CD y el deploy quedaron fuera de esta entrega. Mencionar limitaciones: 8 fotos faltantes, búsqueda sensible a acentos, sin tests. |

**Preguntas probables:**
- ¿Por qué `Scoped` y no `Singleton` para el carrito?
- ¿Por qué `DbContextFactory`?
- ¿Por qué `OnParametersSetAsync` en el detalle?
- ¿Por qué `InvokeAsync` en el handler del evento?
- ¿Qué pasa si no te desuscribís?
- ¿Por qué JS en `OnAfterRenderAsync`?
- ¿Por qué `Precio` se convierte a `double` en SQLite?

Las respuestas están en [GUIA_TECNICA.md](GUIA_TECNICA.md).
