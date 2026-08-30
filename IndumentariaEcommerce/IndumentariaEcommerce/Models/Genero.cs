namespace IndumentariaEcommerce.Models;

/// <summary>
/// Género al que apunta una prenda.
/// EF Core lo persiste como número entero, por eso los valores son explícitos:
/// si se reordenaran los miembros sin fijarlos, los productos ya guardados
/// cambiarían de género en silencio.
/// </summary>
public enum Genero
{
    Hombre = 0,
    Mujer = 1,
    Unisex = 2
}
