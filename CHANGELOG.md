# Changelog

Todos los cambios relevantes del proyecto se documentan en este archivo.

El formato se basa en [Keep a Changelog](https://keepachangelog.com/es-ES/1.1.0/).

**Sobre las versiones.** El repositorio no tiene versiones etiquetadas (tags de
Git), así que no se asignan números de versión. Las entradas se agrupan por
fecha y se basan en el historial de commits. Los cambios todavía no commiteados
figuran en **Sin publicar**.

## [Sin publicar]

Ajustes surgidos de la auditoría de requisitos del TP, y documentación final.

### Agregado
- Búsqueda por nombre en el listado de administración, con regreso a la página 1 al cambiar el criterio y mensaje cuando no hay resultados.
- Validación personalizada `TallesValidosAttribute`: rechaza talles vacíos, con espacios o repetidos.
- Componente reutilizable `Cargando`, que reemplaza el indicador de carga repetido en cinco páginas.
- Componente reutilizable `ConfirmacionEliminar`, con `EventCallback` para confirmar o cancelar la baja.
- Documentación: [README.md](README.md), [GUIA_TECNICA.md](GUIA_TECNICA.md), [GUIA_USUARIO.md](GUIA_USUARIO.md), [CUMPLIMIENTO_REQUISITOS_TP.md](CUMPLIMIENTO_REQUISITOS_TP.md) y este archivo.

### Cambiado
- Layout con HTML5 semántico: `aside` para el menú lateral, `header` para la marca, `main` para el contenido y `footer` fuera de `main`.

### Corregido
- Al recargar `/carrito` con F5 la página mostraba el carrito vacío aunque el contador del menú tuviera productos. La página ahora se suscribe al evento `OnCambio` y se desuscribe en `Dispose`.

## 2026-08-30

Desarrollo inicial: 33 commits.

### Agregado
- Proyecto Blazor Server sobre .NET 9 a partir de la plantilla base.
- Modelos `Categoria` y `Producto` y enum `Genero`, con relación uno a muchos.
- EF Core 9 con SQLite, `AppDbContext` registrado mediante `AddDbContextFactory`.
- Migración inicial con datos semilla: 5 categorías y 30 productos.
- `ProductoService` con filtros, búsqueda y paginación.
- Catálogo con grilla de productos y los componentes `TarjetaProducto` (con CSS Isolation), `FiltroProductos` (género, categoría y búsqueda) y `Paginacion` (con ventana deslizante).
- Página de detalle de producto.
- Carrito: modelo `ItemCarrito`, módulo JS para `localStorage`, `CarritoService` como State Service, `IconoCarrito` con contador reactivo y página de carrito.
- Panel de administración: listado con baja, y formulario de alta y edición con `EditForm`.
- Portada propia con productos destacados.
- Las primeras fotos reales de productos y el listado de imágenes en [IMAGENES.md](IMAGENES.md).
- Convenciones del proyecto en `CLAUDE.md`.

### Cambiado
- Catálogo reorganizado alrededor de las fotos disponibles, con una nueva migración que actualiza el seed (`ActualizarCatalogoConFotos`).
- Paleta visual "streetwear" aplicada a tarjetas, navegación, pie de página, portada, catálogo, paginación, detalle, carrito y pantallas de administración.

### Eliminado
- Páginas de ejemplo y restos de la plantilla inicial de Blazor.
