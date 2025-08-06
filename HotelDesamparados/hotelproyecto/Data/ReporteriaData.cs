using hotelproyecto.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Data;

namespace hotelproyecto.Data
{
    public class ReporteriaData
    {
        private readonly ConexionDB _conexionDB;

        public ReporteriaData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Tabla Productos"
        public async Task<List<Producto>> ListarProductosAsync()
        {
            var lista = new List<Producto>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarProductos", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Producto
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                    DescripcionProducto = reader.GetString(reader.GetOrdinal("DescripcionProducto")),
                    IdUbicacionProducto = reader.GetInt32(reader.GetOrdinal("IdUbicacionProducto")),
                    CantidadProducto = reader.GetInt32(reader.GetOrdinal("CantidadProducto")),
                    CaducidadProducto = reader.GetDateTime(reader.GetOrdinal("CaducidadProducto")),
                    MarcaProducto = reader.GetString(reader.GetOrdinal("MarcaProducto")),
                    EstadoProducto = reader.GetBoolean(reader.GetOrdinal("EstadoProducto"))
                });
            }

            return lista;
        }
        #endregion

        #region "Tabla Ubicación de Productos"
        public async Task<List<UbicacionProducto>> ListarUbicacionesAsync()
        {
            var lista = new List<UbicacionProducto>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUbicaciones", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new UbicacionProducto
                {
                    IdUbicacionProducto = reader.GetInt32(reader.GetOrdinal("IdUbicacionProducto")),
                    NombreUbicacionProducto = reader.GetString(reader.GetOrdinal("NombreUbicacionProducto")),
                    DescripcionUbicacionProducto = reader.GetString(reader.GetOrdinal("DescripcionUbicacionProducto")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
                });
            }

            return lista;
        }
        #endregion

        #region "Tabla Usuario"

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            var lista = new List<Usuario>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUsuarios", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    Gmail = reader.GetString(reader.GetOrdinal("Gmail")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    RolId = reader.GetInt32(reader.GetOrdinal("RolId"))
                });
            }

            return lista;
        }

        #endregion
        
        #region "Tabla Roles"

        public async Task<List<Rol>> ListarRolesAsync()
        {
            var lista = new List<Rol>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarRoles", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Rol
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                });
            }

            return lista;
        }

        #endregion

        #region "Listar Conta"
        public async Task<List<Contabilidad>> ListarContabilidadAsync()
        {
            var lista = new List<Contabilidad>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarContabilidad", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Contabilidad
                {
                    IdContabilidad = reader.GetInt32(0),
                    Fecha = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                    Monto = reader.GetDecimal(2),
                    Detalle = reader.GetString(3),
                    Comentario = reader.GetString(4)
                });
            }
            return lista;
        }
        #endregion

        #region Listar Empleados
        public async Task<List<Empleado>> ListarEmpleadosAsync()
        {
            var lista = new List<Empleado>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarEmpleados", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Empleado
                {
                    Id = reader.GetInt32(0),
                    NumeroEmpleado = reader.GetString(1),
                    SalarioEmpleado = reader.GetDecimal(2),
                    UsuarioId = reader.GetInt32(3),
                    Usuario = new Usuario
                    {
                        Id = reader.GetInt32(3),
                        Username = reader.GetString(4),
                        Rol = new Rol
                        {
                            Id = reader.GetInt32(5),
                            Nombre = reader.GetString(6)
                        }
                    },
                    Estado = reader.GetBoolean(7)
                });
            }

            return lista;
        }
        #endregion

        #region "Listar Habitaciones"
        public async Task<List<Habitacion>> ListarHabitacionesAsync()
        {
            var lista = new List<Habitacion>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarHabitaciones", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Habitacion
                {
                    Id = reader.GetInt32(0),
                    Capacidad = reader.GetInt32(1),
                    Precio = reader.GetDecimal(2),
                    NumHabitacion = reader.GetInt32(3),
                    NumCamas = reader.GetInt32(4),
                    Extras = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Comentarios = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Estado = reader.GetBoolean(7)
                });
            }
            return lista;
        }
        #endregion

        #region "Listar Reservas"
        public async Task<List<Reserva>> ListarReservasAsync()
        {
            var lista = new List<Reserva>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarReservas", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Reserva
                {
                    IdReserva = reader.GetInt32(0),
                    FechaInicio = reader.GetDateTime(1),
                    FechaFinal = reader.GetDateTime(2),
                    Nombre = reader.GetString(3),
                    Telefono = reader.GetString(4),
                    Correo = reader.GetString(5),
                    HabitacionId = reader.GetInt32(6),
                    Estado = reader.GetBoolean(7)
                });
            }
            return lista;
        }
        #endregion

        #region "Listar Punto Venta"
        public async Task<List<PuntoVenta>> ListarPuntosVentaAsync()
        {
            var lista = new List<PuntoVenta>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarPuntoVenta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new PuntoVenta
                {
                    Id = reader.GetInt32(0),
                    DescripcionVenta = reader.GetString(1),
                    Metodo_Pago = reader.GetString(2),
                    Descuento = reader.GetDecimal(3),
                    ReservaId = reader.GetInt32(4),       // IdReserva
                    EmpleadoId = reader.GetInt32(6)       // UsuarioId del empleado
                    // Nota: Si quieres traer el correo reserva y nombre empleado puedes mapear en un ViewModel o DTO separado
                });
            }
            return lista;
        }
        #endregion
    }
}
