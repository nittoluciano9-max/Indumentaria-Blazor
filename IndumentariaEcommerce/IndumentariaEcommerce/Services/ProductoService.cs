using IndumentariaEcommerce.Data;
using IndumentariaEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace IndumentariaEcommerce.Services;

/// <summary>
/// Encapsula todas las consultas del catálogo. Los componentes no arman
/// consultas LINQ: le piden los datos a este servicio, así la lógica de
/// filtrado y paginación existe en un solo lugar.
///
/// Recibe una IDbContextFactory y no un AppDbContext: crea un contexto corto
/// por consulta y lo descarta (ver GUIA_TECNICA.md, sección 12 "EF Core y DbContextFactory").
/// </summary>
public class ProductoService
{
    public const int TamanioPaginaPorDefecto = 12;

    private readonly IDbContextFactory<AppDbContext> _fabrica;

    public ProductoService(IDbContextFactory<AppDbContext> fabrica)
    {
        _fabrica = fabrica;
    }

    /// <summary>
    /// Busca productos aplicando filtros opcionales y devuelve una página de
    /// resultados. Cualquier parámetro en null significa "no filtrar por eso".
    /// </summary>
    public async Task<ResultadoPaginado<Producto>> BuscarAsync(
        Genero? genero = null,
        int? categoriaId = null,
        string? textoBusqueda = null,
        int pagina = 1,
        int tamanioPagina = TamanioPaginaPorDefecto)
    {
        if (pagina < 1)
        {
            pagina = 1;
        }

        await using var contexto = await _fabrica.CreateDbContextAsync();

        // IQueryable compone la consulta sin ejecutarla: cada Where de acá
        // abajo agrega una condición al SQL, no filtra una lista en memoria.
        // La consulta viaja a SQLite recién en CountAsync y ToListAsync.
        IQueryable<Producto> consulta = contexto.Productos
            .AsNoTracking()
            .Include(p => p.Categoria);

        if (genero is not null)
        {
            var generoFiltro = genero.Value;
            consulta = consulta.Where(p => p.Genero == generoFiltro);
        }

        if (categoriaId is not null)
        {
            var categoriaFiltro = categoriaId.Value;
            consulta = consulta.Where(p => p.CategoriaId == categoriaFiltro);
        }

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            var texto = textoBusqueda.Trim();

            // Se traduce al LIKE de SQLite. Tener presente que ese LIKE ignora
            // mayúsculas solo en caracteres ASCII: "algodon" no encuentra
            // "algodón". Resolverlo requeriría una columna normalizada.
            consulta = consulta.Where(p => EF.Functions.Like(p.Nombre, $"%{texto}%"));
        }

        // El total se cuenta ANTES de paginar. Si se contara después de
        // Skip/Take nunca superaría el tamaño de una página y la paginación
        // mostraría siempre una sola.
        var total = await consulta.CountAsync();

        var items = await consulta
            .OrderBy(p => p.Nombre)
            .Skip((pagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync();

        return new ResultadoPaginado<Producto>(items, total, pagina, tamanioPagina);
    }

    /// <summary>
    /// Devuelve un producto con su categoría, o null si el Id no existe.
    /// </summary>
    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        return await contexto.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Todas las categorías, para poblar el filtro del catálogo y el combo
    /// del formulario de administración.
    /// </summary>
    public async Task<List<Categoria>> ObtenerCategoriasAsync()
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        return await contexto.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .ToListAsync();
    }

    /// <summary>
    /// Productos para la portada. El modelo no tiene un campo "destacado" y
    /// agregarlo pediría otra migración, así que se toman los de mayor precio
    /// con stock disponible: es una selección arbitraria, pero estable,
    /// explicable y que nunca muestra algo que no se puede comprar.
    /// </summary>
    public async Task<List<Producto>> ObtenerDestacadosAsync(int cantidad = 4)
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        return await contexto.Productos
            .AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.Stock > 0)
            .OrderByDescending(p => p.Precio)
            .Take(cantidad)
            .ToListAsync();
    }

    // ---------- Escritura (panel de administración) ----------

    /// <summary>
    /// Da de alta un producto y devuelve el Id que le asignó la base.
    /// </summary>
    public async Task<int> CrearAsync(Producto producto)
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        // Se descarta la navegación: el formulario solo completa CategoriaId.
        // Si viniera un objeto Categoria adjunto, EF Core lo trataría como una
        // categoría nueva e intentaría insertarla de nuevo.
        producto.Categoria = null;

        contexto.Productos.Add(producto);
        await contexto.SaveChangesAsync();

        return producto.Id;
    }

    /// <summary>
    /// Actualiza un producto existente. Devuelve false si el Id no existe.
    /// </summary>
    public async Task<bool> ActualizarAsync(Producto producto)
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        // Sin AsNoTracking, al revés que en las consultas del catálogo: acá el
        // rastreo es justamente lo que hace falta. EF Core guarda una copia de
        // los valores originales y, al llamar SaveChanges, compara contra el
        // estado actual y arma un UPDATE solo con las columnas que cambiaron.
        // Traída con AsNoTracking no habría copia contra qué comparar y
        // SaveChanges no generaría ninguna sentencia.
        var existente = await contexto.Productos
            .FirstOrDefaultAsync(p => p.Id == producto.Id);

        if (existente is null)
        {
            return false;
        }

        existente.Nombre = producto.Nombre;
        existente.Descripcion = producto.Descripcion;
        existente.Precio = producto.Precio;
        existente.Stock = producto.Stock;
        existente.ImagenUrl = producto.ImagenUrl;
        existente.Talles = producto.Talles;
        existente.Genero = producto.Genero;
        existente.CategoriaId = producto.CategoriaId;

        await contexto.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Elimina un producto. Devuelve false si el Id no existe.
    /// </summary>
    public async Task<bool> EliminarAsync(int id)
    {
        await using var contexto = await _fabrica.CreateDbContextAsync();

        var producto = await contexto.Productos.FindAsync(id);

        if (producto is null)
        {
            return false;
        }

        contexto.Productos.Remove(producto);
        await contexto.SaveChangesAsync();

        return true;
    }
}
