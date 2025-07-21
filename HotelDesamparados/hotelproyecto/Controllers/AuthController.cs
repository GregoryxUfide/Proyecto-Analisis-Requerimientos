using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Models;
using hotelproyecto.Services;

namespace hotelproyecto.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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

        #region Logout

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        #endregion
    }
}
