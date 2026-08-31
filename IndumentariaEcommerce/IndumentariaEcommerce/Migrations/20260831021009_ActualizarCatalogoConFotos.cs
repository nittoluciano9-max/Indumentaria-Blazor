using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndumentariaEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarCatalogoConFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Remeras y tops de algodón, lisos y estampados.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Jeans, joggings, cargos y bermudas.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Nombre" },
                values: new object[] { "Camperas, buzos con capucha y abrigo para todo el año.", "Camperas y Buzos" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "Vestidos y enteritos para uso diario.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "Zapatillas urbanas, deportivas y de skate.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Remera de algodón peinado con cuello redondo reforzado y una estampa chica al frente. Corte clásico.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Remera negra de corte amplio con estampa serigrafiada en la espalda.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Algodón negro con estampa tipográfica en blanco. Tacto suave y caída recta.", 0, "/imagenes/remera-negra.jpg", "Remera Negra Estampada", 29900.0, 24, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Talles" },
                values: new object[] { "Gris jaspeado con estampa caligráfica al frente. Hombro caído y cuerpo ancho.", "/imagenes/remera-jaspeada.jpg", "Remera Oversize Jaspeada", "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Algodón blanco con estampa central en rojo. Cuello redondo y corte recto.", 1, "/imagenes/remera-estampa.jpg", "Remera Blanca con Estampa", 28900.0, 33, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Remera de modal y algodón, caída suave y escote en V.", 1, "/imagenes/remera-cuello-v.jpg", "Remera Básica Cuello V", 22900.0, 55, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Denim azul de corte recto y largo al tobillo, con cinco bolsillos.", 1, "/imagenes/jean-recto.jpg", "Jean Recto Azul", 69900.0, 21, "34,36,38,40" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Descripcion",
                value: "Denim rígido de tiro alto con calce holgado en la pierna y lavado claro.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Corte clásico de cinco bolsillos en denim rígido, con etiqueta de cuero en la cintura.", 1, "/imagenes/jean-tiro-alto.jpg", "Jean Clásico de Tiro Alto", 76900.0, 15, "34,36,38,40" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Gabardina resistente con bolsillos laterales y puño ajustable.", "/imagenes/cargo-beige.jpg", "Pantalón Cargo Beige", 64900.0, 27 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Jogging de frisa perchada con cordón y puños elastizados.", 1, "/imagenes/jogging-frisa.jpg", "Jogging de Frisa", 45900.0, 40, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Bermuda de gabardina de algodón con cintura ajustable.", 0, "/imagenes/bermuda.jpg", "Bermuda de Gabardina", 42900.0, 21, "38,40,42,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoriaId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 3, "Campera inflable con canesú negro contrastante, cuello alto y relleno sintético.", "/imagenes/campera-puffer.jpg", "Campera Puffer Naranja", 165900.0, 16, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Stock", "Talles" },
                values: new object[] { "Puffer negra de tejido satinado con capucha y puños elastizados.", 0, "/imagenes/campera-puffer-negra.jpg", "Campera Puffer Negra", 9, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Campera de denim con lavado claro, bolsillos con tapa y botones metálicos.", "/imagenes/campera-jean.jpg", "Campera de Jean", 94900.0, 0 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Denim crudo sin lavar, cuello con solapa y costuras contrastantes. Se ablanda con el uso.", 0, "/imagenes/campera-jean-clasica.jpg", "Campera de Jean Clásica", 154900.0, 7, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Buzo de frisa con bolsillo canguro y capucha con cordón.", 2, "/imagenes/buzo-canguro.jpg", "Buzo Canguro con Capucha", 58900.0, 45, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Buzo blanco de corte amplio con capucha forrada y bolsillo canguro.", "/imagenes/buzo-blanco-mujer.jpg", "Buzo Oversize Blanco", 64900.0, 22 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Frisa blanca de gramaje alto, capucha con cordón y puños acanalados.", 0, "/imagenes/buzo-blanco-hombre.jpg", "Buzo con Capucha Blanco", 62900.0, 26, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Buzo naranja con texto estampado al frente, bolsillo canguro y bordes deshilachados.", 2, "/imagenes/buzo-naranja.jpg", "Buzo Naranja Estampado", 67900.0, 14, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CategoriaId", "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 3, "Buzo de frisa sin estampa, disponible en negro y gris. Capucha con cordón plano.", 2, "/imagenes/buzo-liso.jpg", "Buzo de Frisa Liso", 54900.0, 38, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Vestido midi de viscosa con estampa floral y cintura elastizada.", "/imagenes/vestido-midi.jpg", "Vestido Midi Floreado", 84900.0, 15, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Enterito de denim liviano con bolsillo frontal y tiradores.", "/imagenes/enterito-jean.jpg", "Enterito de Jean", 92900.0, 11, "S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CategoriaId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 5, "Zapatillas blancas de caña baja con perforaciones laterales y suela de goma.", "/imagenes/zapatillas-urbanas.jpg", "Zapatillas Urbanas", 119900.0, 30, "36,37,38,39,40" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Zapatillas de entrenamiento con cámara de aire visible en el talón y empeine de malla.", 0, "/imagenes/zapatillas-running.jpg", "Zapatillas Running", 134900.0, 18, "40,41,42,43,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Cuero blanco con suela gruesa y correa al empeine. Un clásico de todos los días.", 2, "/imagenes/zapatillas-blancas.jpg", "Zapatillas Clásicas Blancas", 129900.0, 25, "38,39,40,41,42,43" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Caña media en cuero blanco con paneles rojos y malla lateral. Silueta retro de básquet.", 0, "/imagenes/zapatillas-retro.jpg", "Zapatillas Retro Rojas", 172900.0, 12, "40,41,42,43,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Modelo de skate con paneles de colores, gamuza texturada y suela vulcanizada.", 2, "/imagenes/zapatillas-skate.jpg", "Zapatillas Skate Estampadas", 145900.0, 10, "38,39,40,41,42,43" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Remeras, musculosas y tops de algodón y fibras técnicas.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Jeans, joggings, cargos y pantalones de vestir.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Nombre" },
                values: new object[] { "Camperas, buzos y abrigos para media estación e invierno.", "Camperas" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "Descripcion",
                value: "Vestidos y enteritos para uso diario y ocasiones especiales.");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                column: "Descripcion",
                value: "Zapatillas, borcegos y sandalias urbanas.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descripcion",
                value: "Remera de algodón peinado, cuello redondo reforzado y corte clásico.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Remera de corte amplio con estampa frontal serigrafiada.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Remera de modal y algodón, caída suave y escote en V.", 1, "/imagenes/remera-cuello-v.jpg", "Remera Básica Cuello V", 22900.0, 55, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Talles" },
                values: new object[] { "Manga larga a rayas horizontales en algodón liviano.", "/imagenes/remera-rayada.jpg", "Remera Manga Larga Rayada", "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Tejido técnico de secado rápido con paneles de ventilación.", 2, "/imagenes/remera-dryfit.jpg", "Remera Deportiva Dry-Fit", 29900.0, 36, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Musculosa básica de algodón con sisa amplia.", 0, "/imagenes/musculosa.jpg", "Musculosa de Algodón", 19900.0, 31, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Jean de denim elastizado con cinco bolsillos y lavado clásico.", 0, "/imagenes/jean-slim.jpg", "Jean Slim Fit Azul", 68900.0, 22, "38,40,42,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Descripcion",
                value: "Denim rígido de tiro alto con calce holgado en la pierna.");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Gabardina resistente con bolsillos laterales y puño ajustable.", 2, "/imagenes/cargo-beige.jpg", "Pantalón Cargo Beige", 64900.0, 27, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Jogging de frisa perchada con cordón y puños elastizados.", "/imagenes/jogging-frisa.jpg", "Jogging de Frisa", 45900.0, 40 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Corte recto en tejido de lana fría, con pinzas al frente.", 0, "/imagenes/pantalon-vestir.jpg", "Pantalón de Vestir", 79900.0, 14, "40,42,44,46" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Calza de tiro alto en lycra con bolsillo lateral para celular.", 1, "/imagenes/calza-deportiva.jpg", "Calza Deportiva", 38900.0, 33, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoriaId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 2, "Bermuda de gabardina de algodón con cintura ajustable.", "/imagenes/bermuda.jpg", "Bermuda de Gabardina", 42900.0, 21, "38,40,42,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Stock", "Talles" },
                values: new object[] { "Biker de cuero sintético con cierres metálicos y forro interior.", 1, "/imagenes/campera-cuero.jpg", "Campera de Cuero Ecológico", 8, "S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Campera inflable con relleno sintético y capucha desmontable.", "/imagenes/campera-puffer.jpg", "Campera Puffer", 145900.0, 16 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Buzo de frisa con bolsillo canguro y capucha con cordón.", 2, "/imagenes/buzo-canguro.jpg", "Buzo Canguro con Capucha", 58900.0, 45, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Rompeviento liviano con costuras selladas y capucha plegable.", 0, "/imagenes/rompeviento.jpg", "Campera Rompeviento", 89900.0, 12, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock" },
                values: new object[] { "Cardigan tejido de punto grueso con botones de nácar.", "/imagenes/cardigan.jpg", "Cardigan de Punto", 67900.0, 17 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Campera de denim con lavado stone y bolsillos con tapa.", 2, "/imagenes/campera-jean.jpg", "Campera de Jean", 94900.0, 0, "S,M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Chaleco acolchado sin mangas, ideal para entretiempo.", 0, "/imagenes/chaleco.jpg", "Chaleco Inflable", 78900.0, 20, "M,L,XL" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CategoriaId", "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 4, "Vestido midi de viscosa con estampa floral y cintura elastizada.", 1, "/imagenes/vestido-midi.jpg", "Vestido Midi Floreado", 84900.0, 15, "XS,S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Vestido largo con espalda descubierta y falda de caída fluida.", "/imagenes/vestido-fiesta.jpg", "Vestido Largo de Fiesta", 129900.0, 6, "S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Vestido corto de lino con breteles regulables.", "/imagenes/vestido-lino.jpg", "Vestido de Verano de Lino", 69900.0, 26, "XS,S,M" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CategoriaId", "Descripcion", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { 4, "Enterito de denim liviano con bolsillo frontal y tiradores.", "/imagenes/enterito-jean.jpg", "Enterito de Jean", 92900.0, 11, "S,M,L" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Zapatillas de lona con suela de goma vulcanizada.", 2, "/imagenes/zapatillas-urbanas.jpg", "Zapatillas Urbanas", 119900.0, 30, "38,39,40,41,42,43" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Zapatillas de entrenamiento con amortiguación en el talón.", 0, "/imagenes/zapatillas-running.jpg", "Zapatillas Running", 134900.0, 18, "40,41,42,43,44" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Borcegos de cuero vacuno con cordones y suela tractora.", 1, "/imagenes/borcegos.jpg", "Borcegos de Cuero", 156900.0, 9, "36,37,38,39,40" });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[] { "Sandalias bajas de cuero natural con hebilla ajustable.", 1, "/imagenes/sandalias.jpg", "Sandalias de Cuero", 54900.0, 25, "36,37,38,39,40" });
        }
    }
}
