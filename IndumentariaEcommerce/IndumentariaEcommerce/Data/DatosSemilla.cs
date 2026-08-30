using IndumentariaEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace IndumentariaEcommerce.Data;

/// <summary>
/// Datos iniciales del catálogo. Se aplican desde AppDbContext.OnModelCreating
/// y quedan escritos como INSERT dentro de la migración.
///
/// Los Id se asignan a mano porque la migración se genera en tiempo de diseño,
/// cuando todavía no existe la base que podría autonumerarlos.
/// Los valores deben ser deterministas: si alguno cambiara solo entre builds
/// (por ejemplo un DateTime.Now), EF Core detectaría una diferencia contra el
/// snapshot y pediría generar una migración nueva en cada compilación.
/// </summary>
public static class DatosSemilla
{
    public static void Aplicar(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Remeras", Descripcion = "Remeras, musculosas y tops de algodón y fibras técnicas." },
            new Categoria { Id = 2, Nombre = "Pantalones", Descripcion = "Jeans, joggings, cargos y pantalones de vestir." },
            new Categoria { Id = 3, Nombre = "Camperas", Descripcion = "Camperas, buzos y abrigos para media estación e invierno." },
            new Categoria { Id = 4, Nombre = "Vestidos", Descripcion = "Vestidos y enteritos para uso diario y ocasiones especiales." },
            new Categoria { Id = 5, Nombre = "Calzado", Descripcion = "Zapatillas, borcegos y sandalias urbanas." }
        );

        modelBuilder.Entity<Producto>().HasData(
            // ---------- Remeras ----------
            new Producto { Id = 1, Nombre = "Remera Lisa de Algodón", Descripcion = "Remera de algodón peinado, cuello redondo reforzado y corte clásico.", Precio = 24900m, Stock = 42, ImagenUrl = "/imagenes/remera-lisa.jpg", Talles = "S,M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 2, Nombre = "Remera Oversize Estampada", Descripcion = "Remera de corte amplio con estampa frontal serigrafiada.", Precio = 27500m, Stock = 28, ImagenUrl = "/imagenes/remera-oversize.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 1 },
            new Producto { Id = 3, Nombre = "Remera Básica Cuello V", Descripcion = "Remera de modal y algodón, caída suave y escote en V.", Precio = 22900m, Stock = 55, ImagenUrl = "/imagenes/remera-cuello-v.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 1 },
            new Producto { Id = 4, Nombre = "Remera Manga Larga Rayada", Descripcion = "Manga larga a rayas horizontales en algodón liviano.", Precio = 31900m, Stock = 19, ImagenUrl = "/imagenes/remera-rayada.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 5, Nombre = "Remera Deportiva Dry-Fit", Descripcion = "Tejido técnico de secado rápido con paneles de ventilación.", Precio = 29900m, Stock = 36, ImagenUrl = "/imagenes/remera-dryfit.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 1 },
            new Producto { Id = 6, Nombre = "Musculosa de Algodón", Descripcion = "Musculosa básica de algodón con sisa amplia.", Precio = 19900m, Stock = 31, ImagenUrl = "/imagenes/musculosa.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 7, Nombre = "Top Corto de Algodón", Descripcion = "Top corto de algodón elastizado, ideal para entrenamiento.", Precio = 21500m, Stock = 24, ImagenUrl = "/imagenes/top-corto.jpg", Talles = "XS,S,M", Genero = Genero.Mujer, CategoriaId = 1 },

            // ---------- Pantalones ----------
            new Producto { Id = 8, Nombre = "Jean Slim Fit Azul", Descripcion = "Jean de denim elastizado con cinco bolsillos y lavado clásico.", Precio = 68900m, Stock = 22, ImagenUrl = "/imagenes/jean-slim.jpg", Talles = "38,40,42,44", Genero = Genero.Hombre, CategoriaId = 2 },
            new Producto { Id = 9, Nombre = "Jean Mom Fit", Descripcion = "Denim rígido de tiro alto con calce holgado en la pierna.", Precio = 72900m, Stock = 18, ImagenUrl = "/imagenes/jean-mom.jpg", Talles = "34,36,38,40", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 10, Nombre = "Pantalón Cargo Beige", Descripcion = "Gabardina resistente con bolsillos laterales y puño ajustable.", Precio = 64900m, Stock = 27, ImagenUrl = "/imagenes/cargo-beige.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 2 },
            new Producto { Id = 11, Nombre = "Jogging de Frisa", Descripcion = "Jogging de frisa perchada con cordón y puños elastizados.", Precio = 45900m, Stock = 40, ImagenUrl = "/imagenes/jogging-frisa.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 2 },
            new Producto { Id = 12, Nombre = "Pantalón de Vestir", Descripcion = "Corte recto en tejido de lana fría, con pinzas al frente.", Precio = 79900m, Stock = 14, ImagenUrl = "/imagenes/pantalon-vestir.jpg", Talles = "40,42,44,46", Genero = Genero.Hombre, CategoriaId = 2 },
            new Producto { Id = 13, Nombre = "Calza Deportiva", Descripcion = "Calza de tiro alto en lycra con bolsillo lateral para celular.", Precio = 38900m, Stock = 33, ImagenUrl = "/imagenes/calza-deportiva.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 14, Nombre = "Bermuda de Gabardina", Descripcion = "Bermuda de gabardina de algodón con cintura ajustable.", Precio = 42900m, Stock = 21, ImagenUrl = "/imagenes/bermuda.jpg", Talles = "38,40,42,44", Genero = Genero.Hombre, CategoriaId = 2 },

            // ---------- Camperas ----------
            new Producto { Id = 15, Nombre = "Campera de Cuero Ecológico", Descripcion = "Biker de cuero sintético con cierres metálicos y forro interior.", Precio = 189900m, Stock = 8, ImagenUrl = "/imagenes/campera-cuero.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 3 },
            new Producto { Id = 16, Nombre = "Campera Puffer", Descripcion = "Campera inflable con relleno sintético y capucha desmontable.", Precio = 145900m, Stock = 16, ImagenUrl = "/imagenes/campera-puffer.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 17, Nombre = "Buzo Canguro con Capucha", Descripcion = "Buzo de frisa con bolsillo canguro y capucha con cordón.", Precio = 58900m, Stock = 45, ImagenUrl = "/imagenes/buzo-canguro.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 18, Nombre = "Campera Rompeviento", Descripcion = "Rompeviento liviano con costuras selladas y capucha plegable.", Precio = 89900m, Stock = 12, ImagenUrl = "/imagenes/rompeviento.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },
            new Producto { Id = 19, Nombre = "Cardigan de Punto", Descripcion = "Cardigan tejido de punto grueso con botones de nácar.", Precio = 67900m, Stock = 17, ImagenUrl = "/imagenes/cardigan.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 3 },
            new Producto { Id = 20, Nombre = "Campera de Jean", Descripcion = "Campera de denim con lavado stone y bolsillos con tapa.", Precio = 94900m, Stock = 0, ImagenUrl = "/imagenes/campera-jean.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 21, Nombre = "Chaleco Inflable", Descripcion = "Chaleco acolchado sin mangas, ideal para entretiempo.", Precio = 78900m, Stock = 20, ImagenUrl = "/imagenes/chaleco.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },

            // ---------- Vestidos ----------
            new Producto { Id = 22, Nombre = "Vestido Midi Floreado", Descripcion = "Vestido midi de viscosa con estampa floral y cintura elastizada.", Precio = 84900m, Stock = 15, ImagenUrl = "/imagenes/vestido-midi.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 23, Nombre = "Vestido Largo de Fiesta", Descripcion = "Vestido largo con espalda descubierta y falda de caída fluida.", Precio = 129900m, Stock = 6, ImagenUrl = "/imagenes/vestido-fiesta.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 24, Nombre = "Vestido Camisero", Descripcion = "Vestido camisero de popelina con cinto del mismo género.", Precio = 76900m, Stock = 23, ImagenUrl = "/imagenes/vestido-camisero.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 25, Nombre = "Vestido de Verano de Lino", Descripcion = "Vestido corto de lino con breteles regulables.", Precio = 69900m, Stock = 26, ImagenUrl = "/imagenes/vestido-lino.jpg", Talles = "XS,S,M", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 26, Nombre = "Enterito de Jean", Descripcion = "Enterito de denim liviano con bolsillo frontal y tiradores.", Precio = 92900m, Stock = 11, ImagenUrl = "/imagenes/enterito-jean.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },

            // ---------- Calzado ----------
            new Producto { Id = 27, Nombre = "Zapatillas Urbanas", Descripcion = "Zapatillas de lona con suela de goma vulcanizada.", Precio = 119900m, Stock = 30, ImagenUrl = "/imagenes/zapatillas-urbanas.jpg", Talles = "38,39,40,41,42,43", Genero = Genero.Unisex, CategoriaId = 5 },
            new Producto { Id = 28, Nombre = "Zapatillas Running", Descripcion = "Zapatillas de entrenamiento con amortiguación en el talón.", Precio = 134900m, Stock = 18, ImagenUrl = "/imagenes/zapatillas-running.jpg", Talles = "40,41,42,43,44", Genero = Genero.Hombre, CategoriaId = 5 },
            new Producto { Id = 29, Nombre = "Borcegos de Cuero", Descripcion = "Borcegos de cuero vacuno con cordones y suela tractora.", Precio = 156900m, Stock = 9, ImagenUrl = "/imagenes/borcegos.jpg", Talles = "36,37,38,39,40", Genero = Genero.Mujer, CategoriaId = 5 },
            new Producto { Id = 30, Nombre = "Sandalias de Cuero", Descripcion = "Sandalias bajas de cuero natural con hebilla ajustable.", Precio = 54900m, Stock = 25, ImagenUrl = "/imagenes/sandalias.jpg", Talles = "36,37,38,39,40", Genero = Genero.Mujer, CategoriaId = 5 }
        );
    }
}
