namespace IndumentariaEcommerce.Models;

/// <summary>
/// Categoría de prendas (Remeras, Pantalones, Camperas...).
/// Lado "uno" de la relación uno a muchos con Producto.
/// </summary>
public class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Propiedad de navegación: no existe como columna en la tabla.
    /// EF Core la completa cuando la consulta usa Include(c => c.Productos).
    /// Se inicializa para que nunca sea null y se pueda recorrer sin chequear.
    /// </summary>
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
