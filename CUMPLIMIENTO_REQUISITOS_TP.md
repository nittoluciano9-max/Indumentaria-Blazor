# Cumplimiento de requisitos — TP Integrador Blazor

Relaciona cada requisito de la consigna con su evidencia en el código. Los
enlaces son relativos a la raíz del repositorio y abren el archivo tanto en
GitHub como desde la vista previa Markdown de Visual Studio. Los números de
línea figuran como texto al lado de cada enlace: una vez abierto el archivo en
Visual Studio, se ubican con **Ctrl+G** (Ir a línea).

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

**Resumen conceptual**
- **¿Qué es?** Un componente Razor es una pieza de interfaz (`.razor`) que combina HTML y C# y se usa como una etiqueta. Es reutilizable cuando recibe sus datos por parámetros y no depende de una pantalla en particular.
- **¿Para qué sirve?** Para escribir una parte de la interfaz una sola vez y usarla en varias pantallas.
- **¿Cómo lo aplicamos?** Hay 6 componentes en `Components/Shared/`. Por ejemplo, `TarjetaProducto` se usa en la portada y en el catálogo, y `Paginacion` en el catálogo y en el panel.

**Cómo explicarlo al profesor**
> "Separé la interfaz en componentes chicos que reciben datos con `[Parameter]` y avisan lo que pasa con `EventCallback`. Por ejemplo, `Paginacion` no sabe si está en el catálogo o en el panel: recibe la página actual y el total, y devuelve la página elegida. Así se reutiliza sin copiar código."

#### Detalle técnico

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

**Referencias explicadas.**
- `Paginacion.razor` dibuja los botones Anterior, números y Siguiente. Recibe
  `PaginaActual` y `TotalPaginas` y, al hacer clic, devuelve la página elegida
  con `OnCambioPagina.InvokeAsync(pagina)`.
- `Catalogo.razor` y `AdminProductos.razor` son los padres: guardan en qué
  página están, se la pasan a `Paginacion` y, cuando reciben el aviso, vuelven a
  pedir los datos al servicio.

**Cómo se relacionan.**
1. El padre (por ejemplo `Catalogo`) consulta `ProductoService` y obtiene la
   página actual y el total de páginas.
2. Se los pasa a `<Paginacion>` como parámetros.
3. El usuario toca un número: `Paginacion` no cambia nada por su cuenta, solo
   dispara el `EventCallback` con ese número.
4. El padre recibe el número, actualiza su variable de página y vuelve a
   consultar el servicio.
5. Blazor redibuja al padre y, con él, a `Paginacion` con los valores nuevos.

### 2. Formularios con EditForm y validación avanzada

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** `EditForm` es el formulario de Blazor asociado a un objeto modelo, que valida sus datos antes de enviarlos. La validación avanzada agrega reglas propias a las estándar.
- **¿Para qué sirve?** Para impedir que se guarden datos incorrectos y mostrarle al usuario qué corregir.
- **¿Cómo lo aplicamos?** El alta y la edición de productos validan con atributos declarados en `Producto.cs`. Por ejemplo, `S,M,S` en Talles lo rechaza `[TallesValidos]` y " Remera" en Nombre lo rechaza la expresión regular.

**Cómo explicarlo al profesor**
> "Las reglas están declaradas una sola vez en el modelo con Data Annotations, y el `EditForm` las aplica mediante `DataAnnotationsValidator`. Además de `Required`, `StringLength` y `Range`, hice un atributo propio, `TallesValidos`, y una expresión regular para el nombre. Si algo falla, `OnValidSubmit` no se ejecuta y el producto no se guarda."

#### Detalle técnico

**Cómo está hecho.**
- El formulario de alta y edición usa `EditForm` con `OnValidSubmit`, que
  ejecuta el guardado solo si los datos son válidos.
- `DataAnnotationsValidator` aplica las reglas del modelo; `ValidationSummary`
  y `ValidationMessage` muestran los errores.
- La validación avanzada es un atributo propio, `[TallesValidos]`: rechaza
  talles vacíos, con espacios o repetidos.

**Evidencia.**
- [FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor) — líneas 38 a 40: `EditForm`, `DataAnnotationsValidator` y `ValidationSummary`.
- [Producto.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `Required`, `StringLength`, `Range` y `RegularExpression`.
  - Línea 19: `[RegularExpression(@"^\S(?:.*\S)?$")]` sobre `Nombre`.
  - [Línea 44](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `[TallesValidos]`.
  - [Línea 51](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `[Range(1, …)]`, que obliga a elegir una categoría.
- [TallesValidosAttribute.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Validaciones/TallesValidosAttribute.cs): hereda de `ValidationAttribute` y sobrescribe `IsValid`.

**Referencias explicadas.**
- **`EditForm` (líneas 38 a 40):** envuelve el formulario y lo conecta con el
  objeto `modelo`. `DataAnnotationsValidator` lee los atributos de `Producto` y
  `ValidationSummary` lista todos los errores juntos arriba del formulario.
- **`Producto.cs`:** las reglas viven en el modelo, no en la pantalla.
  `Required` exige un valor, `StringLength` limita el largo y `Range` fija un
  mínimo y un máximo.
- **Línea 19:** expresión regular sobre `Nombre`.
  - `^\S` exige que el primer carácter no sea un espacio.
  - `(?:.*\S)?$` exige lo mismo para el último y permite cualquier cosa en el
    medio.
  - "Remera negra" es válido; " Remera negra" y "Remera negra " no.
  - El vacío lo controla `[Required]`.
- **Línea 44:** aplica la validación propia a la propiedad `Talles`.
- **Línea 51:** `Range(1, …)` sobre `CategoriaId`. El valor 0 corresponde a la
  opción vacía del desplegable ("Seleccioná…") y no es un Id válido, así que obliga a elegir.
- **`TallesValidosAttribute.cs`:** `IsValid` recibe el texto, lo separa por
  comas con `Split(',')` y devuelve `false` si alguna parte está vacía, tiene
  caracteres no permitidos o se repite. Devolver `false` es lo que hace aparecer
  el mensaje de error.

**Cómo se relacionan.**
1. `FormularioProducto` crea un `EditForm` sobre un objeto `Producto`.
2. Al tocar Guardar, `DataAnnotationsValidator` recorre los atributos
   declarados en `Producto.cs`, incluido `[TallesValidos]`.
3. Para `Talles`, .NET llama a `TallesValidosAttribute.IsValid`.
4. Si alguna regla falla, los errores aparecen en `ValidationSummary` y en cada
   `ValidationMessage`, y `OnValidSubmit` **no** se ejecuta.
5. Si todo es válido, se ejecuta `GuardarAsync`, que llama al servicio.

**Qué mostrar.** En Administración → Nuevo producto, escribir `S,M,S` en Talles
y tocar Guardar: aparece el error y no se guarda.

**Qué explicar.**
- Sin `DataAnnotationsValidator` el formulario acepta cualquier dato.
- El atributo personalizado no requiere migración, porque EF Core lo ignora.
- Los talles repetidos romperían `@key` en el detalle del producto.

### 3. Routing con parámetros y navegación programática

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** El routing asocia cada URL a un componente mediante `@page`, y los parámetros de ruta toman valores de la URL. La navegación programática cambia de página desde C#.
- **¿Para qué sirve?** Para que cada producto tenga su propia URL y para redirigir al usuario después de una acción.
- **¿Cómo lo aplicamos?** `/producto/{Id:int}` abre el detalle del producto con ese Id, y después de guardar un producto `NavigateTo` vuelve al listado del panel.

**Cómo explicarlo al profesor**
> "Uso `@page` con parámetros tipados, como `{Id:int}`, que Blazor copia a una propiedad `[Parameter]`. El formulario tiene dos rutas, una de alta y otra de edición, y decide qué hacer según llegue o no el `Id`. Al guardar redirijo con `NavigationManager.NavigateTo`."

#### Detalle técnico

**Cómo está hecho.**
- Rutas con parámetros tipados: `/producto/{Id:int}` y `/admin/productos/{Id:int}/editar`.
- El formulario declara **dos** `@page`: con `Id` edita, sin `Id` da de alta.
- Después de guardar se navega por código con `NavigationManager`.

**Evidencia.**
- [DetalleProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/DetalleProducto.razor) — línea 3: ruta `/producto/{Id:int}`.
- [FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor) — líneas 5 a 6: las dos rutas del formulario.
- [FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor) — línea 193: `NavigateTo`.
- [NavMenu.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor): `NavLink` con `Match="NavLinkMatch.All"`.

**Referencias explicadas.**
- **`DetalleProducto.razor`, línea 3:** `@page "/producto/{Id:int}"` toma el
  número de la URL y lo guarda en la propiedad `[Parameter] public int Id`.
- **`FormularioProducto.razor`, líneas 5 a 6:** dos `@page` para un mismo
  componente. `/admin/productos/nuevo` deja `Id` en `null` (alta) y
  `/admin/productos/{Id:int}/editar` lo completa (edición). La propiedad
  `EsEdicion` decide según eso.
- **`FormularioProducto.razor`, línea 193:** `NavigateTo("/admin/productos")`
  cambia de página desde C#, sin que el usuario haga clic en un enlace.
- **`NavMenu.razor`:** `NavLink` es un enlace que agrega la clase `active`
  cuando la URL coincide. `NavLinkMatch.All` exige coincidencia exacta, para
  que "Inicio" (`/`) no quede resaltado en todas las páginas.

**Cómo se relacionan.**
1. El usuario abre `/admin/productos/5/editar`: el router de Blazor elige
   `FormularioProducto` y asigna `Id = 5`.
2. `OnParametersSetAsync` ve que hay `Id` y pide el producto a
   `ProductoService.ObtenerPorIdAsync`.
3. Al guardar, el formulario llama a `ActualizarAsync` (o a `CrearAsync` si no
   había `Id`).
4. Terminado el guardado, `NavigationManager.NavigateTo` lleva al listado.

**Qué explicar.**
- La restricción `:int` hace que `/producto/abc` dé 404.
- Se usa `OnParametersSetAsync` porque Blazor reutiliza el componente al pasar
  de `/producto/1` a `/producto/2`, y `OnInitializedAsync` no volvería a correr.

### 4. State Service con notificación reactiva

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** Un State Service es un servicio inyectado que guarda un estado compartido entre componentes. La notificación reactiva es un evento que avisa cuando ese estado cambia.
- **¿Para qué sirve?** Para que varias partes de la pantalla muestren el mismo dato actualizado sin pasárselo entre sí.
- **¿Cómo lo aplicamos?** `CarritoService` guarda el carrito. Al agregar un producto desde el detalle, el contador del menú se actualiza solo.

**Cómo explicarlo al profesor**
> "`CarritoService` está registrado como `Scoped`: una instancia por circuito, o sea un carrito por usuario. Cuando cambia dispara el evento `OnCambio`, al que están suscritos `IconoCarrito` y la página `Carrito`, que se redibujan con `InvokeAsync(StateHasChanged)`. Al destruirse se desuscriben en `Dispose`."

#### Detalle técnico

**Cómo está hecho.**
- `CarritoService` guarda el carrito y avisa cada cambio con el evento
  `OnCambio`.
- `IconoCarrito` (el contador del menú) y la página `Carrito` se suscriben,
  redibujan con `InvokeAsync(StateHasChanged)` y se desuscriben en `Dispose`.

**Evidencia.**
- [CarritoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs) — línea 43: `public event Action? OnCambio`.
- [Program.cs](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs) — línea 38: registrado como `Scoped`.
- [IconoCarrito.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor) — línea 23 y [línea 51](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor): suscripción y desuscripción.
- [Carrito.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor) — línea 133 y [línea 144](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor): ídem.

**Referencias explicadas.**
- **`CarritoService.cs`, línea 43:** `OnCambio` es un evento de C#. El servicio
  lo dispara después de agregar, quitar, cambiar cantidades o vaciar, y no
  necesita saber quién lo escucha.
- **`Program.cs`, línea 38:** `AddScoped<CarritoService>()` crea una instancia
  del servicio por circuito, es decir por pestaña conectada.
- **`IconoCarrito.razor`, línea 23:** `OnCambio += AlCambiarElCarrito`
  suscribe el componente. El handler ejecuta `InvokeAsync(StateHasChanged)`
  para redibujar el contador.
- **`IconoCarrito.razor`, línea 51:** en `Dispose`, `OnCambio -= …` lo
  desuscribe cuando el componente se destruye.
- **`Carrito.razor`, líneas 133 y 144:** el mismo patrón en la página del
  carrito, para que se redibuje cuando el carrito se carga desde
  `localStorage` después de un F5.

**Cómo se relacionan.**
1. `DetalleProducto` llama a `CarritoService.AgregarAsync`.
2. El servicio actualiza su lista interna y dispara `OnCambio`.
3. Todos los componentes suscritos (`IconoCarrito` y, si está abierta, la
   página `Carrito`) ejecutan su handler.
4. Cada handler llama a `InvokeAsync(StateHasChanged)` y Blazor redibuja ese
   componente con los datos nuevos.
5. Al salir de una página, `Dispose` quita la suscripción.

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

**Resumen conceptual**
- **¿Qué es?** Es usar etiquetas que describen el rol del contenido (`header`, `nav`, `main`, `aside`, `footer`, `section`) en lugar de usar solo `div`.
- **¿Para qué sirve?** Para mejorar la accesibilidad y dar al documento una estructura que los navegadores y lectores de pantalla entienden.
- **¿Cómo lo aplicamos?** `MainLayout` usa `aside` para el menú, `main` para la página y `footer` para el pie. El buscador del panel es un `form role="search"`.

**Cómo explicarlo al profesor**
> "Armé el layout con etiquetas semánticas: `aside` para el menú lateral, `header` para la marca, `nav` para los enlaces, `main` para el contenido y `footer` para el pie. Las páginas agregan `section` y `nav` con `aria-label`. Uso `div` solo donde el elemento no tiene significado, como la columna que agrupa `main` y `footer`."

#### Detalle técnico

**Cómo está hecho.** El layout usa `aside` para la columna lateral, `header`
para la marca, `nav` para los enlaces, `main` para el contenido y `footer` como
pie, fuera de `main`.

**Evidencia.**
- [MainLayout.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor) — línea 9 (`aside`), [línea 14](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor) (`main`) y [línea 19](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor) (`footer`).
- [NavMenu.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor) — línea 3: `header`.
- En las páginas: `section` en [Home.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Home.razor), `nav aria-label` en la paginación y los breadcrumbs, y `form role="search"` en el panel.

**Referencias explicadas.**
- **`MainLayout.razor`:** es la plantilla común a todas las páginas.
  - `aside` (línea 9) contiene el menú lateral.
  - `main` (línea 14) contiene `@Body`, donde Blazor inserta la página actual.
  - `footer` (línea 19) es el pie.
- **`NavMenu.razor`, línea 3:** `header` agrupa la marca de la tienda y el
  botón del menú móvil.
- **Páginas:** `section` agrupa bloques con título propio. `aria-label` le da
  nombre a cada `nav` para los lectores de pantalla. `role="search"` identifica
  el formulario como buscador.

**Cómo se relacionan.**
1. Toda página con `@page` se dibuja dentro de `MainLayout`.
2. `MainLayout` incluye `NavMenu` dentro del `aside` y la página dentro de
   `main`.
3. Cada página agrega su propia estructura (`section`, `nav`, `form`) dentro
   de `main`.

**Qué mostrar.** F12 → Elements: la estructura `aside`, `main` y `footer`.

**Qué explicar.** El `div.columna` no tiene significado: solo agrupa `main` y
`footer` para mantener el diseño en escritorio.

### 6. CSS Isolation en al menos 3 componentes

**Estado: CUMPLE.** Hay 11 archivos `.razor.css`: 9 de componentes propios y 2
de layout.

**Resumen conceptual**
- **¿Qué es?** Son estilos escritos en un archivo `.razor.css` que se aplican solo a su componente. Blazor lo logra agregando un atributo único a los elementos.
- **¿Para qué sirve?** Para que los estilos de un componente no afecten a otros.
- **¿Cómo lo aplicamos?** Hay 11 archivos `.razor.css`. Por ejemplo, `TarjetaProducto.razor.css` define el aspecto de la tarjeta sin tocar el resto del sitio.

**Cómo explicarlo al profesor**
> "Cada componente con estilo propio tiene su `.razor.css`. Al compilar, Blazor agrega un atributo como `b-xxxx` a los selectores y a los elementos, y junta todo en `IndumentariaEcommerce.styles.css`. Cuando necesito alcanzar un elemento de un componente hijo, como el `<a>` que dibuja `NavLink`, uso `::deep`."

#### Detalle técnico

**Cómo está hecho.** Cada `Componente.razor.css` se aplica solo a ese
componente. Blazor agrega un atributo único a los elementos y los reúne en
`IndumentariaEcommerce.styles.css`.

**Evidencia.**
- Archivos: [TarjetaProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/TarjetaProducto.razor.css), [Paginacion.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/Paginacion.razor.css), [IconoCarrito.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor.css), [Home.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Home.razor.css), [Catalogo.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor.css), [DetalleProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/DetalleProducto.razor.css), [Carrito.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Carrito.razor.css), [AdminProductos.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor.css), [FormularioProducto.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor.css), [MainLayout.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/MainLayout.razor.css) y [NavMenu.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor.css).
- [App.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/App.razor) — línea 13: enlace al bundle `IndumentariaEcommerce.styles.css`.

**Referencias explicadas.**
- **Archivos `.razor.css`:** cada uno lleva el mismo nombre que su componente.
  Al compilar, Blazor les agrega un atributo único (por ejemplo `b-3x7k…`) a los
  selectores y a los elementos de ese componente, así que una regla de
  `TarjetaProducto` no afecta a otros componentes.
- **`App.razor`, línea 13:** incluye `IndumentariaEcommerce.styles.css`, el
  archivo que Blazor genera reuniendo todos los `.razor.css`. Sin esa línea, los
  estilos aislados no se cargarían.

**Cómo se relacionan.**
1. Se escribe CSS en `Componente.razor.css`.
2. Al compilar, Blazor reescribe cada selector agregándole el atributo del
   componente.
3. Todo se junta en `IndumentariaEcommerce.styles.css`.
4. `App.razor` carga ese archivo y el navegador aplica cada regla solo a los
   elementos que tienen el atributo correspondiente.

**Qué mostrar.** F12: un elemento de la tarjeta con su atributo `b-xxxxxxxx`.

**Qué explicar.** `::deep`
([NavMenu.razor.css](IndumentariaEcommerce/IndumentariaEcommerce/Components/Layout/NavMenu.razor.css) — línea 79)
hace falta porque el `<a>` lo dibuja el componente hijo `NavLink`.

### 7. Diseño responsive con Bootstrap 5 (mobile-first)

**Estado: CUMPLE.** Usa Bootstrap 5.3.3, incluido en `wwwroot/lib/bootstrap`.

**Resumen conceptual**
- **¿Qué es?** Bootstrap 5 es un framework CSS con grilla y componentes listos. Mobile-first significa diseñar primero para celular y agregar ajustes a medida que crece la pantalla.
- **¿Para qué sirve?** Para que la tienda se vea bien en celular, tablet y escritorio.
- **¿Cómo lo aplicamos?** La grilla del catálogo muestra 1 tarjeta por fila en celular y hasta 4 en pantallas grandes, con las clases `row-cols-*`.

**Cómo explicarlo al profesor**
> "Las clases sin prefijo valen para celular y las de `sm`, `md`, `lg` y `xl` se suman al crecer la pantalla. El catálogo va de `row-cols-1` a `row-cols-xl-4`, la tabla del panel usa `table-responsive` y el menú lateral pasa a ser una barra desplegable en celular. Los colores se cambiaron redefiniendo las variables `--bs-*` en `app.css`."

#### Detalle técnico

**Cómo está hecho.** Las clases sin prefijo valen para móvil y los prefijos
`sm`, `md`, `lg` y `xl` agregan columnas al crecer la pantalla. Las media
queries propias usan `min-width`, que también es mobile-first.

**Evidencia.**
- [Catalogo.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/Catalogo.razor) — línea 31: `row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4`.
- [AdminProductos.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor): `table-responsive`.
- [FormularioProducto.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/FormularioProducto.razor): `col-6 col-md-3`.
- [app.css](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/app.css): la paleta, que redefine variables `--bs-*`.

**Referencias explicadas.**
- **`Catalogo.razor`, línea 31:** `row-cols-*` indica cuántas tarjetas van por
  fila: 1 en celular, 2 desde `sm`, 3 desde `lg` y 4 desde `xl`.
- **`AdminProductos.razor`:** `table-responsive` agrega desplazamiento
  horizontal a la tabla en pantallas angostas, en lugar de romper el diseño.
- **`FormularioProducto.razor`:** `col-6 col-md-3` ocupa media fila en
  celular y un cuarto de fila desde `md`.
- **`app.css`:** cambia los colores de Bootstrap redefiniendo sus variables
  CSS (`--bs-*`), sin modificar los archivos de Bootstrap.

**Cómo se relacionan.**
1. `App.razor` carga primero Bootstrap y después `app.css`.
2. `app.css` ajusta la paleta mediante las variables de Bootstrap.
3. Cada página usa las clases de grilla de Bootstrap para acomodarse al ancho
   de la pantalla.
4. Los `.razor.css` agregan solo lo que Bootstrap no resuelve.

**Qué mostrar.** F12 → modo dispositivo: el catálogo pasa de 1 a 4 columnas y
el menú lateral se convierte en barra superior desplegable.

### 8. JS Interop

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** JS Interop es el mecanismo de Blazor para llamar a JavaScript desde C#, mediante el servicio `IJSRuntime`.
- **¿Para qué sirve?** Para usar funciones del navegador que C# no tiene disponibles, como `localStorage`.
- **¿Cómo lo aplicamos?** El carrito se guarda en `localStorage` y se recupera al recargar la página con F5.

**Cómo explicarlo al profesor**
> "Hice un script, `almacenamiento.js`, con las funciones `guardar`, `leer` y `borrar` sobre `localStorage`. `CarritoService` las llama con `IJSRuntime` cada vez que cambia el carrito. La lectura se hace en `OnAfterRenderAsync` del primer render, porque durante el prerender todavía no hay navegador."

#### Detalle técnico

**Cómo está hecho.** El carrito se guarda en `localStorage` del navegador
mediante un módulo JavaScript propio, invocado desde C# con `IJSRuntime`.

**Evidencia.**
- [almacenamiento.js](IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/js/almacenamiento.js): `guardar`, `leer` y `borrar`, cada una con `try/catch`.
- [App.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/App.razor) — línea 21: carga del script.
- [CarritoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs) — línea 76 (leer), [línea 99](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs) (borrar) y [línea 204](IndumentariaEcommerce/IndumentariaEcommerce/Services/CarritoService.cs) (guardar).
- [IconoCarrito.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Shared/IconoCarrito.razor) — línea 35: rehidratación en `OnAfterRenderAsync(firstRender)`.

**Referencias explicadas.**
- **`almacenamiento.js`:** define el objeto global `window.almacenamiento` con
  tres funciones sobre `localStorage`. El `try/catch` evita que la aplicación
  falle si el navegador bloquea el almacenamiento.
- **`App.razor`, línea 21:** carga el script en la página para que esas
  funciones existan antes de llamarlas desde C#.
- **`CarritoService.cs`, línea 76:** `InvokeAsync<string?>("almacenamiento.leer", …)`
  trae el carrito guardado como texto JSON.
- **`CarritoService.cs`, línea 99:** si el JSON está dañado, llama a `borrar`
  y se empieza con el carrito vacío.
- **`CarritoService.cs`, línea 204:** después de cada cambio, convierte la lista
  a JSON y llama a `guardar`.
- **`IconoCarrito.razor`, línea 35:** en el primer render llama a
  `InicializarAsync`, que lee lo guardado. Como el ícono está en el menú, esto
  ocurre en cualquier página por la que entre el usuario.

**Cómo se relacionan.**
1. El navegador carga `almacenamiento.js` desde `App.razor`.
2. En el primer render, `IconoCarrito` llama a `CarritoService.InicializarAsync`.
3. El servicio usa `IJSRuntime` para ejecutar `almacenamiento.leer`, convierte
   el JSON en una lista y dispara `OnCambio`.
4. Cada vez que el carrito cambia, el servicio llama a `almacenamiento.guardar`.
5. Después de un F5 se repite el paso 2 y el carrito se recupera.

**Qué mostrar.** F12 → Application → Local Storage → clave `carrito`.

**Qué explicar.** JS solo se puede llamar después del primer render, porque
durante el prerender no hay navegador.

---

## Unidad 3 — Persistencia

### 9. EF Core con SQLite y DbContextFactory

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** EF Core es el ORM de .NET: traduce clases C# a tablas y consultas LINQ a SQL. `DbContextFactory` crea un `DbContext` nuevo cada vez que se le pide.
- **¿Para qué sirve?** Para acceder a la base SQLite sin escribir SQL a mano y de forma segura en Blazor Server.
- **¿Cómo lo aplicamos?** `ProductoService` crea un contexto corto para cada operación, por ejemplo al buscar los productos del catálogo.

**Cómo explicarlo al profesor**
> "Registré `AddDbContextFactory` con `UseSqlite` en lugar de `AddDbContext`. En Blazor Server un `DbContext` scoped viviría todo el circuito y no es thread-safe, así que dos eventos simultáneos podrían romperlo. Con la fábrica, cada método del servicio abre un contexto, consulta y lo libera con `await using`."

#### Detalle técnico

**Cómo está hecho.** Se registra una fábrica de contextos. Cada método de
`ProductoService` crea un contexto corto con
`await using var contexto = await _fabrica.CreateDbContextAsync()`.

**Evidencia.**
- [Program.cs](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs) — línea 27: `AddDbContextFactory<AppDbContext>` con `UseSqlite`.
- [ProductoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs): recibe `IDbContextFactory<AppDbContext>`.
- [appsettings.json](IndumentariaEcommerce/IndumentariaEcommerce/appsettings.json): cadena `ConexionSqlite`.
- [IndumentariaEcommerce.csproj](IndumentariaEcommerce/IndumentariaEcommerce/IndumentariaEcommerce.csproj): EF Core Sqlite 9.0.19.

**Referencias explicadas.**
- **`Program.cs`, línea 27:** registra la fábrica en lugar del contexto.
  `UseSqlite` indica que la base es SQLite y toma la cadena de conexión de la
  configuración.
- **`ProductoService.cs`:** recibe `IDbContextFactory<AppDbContext>` en el
  constructor. Cada método crea su propio contexto y lo libera al terminar
  (`await using`).
- **`appsettings.json`:** `ConexionSqlite` vale `Data Source=indumentaria.db`,
  el archivo de la base.
- **`.csproj`:** declara el paquete `Microsoft.EntityFrameworkCore.Sqlite`, que
  aporta EF Core y el proveedor para SQLite.

**Cómo se relacionan.**
1. Al arrancar, `Program.cs` lee `ConexionSqlite` y registra la fábrica.
2. Blazor crea `ProductoService` y le entrega la fábrica.
3. Cuando un componente pide datos, el servicio crea un `AppDbContext`, hace la
   consulta y lo descarta.
4. Así cada operación usa un contexto propio y dos eventos simultáneos nunca
   comparten el mismo.

**Qué explicar.** En Blazor Server un `DbContext` *scoped* viviría todo el
circuito, durante minutos u horas. Además no es thread-safe: dos eventos
solapados lo romperían. Es la pregunta más probable de la defensa.

### 10. Al menos 2 entidades relacionadas

**Estado: CUMPLE.** `Categoria` (1) se relaciona con `Producto` (N).

**Resumen conceptual**
- **¿Qué es?** Son dos clases del modelo que EF Core guarda como tablas vinculadas por una clave foránea.
- **¿Para qué sirve?** Para representar que cada producto pertenece a una categoría.
- **¿Cómo lo aplicamos?** Una `Categoria` tiene muchos `Producto`, y cada producto guarda su `CategoriaId`. Por ejemplo, una remera pertenece a la categoría Remeras.

**Cómo explicarlo al profesor**
> "`Producto` tiene `CategoriaId` como clave foránea y `Categoria` como navegación, y `Categoria` tiene la colección `Productos`. En `AppDbContext` configuré la relación con `HasOne`, `WithMany` y `HasForeignKey`, y `OnDelete(Restrict)` para que borrar una categoría no borre sus productos. Al consultar uso `Include` para traer la categoría."

#### Detalle técnico

**Evidencia.**
- [Categoria.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Categoria.cs): `ICollection<Producto> Productos`.
- [Producto.cs](IndumentariaEcommerce/IndumentariaEcommerce/Models/Producto.cs): `CategoriaId` y `Categoria`.
- [AppDbContext.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/AppDbContext.cs) — líneas 44 a 47: `HasOne` / `WithMany` / `HasForeignKey` / `OnDelete(Restrict)`.

**Referencias explicadas.**
- **`Categoria.cs`:** la colección `Productos` es la propiedad de navegación
  del lado "uno": desde una categoría se llega a sus productos.
- **`Producto.cs`:** `CategoriaId` es la clave foránea (el número que se guarda
  en la tabla). `Categoria` es la navegación del lado "muchos": desde un
  producto se llega a su categoría.
- **`AppDbContext.cs`, líneas 44 a 47:** declaran la relación de forma
  explícita. Cada producto tiene una categoría (`HasOne`), cada categoría tiene
  muchos productos (`WithMany`) y la unión es `CategoriaId` (`HasForeignKey`).
  `Restrict` impide borrar una categoría que todavía tiene productos.

**Cómo se relacionan.**
1. Las dos clases del modelo se apuntan entre sí con sus propiedades de
   navegación.
2. `AppDbContext` le dice a EF Core cómo traducir esa relación a tablas.
3. La migración crea la columna `CategoriaId` con su clave foránea.
4. Al consultar, `Include(p => p.Categoria)` hace que EF Core traiga también
   la categoría de cada producto.

**Qué explicar.**
- `Restrict` evita que borrar una categoría elimine sus productos en silencio.
- `Include(p => p.Categoria)` carga la relación en las consultas.

### 11. Migraciones versionadas y datos de seed

**Estado: CUMPLE.** Hay 2 migraciones, 5 categorías y 30 productos.

**Resumen conceptual**
- **¿Qué es?** Una migración es código generado que describe cómo cambiar la base para que coincida con el modelo. El seed son los datos iniciales que se insertan con las migraciones.
- **¿Para qué sirve?** Para que cualquiera que clone el repositorio obtenga la misma base, con datos de prueba.
- **¿Cómo lo aplicamos?** Dos migraciones crean las tablas y cargan 5 categorías y 30 productos, y la aplicación las aplica sola al arrancar.

**Cómo explicarlo al profesor**
> "Los datos iniciales están en `DatosSemilla` con `HasData`. Las migraciones se generan con `dotnet ef migrations add` y se versionan en Git; la base no. Al arrancar, `Database.Migrate()` crea la base si no existe y aplica las migraciones pendientes."

#### Detalle técnico

**Evidencia.**
- [DatosSemilla.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/DatosSemilla.cs): `HasData`, aplicado desde [AppDbContext.cs](IndumentariaEcommerce/IndumentariaEcommerce/Data/AppDbContext.cs) — línea 56.
- [20260830215822_MigracionInicial.cs](IndumentariaEcommerce/IndumentariaEcommerce/Migrations/20260830215822_MigracionInicial.cs): tablas, clave foránea, índice y seed.
- [20260831021009_ActualizarCatalogoConFotos.cs](IndumentariaEcommerce/IndumentariaEcommerce/Migrations/20260831021009_ActualizarCatalogoConFotos.cs): `UpdateData` del seed.
- [Program.cs](IndumentariaEcommerce/IndumentariaEcommerce/Program.cs) — línea 50: `Database.Migrate()` al arrancar.

**Referencias explicadas.**
- **`DatosSemilla.cs`:** define las 5 categorías y los 30 productos iniciales
  con `HasData`. `AppDbContext` lo invoca en la línea 56, dentro de
  `OnModelCreating`.
- **`MigracionInicial`:** código generado por `dotnet ef migrations add`. Crea
  las tablas, la clave foránea, el índice por `Genero` e inserta el seed.
- **`ActualizarCatalogoConFotos`:** segunda migración. Con `UpdateData`
  actualiza las filas del seed cuando cambiaron nombres e imágenes.
- **`Program.cs`, línea 50:** `Database.Migrate()` aplica las migraciones
  pendientes. Si la base no existe, la crea.

**Cómo se relacionan.**
1. Se modifica el modelo o el seed en C#.
2. `dotnet ef migrations add` compara ese modelo con la última migración y
   genera una nueva con las diferencias.
3. Las migraciones se versionan en Git; la base no.
4. Al ejecutar la app, `Migrate()` aplica a la base las migraciones que
   todavía no aplicó.

**Comprobación realizada.** `dotnet ef migrations has-pending-model-changes`
respondió que el modelo no tiene cambios sin migrar.

**Qué explicar.** La base (`*.db`) no se versiona
([.gitignore](.gitignore)): cada clon la regenera desde las migraciones.

### 12. CRUD completo con paginación y búsqueda

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** CRUD son las cuatro operaciones básicas sobre datos: crear, leer, actualizar y eliminar. La paginación divide los resultados en páginas y la búsqueda los filtra por texto.
- **¿Para qué sirve?** Para administrar el catálogo completo desde la aplicación sin que los listados se vuelvan lentos o interminables.
- **¿Cómo lo aplicamos?** El panel lista los productos de a 10, busca por nombre y permite alta, edición y baja con confirmación.

**Cómo explicarlo al profesor**
> "`ProductoService` tiene un método por operación: `BuscarAsync`, `ObtenerPorIdAsync`, `CrearAsync`, `ActualizarAsync` y `EliminarAsync`. La búsqueda usa `EF.Functions.Like` y la paginación `Skip` y `Take`, contando el total antes de recortar. Al cambiar el texto de búsqueda se vuelve a la página 1."

#### Detalle técnico

**Evidencia** en [ProductoService.cs](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs):

| Operación | Método | Línea |
|---|---|---|
| Listar / buscar / paginar | `BuscarAsync` | [L30](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs) |
| Leer | `ObtenerPorIdAsync` | [L90](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs) |
| Crear | `CrearAsync` | [L138](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs) |
| Actualizar | `ActualizarAsync` | [L156](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs) |
| Eliminar | `EliminarAsync` | [L191](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs) |

- La búsqueda usa `EF.Functions.Like` ([línea 70](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs)) y el total se cuenta antes de paginar ([línea 76](IndumentariaEcommerce/IndumentariaEcommerce/Services/ProductoService.cs)).
- En el panel:
  - Formulario de búsqueda: [AdminProductos.razor](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor) — línea 17.
  - Paso del texto a `BuscarAsync`: [línea 180](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor).
  - Baja con `ConfirmacionEliminar`: [línea 96](IndumentariaEcommerce/IndumentariaEcommerce/Components/Pages/AdminProductos.razor).

**Referencias explicadas.**
- **`BuscarAsync`:** arma la consulta en pasos y devuelve un
  `ResultadoPaginado` con los productos de la página y el total.
  - Aplica los filtros opcionales: género, categoría y texto.
  - Cuenta el total con `CountAsync`.
  - Recorta la página con `Skip` (saltear las anteriores) y `Take` (tomar el
    tamaño de página).
- **`ObtenerPorIdAsync`:** trae un producto con su categoría. Devuelve `null`
  si no existe.
- **`CrearAsync`:** agrega el producto con `Add` y lo guarda con
  `SaveChangesAsync`.
- **`ActualizarAsync`:** trae el producto **con seguimiento** (sin
  `AsNoTracking`), copia los valores nuevos y guarda. EF Core genera el
  `UPDATE` solo con los campos que cambiaron.
- **`EliminarAsync`:** busca el producto, lo quita con `Remove` y guarda.
- **`EF.Functions.Like` (línea 70):** se traduce al `LIKE '%texto%'` de SQL, es
  decir "contiene".
- **Línea 76:** el total se cuenta antes de `Skip`/`Take`, porque con él se
  calcula cuántas páginas hay.
- **`AdminProductos.razor`:**
  - Línea 17: el `form` ejecuta `BuscarAsync` del componente al presionar Enter
    o tocar Buscar.
  - Línea 180: `CargarAsync` le pasa al servicio el texto aplicado y la página.
  - Línea 96: muestra `ConfirmacionEliminar` solo en la fila que se está
    borrando.

**Cómo se relacionan.**
1. El usuario escribe en el buscador del panel y confirma: el componente guarda
   el texto, vuelve a la página 1 y llama a `CargarAsync`.
2. `CargarAsync` llama a `ProductoService.BuscarAsync` con texto, página y
   tamaño.
3. El servicio filtra con `LIKE`, cuenta el total, recorta la página y
   devuelve el resultado.
4. La tabla muestra las filas y `Paginacion` recibe el total de páginas.
5. Editar lleva a `FormularioProducto`, y Eliminar abre `ConfirmacionEliminar`,
   que al confirmar llama a `EliminarAsync` y recarga la lista.

**Qué mostrar.** En el panel, buscar `zapatillas` estando en la página 2: vuelve
a la página 1 con los resultados. Después crear, editar y eliminar un producto.

**Qué explicar.**
- Contar después de `Skip`/`Take` daría siempre una sola página.
- Al cambiar el criterio de búsqueda se vuelve a la página 1.

---

## Unidad 4 — Prácticas profesionales

### 13. Repositorio GitHub con historial limpio y Conventional Commits

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** Git registra el historial de cambios y GitHub lo publica en línea. Conventional Commits es una convención para los mensajes: un tipo, un alcance opcional y una descripción.
- **¿Para qué sirve?** Para que el historial se entienda y se pueda ver qué cambió y por qué.
- **¿Cómo lo aplicamos?** El repositorio es público en GitHub y cada commit sigue el formato, por ejemplo `feat(validaciones): agregar expresion regular y actualizar documentacion`.

**Cómo explicarlo al profesor**
> "Cada commit es una unidad de trabajo con un mensaje `tipo(alcance): descripción`, en español y en imperativo. Uso `feat` para funcionalidades, `fix` para correcciones y `docs`, `style`, `refactor` y `chore` para el resto. El `.gitignore` deja afuera la base de datos y los archivos de compilación."

#### Detalle técnico

**Evidencia verificada.**
- Repositorio público: `https://github.com/nittoluciano9-max/Indumentaria-Blazor`, rama `main`.
- Los 38 commits publicados, incluido el de esta versión de la guía, siguen el formato `tipo(alcance): descripción`.
  Se usan los tipos `feat`, `fix`, `style`, `refactor`, `docs` y `chore`.
- Las bases de datos y los artefactos de compilación están excluidos en [.gitignore](.gitignore).

**Estado de la publicación.** Todo el trabajo está publicado en GitHub, en la
rama `main`: el código, los ajustes técnicos, la validación con expresión
regular y la documentación, incluida esta guía.

**Qué mostrar.** `git log --oneline` o la pestaña Git Changes / Git Repository
de Visual Studio.

**Referencias explicadas.**
- **Conventional Commits:** cada mensaje empieza con un tipo que dice qué clase
  de cambio es. `feat` es una funcionalidad nueva, `fix` una corrección, `docs`
  documentación, `style` cambios visuales, `refactor` una reorganización sin
  cambio de comportamiento y `chore` mantenimiento. El alcance entre paréntesis
  indica la zona afectada, por ejemplo `fix(carrito)`.
- **`.gitignore`:** lista lo que Git no debe versionar: la base `*.db`, que se
  regenera, y las carpetas `bin/` y `obj/`, que produce la compilación.

### 14. CI/CD con GitHub Actions

**Estado: PENDIENTE — fuera del alcance de esta entrega.**

**Resumen conceptual**
- **¿Qué es?** CI (integración continua) compila y prueba el código automáticamente en cada push, y CD (despliegue continuo) lo publica automáticamente. GitHub Actions es el servicio de GitHub que ejecuta esos procesos.
- **¿Para qué sirve?** Para detectar errores apenas se sube el código y publicar sin pasos manuales.
- **¿Cómo lo aplicamos?** No está implementado en esta entrega. Un workflow en `.github/workflows/` ejecutaría `dotnet build` en cada push.

**Cómo explicarlo al profesor**
> "CI/CD quedó fuera del alcance acordado para esta entrega, junto con el despliegue. Para implementarlo agregaría un archivo YAML en `.github/workflows/` que, en cada push, restaure las dependencias, compile y, cuando haya pruebas, las ejecute. La parte de repositorio e historial con Conventional Commits sí está cumplida."

#### Detalle técnico

- La parte de **Git y GitHub** está cumplida (requisito 13).
- La parte de **CI/CD no está implementada.** La carpeta `.github/workflows/`
  está vacía y no hay ningún workflow versionado, así que GitHub no ejecuta
  build ni deploy automáticos.
- Se dejó fuera junto con el despliegue (requisito 16), por decisión acordada
  para esta entrega.

**Referencias explicadas.** GitHub Actions ejecuta los archivos `.yml` que
estén en `.github/workflows/`. Un workflow de CI, por ejemplo, ejecutaría
`dotnet build` en cada push para avisar si el código dejó de compilar. Como no
hay ningún archivo en esa carpeta, GitHub no ejecuta nada.

### 15. README.md profesional

**Estado: CUMPLE.**

**Resumen conceptual**
- **¿Qué es?** Es el archivo de presentación del repositorio, que GitHub muestra en su página principal.
- **¿Para qué sirve?** Para que cualquiera entienda y ejecute el proyecto sin leer el código.
- **¿Cómo lo aplicamos?** `README.md` describe la tienda, las tecnologías, la estructura, cómo ejecutarla con Visual Studio o `dotnet` y las limitaciones conocidas.

**Cómo explicarlo al profesor**
> "El README explica qué es el proyecto, con qué tecnologías y versiones está hecho y cómo ejecutarlo desde Visual Studio o la línea de comandos. También documenta la base SQLite y las limitaciones conocidas. Para el detalle técnico remite a `GUIA_TECNICA.md`."

#### Detalle técnico

[README.md](README.md) incluye descripción, tecnologías con versiones,
características, estructura, requisitos previos, ejecución con Visual Studio y
con `dotnet`, configuración de SQLite y limitaciones conocidas. Lo complementa
[GUIA_TECNICA.md](GUIA_TECNICA.md).

**Referencias explicadas.**
- **`README.md`:** es la portada del repositorio en GitHub. Explica qué es el
  proyecto y cómo ejecutarlo sin tener que leer el código.
- **`GUIA_TECNICA.md`:** amplía cada concepto de Blazor aplicado y responde
  las preguntas probables de la defensa.

### 16. Aplicación desplegada en URL pública

**Estado: PENDIENTE — excluido por el docente para esta entrega.**

**Resumen conceptual**
- **¿Qué es?** Es publicar la aplicación en un servidor accesible desde internet, con una URL pública.
- **¿Para qué sirve?** Para que cualquiera pueda usarla sin instalar nada.
- **¿Cómo lo aplicamos?** No se desplegó en esta entrega: la aplicación se ejecuta localmente en `https://localhost:7220`.

**Cómo explicarlo al profesor**
> "El despliegue quedó fuera del alcance de esta entrega. Para publicarla necesitaría un hosting compatible con .NET 9 y con WebSockets, porque Blazor Server mantiene un circuito por SignalR, y resolver dónde se guarda el archivo SQLite. Localmente funciona completa."

#### Detalle técnico

No hay despliegue ni URL pública. La aplicación se ejecuta localmente
(`https://localhost:7220`).

**Referencias explicadas.** `localhost` es la propia computadora: la dirección
solo responde en la máquina donde se ejecuta la aplicación. Publicarla
requeriría un servidor que soporte .NET 9 y WebSockets (que Blazor Server usa
para el circuito) y un lugar donde guardar el archivo SQLite.

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

**Qué hace cada uno.**
1. **`TarjetaProducto`:** muestra foto, datos, precio y aviso de stock de un
   producto, y enlaza a su detalle.
2. **`Paginacion`:** dibuja los botones de página y avisa cuál eligió el
   usuario.
3. **`FiltroProductos`:** reúne género, categoría y texto en un
   `FiltrosCatalogo` y lo envía al catálogo. La búsqueda espera un instante a
   que el usuario deje de escribir.
4. **`IconoCarrito`:** muestra la cantidad total de artículos y se redibuja
   con cada cambio del carrito.
5. **`Cargando`:** mensaje de espera con texto configurable.
6. **`ConfirmacionEliminar`:** muestra "Sí" y "No" y avisa al padre cuál se
   eligió. El padre decide si borra.

---

## Glosario de términos

- **Componente Razor:** archivo `.razor` que combina HTML y C#. Se usa como
  etiqueta, por ejemplo `<Paginacion />`.
- **`@page`:** convierte un componente en página accesible por URL.
- **`[Parameter]`:** marca una propiedad que el componente padre puede
  completar como atributo de la etiqueta. Es la forma de pasar datos **hacia
  abajo**.
- **`[EditorRequired]`:** indica que ese parámetro es obligatorio. El
  compilador avisa si el padre no lo pasa.
- **`EventCallback`:** parámetro por el que el hijo avisa algo al padre (datos
  **hacia arriba**). A diferencia de un `Action` común, Blazor redibuja al
  padre automáticamente después de ejecutarlo.
- **`EditForm`:** formulario de Blazor asociado a un objeto modelo.
  `OnValidSubmit` se ejecuta solo si la validación pasó.
- **`DataAnnotationsValidator`:** activa en el `EditForm` las reglas
  declaradas con atributos en el modelo (`Required`, `Range`, etc.).
- **`ValidationAttribute`:** clase base para crear validaciones propias. Se
  sobrescribe `IsValid`.
- **`NavigationManager`:** servicio para leer la URL actual y navegar desde C#
  con `NavigateTo`.
- **State Service:** servicio que guarda estado compartido entre componentes,
  como el carrito, y avisa sus cambios.
- **`Scoped`:** una instancia por circuito, es decir por pestaña conectada.
  **`Singleton`** sería una sola para todos los usuarios.
- **Circuito:** la conexión permanente (SignalR) entre una pestaña del
  navegador y el servidor en Blazor Server.
- **`StateHasChanged`:** le indica a Blazor que el componente debe
  redibujarse.
- **`InvokeAsync`:** ejecuta código en el hilo de render del componente. Hace
  falta cuando el aviso llega desde afuera, como un evento del servicio.
- **`IDisposable` / `Dispose`:** método que Blazor llama al destruir el
  componente. Se usa para desuscribirse de eventos.
- **Ciclo de vida:**
  - `OnInitializedAsync` se ejecuta una vez al crear el componente.
  - `OnParametersSetAsync` se ejecuta cada vez que cambian sus parámetros.
  - `OnAfterRenderAsync` se ejecuta después de dibujarlo, y es el primer
    momento en que se puede usar JavaScript.
- **Prerender:** primer dibujo que el servidor genera como HTML estático antes
  de conectar el circuito. En ese momento no hay navegador disponible.
- **`IJSRuntime`:** servicio para llamar funciones JavaScript desde C#.
- **`localStorage`:** almacenamiento del navegador que persiste aunque se
  cierre la pestaña.
- **CSS Isolation:** estilos en `Componente.razor.css` que se aplican solo a
  ese componente.
- **`::deep`:** permite que una regla aislada alcance elementos dibujados por
  un componente hijo.
- **Mobile-first:** se diseña primero para celular y se agregan cambios para
  pantallas más grandes.
- **`DbContext`:** objeto de EF Core que representa una sesión con la base:
  consulta y guarda cambios.
- **`DbContextFactory`:** crea un `DbContext` nuevo cada vez que se lo pide.
- **Migración:** clase generada que describe cómo pasar la base de un estado
  del modelo al siguiente.
- **Seed (`HasData`):** datos iniciales que se cargan con las migraciones.
- **Clave foránea:** columna que guarda el Id de otra tabla, como
  `CategoriaId`.
- **`Include`:** pide a EF Core que traiga también una entidad relacionada.
- **`AsNoTracking`:** consulta de solo lectura. EF Core no sigue los cambios
  de esos objetos, lo que la hace más liviana.
- **`Skip` / `Take`:** saltear N registros y tomar M. Así se implementa la
  paginación.

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
