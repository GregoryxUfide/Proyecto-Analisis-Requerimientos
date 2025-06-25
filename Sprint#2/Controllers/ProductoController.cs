using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sprint_2.Data;
using Sprint_2.Models;

namespace Sprint_2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly string _connectionString;

        public ProductoController(AppDbContext context)
        {
            _connectionString = context.Database.GetConnectionString() ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
        }
        #region"ListarProductos"
        // LISTAR
        public IActionResult Index()
        {
            List<Producto> productos = new();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ListarProductos", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            DescripcionProducto = reader["DescripcionProducto"].ToString(),
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            CantidadProducto = (int)reader["CantidadProducto"],
                            CaducidadProducto = reader["CaducidadProducto"] != DBNull.Value ? (DateTime?)reader["CaducidadProducto"] : null,
                            MarcaProducto = reader["MarcaProducto"].ToString(),
                            EstadoProducto = (bool)reader["EstadoProducto"],
                            UbicacionProducto = new UbicacionProducto
                            {
                                NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString()
                            }
                        });
                    }
                }
            }

            return View(productos);
        }
        #endregion
        #region"Create"
        // GET CREAR
        public IActionResult Create()
        {
            CargarUbicaciones();
            return View();
        }

        // POST CREAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                CargarUbicaciones();
                return View(producto);
            }

            try
            {
                using (SqlConnection conn = new(_connectionString))
                using (SqlCommand cmd = new("sp_CrearProducto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                    cmd.Parameters.AddWithValue("@DescripcionProducto", producto.DescripcionProducto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdUbicacionProducto", producto.IdUbicacionProducto);
                    cmd.Parameters.AddWithValue("@CantidadProducto", producto.CantidadProducto);
                    cmd.Parameters.AddWithValue("@CaducidadProducto", (object?)producto.CaducidadProducto ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EstadoProducto", producto.EstadoProducto);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result == 0)
                    {
                        ModelState.AddModelError("", "No se insertó el producto.");
                        CargarUbicaciones();
                        return View(producto);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear producto: " + ex.Message);
                CargarUbicaciones();
                return View(producto);
            }
        }
        #endregion
        #region"Editar"
        // GET EDITAR
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Producto producto = ObtenerProductoPorId(id.Value);
            if (producto == null)
                return NotFound();

            CargarUbicaciones();
            return View(producto);
        }

        // POST EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.IdProducto)
                return NotFound();

            if (!ModelState.IsValid)
            {
                CargarUbicaciones();
                return View(producto);
            }

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_EditarProducto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                cmd.Parameters.AddWithValue("@DescripcionProducto", producto.DescripcionProducto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IdUbicacionProducto", producto.IdUbicacionProducto);
                cmd.Parameters.AddWithValue("@CantidadProducto", producto.CantidadProducto);
                cmd.Parameters.AddWithValue("@CaducidadProducto", (object?)producto.CaducidadProducto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MarcaProducto", producto.MarcaProducto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EstadoProducto", producto.EstadoProducto);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region"Detalles"
        // DETALLES
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            Producto producto = ObtenerProductoPorId(id.Value);
            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // INACTIVAR
        public IActionResult Inactivar(int? id)
        {
            if (id == null)
                return NotFound();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_InactivarProducto", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region"Metodos"
        // METODO PARA OBTENER DETALLE POR ID
        private Producto ObtenerProductoPorId(int id)
        {
            Producto producto = null;

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("sp_ObtenerProductoPorId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", id);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto
                        {
                            IdProducto = (int)reader["IdProducto"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            DescripcionProducto = reader["DescripcionProducto"].ToString(),
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            CantidadProducto = (int)reader["CantidadProducto"],
                            CaducidadProducto = reader["CaducidadProducto"] != DBNull.Value ? (DateTime?)reader["CaducidadProducto"] : null,
                            MarcaProducto = reader["MarcaProducto"].ToString(),
                            EstadoProducto = (bool)reader["EstadoProducto"],
                            UbicacionProducto = new UbicacionProducto
                            {
                                NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString()
                            }
                        };
                    }
                }
            }

            return producto;
        }

        // METODO PARA CARGAR UBICACIONES EN DROPDOWN
        private void CargarUbicaciones()
        {
            List<UbicacionProducto> ubicaciones = new();

            using (SqlConnection conn = new(_connectionString))
            using (SqlCommand cmd = new("SELECT IdUbicacionProducto, NombreUbicacionProducto FROM UbicacionProducto", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ubicaciones.Add(new UbicacionProducto
                        {
                            IdUbicacionProducto = (int)reader["IdUbicacionProducto"],
                            NombreUbicacionProducto = reader["NombreUbicacionProducto"].ToString()
                        });
                    }
                }
            }

            ViewBag.Ubicaciones = ubicaciones;
        }
        #endregion
    }
}
