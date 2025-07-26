using hotelproyecto.Data;
using hotelproyecto.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotelproyecto.Controllers
{
    public class RecuperacionController : Controller
    {
        private readonly AuthService _authService;
        private readonly UsuarioData _usuarioData;

        public RecuperacionController(AuthService authService, UsuarioData usuarioData)
        {
            _authService = authService;
            _usuarioData = usuarioData;
        }
        #region"Solicitar"        
        public IActionResult Solicitar() => View();

        [HttpPost]
        public async Task<IActionResult> Solicitar(string correo)
        {
            var usuario = await _usuarioData.ObtenerUsuarioPorCorreoAsync(correo);
            if (usuario == null || !usuario.Estado)
            {
                ViewBag.Error = "Correo no registrado o usuario inactivo.";
                return View();
            }

            var token = await _authService.GenerarTokenRecuperacionAsync(usuario.Id);
            
            return RedirectToAction("IngresarToken", new {token = token, usuarioId = usuario.Id });
        }
        #endregion

        #region"Ingresar Token"                
        public IActionResult IngresarToken(string token, int usuarioId)
        {
            ViewBag.TokenGenerado = token;
            ViewBag.UsuarioId = usuarioId;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> IngresarToken(int usuarioId, string token)
        {
            var valido = await _authService.ValidarTokenRecuperacionAsync(usuarioId, token);
            if (!valido)
            {
                ViewBag.Error = "Token inválido o expirado.";
                return View();
            }

            ViewBag.UsuarioId = usuarioId;
            ViewBag.Token = token;
            return View("NuevaContrasena");
        }
        #endregion

        #region"Nueva Contraseña"
        [HttpPost]
        public async Task<IActionResult> NuevaContrasena(int usuarioId, string token, string nuevaContrasena, string confirmarContrasena)
        {
            var valido = await _authService.ValidarTokenRecuperacionAsync(usuarioId, token);
            if (!valido)
                return View("TokenInvalido");

            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).{6,}$");

            if (string.IsNullOrWhiteSpace(nuevaContrasena) || !regex.IsMatch(nuevaContrasena))
                ModelState.AddModelError("", "La contraseña debe tener al menos 6 caracteres y un carácter especial.");
            else if (nuevaContrasena != confirmarContrasena)
                ModelState.AddModelError("", "Las contraseñas no coinciden.");

            if (!ModelState.IsValid)
            {
                ViewBag.UsuarioId = usuarioId;
                ViewBag.Token = token;
                return View();
            }

            await _authService.ActualizarContrasenaAsync(usuarioId, nuevaContrasena);
            await _authService.MarcarTokenComoUsadoAsync(usuarioId, token);

            return View("ContrasenaActualizada");
        }
        #endregion

    }
}
