using Microsoft.Data.SqlClient;

namespace Sprint_2.Data
{
    public class ConexionDB
    {
        private readonly string _cadenaConexion;

        public ConexionDB(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("BDHotel");
        }

        public async Task<SqlConnection> ObtenerConexionAsync()
        {
            var conexion = new SqlConnection(_cadenaConexion);
            await conexion.OpenAsync();
            return conexion;
        }
    }
}
