using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Models;


namespace hotelproyecto.Data
{
    public class EmpleadoData
    {
        private readonly ConexionDB _conexionDB;

        public EmpleadoData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region Crear
        public async Task CrearEmpleadoAsync(Empleado empleado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearEmpleado", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NumeroEmpleado", empleado.NumeroEmpleado);
            cmd.Parameters.AddWithValue("@SalarioEmpleado", empleado.SalarioEmpleado);
            cmd.Parameters.AddWithValue("@UsuarioId", empleado.UsuarioId);
            cmd.Parameters.AddWithValue("@RolId", empleado.RolId);
            cmd.Parameters.AddWithValue("@Estado", empleado.Estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region ObtenerPorId
        public async Task<Empleado?> ObtenerEmpleadoPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerEmpleadoPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Empleado
                {
                    Id = reader.GetInt32(0),
                    NumeroEmpleado = reader.GetString(1),
                    SalarioEmpleado = reader.GetDecimal(2),
                    UsuarioId = reader.GetInt32(3),
                    Usuario = new Usuario
                    {
                        Id = reader.GetInt32(3),
                        Username = reader.GetString(4)
                    },
                    RolId = reader.GetInt32(5),
                    Rol = new Rol
                    {
                        Id = reader.GetInt32(5),
                        Nombre = reader.GetString(6)
                    },
                    Estado = reader.GetBoolean(7)
                };
            }

            return null;
        }
        #endregion

        #region Listar
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
                        Username = reader.GetString(4)
                    },
                    RolId = reader.GetInt32(5),
                    Rol = new Rol
                    {
                        Id = reader.GetInt32(5),
                        Nombre = reader.GetString(6)
                    },
                    Estado = reader.GetBoolean(7)
                });
            }

            return lista;
        }
        #endregion

        #region Actualizar
        public async Task ActualizarEmpleadoAsync(Empleado empleado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarEmpleado", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", empleado.Id);
            cmd.Parameters.AddWithValue("@NumeroEmpleado", empleado.NumeroEmpleado);
            cmd.Parameters.AddWithValue("@SalarioEmpleado", empleado.SalarioEmpleado);
            cmd.Parameters.AddWithValue("@RolId", empleado.RolId);
            cmd.Parameters.AddWithValue("@Estado", empleado.Estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region Estado
        public async Task CambiarEstadoEmpleadoAsync(int id, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoEmpleado", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Estado", estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
