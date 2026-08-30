namespace IndumentariaEcommerce.Models;

/// <summary>
/// Una página de resultados junto con la información necesaria para dibujar
/// la paginación. No es una entidad: es un modelo de transporte entre el
/// servicio y los componentes.
///
/// Es genérico porque tanto el catálogo como el panel de administración
/// necesitan paginar, y es un record porque un resultado de consulta no
/// debería poder modificarse después de devuelto.
/// </summary>
/// <param name="Items">Los elementos de la página actual.</param>
/// <param name="TotalItems">Cantidad total de elementos que cumplen el filtro,
/// no solo los de esta página.</param>
public record ResultadoPaginado<T>(
    IReadOnlyList<T> Items,
    int TotalItems,
    int PaginaActual,
    int TamanioPagina)
{
    // El cálculo vive acá y no en el componente: así Paginacion.razor
    // solamente lee un valor en lugar de hacer la cuenta.
    public int TotalPaginas =>
        TamanioPagina <= 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)TamanioPagina);
}
