using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{    
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoService _empleadoService;
        private readonly UsuarioService _usuarioService;
        private readonly RolService _rolService;

        public EmpleadoController(EmpleadoService empleadoService, UsuarioService usuarioService, RolService rolService)
        {
            _empleadoService = empleadoService;
            _usuarioService = usuarioService;
            _rolService = rolService;
        }
        
        #region Index
        public async Task<IActionResult> Index()
        {
            var empleados = await _empleadoService.ListarEmpleadosViewModelAsync();
            return View(empleados);
        }
        #endregion

        #region Detalles
        public async Task<IActionResult> Detalles(int id)
        {
            var empleadoVM = await _empleadoService.ObtenerEmpleadoViewModelPorIdAsync(id);
            if (empleadoVM == null)
                return NotFound();

            return View(empleadoVM);
        }
        #endregion

        #region Crear
        public async Task<IActionResult> Crear()
        {
            var roles = await _rolService.ListarRolesAsync();

            // Buscar el rol "Empleado" para filtrar usuarios que tengan ese rol
            var rolEmpleado = roles.FirstOrDefault(r => r.Nombre.ToLower().Contains("empleado"));

            var usuarios = rolEmpleado != null
                ? await _usuarioService.ListarUsuariosViewModelAsync(rolEmpleado.Id, null, null)
                : new System.Collections.Generic.List<UsuarioViewModel>();

            var vm = new EmpleadoViewModel
            {
                RolesDisponibles = roles,
                UsuariosDisponibles = usuarios
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(EmpleadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.RolesDisponibles = await _rolService.ListarRolesAsync();
                vm.UsuariosDisponibles = await _usuarioService.ListarUsuariosViewModelAsync(vm.RolId, null, null);
                return View(vm);
            }

            await _empleadoService.CrearEmpleadoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        
        #region Editar
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _empleadoService.ObtenerEmpleadoViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Editar(EmpleadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.RolesDisponibles = await _rolService.ListarRolesAsync();                
                return View(vm);
            }

            await _empleadoService.ActualizarEmpleadoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Estado
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _empleadoService.CambiarEstadoEmpleadoAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
