namespace hotelproyecto.ViewModel
{
    public class RecuperacionContrasenaViewModel
    {
        public string Gmail { get; set; } = null!;

        // Solo se usa después de que el token se ha enviado
        public string? Token { get; set; }

        public string? NuevaContrasena { get; set; }

        public string? ConfirmarContrasena { get; set; }
    }
}
