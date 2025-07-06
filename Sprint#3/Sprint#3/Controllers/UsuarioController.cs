using Microsoft.AspNetCore.Mvc;
using Sprint_2.Data;
using Sprint_2.Models;
using Sprint_2.Services;
using Sprint2.ViewModel;

namespace Sprint_2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly RolData _rolData;

        public UsuarioController(UsuarioService usuarioService, RolData rolData)
        {
            _usuarioService = usuarioService;
            _rolData = rolData;
        }

        #region"Index"
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ListarUsuariosViewModelAsync();
            return View(usuarios);
        }
        #endregion

        #region"Crear"
        public async Task<IActionResult> Crear()
        {
            var roles = await _rolData.ListarRolesAsync();
            var vm = new UsuarioViewModel { Roles = roles };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Roles = await _rolData.ListarRolesAsync();
                return View(vm);
            }

            bool existe = await _usuarioService.ExisteCorreoAsync(vm.Gmail);
            if (existe)
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                vm.Roles = await _rolData.ListarRolesAsync();
                return View(vm);
            }

            await _usuarioService.CrearUsuarioAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region"Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _usuarioService.ObtenerUsuarioViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }        
        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Roles = await _rolData.ListarRolesAsync();
                return View(vm);
            }

            await _usuarioService.ActualizarUsuarioAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Actualizar Contraseña"
        public async Task<IActionResult> CambiarContrasena(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioViewModelPorIdAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.UsuarioId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(int id, string nuevaContrasena, string confirmarContrasena)
        {
            if (string.IsNullOrWhiteSpace(nuevaContrasena) || nuevaContrasena.Length < 6)
            {
                ModelState.AddModelError("", "La contraseña debe tener al menos 6 caracteres.");
                ViewBag.UsuarioId = id;
                return View();
            }

            if (nuevaContrasena != confirmarContrasena)
            {
                ModelState.AddModelError("", "Las contraseñas no coinciden.");
                ViewBag.UsuarioId = id;
                return View();
            }

            await _usuarioService.ActualizarContrasenaAsync(id, nuevaContrasena);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Detalles"
        public async Task<IActionResult> Detalles(int id)
        {
            var usuarioVM = await _usuarioService.ObtenerUsuarioViewModelPorIdAsync(id);
            if (usuarioVM == null)
                return NotFound();

            return View(usuarioVM);
        }
        #endregion

        #region"Estado"
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _usuarioService.CambiarEstadoUsuarioAsync(id, estado);
            return RedirectToAction("Index");
        }
    }
}
#endregion
