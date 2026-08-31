# Imágenes del catálogo

Van en `IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/imagenes/`.

**Estado: 22 de 30 productos tienen foto real.** Los 8 restantes muestran el
placeholder `sin-imagen.svg`, que aparece solo gracias al `onerror` de
`TarjetaProducto`.

## Antes de agregar una

**Los nombres no se eligen: están fijados en la migración.** Cada `ImagenUrl`
forma parte de un `INSERT` dentro de `Migrations/`. Si renombrás un archivo hay
que editar `DatosSemilla.cs` y generar una migración nueva.

Si tu imagen es `.png` o `.webp`, convertila a `.jpg` antes que cambiar el
nombre en el seed.

**Formato sugerido:** JPG, aproximadamente 600 × 800 px (relación 3:4).
El CSS usa `aspect-ratio: 3 / 4` con `object-fit: cover`, así que recorta lo que
sobra; una imagen 3:4 se recorta menos.

## Las 8 que faltan

| Archivo | Producto | Género | Categoría |
|---|---|---|---|
| `remera-cuello-v.jpg` | Remera Básica Cuello V | Mujer | Remeras |
| `top-corto.jpg` | Top Corto de Algodón | Mujer | Remeras |
| `cargo-beige.jpg` | Pantalón Cargo Beige | Unisex | Pantalones |
| `jogging-frisa.jpg` | Jogging de Frisa | Mujer | Pantalones |
| `bermuda.jpg` | Bermuda de Gabardina | Hombre | Pantalones |
| `vestido-midi.jpg` | Vestido Midi Floreado | Mujer | Vestidos |
| `vestido-camisero.jpg` | Vestido Camisero | Mujer | Vestidos |
| `enterito-jean.jpg` | Enterito de Jean | Mujer | Vestidos |

## Verificar cuáles faltan

```bash
cd IndumentariaEcommerce/IndumentariaEcommerce
grep -o '/imagenes/[a-z0-9-]*\.jpg' Data/DatosSemilla.cs | sed 's|/imagenes/||' | sort > /tmp/esperadas.txt
ls wwwroot/imagenes/*.jpg | xargs -n1 basename | sort > /tmp/presentes.txt
comm -23 /tmp/esperadas.txt /tmp/presentes.txt
```

## Las 22 que ya están

| Archivo | Producto | Género |
|---|---|---|
| `remera-lisa.jpg` | Remera Lisa de Algodón | Hombre |
| `remera-oversize.jpg` | Remera Oversize Estampada | Mujer |
| `remera-negra.jpg` | Remera Negra Estampada | Hombre |
| `remera-jaspeada.jpg` | Remera Oversize Jaspeada | Hombre |
| `remera-estampa.jpg` | Remera Blanca con Estampa | Mujer |
| `jean-recto.jpg` | Jean Recto Azul | Mujer |
| `jean-mom.jpg` | Jean Mom Fit | Mujer |
| `jean-tiro-alto.jpg` | Jean Clásico de Tiro Alto | Mujer |
| `campera-puffer.jpg` | Campera Puffer Naranja | Hombre |
| `campera-puffer-negra.jpg` | Campera Puffer Negra | Hombre |
| `campera-jean.jpg` | Campera de Jean | Unisex |
| `campera-jean-clasica.jpg` | Campera de Jean Clásica | Hombre |
| `buzo-canguro.jpg` | Buzo Canguro con Capucha | Unisex |
| `buzo-blanco-mujer.jpg` | Buzo Oversize Blanco | Mujer |
| `buzo-blanco-hombre.jpg` | Buzo con Capucha Blanco | Hombre |
| `buzo-naranja.jpg` | Buzo Naranja Estampado | Unisex |
| `buzo-liso.jpg` | Buzo de Frisa Liso | Unisex |
| `zapatillas-urbanas.jpg` | Zapatillas Urbanas | Mujer |
| `zapatillas-running.jpg` | Zapatillas Running | Hombre |
| `zapatillas-blancas.jpg` | Zapatillas Clásicas Blancas | Unisex |
| `zapatillas-retro.jpg` | Zapatillas Retro Rojas | Hombre |
| `zapatillas-skate.jpg` | Zapatillas Skate Estampadas | Unisex |
