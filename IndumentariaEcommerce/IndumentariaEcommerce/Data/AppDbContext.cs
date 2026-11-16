using IndumentariaEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace IndumentariaEcommerce.Data;

/// <summary>
/// Contexto de EF Core: traduce las clases de Models a tablas de SQLite.
/// Se crea a través de IDbContextFactory, nunca inyectado directamente en los
/// componentes (ver GUIA_TECNICA.md, sección 12 "EF Core y DbContextFactory").
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Cada DbSet es una tabla. Se exponen como propiedades de solo lectura con
    // Set<T>() para no tener que inicializarlas con "null!" y mentirle al
    // analizador de nullables.
    public DbSet<Categoria> Categorias => Set<Categoria>();

    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // SQLite no tiene un tipo decimal nativo: EF Core lo guardaría como
        // texto y las comparaciones se harían alfabéticamente, con lo cual
        // "9000" resultaría mayor que "50000". Guardarlo como double mantiene
        // el orden numérico correcto al ordenar o filtrar por precio.
        // En C# la propiedad sigue siendo decimal; la conversión ocurre solo
        // en el borde con la base de datos.
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasConversion<double>();

        // Relación uno a muchos declarada de forma explícita.
        // Restrict en lugar del Cascade que EF Core aplica por defecto: si se
        // intenta borrar una categoría que todavía tiene productos, la
        // operación falla en vez de borrar esos productos en silencio.
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // El catálogo filtra por género en casi todas sus consultas.
        // Con el volumen de datos de este trabajo no cambia el rendimiento,
        // pero es donde corresponde declarar el índice.
        modelBuilder.Entity<Producto>()
            .HasIndex(p => p.Genero);

        // Datos semilla: categorías y productos iniciales del catálogo.
        DatosSemilla.Aplicar(modelBuilder);
    }
}
