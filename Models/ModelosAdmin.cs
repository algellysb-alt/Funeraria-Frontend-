namespace FunerariaWeb.Models
{
    public class PedidoAdmin
    {
        public int id { get; set; }
        public string nombreDifunto { get; set; } = string.Empty;
        public DateTime fechaServicio { get; set; }
        public decimal totalEstimado { get; set; }
        public string estado { get; set; } = "pendiente";
        public UsuarioCorto usuario { get; set; } = new();
        public List<DetalleAdmin> detalles { get; set; } = new();
    }

    public class UsuarioCorto
    {
        public string nombreCompleto { get; set; } = string.Empty;
    }

    public class DetalleAdmin
    {
        public int cantidad { get; set; }
        public ProductoCorto producto { get; set; } = new();
    }

    public class ProductoCorto
    {
        public string nombre { get; set; } = string.Empty;
    }
}