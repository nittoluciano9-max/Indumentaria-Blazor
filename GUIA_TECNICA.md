# Guía técnica

Explica cómo está construida la aplicación y por qué se tomó cada decisión.
Todas las rutas son relativas a la raíz del repositorio. Para abreviar, en el
texto **`web/`** equivale a `IndumentariaEcommerce/IndumentariaEcommerce/`
(los enlaces sí llevan la ruta completa).

## Índice

1. [Arquitectura](#1-arquitectura)
2. [Modo de render](#2-modo-de-render)
3. [Componentes Razor](#3-componentes-razor)
4. [Parámetros y EventCallback](#4-parámetros-y-eventcallback)
5. [Routing](#5-routing)
6. [EditForm y validaciones](#6-editform-y-validaciones)
7. [State Service del carrito](#7-state-service-del-carrito)
8. [JS Interop](#8-js-interop)
9. [HTML5 semántico](#9-html5-semántico)
10. [CSS Isolation](#10-css-isolation)
11. [Bootstrap 5 y diseño responsive](#11-bootstrap-5-y-diseño-responsive)
12. [EF Core y DbContextFactory](#12-ef-core-y-dbcontextfactory)
13. [Entidades y relación](#13-entidades-y-relación)
14. [Migraciones y datos semilla](#14-migraciones-y-datos-semilla)
15. [CRUD, búsqueda y paginación](#15-crud-búsqueda-y-paginación)

---

## 1. Arquitectura

La aplicación tiene tres capas, y cada una solo conoce a la de abajo:

```
Componentes (Components/)        UI: muestran datos y reciben eventos
        │  inyectan
        ▼
Servicios (Services/)            ProductoService: consultas y escritura
                                 CarritoService:  estado del carrito
        │  usan
        ▼
Datos (Data/ + Models/)          AppDbContext, entidades, SQLite
```

**Regla principal:** los componentes no consultan la base. Le piden los datos a
[ProductoService](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs).
Así, el filtrado y la paginación existen en un solo lugar y los componentes no
dependen de EF Core.

Los servicios se registran en
[Program.cs](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs):

| Servicio | Ciclo de vida | Por qué |
|---|---|---|
| `IDbContextFactory<AppDbContext>` | `AddDbContextFactory` | Entrega un contexto nuevo y de vida corta por consulta (ver [sección 12](#12-ef-core-y-dbcontextfactory)) |
| `ProductoService` | `Scoped` | No guarda estado; `Scoped` es el valor seguro por defecto |
| `CarritoService` | `Scoped` | En Blazor Server, *Scoped* significa una instancia por circuito, o sea por pestaña conectada: cada usuario tiene su carrito. Con `Singleton` todos los usuarios compartirían el mismo |

En `Program.cs` también se fija la cultura `es-AR`, para que los precios se
muestren como `$ 24.900`, y se aplican las migraciones al arrancar.

## 2. Modo de render

En [App.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/App.razor)
el modo se aplica a toda la app: `<Routes @rendermode="InteractiveServer" />`.

- **Interactive Server:** los componentes se ejecutan en el servidor. El
  navegador mantiene una conexión SignalR (el *circuito*) por la que viajan los
  eventos y las diferencias del HTML.
- **Prerender:** está activo por defecto. La primera respuesta es HTML ya
  dibujado en el servidor, sin JavaScript disponible. Por eso cualquier
  llamada a JS se hace en `OnAfterRenderAsync(firstRender: true)` y no en
  `OnInitialized` (ver [sección 8](#8-js-interop)).

## 3. Componentes Razor

**Componentes reutilizables**, en
[web/Components/Shared/](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/).
No tienen `@page` y reciben todo por parámetros:

| Componente | Responsabilidad | Se usa en |
|---|---|---|
| [TarjetaProducto](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/TarjetaProducto.razor) | Muestra un producto en la grilla | Home, Catálogo |
| [Paginacion](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Paginacion.razor) | Botones de página con ventana deslizante | Catálogo, Admin |
| [FiltroProductos](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/FiltroProductos.razor) | Búsqueda, género y categoría | Catálogo |
| [IconoCarrito](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor) | Acceso al carrito con contador reactivo | NavMenu |
| [Cargando](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Cargando.razor) | Indicador de carga | Home, Catálogo, Detalle, Admin, Formulario |
| [ConfirmacionEliminar](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/ConfirmacionEliminar.razor) | Pregunta "¿Eliminar?" con Sí / No | Admin |

**Páginas**, en
[web/Components/Pages/](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/):
`Home`, `Catalogo`, `DetalleProducto`, `Carrito`, `AdminProductos` y
`FormularioProducto`.

**Layout:** `MainLayout` y `NavMenu`.

**Encapsulamiento:** cada componente decide qué expone. Por ejemplo,
`Paginacion` no sabe qué está paginando: recibe dos números y devuelve uno.
Eso le permite servir igual para el catálogo y para el panel.

**`@key`:** en las listas (`Catalogo`, `AdminProductos`, `Carrito`,
`Paginacion`) se usa `@key` para que Blazor identifique cada elemento por su
Id y no por su posición. Sin eso, al filtrar podría reutilizar el nodo de un
producto para mostrar otro.

## 4. Parámetros y EventCallback

**Parámetros (padre → hijo).** Se declaran con `[Parameter]`, y con
`[EditorRequired]` cuando el componente no funciona sin ese dato: si el padre se
olvida de pasarlo, el compilador avisa. Ejemplo:
`[Parameter, EditorRequired] public Producto Producto` en
[TarjetaProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/TarjetaProducto.razor).
`Cargando` muestra el caso contrario: su `Texto` tiene un valor por defecto,
así que no es obligatorio.

**EventCallback (hijo → padre).** El hijo avisa que pasó algo sin conocer a
quién le avisa.

- `FiltroProductos` → `EventCallback<FiltrosCatalogo> OnCambio`: le pasa al
  catálogo los filtros elegidos. El dato viaja como el *record*
  [FiltrosCatalogo](IndumentariaEcommerce/IndumentariaEcommerce/Models/FiltrosCatalogo.cs).
- `Paginacion` → `EventCallback<int> OnCambioPagina`.
- `ConfirmacionEliminar` → `EventCallback OnConfirmar` y `OnCancelar`.

Se usa `EventCallback` y no `Action` porque, al terminar, Blazor redibuja
automáticamente al componente padre.

**Debounce en la búsqueda del catálogo.** `FiltroProductos` usa
`@bind:event="oninput"` para leer cada tecla y `@bind:after` para esperar 400
ms. Cada tecla cancela la espera anterior (`CancellationTokenSource`), así
escribir "campera" produce una sola consulta y no siete. Al destruirse el
componente, `Dispose` cancela la búsqueda que haya quedado en curso.

## 5. Routing

| Ruta | Componente | Detalle |
|---|---|---|
| `/` | [Home](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Home.razor) | |
| `/catalogo` | [Catalogo](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor) | |
| `/producto/{Id:int}` | [DetalleProducto](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/DetalleProducto.razor) | Parámetro con restricción de tipo |
| `/carrito` | [Carrito](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor) | |
| `/admin/productos` | [AdminProductos](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor) | |
| `/admin/productos/nuevo` y `/admin/productos/{Id:int}/editar` | [FormularioProducto](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor) | Dos `@page` en un mismo componente |

- **Restricción `:int`:** `/producto/abc` no coincide con la ruta y da 404, en
  lugar de fallar al convertir el valor.
- **Parámetro opcional:** en `FormularioProducto`, `Id` es `int?`. Es `null` en
  `/nuevo` y tiene valor en `/editar`; ese `null` es toda la diferencia entre
  alta y edición.
- **`OnParametersSetAsync` y no `OnInitializedAsync`:** al navegar de
  `/producto/1` a `/producto/2`, Blazor reutiliza la misma instancia del
  componente. `OnInitializedAsync` corre una sola vez y seguiría mostrando el
  producto anterior.
- **Navegación programática:** `NavigationManager.NavigateTo("/admin/productos")`
  después de guardar, en `FormularioProducto.GuardarAsync`.
- **`NavLink`:** en el menú, agrega la clase `active` según la URL. "Inicio"
  usa `Match="NavLinkMatch.All"` porque su ruta vacía es prefijo de todas las
  demás.

## 6. EditForm y validaciones

Formulario:
[FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor).

- `<EditForm Model="modelo" OnValidSubmit="GuardarAsync">`: el método de
  guardado solo corre si la validación pasó.
- `<DataAnnotationsValidator />` activa los atributos del modelo. Sin él, el
  formulario acepta cualquier dato.
- `<ValidationSummary />` lista todos los errores y `<ValidationMessage For=...>`
  muestra cada uno bajo su campo.
- `InputText`, `InputTextArea`, `InputNumber` e `InputSelect` están conectados
  al `EditContext`: validan el campo al cambiar y agregan las clases
  `valid`/`invalid`.

**Reglas**, declaradas una sola vez en
[Producto.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs):
`[Required]`, `[StringLength]` y `[Range]`.

- `[Range(1, int.MaxValue)]` sobre `CategoriaId` obliga a elegir una categoría,
  porque la opción "Seleccioná una categoría" vale 0.
- EF Core también usa estos atributos: `StringLength` define el largo de las
  columnas.

**Validación personalizada:**
[TallesValidosAttribute.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Validaciones/TallesValidosAttribute.cs).

- Hereda de `ValidationAttribute` y sobrescribe `IsValid`.
- Rechaza talles vacíos (`S,,M`), con espacios o símbolos, y repetidos (`S,M,S`).
- Los repetidos importan porque `DetalleProducto` usa cada talle como `@key`, y
  Blazor no admite claves duplicadas.
- Si el valor está vacío lo considera válido: de eso se ocupa `[Required]`, así
  cada atributo valida una sola cosa.
- Como es un atributo, el `DataAnnotationsValidator` lo ejecuta sin cambiar el
  formulario, y EF Core lo ignora, así que no requiere migración.

## 7. State Service del carrito

Servicio:
[CarritoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs).

- **Estado:** una `List<ItemCarrito>` privada, expuesta como
  `IReadOnlyList<ItemCarrito>`. Ningún componente puede modificarla por afuera,
  así que todo cambio pasa por métodos que guardan y notifican: `AgregarAsync`,
  `CambiarCantidadAsync`, `QuitarAsync` y `VaciarAsync`.
- **Notificación:** `public event Action? OnCambio`, disparado en
  `GuardarYNotificarAsync`.
- **Suscriptores:**
  [IconoCarrito](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor)
  y [Carrito](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor).
  Ambos siguen el mismo patrón:
  1. Se suscriben en `OnInitialized`: `OnCambio += AlCambiarElCarrito`.
  2. Redibujan con `InvokeAsync(StateHasChanged)`. Hace falta `InvokeAsync`
     porque el evento llega desde otro componente, fuera de su contexto de
     render.
  3. Implementan `IDisposable` y se desuscriben en `Dispose`. El servicio vive
     todo el circuito: sin desuscribirse, quedaría referenciando componentes
     destruidos.
- **Por qué `Carrito.razor` también se suscribe.** Sus propios clics no lo
  necesitarían, porque Blazor redibuja al terminar un `@onclick`. Pero la
  rehidratación desde `localStorage` la dispara `IconoCarrito`. Sin la
  suscripción, al presionar F5 en `/carrito` el contador mostraba productos y
  la página decía que el carrito estaba vacío.
- **Por qué `Action` y no `EventCallback`:** quien emite es un servicio, no un
  componente, así que Blazor no lo conoce y no redibuja a nadie por su cuenta.
- **Datos copiados:**
  [ItemCarrito](IndumentariaEcommerce/IndumentariaEcommerce/Models/ItemCarrito.cs)
  copia los datos del producto en lugar de guardar una referencia. Así congela
  el precio, evita serializar la navegación `Categoria` y permite dibujar el
  carrito sin consultar la base.
- **Identidad de una línea:** el par producto + talle. La misma remera en S y
  en L son dos líneas.

## 8. JS Interop

- **Módulo:**
  [almacenamiento.js](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/js/almacenamiento.js)
  define `window.almacenamiento` con `guardar`, `leer` y `borrar`. Se carga en
  `App.razor`.
- **Llamadas desde C#:** `CarritoService` usa `IJSRuntime.InvokeAsync<string?>("almacenamiento.leer", ...)`
  y `InvokeVoidAsync("almacenamiento.guardar", ...)`.
- **Cuándo se lee:** `IconoCarrito.OnAfterRenderAsync(firstRender: true)` llama
  a `InicializarAsync()`. Durante el prerender no hay navegador, y una llamada a
  JS lanzaría una excepción. `IconoCarrito` está en el menú, presente en todas
  las páginas, así que el carrito se rehidrata entre por donde entre el
  usuario.
- **Robustez:**
  - Cada función JS tiene `try/catch`: `localStorage` puede estar bloqueado en
    modo incógnito o sin cuota.
  - En C#, un JSON corrupto se descarta con `catch (JsonException)`.
  - Un flag `_inicializado` evita leer dos veces.

## 9. HTML5 semántico

En [MainLayout.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor)
y [NavMenu.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor):

- `<aside>` para la columna lateral, con `<header>` (la marca) y `<nav>` (los
  enlaces).
- `<main>` para el contenido de cada página.
- `<footer>` como pie del sitio, **fuera** de `main`.
- Un `div.columna` sin significado agrupa `main` y `footer` para que, en
  escritorio, queden a la derecha del menú.

En las páginas: `<section>` en la portada, `<nav aria-label>` en la paginación y
en los breadcrumbs, `<form role="search">` en el panel, `<table>` con
`<thead>`/`<th scope="col">` y `<label for>` en todos los campos.

## 10. CSS Isolation

Un archivo `Componente.razor.css` se aplica **solo** a ese componente. Al
compilar, Blazor agrega un atributo único (`b-xxxx`) a los elementos y a los
selectores, y junta todo en `IndumentariaEcommerce.styles.css`, que se enlaza
en `App.razor`.

Componentes con CSS aislado: `TarjetaProducto`, `Paginacion`, `IconoCarrito`,
`Home`, `Catalogo`, `DetalleProducto`, `Carrito`, `AdminProductos`,
`FormularioProducto`, `MainLayout` y `NavMenu`.

- **`::deep`:** en
  [NavMenu.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor.css),
  `.nav-item ::deep .nav-link` hace falta porque el `<a>` lo genera el
  componente hijo `NavLink`, que no recibe el atributo de aislamiento del padre.
- **Estilos globales:** la paleta (variables CSS como `--negro` o `--acento`) y
  la adaptación de Bootstrap viven en
  [app.css](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/app.css),
  porque son globales por naturaleza.

## 11. Bootstrap 5 y diseño responsive

- Bootstrap 5.3.3, incluido localmente en `wwwroot/lib/bootstrap`.
- **Mobile-first:** las clases sin prefijo valen para pantallas chicas y los
  prefijos agregan columnas a medida que crece la pantalla. Ejemplo del
  catálogo: `row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4`.
- El detalle y el carrito usan `col-12 col-md-6` y `col-12 col-lg-8 / col-lg-4`.
- La tabla del panel va dentro de `table-responsive`; el formulario usa
  `col-6 col-md-3`.
- Las media queries propias también son mobile-first (`min-width`). En el
  layout, el menú lateral se fija a la izquierda desde 641 px; debajo de esa
  medida se convierte en una barra superior con menú desplegable.
- Las variables de Bootstrap (`--bs-body-bg`, `--bs-btn-bg`, etc.) se
  redefinen en `app.css` para aplicar la paleta sin reescribir componentes.

## 12. EF Core y DbContextFactory

Contexto:
[AppDbContext.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/AppDbContext.cs).

**Por qué una factory.** En Blazor Server un servicio `Scoped` vive todo el
circuito, que puede durar minutos u horas. `DbContext` no es thread-safe: dos
eventos solapados, como un doble clic, romperían un contexto compartido. Además
acumularía entidades rastreadas sin límite. Con `IDbContextFactory`, cada
método de `ProductoService` abre su propio contexto y lo descarta:

```csharp
await using var contexto = await _fabrica.CreateDbContextAsync();
```

**Otras decisiones:**
- **`AsNoTracking`** en las lecturas: no hace falta rastrear cambios en datos
  que solo se muestran.
- **Sin `AsNoTracking`** en `ActualizarAsync`: el rastreo es lo que permite que
  `SaveChanges` genere el `UPDATE` con las columnas modificadas.
- **`Precio` con `HasConversion<double>()`:** SQLite no tiene decimal nativo.
  Guardarlo como texto haría que "9000" se ordene después de "50000".
- **Índice sobre `Genero`:** es el filtro más usado del catálogo.

## 13. Entidades y relación

- [Categoria](IndumentariaEcommerce/IndumentariaEcommerce/Models/Categoria.cs):
  1 categoría tiene N productos (`ICollection<Producto> Productos`).
- [Producto](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs):
  `CategoriaId` (clave foránea) y `Categoria` (navegación, nullable).
- [Genero](IndumentariaEcommerce/IndumentariaEcommerce/Models/Genero.cs): enum
  con valores explícitos (`Hombre = 0`, `Mujer = 1`, `Unisex = 2`). Se guarda
  como entero, así que reordenar los miembros sin fijarlos cambiaría el género
  de los productos ya guardados.

La relación se configura con Fluent API en `AppDbContext.OnModelCreating`:

```csharp
.HasOne(p => p.Categoria).WithMany(c => c.Productos)
.HasForeignKey(p => p.CategoriaId).OnDelete(DeleteBehavior.Restrict);
```

Se usa `Restrict` en lugar del `Cascade` por defecto: borrar una categoría con
productos falla en lugar de borrarlos en silencio.

`Talles` es un texto separado por comas (`"S,M,L,XL"`). Es una decisión
deliberada para no crear una tabla intermedia que la consigna no pide; se separa
en la UI con `Split(',')`.

## 14. Migraciones y datos semilla

- **Seed:**
  [DatosSemilla.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/DatosSemilla.cs)
  usa `HasData` con 5 categorías y 30 productos, con Ids fijos.
- **Migraciones**, en
  [web/Migrations/](IndumentariaEcommerce/IndumentariaEcommerce/Migrations/):
  1. `20260830215822_MigracionInicial`: crea las tablas, la clave foránea, el
     índice e inserta el seed.
  2. `20260831021009_ActualizarCatalogoConFotos`: actualiza el seed (`UpdateData`)
     para alinear el catálogo con las fotos disponibles.
- **`AppDbContextModelSnapshot.cs`:** representa el modelo actual. EF Core lo
  compara contra el código para generar la próxima migración.
- **Al arrancar,** `Program.cs` llama a `Database.Migrate()`. En producción esto
  se haría durante el despliegue; acá permite clonar y ejecutar sin pasos
  extra.

## 15. CRUD, búsqueda y paginación

Toda la lógica está en
[ProductoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs).

| Operación | Método | Pantalla |
|---|---|---|
| Listar / buscar / paginar | `BuscarAsync(genero, categoriaId, textoBusqueda, pagina, tamanioPagina)` | Catálogo y AdminProductos |
| Leer uno | `ObtenerPorIdAsync` | DetalleProducto, FormularioProducto |
| Crear | `CrearAsync` | FormularioProducto (`/nuevo`) |
| Actualizar | `ActualizarAsync` | FormularioProducto (`/editar`) |
| Eliminar | `EliminarAsync` | AdminProductos, con `ConfirmacionEliminar` |

**`BuscarAsync`:**
- Arma un `IQueryable` con un `Where` por cada filtro presente. La consulta no
  se ejecuta hasta `CountAsync`/`ToListAsync`, así que los filtros viajan a
  SQLite como SQL.
- La búsqueda se traduce a `LIKE '%texto%'` con `EF.Functions.Like`.
- **El total se cuenta antes de paginar.** Contado después de `Skip`/`Take`,
  nunca superaría el tamaño de una página.
- Devuelve un
  [ResultadoPaginado&lt;T&gt;](IndumentariaEcommerce/IndumentariaEcommerce/Models/ResultadoPaginado.cs),
  un *record* genérico que calcula `TotalPaginas`.

**En las pantallas:**
- **Volver a la página 1 al cambiar el filtro** (en el catálogo y en el panel):
  si no, se podría pedir la página 3 de un resultado que tiene una sola.
- **Búsqueda del panel:** usa un formulario con botón. `busqueda` es lo escrito
  y `busquedaAplicada` lo efectivamente buscado, así que paginar no aplica texto
  que el usuario todavía no confirmó.
- **Al eliminar el último elemento de una página,** el panel retrocede una
  página.
- **`Paginacion`** muestra como máximo 5 números en una ventana que se desplaza
  alrededor de la página actual.
