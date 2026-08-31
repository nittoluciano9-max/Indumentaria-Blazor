# Imágenes del catálogo

Las 30 imágenes que espera `DatosSemilla.cs`. Van en:

```
IndumentariaEcommerce/IndumentariaEcommerce/wwwroot/imagenes/
```

## Antes de empezar

**Los nombres no se cambian.** Están escritos dentro de la migración inicial como
parte del `INSERT`. Si renombrás un archivo tenés que editar `DatosSemilla.cs`, y
eso obliga a generar una migración nueva (`dotnet ef migrations add ...`), porque
EF Core detecta que los datos semilla ya no coinciden con el snapshot.

Si tu imagen es `.png` o `.webp`, conviene convertirla a `.jpg` antes que cambiar
el nombre en el seed.

**Formato sugerido:** JPG, aproximadamente 600 × 800 px (relación 3:4).

La relación importa poco en la práctica, porque el CSS usa `aspect-ratio: 3 / 4`
con `object-fit: cover` y recorta lo que sobra. Pero una imagen 3:4 se recorta
menos y el producto queda mejor encuadrado.

**No hace falta tenerlas todas.** Las que falten muestran el placeholder
`sin-imagen.svg` gracias al `onerror` de `TarjetaProducto`. Podés ir de a poco.

## Verificar cuáles faltan

```bash
cd IndumentariaEcommerce/IndumentariaEcommerce
grep -o '/imagenes/[a-z0-9-]*\.jpg' Data/DatosSemilla.cs | sed 's|/imagenes/||' | sort > /tmp/esperadas.txt
ls wwwroot/imagenes/*.jpg 2>/dev/null | xargs -n1 basename | sort > /tmp/presentes.txt
comm -23 /tmp/esperadas.txt /tmp/presentes.txt
```

## Listado

| # | Archivo | Producto | Género | Categoría |
|---|---|---|---|---|
| 1 | `remera-lisa.jpg` | Remera Lisa de Algodón | Hombre | Remeras |
| 2 | `remera-oversize.jpg` | Remera Oversize Estampada | Mujer | Remeras |
| 3 | `remera-cuello-v.jpg` | Remera Básica Cuello V | Mujer | Remeras |
| 4 | `remera-rayada.jpg` | Remera Manga Larga Rayada | Hombre | Remeras |
| 5 | `remera-dryfit.jpg` | Remera Deportiva Dry-Fit | Unisex | Remeras |
| 6 | `musculosa.jpg` | Musculosa de Algodón | Hombre | Remeras |
| 7 | `top-corto.jpg` | Top Corto de Algodón | Mujer | Remeras |
| 8 | `jean-slim.jpg` | Jean Slim Fit Azul | Hombre | Pantalones |
| 9 | `jean-mom.jpg` | Jean Mom Fit | Mujer | Pantalones |
| 10 | `cargo-beige.jpg` | Pantalón Cargo Beige | Unisex | Pantalones |
| 11 | `jogging-frisa.jpg` | Jogging de Frisa | Unisex | Pantalones |
| 12 | `pantalon-vestir.jpg` | Pantalón de Vestir | Hombre | Pantalones |
| 13 | `calza-deportiva.jpg` | Calza Deportiva | Mujer | Pantalones |
| 14 | `bermuda.jpg` | Bermuda de Gabardina | Hombre | Pantalones |
| 15 | `campera-cuero.jpg` | Campera de Cuero Ecológico | Mujer | Camperas |
| 16 | `campera-puffer.jpg` | Campera Puffer | Unisex | Camperas |
| 17 | `buzo-canguro.jpg` | Buzo Canguro con Capucha | Unisex | Camperas |
| 18 | `rompeviento.jpg` | Campera Rompeviento | Hombre | Camperas |
| 19 | `cardigan.jpg` | Cardigan de Punto | Mujer | Camperas |
| 20 | `campera-jean.jpg` | Campera de Jean | Unisex | Camperas |
| 21 | `chaleco.jpg` | Chaleco Inflable | Hombre | Camperas |
| 22 | `vestido-midi.jpg` | Vestido Midi Floreado | Mujer | Vestidos |
| 23 | `vestido-fiesta.jpg` | Vestido Largo de Fiesta | Mujer | Vestidos |
| 24 | `vestido-camisero.jpg` | Vestido Camisero | Mujer | Vestidos |
| 25 | `vestido-lino.jpg` | Vestido de Verano de Lino | Mujer | Vestidos |
| 26 | `enterito-jean.jpg` | Enterito de Jean | Mujer | Vestidos |
| 27 | `zapatillas-urbanas.jpg` | Zapatillas Urbanas | Unisex | Calzado |
| 28 | `zapatillas-running.jpg` | Zapatillas Running | Hombre | Calzado |
| 29 | `borcegos.jpg` | Borcegos de Cuero | Mujer | Calzado |
| 30 | `sandalias.jpg` | Sandalias de Cuero | Mujer | Calzado |

**Total: 30 archivos.** Repartidos en 7 remeras, 7 pantalones, 7 camperas, 5 vestidos y 4 de calzado.
