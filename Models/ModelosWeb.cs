

namespace FunerariaWeb.Models
{
    // Clase compartida para los productos
    public class ProductoCliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }

    // Clase para enviar el pedido a la API
    public class CrearPedidoDto
    {
        public string NombreDifunto { get; set; } = string.Empty;
        public DateTime FechaServicio { get; set; } = DateTime.Now.AddDays(1);
        public string Notas { get; set; } = string.Empty;
        public List<DetallePedidoDto> Items { get; set; } = new();
    }

    public class DetallePedidoDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}