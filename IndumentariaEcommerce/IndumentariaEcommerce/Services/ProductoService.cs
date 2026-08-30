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
/// por consulta y lo descarta (ver CLAUDE.md, sección "Acceso a datos").
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
}
