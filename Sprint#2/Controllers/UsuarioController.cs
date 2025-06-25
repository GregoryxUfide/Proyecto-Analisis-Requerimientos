using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sprint_2.Data;
using Sprint_2.Models;

namespace Sprint_2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly string _connectionString;

        public UsuarioController(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString() ?? throw new InvalidOperationException("Connection string no encontrada.");
        }
        #region "Index"
        public IActionResult Index()
        {
            List<Usuario> usuarios = new();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ListarUsuarios", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Gmail = reader["Gmail"].ToString(),
                            Username = reader["Username"].ToString(),
                            Estado = (bool)reader["Estado"],
                            RolId = (int)reader["RolId"],
                            Rol = new Rol { Nombre = reader["RolNombre"].ToString() }
                        });
                    }
                }
            }

            return View(usuarios);
        }
        #endregion
        #region"Create"
        public IActionResult Create()
        {
            ViewBag.Roles = ObtenerListaRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = ObtenerListaRoles();
                return View(usuario);
            }
            try
            {
                int rolId = usuario.RolId;                
                if (rolId == 0)
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerRolPorNombre", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", "Usuario");
                        conn.Open();
                        rolId = (int)(cmd.ExecuteScalar() ?? 0);
                    }
                }
                if (rolId == 0)
                {
                    ViewBag.Error = "Rol no encontrado.";
                    ViewBag.Roles = ObtenerListaRoles();
                    return View(usuario);
                }

                if (string.IsNullOrEmpty(usuario.Contrasena))
                {
                    ModelState.AddModelError("Contrasena", "La contraseña es requerida.");
                    ViewBag.Roles = ObtenerListaRoles();
                    return View(usuario);
                }

                string hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_CrearUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@Gmail", usuario.Gmail);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Contrasena", hash);
                    cmd.Parameters.AddWithValue("@Estado", true); 
                    cmd.Parameters.AddWithValue("@RolId", rolId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Roles = ObtenerListaRoles();
                ModelState.AddModelError("", "Error al crear usuario: " + ex.Message);
                return View(usuario);
            }
        }
        #endregion
        #region"Edit"
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Usuario usuario = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerUsuarioPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Gmail = reader["Gmail"].ToString(),
                            Username = reader["Username"].ToString(),
                            Estado = (bool)reader["Estado"],
                            RolId = (int)reader["RolId"],
                            Rol = new Rol { Nombre = reader["RolNombre"].ToString() },
                            Contrasena = ""
                        };
                    }
                }
            }

            if (usuario == null)
                return NotFound();

            ViewBag.Roles = ObtenerListaRoles();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            ModelState.Remove("Contrasena");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = ObtenerListaRoles();
                return View(usuario);
            }

            try
            {
                string passwordParam;

                if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                {
                    using (SqlConnection conn = new(_connectionString))
                    using (SqlCommand cmd = new("SELECT Contrasena FROM Usuarios WHERE Id = @Id", conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        conn.Open();
                        var result = cmd.ExecuteScalar();
                        passwordParam = result?.ToString() ?? "";
                    }
                }
                else
                {
                    passwordParam = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
                }

                using (SqlConnection conn = new(_connectionString))
                using (SqlCommand cmd = new("sp_ActualizarUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("@Gmail", usuario.Gmail);
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@Contrasena", passwordParam);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@RolId", usuario.RolId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Roles = ObtenerListaRoles();
                ModelState.AddModelError("", "Error al actualizar usuario: " + ex.Message);
                return View(usuario);
            }
        }
        #endregion
        #region"Inactivar"
        public IActionResult Inactivar(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                using (SqlConnection conn = new(_connectionString))
                using (SqlCommand cmd = new("UPDATE Usuarios SET Estado = 0 WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al inactivar usuario: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion
        #region"Details"
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            Usuario usuario = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerUsuarioPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Apellidos = reader["Apellidos"].ToString(),
                            Gmail = reader["Gmail"].ToString(),
                            Username = reader["Username"].ToString(),
                            Estado = (bool)reader["Estado"],
                            RolId = (int)reader["RolId"],
                            Rol = new Rol { Nombre = reader["RolNombre"].ToString() }
                        };
                    }
                }
            }

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }
        #endregion
        #region"ListaRoles"
        private List<Rol> ObtenerListaRoles()
        {
            List<Rol> roles = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ListarRoles", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString()
                        });
                    }
                }
            }

            return roles;
        }
        #endregion
    }
}
