namespace FunerariaWeb
{
    public static class AppEstado
    {
        public static int UsuarioId { get; set; }
        // Inicializamos con cadena vacía para evitar error de nulo
        public static string Rol { get; set; } = string.Empty;
    }
}