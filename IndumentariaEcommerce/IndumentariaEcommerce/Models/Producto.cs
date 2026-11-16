using System.ComponentModel.DataAnnotations;
using IndumentariaEcommerce.Models.Validaciones;

namespace IndumentariaEcommerce.Models;

/// <summary>
/// Prenda del catálogo. Lado "muchos" de la relación con Categoria.
/// Las DataAnnotations cumplen doble función: EF Core las usa para generar el
/// esquema de la tabla y el EditForm del panel admin las usa para validar.
/// </summary>
public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    // Primer y último carácter distintos de espacio: evita " Remera" o "Remera ",
    // que se ven iguales en pantalla pero ordenan y se buscan distinto.
    [RegularExpression(@"^\S(?:.*\S)?$", ErrorMessage = "El nombre no puede comenzar ni terminar con espacios.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    // decimal y no double: double es punto flotante binario y arrastra errores
    // de redondeo en los centavos. Para importes de dinero siempre decimal.
    [Range(0.01, 9_999_999, ErrorMessage = "El precio debe ser mayor a cero.")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    // Ruta relativa dentro de wwwroot (ej: "/imagenes/remera-lisa.jpg").
    // Sin [Url] justamente porque ese atributo exige una URL absoluta.
    [Required(ErrorMessage = "La imagen es obligatoria.")]
    [StringLength(300)]
    public string ImagenUrl { get; set; } = string.Empty;

    // Talles disponibles separados por coma (ej: "S,M,L,XL").
    // Se guarda como texto para no agregar una tabla que la consigna no pide.
    [Required(ErrorMessage = "Debe indicar al menos un talle.")]
    [StringLength(50)]
    [TallesValidos]
    public string Talles { get; set; } = string.Empty;

    public Genero Genero { get; set; }

    // Clave foránea. El Range fuerza elegir una opción real del <select>,
    // porque un select sin seleccionar devuelve 0.
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría.")]
    public int CategoriaId { get; set; }

    // Navegación nullable: al crear un producto desde el formulario solo se
    // completa CategoriaId; el objeto Categoria todavía no está cargado.
    public Categoria? Categoria { get; set; }
}
