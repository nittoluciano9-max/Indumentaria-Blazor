using System.ComponentModel.DataAnnotations;

namespace IndumentariaEcommerce.Models.Validaciones;

/// <summary>
/// Valida el formato de Producto.Talles: valores separados por coma, sin
/// espacios, sin entradas vacías y sin repetidos (ej: "S,M,L,XL" o "38,40,42").
///
/// Hace falta porque Talles se guarda como texto y se parsea en la UI. Un
/// talle repetido rompe DetalleProducto, que usa cada talle como @key de su
/// botón, y Blazor no admite claves duplicadas.
///
/// Al heredar de ValidationAttribute, el DataAnnotationsValidator del
/// EditForm lo ejecuta igual que a [Required] o [Range]: no hay que tocar el
/// formulario. Tampoco afecta a EF Core, que ignora este atributo al generar
/// el esquema.
/// </summary>
public class TallesValidosAttribute : ValidationAttribute
{
    public TallesValidosAttribute()
        : base("Los talles deben ir separados por coma, sin espacios ni repetidos. Por ejemplo: S,M,L,XL")
    {
    }

    public override bool IsValid(object? value)
    {
        // Vacío se considera válido a propósito: que el campo sea obligatorio
        // es tarea de [Required]. Así cada atributo valida una sola cosa y no
        // aparecen dos mensajes de error para el mismo problema.
        if (value is not string texto || string.IsNullOrEmpty(texto))
        {
            return true;
        }

        // Sin RemoveEmptyEntries: justamente se quiere detectar "S,,M" o una
        // coma al final.
        var talles = texto.Split(',');

        if (talles.Any(talle => talle.Length == 0 || !talle.All(char.IsLetterOrDigit)))
        {
            return false;
        }

        // Ignora mayúsculas al comparar: "m" y "M" son el mismo talle.
        return talles.Distinct(StringComparer.OrdinalIgnoreCase).Count() == talles.Length;
    }
}
