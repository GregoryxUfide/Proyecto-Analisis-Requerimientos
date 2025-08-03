using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace hotelproyecto.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly RecuperacionService _recuperacionService;
        private readonly UsuarioData _usuarioData;

        public AuthController(AuthService authService, RecuperacionService recuperacionService, UsuarioData usuarioData)
        {
            _authService = authService;
            _recuperacionService = recuperacionService;
            _usuarioData = usuarioData;
        }

        #region Login

        public IActionResult Login() => View();
        
        [HttpPost]
        public async Task<IActionResult> Login(string gmail, string contrasena)
        {
            var usuario = await _authService.ValidarCredencialesAsync(gmail, contrasena);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("UsuarioID", usuario.Id);
                HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
                HttpContext.Session.SetString("Nombre", usuario.Nombre);

                var rol = usuario.Rol.Nombre.ToLower();

                // Redirige al dashboard si es trabajador
                if (rol == "admin" || rol == "vendedor" || rol == "conserje")
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                // Para clientes u otros roles
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas o usuario inactivo.";
            return View();
        }



        #endregion

        #region Registro

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            var resultado = await _authService.RegistrarUsuarioAsync(usuario);

            if (resultado.Exito)
                return RedirectToAction("Login");

            ViewBag.Error = resultado.Mensaje;
            return View(usuario);
        }

        #endregion

        #region Solicitar Token

        [HttpGet]
        public IActionResult SolicitarToken() => View();

        [HttpPost]
        public async Task<IActionResult> SolicitarToken(RecuperacionContrasenaViewModel vm)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(vm.Gmail))
            {
                ViewBag.Error = "Debe ingresar un correo válido.";
                return View(vm);
            }

            var enviado = await _recuperacionService.EnviarTokenRecuperacionAsync(vm.Gmail);
            if (enviado)
            {
                var usuario = await _usuarioData.ObtenerUsuarioPorCorreoAsync(vm.Gmail);

                if (usuario == null)
                {
                    ViewBag.Error = "El correo no está registrado en el sistema.";
                    return View(vm);
                }
                TempData["UsuarioId"] = usuario.Id;
                TempData["Gmail"] = vm.Gmail;

                return RedirectToAction("IngresarToken");
            }
                ViewBag.Error = "No se pudo enviar el correo. Verifique el correo ingresado.";
            return View(vm);
        }

        #endregion

        #region Ingresar Token

        [HttpGet]
        public IActionResult IngresarToken()
        {
            ViewBag.UsuarioId = TempData["UsuarioId"];
            ViewBag.Gmail = TempData["Gmail"];
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IngresarToken(int usuarioId, string token)
        {
            var tokenValido = await _recuperacionService.ValidarTokenAsync(usuarioId, token);

            if (tokenValido != null)
            {
                TempData["TokenId"] = tokenValido.Id;
                TempData["UsuarioId"] = usuarioId;
                return RedirectToAction("NuevaContrasena");
            }

            ViewBag.UsuarioId = usuarioId; // volver a pasarlo a la vista
            ViewBag.Error = "El token ingresado es inválido o ha expirado.";
            TempData["UsuarioId"] = usuarioId; // mantenerlo para futuros intentos
            TempData.Keep();
            return View();
        }


        #endregion

        #region Nueva Contraseña

        [HttpGet]
        public IActionResult NuevaContrasena()
        {
            ViewBag.UsuarioId = TempData["UsuarioId"];
            ViewBag.TokenId = TempData["TokenId"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevaContrasena(int usuarioId, int tokenId, RecuperacionContrasenaViewModel vm)
        {
            if (vm.NuevaContrasena != vm.ConfirmarContrasena)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View(vm);
            }

            await _recuperacionService.CambiarContrasenaAsync(usuarioId, vm.NuevaContrasena!, tokenId);
            ViewBag.Mensaje = "Contraseña actualizada correctamente.";
            return RedirectToAction("Login");
        }

        #endregion

        #region Logout

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        #endregion
    }
}
