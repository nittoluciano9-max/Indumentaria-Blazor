using System.Text.Json.Serialization;

namespace IndumentariaEcommerce.Models;

/// <summary>
/// Una línea del carrito: un producto, un talle y una cantidad.
///
/// No es una entidad de EF Core: no tiene DbSet ni tabla propia. Vive en
/// memoria dentro de CarritoService y se persiste en el localStorage del
/// navegador, tal como pide la consigna (sin tabla de pedidos).
///
/// Guarda una copia de los datos del producto en lugar de una referencia a
/// Producto. Por tres motivos: evita arrastrar la navegación Categoria al
/// serializar a JSON (referencia circular), congela el precio que el usuario
/// vio al agregar el ítem, y permite dibujar el carrito sin consultar la base.
/// </summary>
public class ItemCarrito
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public decimal PrecioUnitario { get; set; }

    public string ImagenUrl { get; set; } = string.Empty;

    /// <summary>
    /// Junto con ProductoId identifica la línea: el mismo producto en dos
    /// talles distintos son dos líneas separadas del carrito.
    /// </summary>
    public string Talle { get; set; } = string.Empty;

    public int Cantidad { get; set; }

    /// <summary>
    /// Valor derivado: se calcula, no se almacena. JsonIgnore evita escribirlo
    /// en el localStorage, donde sería un dato redundante que podría quedar
    /// en contradicción con PrecioUnitario y Cantidad.
    /// </summary>
    [JsonIgnore]
    public decimal Subtotal => PrecioUnitario * Cantidad;
}
