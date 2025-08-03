using hotelproyecto.Data;
using hotelproyecto.Models;
using System.Net.Mail;
using System.Net;
using BCrypt.Net;

namespace hotelproyecto.Services
{
    public class RecuperacionService
    {
        private readonly UsuarioData _usuarioData;
        private readonly TokenRecuperacionData _tokenData;
        private readonly IConfiguration _config;

        public RecuperacionService(UsuarioData usuarioData, TokenRecuperacionData tokenData, IConfiguration config)
        {
            _usuarioData = usuarioData;
            _tokenData = tokenData;
            _config = config;
        }
        // Paso 1: Generar y enviar token al correo
        public async Task<bool> EnviarTokenRecuperacionAsync(string correoDestino)
        {
            var usuario = await _usuarioData.ObtenerUsuarioPorCorreoAsync(correoDestino);
            if (usuario == null) return false;

            string tokenPlano = Guid.NewGuid().ToString("N").Substring(0, 6); // Ej: "abc123"
            string tokenHash = BCrypt.Net.BCrypt.HashPassword(tokenPlano);
            DateTime expiracion = DateTime.Now.AddMinutes(15);

            await _tokenData.GuardarTokenAsync(usuario.Id, tokenHash, expiracion);

            // Enviar correo
            var resultado = await EnviarCorreoAsync(usuario.Gmail, tokenPlano);
            return resultado;
        }

        // Paso 2: Validar token ingresado
        public async Task<TokenRecuperacion?> ValidarTokenAsync(int usuarioId, string tokenIngresado)
        {
            var tokenDB = await _tokenData.ObtenerTokenPorUsuarioAsync(usuarioId);

            if (tokenDB == null) return null;

            // Verificar token con hash bcrypt
            if (BCrypt.Net.BCrypt.Verify(tokenIngresado, tokenDB.Token))
                return tokenDB;

            return null;
        }

        // Paso 3: Actualizar contraseña
        public async Task<bool> CambiarContrasenaAsync(int usuarioId, string nuevaContrasena, int tokenId)
        {
            string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            await _usuarioData.ActualizarContrasenaAsync(usuarioId, contrasenaHash);
            await _tokenData.MarcarTokenComoUsadoAsync(tokenId);
            return true;
        }

        // Función para enviar correo con el token plano
        private async Task<bool> EnviarCorreoAsync(string destino, string token)
        {
            try
            {
                var smtpHost = _config["SmtpSettings:Host"];
                var smtpPort = int.Parse(_config["SmtpSettings:Port"]);
                var smtpUser = _config["SmtpSettings:User"];
                var smtpPass = _config["SmtpSettings:Password"];
                var enableSsl = bool.Parse(_config["SmtpSettings:EnableSsl"]);

                using var smtp = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = enableSsl
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(smtpUser, "Soporte Hotel"),
                    Subject = "Recuperación de Contraseña",
                    Body = $"Tu código de recuperación es: <b>{token}</b>. Vence en 15 minutos.",
                    IsBodyHtml = true
                };

                mensaje.To.Add(destino);
                await smtp.SendMailAsync(mensaje);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
                return false;
            }
        }

    }
}
