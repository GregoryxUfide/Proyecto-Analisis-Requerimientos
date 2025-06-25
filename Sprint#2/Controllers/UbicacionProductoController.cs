using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sprint_2.Data;
using Sprint_2.Models;

namespace Sprint_2.Controllers
{
    public class UbicacionProductoController : Controller
    {
        private readonly string _connectionString;

        public UbicacionProductoController(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString() ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
        }
        #region"Listar"
        // LISTAR
        public IActionResult Index()
        {
            List<UbicacionProducto> lista = new();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ListarUbicaciones", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new UbicacionProducto
                        {
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString(),
                            DescripcionUbicacionProducto = reader["DescripcionUbicacionProducto"].ToString()
                        });
                    }
                }
            }

            return View(lista);
        }
        #endregion
        #region"Detalle"
        // DETALLE
        public IActionResult Details(int id)
        {
            UbicacionProducto ubicacion = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerUbicacionPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUbicacionProducto", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ubicacion = new UbicacionProducto
                        {
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString(),
                            DescripcionUbicacionProducto = reader["DescripcionUbicacionProducto"].ToString()
                        };
                    }
                }
            }

            if (ubicacion == null)
                return NotFound();

            return View(ubicacion);
        }
        #endregion
        #region"Create"
        // CREAR (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UbicacionProducto ubicacion)
        {
            if (!ModelState.IsValid)
                return View(ubicacion);

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_CrearUbicacion", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreUbicacionProducto", ubicacion.NombreUbicacionProducto);
                cmd.Parameters.AddWithValue("@DescripcionUbicacionProducto", ubicacion.DescripcionUbicacionProducto ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region"Edit"
        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            UbicacionProducto ubicacion = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerUbicacionPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUbicacionProducto", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ubicacion = new UbicacionProducto
                        {
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString(),
                            DescripcionUbicacionProducto = reader["DescripcionUbicacionProducto"].ToString()
                        };
                    }
                }
            }

            if (ubicacion == null)
                return NotFound();

            return View(ubicacion);
        }

        // EDITAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UbicacionProducto ubicacion)
        {
            if (id != ubicacion.IdUbicacionProducto)
                return NotFound();

            if (!ModelState.IsValid)
                return View(ubicacion);

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ActualizarUbicacion", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUbicacionProducto", ubicacion.IdUbicacionProducto);
                cmd.Parameters.AddWithValue("@NombreUbicacionProducto", ubicacion.NombreUbicacionProducto);
                cmd.Parameters.AddWithValue("@DescripcionUbicacionProducto", ubicacion.DescripcionUbicacionProducto ?? (object)DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
