using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IndumentariaEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Talles = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Genero = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Remeras, musculosas y tops de algodón y fibras técnicas.", "Remeras" },
                    { 2, "Jeans, joggings, cargos y pantalones de vestir.", "Pantalones" },
                    { 3, "Camperas, buzos y abrigos para media estación e invierno.", "Camperas" },
                    { 4, "Vestidos y enteritos para uso diario y ocasiones especiales.", "Vestidos" },
                    { 5, "Zapatillas, borcegos y sandalias urbanas.", "Calzado" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "Genero", "ImagenUrl", "Nombre", "Precio", "Stock", "Talles" },
                values: new object[,]
                {
                    { 1, 1, "Remera de algodón peinado, cuello redondo reforzado y corte clásico.", 0, "/imagenes/remera-lisa.jpg", "Remera Lisa de Algodón", 24900.0, 42, "S,M,L,XL" },
                    { 2, 1, "Remera de corte amplio con estampa frontal serigrafiada.", 1, "/imagenes/remera-oversize.jpg", "Remera Oversize Estampada", 27500.0, 28, "S,M,L" },
                    { 3, 1, "Remera de modal y algodón, caída suave y escote en V.", 1, "/imagenes/remera-cuello-v.jpg", "Remera Básica Cuello V", 22900.0, 55, "XS,S,M,L" },
                    { 4, 1, "Manga larga a rayas horizontales en algodón liviano.", 0, "/imagenes/remera-rayada.jpg", "Remera Manga Larga Rayada", 31900.0, 19, "M,L,XL" },
                    { 5, 1, "Tejido técnico de secado rápido con paneles de ventilación.", 2, "/imagenes/remera-dryfit.jpg", "Remera Deportiva Dry-Fit", 29900.0, 36, "S,M,L,XL" },
                    { 6, 1, "Musculosa básica de algodón con sisa amplia.", 0, "/imagenes/musculosa.jpg", "Musculosa de Algodón", 19900.0, 31, "M,L,XL" },
                    { 7, 1, "Top corto de algodón elastizado, ideal para entrenamiento.", 1, "/imagenes/top-corto.jpg", "Top Corto de Algodón", 21500.0, 24, "XS,S,M" },
                    { 8, 2, "Jean de denim elastizado con cinco bolsillos y lavado clásico.", 0, "/imagenes/jean-slim.jpg", "Jean Slim Fit Azul", 68900.0, 22, "38,40,42,44" },
                    { 9, 2, "Denim rígido de tiro alto con calce holgado en la pierna.", 1, "/imagenes/jean-mom.jpg", "Jean Mom Fit", 72900.0, 18, "34,36,38,40" },
                    { 10, 2, "Gabardina resistente con bolsillos laterales y puño ajustable.", 2, "/imagenes/cargo-beige.jpg", "Pantalón Cargo Beige", 64900.0, 27, "S,M,L,XL" },
                    { 11, 2, "Jogging de frisa perchada con cordón y puños elastizados.", 2, "/imagenes/jogging-frisa.jpg", "Jogging de Frisa", 45900.0, 40, "S,M,L,XL" },
                    { 12, 2, "Corte recto en tejido de lana fría, con pinzas al frente.", 0, "/imagenes/pantalon-vestir.jpg", "Pantalón de Vestir", 79900.0, 14, "40,42,44,46" },
                    { 13, 2, "Calza de tiro alto en lycra con bolsillo lateral para celular.", 1, "/imagenes/calza-deportiva.jpg", "Calza Deportiva", 38900.0, 33, "XS,S,M,L" },
                    { 14, 2, "Bermuda de gabardina de algodón con cintura ajustable.", 0, "/imagenes/bermuda.jpg", "Bermuda de Gabardina", 42900.0, 21, "38,40,42,44" },
                    { 15, 3, "Biker de cuero sintético con cierres metálicos y forro interior.", 1, "/imagenes/campera-cuero.jpg", "Campera de Cuero Ecológico", 189900.0, 8, "S,M,L" },
                    { 16, 3, "Campera inflable con relleno sintético y capucha desmontable.", 2, "/imagenes/campera-puffer.jpg", "Campera Puffer", 145900.0, 16, "S,M,L,XL" },
                    { 17, 3, "Buzo de frisa con bolsillo canguro y capucha con cordón.", 2, "/imagenes/buzo-canguro.jpg", "Buzo Canguro con Capucha", 58900.0, 45, "S,M,L,XL" },
                    { 18, 3, "Rompeviento liviano con costuras selladas y capucha plegable.", 0, "/imagenes/rompeviento.jpg", "Campera Rompeviento", 89900.0, 12, "M,L,XL" },
                    { 19, 3, "Cardigan tejido de punto grueso con botones de nácar.", 1, "/imagenes/cardigan.jpg", "Cardigan de Punto", 67900.0, 17, "S,M,L" },
                    { 20, 3, "Campera de denim con lavado stone y bolsillos con tapa.", 2, "/imagenes/campera-jean.jpg", "Campera de Jean", 94900.0, 0, "S,M,L,XL" },
                    { 21, 3, "Chaleco acolchado sin mangas, ideal para entretiempo.", 0, "/imagenes/chaleco.jpg", "Chaleco Inflable", 78900.0, 20, "M,L,XL" },
                    { 22, 4, "Vestido midi de viscosa con estampa floral y cintura elastizada.", 1, "/imagenes/vestido-midi.jpg", "Vestido Midi Floreado", 84900.0, 15, "XS,S,M,L" },
                    { 23, 4, "Vestido largo con espalda descubierta y falda de caída fluida.", 1, "/imagenes/vestido-fiesta.jpg", "Vestido Largo de Fiesta", 129900.0, 6, "S,M,L" },
                    { 24, 4, "Vestido camisero de popelina con cinto del mismo género.", 1, "/imagenes/vestido-camisero.jpg", "Vestido Camisero", 76900.0, 23, "S,M,L" },
                    { 25, 4, "Vestido corto de lino con breteles regulables.", 1, "/imagenes/vestido-lino.jpg", "Vestido de Verano de Lino", 69900.0, 26, "XS,S,M" },
                    { 26, 4, "Enterito de denim liviano con bolsillo frontal y tiradores.", 1, "/imagenes/enterito-jean.jpg", "Enterito de Jean", 92900.0, 11, "S,M,L" },
                    { 27, 5, "Zapatillas de lona con suela de goma vulcanizada.", 2, "/imagenes/zapatillas-urbanas.jpg", "Zapatillas Urbanas", 119900.0, 30, "38,39,40,41,42,43" },
                    { 28, 5, "Zapatillas de entrenamiento con amortiguación en el talón.", 0, "/imagenes/zapatillas-running.jpg", "Zapatillas Running", 134900.0, 18, "40,41,42,43,44" },
                    { 29, 5, "Borcegos de cuero vacuno con cordones y suela tractora.", 1, "/imagenes/borcegos.jpg", "Borcegos de Cuero", 156900.0, 9, "36,37,38,39,40" },
                    { 30, 5, "Sandalias bajas de cuero natural con hebilla ajustable.", 1, "/imagenes/sandalias.jpg", "Sandalias de Cuero", 54900.0, 25, "36,37,38,39,40" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Genero",
                table: "Productos",
                column: "Genero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
