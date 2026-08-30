using System.Text.Json;
using IndumentariaEcommerce.Models;
using Microsoft.JSInterop;

namespace IndumentariaEcommerce.Services;

/// <summary>
/// Estado del carrito de compras (State Service).
///
/// Se registra como Scoped: en Blazor Server eso significa una instancia por
/// circuito, es decir por pestaña conectada. Registrarlo como Singleton haría
/// que todos los usuarios del sitio compartieran el mismo carrito.
///
/// El contenido vive en memoria y se replica en el localStorage del navegador
/// para sobrevivir a un F5. No hay tabla de pedidos: así lo pide la consigna.
/// </summary>
public class CarritoService
{
    private const string ClaveAlmacenamiento = "carrito";

    /// <summary>Tope por línea. Sin instancia de compra no hay un momento
    /// posterior donde revalidar stock, así que se acota acá.</summary>
    private const int MaximoPorLinea = 20;

    private readonly IJSRuntime _js;
    private readonly List<ItemCarrito> _items = new();

    private bool _inicializado;

    public CarritoService(IJSRuntime js)
    {
        _js = js;
    }

    /// <summary>
    /// Se dispara cada vez que cambia el contenido del carrito.
    ///
    /// Es un event Action de C# y no un EventCallback porque quien lo emite es
    /// un servicio, no un componente: Blazor no lo conoce y no va a redibujar
    /// a nadie por su cuenta. Cada componente suscripto tiene que llamar a
    /// StateHasChanged por las suyas (ver IconoCarrito).
    /// </summary>
    public event Action? OnCambio;

    /// <summary>
    /// Solo lectura para que ningún componente pueda agregar o quitar items
    /// por afuera, salteándose el guardado y la notificación.
    /// </summary>
    public IReadOnlyList<ItemCarrito> Items => _items;

    public int CantidadTotal => _items.Sum(item => item.Cantidad);

    public decimal Total => _items.Sum(item => item.Subtotal);

    public bool EstaVacio => _items.Count == 0;

    /// <summary>
    /// Rehidrata el carrito desde el localStorage.
    ///
    /// No se hace en el constructor por dos razones: un constructor no puede
    /// ser async, y aunque pudiera correría durante el prerender, cuando
    /// todavía no hay navegador con el cual hablar. Los componentes llaman a
    /// este método desde OnAfterRenderAsync(firstRender: true).
    /// </summary>
    public async Task InicializarAsync()
    {
        // El flag se levanta antes del primer await: si dos componentes llaman
        // a la vez, el segundo sale sin volver a leer el almacenamiento.
        if (_inicializado)
        {
            return;
        }

        _inicializado = true;

        var json = await _js.InvokeAsync<string?>("almacenamiento.leer", ClaveAlmacenamiento);

        if (string.IsNullOrWhiteSpace(json))
        {
            return;
        }

        try
        {
            var guardados = JsonSerializer.Deserialize<List<ItemCarrito>>(json);

            if (guardados is { Count: > 0 })
            {
                _items.Clear();
                _items.AddRange(guardados);
                OnCambio?.Invoke();
            }
        }
        catch (JsonException)
        {
            // El JSON quedó corrupto o pertenece a una versión anterior del
            // modelo. Se descarta y se arranca con el carrito vacío, en lugar
            // de dejar al usuario con una página que no carga nunca.
            await _js.InvokeVoidAsync("almacenamiento.borrar", ClaveAlmacenamiento);
        }
    }

    /// <summary>
    /// Agrega un producto en un talle determinado. Si esa combinación ya está
    /// en el carrito, suma cantidad en lugar de crear otra línea.
    /// </summary>
    public async Task AgregarAsync(Producto producto, string talle, int cantidad = 1)
    {
        if (producto.Stock <= 0 || cantidad <= 0 || string.IsNullOrWhiteSpace(talle))
        {
            return;
        }

        var tope = Math.Min(producto.Stock, MaximoPorLinea);
        var existente = Buscar(producto.Id, talle);

        if (existente is null)
        {
            // Se copian los datos del producto en lugar de referenciarlo: así
            // el precio queda congelado y el JSON no arrastra la categoría.
            _items.Add(new ItemCarrito
            {
                ProductoId = producto.Id,
                Nombre = producto.Nombre,
                PrecioUnitario = producto.Precio,
                ImagenUrl = producto.ImagenUrl,
                Talle = talle,
                Cantidad = Math.Min(cantidad, tope)
            });
        }
        else
        {
            existente.Cantidad = Math.Min(existente.Cantidad + cantidad, tope);
        }

        await GuardarYNotificarAsync();
    }

    /// <summary>
    /// Cambia la cantidad de una línea. Una cantidad de cero o menos equivale
    /// a quitar el item.
    /// </summary>
    public async Task CambiarCantidadAsync(int productoId, string talle, int nuevaCantidad)
    {
        var item = Buscar(productoId, talle);

        if (item is null)
        {
            return;
        }

        if (nuevaCantidad <= 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.Cantidad = Math.Min(nuevaCantidad, MaximoPorLinea);
        }

        await GuardarYNotificarAsync();
    }

    public async Task QuitarAsync(int productoId, string talle)
    {
        var item = Buscar(productoId, talle);

        if (item is null)
        {
            return;
        }

        _items.Remove(item);
        await GuardarYNotificarAsync();
    }

    public async Task VaciarAsync()
    {
        if (_items.Count == 0)
        {
            return;
        }

        _items.Clear();
        await GuardarYNotificarAsync();
    }

    /// <summary>
    /// Una línea del carrito se identifica por el par producto + talle: la
    /// misma remera en S y en L son dos líneas distintas, no una con cantidad
    /// dos.
    /// </summary>
    private ItemCarrito? Buscar(int productoId, string talle) =>
        _items.FirstOrDefault(item => item.ProductoId == productoId && item.Talle == talle);

    private async Task GuardarYNotificarAsync()
    {
        // Primero se avisa a la UI y después se persiste: si el navegador
        // tiene bloqueado el almacenamiento, la pantalla igual queda al día y
        // el carrito sigue funcionando en memoria.
        OnCambio?.Invoke();

        var json = JsonSerializer.Serialize(_items);
        await _js.InvokeVoidAsync("almacenamiento.guardar", ClaveAlmacenamiento, json);
    }
}
