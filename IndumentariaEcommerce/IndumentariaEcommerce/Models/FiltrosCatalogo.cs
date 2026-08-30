namespace IndumentariaEcommerce.Models;

/// <summary>
/// Los filtros elegidos por el usuario en el catálogo. Viaja del componente
/// FiltroProductos hacia Catalogo a través de un EventCallback.
///
/// Un null en cualquiera de los campos significa "no filtrar por eso", que es
/// exactamente lo que espera ProductoService.BuscarAsync.
/// </summary>
public record FiltrosCatalogo(Genero? Genero, int? CategoriaId, string? Texto)
{
    /// <summary>Sin ningún filtro aplicado: el catálogo completo.</summary>
    public static FiltrosCatalogo Vacio { get; } = new(null, null, null);
}
