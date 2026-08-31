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
///
/// El catálogo está armado alrededor de las fotos disponibles: 22 de los 30
/// productos tienen imagen real y los 8 restantes apuntan a un archivo que
/// todavía no existe, de modo que TarjetaProducto muestra el placeholder hasta
/// que se agregue la foto (ver IMAGENES.md).
///
/// Tres restricciones que se mantuvieron a propósito:
///   - 30 productos, para que la paginación siga dando tres páginas.
///   - 13 productos de mujer, para que filtrar por género siga dando dos.
///   - Los cuatro más caros con stock tienen foto, porque son los que la
///     portada muestra como destacados.
/// </summary>
public static class DatosSemilla
{
    public static void Aplicar(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Remeras", Descripcion = "Remeras y tops de algodón, lisos y estampados." },
            new Categoria { Id = 2, Nombre = "Pantalones", Descripcion = "Jeans, joggings, cargos y bermudas." },
            new Categoria { Id = 3, Nombre = "Camperas y Buzos", Descripcion = "Camperas, buzos con capucha y abrigo para todo el año." },
            new Categoria { Id = 4, Nombre = "Vestidos", Descripcion = "Vestidos y enteritos para uso diario." },
            new Categoria { Id = 5, Nombre = "Calzado", Descripcion = "Zapatillas urbanas, deportivas y de skate." }
        );

        modelBuilder.Entity<Producto>().HasData(
            // ---------- Remeras ----------
            new Producto { Id = 1, Nombre = "Remera Lisa de Algodón", Descripcion = "Remera de algodón peinado con cuello redondo reforzado y una estampa chica al frente. Corte clásico.", Precio = 24900m, Stock = 42, ImagenUrl = "/imagenes/remera-lisa.jpg", Talles = "S,M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 2, Nombre = "Remera Oversize Estampada", Descripcion = "Remera negra de corte amplio con estampa serigrafiada en la espalda.", Precio = 27500m, Stock = 28, ImagenUrl = "/imagenes/remera-oversize.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 1 },
            new Producto { Id = 3, Nombre = "Remera Negra Estampada", Descripcion = "Algodón negro con estampa tipográfica en blanco. Tacto suave y caída recta.", Precio = 29900m, Stock = 24, ImagenUrl = "/imagenes/remera-negra.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 4, Nombre = "Remera Oversize Jaspeada", Descripcion = "Gris jaspeado con estampa caligráfica al frente. Hombro caído y cuerpo ancho.", Precio = 31900m, Stock = 19, ImagenUrl = "/imagenes/remera-jaspeada.jpg", Talles = "S,M,L,XL", Genero = Genero.Hombre, CategoriaId = 1 },
            new Producto { Id = 5, Nombre = "Remera Blanca con Estampa", Descripcion = "Algodón blanco con estampa central en rojo. Cuello redondo y corte recto.", Precio = 28900m, Stock = 33, ImagenUrl = "/imagenes/remera-estampa.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 1 },
            new Producto { Id = 6, Nombre = "Remera Básica Cuello V", Descripcion = "Remera de modal y algodón, caída suave y escote en V.", Precio = 22900m, Stock = 55, ImagenUrl = "/imagenes/remera-cuello-v.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 1 },
            new Producto { Id = 7, Nombre = "Top Corto de Algodón", Descripcion = "Top corto de algodón elastizado, ideal para entrenamiento.", Precio = 21500m, Stock = 24, ImagenUrl = "/imagenes/top-corto.jpg", Talles = "XS,S,M", Genero = Genero.Mujer, CategoriaId = 1 },

            // ---------- Pantalones ----------
            new Producto { Id = 8, Nombre = "Jean Recto Azul", Descripcion = "Denim azul de corte recto y largo al tobillo, con cinco bolsillos.", Precio = 69900m, Stock = 21, ImagenUrl = "/imagenes/jean-recto.jpg", Talles = "34,36,38,40", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 9, Nombre = "Jean Mom Fit", Descripcion = "Denim rígido de tiro alto con calce holgado en la pierna y lavado claro.", Precio = 72900m, Stock = 18, ImagenUrl = "/imagenes/jean-mom.jpg", Talles = "34,36,38,40", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 10, Nombre = "Jean Clásico de Tiro Alto", Descripcion = "Corte clásico de cinco bolsillos en denim rígido, con etiqueta de cuero en la cintura.", Precio = 76900m, Stock = 15, ImagenUrl = "/imagenes/jean-tiro-alto.jpg", Talles = "34,36,38,40", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 11, Nombre = "Pantalón Cargo Beige", Descripcion = "Gabardina resistente con bolsillos laterales y puño ajustable.", Precio = 64900m, Stock = 27, ImagenUrl = "/imagenes/cargo-beige.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 2 },
            new Producto { Id = 12, Nombre = "Jogging de Frisa", Descripcion = "Jogging de frisa perchada con cordón y puños elastizados.", Precio = 45900m, Stock = 40, ImagenUrl = "/imagenes/jogging-frisa.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 2 },
            new Producto { Id = 13, Nombre = "Bermuda de Gabardina", Descripcion = "Bermuda de gabardina de algodón con cintura ajustable.", Precio = 42900m, Stock = 21, ImagenUrl = "/imagenes/bermuda.jpg", Talles = "38,40,42,44", Genero = Genero.Hombre, CategoriaId = 2 },

            // ---------- Camperas y Buzos ----------
            new Producto { Id = 14, Nombre = "Campera Puffer Naranja", Descripcion = "Campera inflable con canesú negro contrastante, cuello alto y relleno sintético.", Precio = 165900m, Stock = 16, ImagenUrl = "/imagenes/campera-puffer.jpg", Talles = "S,M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },
            new Producto { Id = 15, Nombre = "Campera Puffer Negra", Descripcion = "Puffer negra de tejido satinado con capucha y puños elastizados.", Precio = 189900m, Stock = 9, ImagenUrl = "/imagenes/campera-puffer-negra.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },
            new Producto { Id = 16, Nombre = "Campera de Jean", Descripcion = "Campera de denim con lavado claro, bolsillos con tapa y botones metálicos.", Precio = 94900m, Stock = 0, ImagenUrl = "/imagenes/campera-jean.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 17, Nombre = "Campera de Jean Clásica", Descripcion = "Denim crudo sin lavar, cuello con solapa y costuras contrastantes. Se ablanda con el uso.", Precio = 154900m, Stock = 7, ImagenUrl = "/imagenes/campera-jean-clasica.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },
            new Producto { Id = 18, Nombre = "Buzo Canguro con Capucha", Descripcion = "Buzo de frisa con bolsillo canguro y capucha con cordón.", Precio = 58900m, Stock = 45, ImagenUrl = "/imagenes/buzo-canguro.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 19, Nombre = "Buzo Oversize Blanco", Descripcion = "Buzo blanco de corte amplio con capucha forrada y bolsillo canguro.", Precio = 64900m, Stock = 22, ImagenUrl = "/imagenes/buzo-blanco-mujer.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 3 },
            new Producto { Id = 20, Nombre = "Buzo con Capucha Blanco", Descripcion = "Frisa blanca de gramaje alto, capucha con cordón y puños acanalados.", Precio = 62900m, Stock = 26, ImagenUrl = "/imagenes/buzo-blanco-hombre.jpg", Talles = "M,L,XL", Genero = Genero.Hombre, CategoriaId = 3 },
            new Producto { Id = 21, Nombre = "Buzo Naranja Estampado", Descripcion = "Buzo naranja con texto estampado al frente, bolsillo canguro y bordes deshilachados.", Precio = 67900m, Stock = 14, ImagenUrl = "/imagenes/buzo-naranja.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },
            new Producto { Id = 22, Nombre = "Buzo de Frisa Liso", Descripcion = "Buzo de frisa sin estampa, disponible en negro y gris. Capucha con cordón plano.", Precio = 54900m, Stock = 38, ImagenUrl = "/imagenes/buzo-liso.jpg", Talles = "S,M,L,XL", Genero = Genero.Unisex, CategoriaId = 3 },

            // ---------- Vestidos ----------
            new Producto { Id = 23, Nombre = "Vestido Midi Floreado", Descripcion = "Vestido midi de viscosa con estampa floral y cintura elastizada.", Precio = 84900m, Stock = 15, ImagenUrl = "/imagenes/vestido-midi.jpg", Talles = "XS,S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 24, Nombre = "Vestido Camisero", Descripcion = "Vestido camisero de popelina con cinto del mismo género.", Precio = 76900m, Stock = 23, ImagenUrl = "/imagenes/vestido-camisero.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },
            new Producto { Id = 25, Nombre = "Enterito de Jean", Descripcion = "Enterito de denim liviano con bolsillo frontal y tiradores.", Precio = 92900m, Stock = 11, ImagenUrl = "/imagenes/enterito-jean.jpg", Talles = "S,M,L", Genero = Genero.Mujer, CategoriaId = 4 },

            // ---------- Calzado ----------
            new Producto { Id = 26, Nombre = "Zapatillas Urbanas", Descripcion = "Zapatillas blancas de caña baja con perforaciones laterales y suela de goma.", Precio = 119900m, Stock = 30, ImagenUrl = "/imagenes/zapatillas-urbanas.jpg", Talles = "36,37,38,39,40", Genero = Genero.Mujer, CategoriaId = 5 },
            new Producto { Id = 27, Nombre = "Zapatillas Running", Descripcion = "Zapatillas de entrenamiento con cámara de aire visible en el talón y empeine de malla.", Precio = 134900m, Stock = 18, ImagenUrl = "/imagenes/zapatillas-running.jpg", Talles = "40,41,42,43,44", Genero = Genero.Hombre, CategoriaId = 5 },
            new Producto { Id = 28, Nombre = "Zapatillas Clásicas Blancas", Descripcion = "Cuero blanco con suela gruesa y correa al empeine. Un clásico de todos los días.", Precio = 129900m, Stock = 25, ImagenUrl = "/imagenes/zapatillas-blancas.jpg", Talles = "38,39,40,41,42,43", Genero = Genero.Unisex, CategoriaId = 5 },
            new Producto { Id = 29, Nombre = "Zapatillas Retro Rojas", Descripcion = "Caña media en cuero blanco con paneles rojos y malla lateral. Silueta retro de básquet.", Precio = 172900m, Stock = 12, ImagenUrl = "/imagenes/zapatillas-retro.jpg", Talles = "40,41,42,43,44", Genero = Genero.Hombre, CategoriaId = 5 },
            new Producto { Id = 30, Nombre = "Zapatillas Skate Estampadas", Descripcion = "Modelo de skate con paneles de colores, gamuza texturada y suela vulcanizada.", Precio = 145900m, Stock = 10, ImagenUrl = "/imagenes/zapatillas-skate.jpg", Talles = "38,39,40,41,42,43", Genero = Genero.Unisex, CategoriaId = 5 }
        );
    }
}
