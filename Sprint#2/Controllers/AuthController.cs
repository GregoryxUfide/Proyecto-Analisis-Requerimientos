using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sprint_2.Data;
using Sprint_2.Models;

namespace Sprint_2.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _connectionString;

        public AuthController(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString() ?? throw new InvalidOperationException("Connection string no encontrada.");
        }

        #region"Login"
        // GET: Login
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public IActionResult Login(string gmail, string contraseña)
        {
            Usuario usuario = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorCorreo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Gmail", gmail);

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
                                Contrasena = reader["Contrasena"].ToString(),
                                Estado = (bool)reader["Estado"],
                                RolId = (int)reader["RolId"],
                                Rol = new Rol
                                {
                                    Id = (int)reader["RolId"],
                                    Nombre = reader["RolNombre"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            if (usuario != null && usuario.Estado && BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contrasena))
            {
                HttpContext.Session.SetInt32("UsuarioID", usuario.Id);
                HttpContext.Session.SetString("Rol", usuario.Rol.Nombre);
                HttpContext.Session.SetString("Nombre", usuario.Nombre);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas o usuario inactivo.";
            return View();
        }
        #endregion
        #region "Registro"
        // GET: Register
        public IActionResult Register() => View();

        // POST: Register
        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            try
            {
                string rolPorDefecto = "Usuario";
                int rolId = 0;

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerRolPorNombre", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", rolPorDefecto);

                        conn.Open();
                        rolId = (int)(cmd.ExecuteScalar() ?? 0);
                    }
                }

                if (rolId == 0)
                {
                    ViewBag.Error = "Rol no encontrado.";
                    return View(usuario);
                }

                string hash = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
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
                }

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(usuario);
            }
        }
        #endregion
        #region "Logout"
        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
    #endregion
}
