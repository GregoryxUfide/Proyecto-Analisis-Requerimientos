using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;
using hotelproyecto.Data;   

namespace hotelproyecto.Controllers
{    
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly RolService _rolService;

        public UsuarioController(UsuarioService usuarioService, RolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        #region "Index"
        public async Task<IActionResult> Index(int? rolId, bool? estado, string busqueda)
        {
            var usuarios = await _usuarioService.ListarUsuariosViewModelAsync(rolId, estado, busqueda);
            var roles = await _rolService.ObtenerRolesActivosAsync();

            var viewModel = new UsuarioFiltroViewModel
            {
                RolSeleccionadoId = rolId,
                EstadoSeleccionado = estado,
                Busqueda = busqueda,
                Roles = roles,
                Usuarios = usuarios
            };

            return View(viewModel);
        }

        #endregion

        #region "Crear"
        public async Task<IActionResult> Crear()
        {
            var roles = await _rolService.ObtenerRolesActivosAsync();
            var vm = new UsuarioViewModel { Roles = roles };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Roles = await _rolService.ObtenerRolesActivosAsync();
                return View(vm);
            }

            bool existe = await _usuarioService.ExisteCorreoAsync(vm.Gmail);
            if (existe)
            {
                ModelState.AddModelError("", "El correo ya está registrado.");
                vm.Roles = await _rolService.ObtenerRolesActivosAsync();
                return View(vm);
            }

            await _usuarioService.CrearUsuarioAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _usuarioService.ObtenerUsuarioViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            var usuarioLogueadoId = HttpContext.Session.GetInt32("UsuarioID");
            var rolLogueado = HttpContext.Session.GetString("Rol");

            if (usuarioLogueadoId == id && rolLogueado?.ToLower() == "admin")
            {
                vm.EsEdicionPropiaComoAdmin = true;
            }

            vm.Roles = await _rolService.ObtenerRolesActivosAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel vm)
        {
            var usuarioLogueadoId = HttpContext.Session.GetInt32("UsuarioID");
            var rolLogueado = HttpContext.Session.GetString("Rol");

            if (usuarioLogueadoId == vm.Id && rolLogueado?.ToLower() == "admin")
            {
                var original = await _usuarioService.ObtenerUsuarioViewModelPorIdAsync(vm.Id);
                vm.RolId = original.RolId;
                vm.Estado = original.Estado;
            }

            if (!ModelState.IsValid)
            {
                vm.Roles = await _rolService.ObtenerRolesActivosAsync();
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
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).{6,}$");

            if (string.IsNullOrWhiteSpace(nuevaContrasena) || !regex.IsMatch(nuevaContrasena))
            {
                ModelState.AddModelError("", "La contraseña debe tener al menos 6 caracteres y un carácter especial.");
            }
            else if (nuevaContrasena != confirmarContrasena)
            {
                ModelState.AddModelError("", "Las contraseñas no coinciden.");
            }

            if (!ModelState.IsValid)
            {
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
