# CLAUDE.md

Convenciones del proyecto. Leer antes de escribir código.

## Proyecto

E-commerce de indumentaria masculina y femenina. Proyecto integrador anual de la
materia (Blazor, Unidad 4).

- **Stack:** Blazor Server, .NET 9, EF Core con SQLite.
- **Solución:** `IndumentariaEcommerce/IndumentariaEcommerce.sln`
- **Proyecto web:** `IndumentariaEcommerce/IndumentariaEcommerce/`
- **Namespace raíz:** `IndumentariaEcommerce`
- **Idioma:** todo el código, nombres, comentarios y textos de la UI en español.

## Cómo trabajar en este repo

Esto es lo más importante del archivo. El autor es estudiante y tiene que
**defender el trabajo oralmente**, así que el objetivo no es solo que la app
funcione: es que entienda cada línea.

1. **Explicar antes de generar.** Antes de tocar un archivo, decir en pocas
   frases qué se va a hacer y por qué (qué problema resuelve, qué alternativas
   había). Recién después escribir el código.
2. **Un archivo por vez.** No generar la app completa de una. Un archivo,
   explicación, y esperar antes de seguir con el siguiente.
3. **Señalar lo aprendible.** Si en el código hay algo que vale la pena entender
   (por qué `@rendermode`, por qué `Scoped` y no `Singleton`, cómo funciona el
   binding de `EditForm`, qué hace `StateHasChanged`), decirlo explícitamente.
   No dar por sabido nada de Blazor.
4. **Nada de magia.** Preferir la solución simple y explicable a la solución
   corta pero opaca. Si algo requiere una abstracción, justificarla.
5. **Comentarios en español**, sobrios, explicando el *por qué* y no el *qué*.

## Modelo de datos

```
Categoria
  Id (int, PK)
  Nombre (string)
  Descripcion (string)
  Productos (ICollection<Producto>)   // navegación

Producto
  Id (int, PK)
  Nombre (string)
  Descripcion (string)
  Precio (decimal)
  Stock (int)
  ImagenUrl (string)
  Talles (string)                     // ej: "S,M,L,XL"
  Genero (enum Genero)                // Hombre | Mujer | Unisex
  CategoriaId (int, FK)
  Categoria (Categoria)               // navegación
```

Relación **uno a muchos**: una `Categoria` tiene muchos `Producto`.

`Talles` se guarda como string separado por comas (decisión deliberada: evita
una tabla intermedia que la consigna no pide; se parsea en la UI cuando hace
falta).

## Alcance

**Incluye:**
- Catálogo con filtro por género y por categoría, búsqueda por nombre y paginación.
- Página de detalle de producto.
- Carrito manejado con un State Service.
- Panel de administración con ABM (CRUD) de productos.

**No incluye (no implementar, no proponer):**
- Login / autenticación / roles.
- Pasarela de pagos.
- Tabla de pedidos u órdenes.

## Requisitos de la consigna

Estos puntos se evalúan, así que ninguno es opcional. Los ocho están cumplidos;
al lado de cada uno, dónde se demuestra.

- [x] Mínimo **6 componentes Razor** propios — hay 9: `Catalogo`, `TarjetaProducto`, `FiltroProductos`, `Paginacion`, `DetalleProducto`, `IconoCarrito`, `Carrito`, `AdminProductos` y `FormularioProducto`.
- [x] **EditForm** con validaciones — `FormularioProducto.razor`, con `DataAnnotationsValidator`, `ValidationSummary` y siete `ValidationMessage`. Las reglas se declaran una sola vez, en `Producto.cs`.
- [x] **State Service** para el carrito — `Services/CarritoService.cs`, registrado como `Scoped` en `Program.cs`.
- [x] **CSS Isolation** — seis `.razor.css` propios, más los dos que traía la plantilla.
- [x] **Bootstrap** y diseño responsive — grilla del catálogo, tabla del panel con `table-responsive` y formulario en dos columnas.
- [x] **JS Interop** con `localStorage` — `wwwroot/js/almacenamiento.js`, invocado desde `CarritoService` para persistir y rehidratar el carrito.
- [x] **Migraciones de EF Core** con datos semilla — `MigracionInicial`, con 5 categorías y 30 productos.
- [x] Commits en formato **Conventional Commits** — todo el historial.

Pendiente fuera de la consigna: faltan las 30 imágenes reales del catálogo (ver
`IMAGENES.md`). Hasta que estén, las tarjetas muestran un placeholder.

## Estructura de carpetas

Dentro de `IndumentariaEcommerce/IndumentariaEcommerce/`:

```
Models/                 entidades (Producto, Categoria, Genero)
Data/                   AppDbContext, configuración y datos semilla
Services/               CarritoService y demás servicios de estado/datos
Components/
  Layout/               MainLayout, NavMenu
  Pages/                componentes ruteables (@page)
  Shared/               componentes reutilizables sin ruta
Migrations/             generada por EF Core, no editar a mano
wwwroot/                css, imágenes, js de interop
```

## Convenciones de código

- **C#:** PascalCase para clases, propiedades y métodos; camelCase para
  variables locales y parámetros. Nullable habilitado: respetar `?` y no
  silenciar warnings con `!` salvo que esté justificado.
- **Componentes:** un componente por archivo, nombre en PascalCase
  (`TarjetaProducto.razor`). Componentes con ruta van en `Components/Pages/`,
  los reutilizables en `Components/Shared/`.
- **Parámetros de componente:** `[Parameter] public Tipo Nombre { get; set; }`,
  con `[EditorRequired]` cuando el componente no funciona sin ese dato.
- **Código C# en componentes:** bloque `@code { }` al final del `.razor`. Si un
  componente crece mucho, recién ahí evaluar code-behind (`.razor.cs`).
- **Estilos:** primero utilidades de Bootstrap; CSS propio solo cuando Bootstrap
  no alcanza, y siempre en el `.razor.css` del componente (CSS Isolation).
- **Acceso a datos:** los componentes **no** consultan la base directamente:
  le piden los datos a un servicio (`ProductoService`). Ese servicio usa
  `IDbContextFactory<AppDbContext>` y abre un contexto corto por consulta.
  En Blazor Server un `DbContext` scoped vive todo el circuito (minutos u
  horas) y no es thread-safe: dos eventos solapados lo rompen. Este es uno de
  los puntos a poder explicar en la defensa.
- **Async:** métodos asíncronos con sufijo `Async` y `await` real; nada de
  `.Result` ni `.Wait()`.

## Comandos

Ejecutar desde `IndumentariaEcommerce/`:

```bash
dotnet build
dotnet run --project IndumentariaEcommerce
dotnet ef migrations add <NombreMigracion> --project IndumentariaEcommerce
dotnet ef database update --project IndumentariaEcommerce
```

## Commits

Conventional Commits, en español, en imperativo y con alcance cuando ayude:

```
feat(catalogo): agregar filtro por genero
fix(carrito): corregir calculo del total
refactor(servicios): extraer logica de paginacion
docs: documentar convenciones del proyecto
chore(db): agregar migracion inicial con datos semilla
```

Un commit por unidad de trabajo entendible. Nada de commits gigantes que
mezclen varias funcionalidades.
