using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sprint_2.Data;
using Sprint_2.Models;

namespace Sprint_2.Controllers
{
    public class RolController : Controller
    {
        private readonly string _connectionString;

        public RolController(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString() ?? throw new InvalidOperationException("Connection string no encontrada.");
        }
        #region"Listar Roles"
        // Listar roles
        public IActionResult Index()
        {
            List<Rol> roles = new();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ListarRoles", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString()
                        });
                    }
                }
            }

            return View(roles);
        }
        #endregion
        #region"Create"
        // GET Crear rol
        public IActionResult Create()
        {
            return View();
        }

        // POST Crear rol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rol rol)
        {
            if (!ModelState.IsValid)
                return View(rol);

            try
            {
                using (SqlConnection conn = new(_connectionString))
                using (SqlCommand cmd = new("sp_CrearRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear rol: " + ex.Message);
                return View(rol);
            }
        }
        #endregion
        #region"Editar"
        // GET Editar rol
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Rol rol = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerRolPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rol = new Rol
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString()
                        };
                    }
                }
            }

            if (rol == null)
                return NotFound();

            return View(rol);
        }

        // POST Editar rol
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Rol rol)
        {
            if (id != rol.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(rol);

            try
            {
                using (SqlConnection conn = new(_connectionString))
                using (SqlCommand cmd = new("sp_ActualizarRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", rol.Id);
                    cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar rol: " + ex.Message);
                return View(rol);
            }
        }
        #endregion
    }
}

