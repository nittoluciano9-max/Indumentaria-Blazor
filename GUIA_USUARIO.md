# Guía de usuario

Cómo usar la tienda y el panel de administración. Para instalar y ejecutar la
aplicación, ver el [README](README.md).

## Índice

1. [Navegación general](#1-navegación-general)
2. [Inicio](#2-inicio)
3. [Catálogo y filtros](#3-catálogo-y-filtros)
4. [Detalle de producto y talles](#4-detalle-de-producto-y-talles)
5. [Carrito](#5-carrito)
6. [Administración de productos](#6-administración-de-productos)
7. [Datos de demostración](#7-datos-de-demostración)
8. [Preguntas frecuentes](#8-preguntas-frecuentes)

---

## 1. Navegación general

- **Menú:** tiene **Inicio**, **Catálogo**, **Carrito** y **Administración**.
  - En pantallas grandes está fijo a la izquierda.
  - En el celular se abre con el botón de menú de la barra superior.
  - La sección en la que estás aparece resaltada.
- **Contador del carrito:** junto a "Carrito" se ve la cantidad total de
  artículos. Se actualiza solo, desde cualquier página.
- **Pie de página:** repite los mismos enlaces.

## 2. Inicio

La portada muestra un acceso al catálogo (**Ver el catálogo**) y una sección
**Destacados** con cuatro productos con stock. Hacer clic en una tarjeta abre
su detalle.

## 3. Catálogo y filtros

En **Catálogo** se ven los productos en una grilla, de a 12 por página y
ordenados por nombre. Cada tarjeta muestra:

- Foto, categoría, género, nombre y precio.
- El aviso **Sin stock** o **Últimas N unidades**, cuando quedan 10 o menos.

**Filtros**, que se pueden combinar:

| Filtro | Cómo se usa |
|---|---|
| **Buscar** | Escribir parte del nombre. La búsqueda se aplica sola un instante después de dejar de escribir. |
| **Género** | Todos, Hombre, Mujer o Unisex. |
| **Categoría** | Todas, Remeras, Pantalones, Camperas y Buzos, Vestidos o Calzado. |

- Arriba de la grilla se indica cuántos productos se encontraron y en qué
  página estás.
- Al cambiar un filtro se vuelve a la página 1.
- Si ningún producto coincide, aparece un mensaje que lo indica.
- **Paginación:** está al pie de la grilla, con **Anterior**, los números de
  página y **Siguiente**.

> La búsqueda no distingue mayúsculas, pero sí acentos: `algodon` no encuentra
> "Algodón".

## 4. Detalle de producto y talles

Al abrir un producto se ven la foto, la categoría, el género, el precio y la
descripción. Arriba hay una ruta de navegación (**Catálogo › Producto**) para
volver.

**Para agregar al carrito:**

1. Elegir un **talle**. El botón seleccionado se ve relleno.
2. Hasta elegir un talle, **Agregar al carrito** está deshabilitado y se lee
   "Elegí un talle para continuar."
3. Tocar **Agregar al carrito**. Aparece "Agregado al carrito." con un acceso a
   **Ver carrito**, y el contador del menú aumenta.

**Casos especiales:**
- Si quedan 10 unidades o menos, se avisa "Quedan N unidades."
- Si el producto no tiene stock, no se muestran talles ni botón: aparece
  "Este producto no tiene stock disponible."
- Si se abre un producto que no existe, se muestra "Producto no encontrado" con
  un botón para volver al catálogo.

## 5. Carrito

En **Carrito** se lista cada producto agregado. Cada combinación de **producto +
talle** es una línea distinta: la misma remera en S y en L ocupa dos líneas.

| Acción | Cómo |
|---|---|
| Cambiar la cantidad | Botones **+** y **−**. Si la cantidad llega a 0, la línea se quita. |
| Quitar un producto | Botón **X** de la línea. |
| Vaciar el carrito | **Vaciar carrito**, en el resumen. Se vacía en el acto, sin confirmación. |
| Volver a comprar | **Seguir comprando**. |

- El **Resumen** muestra la cantidad de artículos y el total.
- Cada línea muestra el precio unitario y el subtotal.
- **Límites por línea:** como máximo 20 unidades. Al agregar desde el detalle,
  tampoco se supera el stock disponible.
- **El carrito se conserva** al recargar la página (F5) o al cerrar y volver a
  abrir el navegador, porque se guarda en el propio navegador. Es independiente
  en cada navegador.
- No hay proceso de compra ni pago: el carrito es la última etapa de la tienda.

## 6. Administración de productos

En **Administración** se gestiona el catálogo. No requiere usuario ni
contraseña.

### Listado y búsqueda

- La tabla muestra miniatura, nombre, categoría, género, precio, stock y
  acciones, de a 10 productos por página.
- **Buscar:** escribir parte del nombre y tocar **Buscar** o presionar Enter.
  - Se vuelve a la página 1 y la paginación recorre solo los resultados.
  - Si no hay coincidencias, se indica "No se encontraron productos que
    contengan …".
  - Para volver a ver todos, borrar el texto y tocar **Buscar**.

### Alta

1. Tocar **Nuevo producto**.
2. Completar el formulario y tocar **Guardar**. **Cancelar** vuelve al listado
   sin guardar.
3. Al guardar, se vuelve al listado.

### Modificación

Tocar **Editar** en la fila del producto. Se abre el mismo formulario con los
datos cargados. Se modifica y se toca **Guardar**.

### Eliminación

1. Tocar **Eliminar** en la fila.
2. En esa misma fila aparece **¿Eliminar?** con **Sí** y **No**.
3. **Sí** borra el producto y muestra un mensaje de confirmación, que se cierra
   con la **×**. **No** cancela.

### Validaciones del formulario

Si algún dato no es válido, el producto no se guarda. Los errores aparecen
arriba del formulario y debajo de cada campo.

| Campo | Regla |
|---|---|
| Nombre | Obligatorio, hasta 100 caracteres |
| Descripción | Obligatoria, hasta 500 caracteres |
| Precio | Mayor a cero |
| Stock | No puede ser negativo |
| Género | Hombre, Mujer o Unisex |
| Categoría | Hay que elegir una |
| Talles | Obligatorio. Separados por coma, **sin espacios, sin vacíos y sin repetidos**. Válidos: `S,M,L,XL` o `38,40,42`. Inválidos: `S, M`, `S,,M`, `S,M,S` |
| Imagen | Obligatoria. Ruta dentro de la carpeta de imágenes, por ejemplo `/imagenes/remera-lisa.jpg` |

Si la ruta de la imagen no existe, la tienda muestra una imagen genérica en su
lugar.

## 7. Datos de demostración

La primera vez que se ejecuta la aplicación se cargan **5 categorías** y **30
productos**. Algunos sirven para probar casos puntuales:

- **Campera de Jean:** sin stock.
- **Campera de Jean Clásica** (7 unidades) y **Campera Puffer Negra** (9
  unidades): muestran el aviso de últimas unidades.
- **8 productos sin foto real,** entre ellos los tres de la categoría Vestidos:
  muestran la imagen genérica (ver [IMAGENES.md](IMAGENES.md)).

**Para volver a los datos originales** después de hacer pruebas: cerrar la
aplicación, borrar `indumentaria.db`, `indumentaria.db-shm` e
`indumentaria.db-wal` de la carpeta
`IndumentariaEcommerce/IndumentariaEcommerce/` y volver a ejecutar.

## 8. Preguntas frecuentes

**¿Por qué el contador del carrito muestra productos que agregué ayer?**
Porque el carrito se guarda en el navegador. Para empezar de cero, usar
**Vaciar carrito**.

**¿El carrito se comparte entre navegadores o computadoras?**
No. Cada navegador tiene su propio carrito.

**Cambié el precio de un producto, pero el carrito muestra el anterior.**
El carrito guarda el precio que tenía el producto al agregarlo. Para ver el
nuevo, quitar la línea y volver a agregarla.

**No encuentro un producto buscando con acentos o sin ellos.**
La búsqueda distingue acentos. Probar con la palabra escrita igual que en el
nombre del producto.
