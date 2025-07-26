using hotelproyecto.Data;

namespace hotelproyecto.Services
{
    public class TokenRecuperacionService
    {
        private readonly TokenRecuperacionData _tokenData;

        public TokenRecuperacionService(TokenRecuperacionData tokenData)
        {
            _tokenData = tokenData;
        }

        #region Generar y Guardar Token        
        public async Task<string> GenerarYGuardarTokenAsync(int usuarioId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime expiracion = DateTime.Now.AddHours(1);

            await _tokenData.GuardarTokenAsync(usuarioId, token, expiracion);
            return token;
        }
        #endregion

        #region Validar Token        
        public async Task<bool> ValidarTokenAsync(int usuarioId, string token)
        {
            var tokenRec = await _tokenData.ObtenerTokenPorUsuarioAsync(usuarioId, token);
            if (tokenRec == null)
                return false;

            if (tokenRec.Usado)
                return false;

            if (tokenRec.FechaExpiracion < DateTime.Now)
                return false;

            return true;
        }
        #endregion

        #region Marcar Token como Usado        
        public async Task MarcarTokenComoUsadoAsync(int usuarioId, string token)
        {
            var tokenRec = await _tokenData.ObtenerTokenPorUsuarioAsync(usuarioId, token);
            if (tokenRec != null && !tokenRec.Usado)
            {
                await _tokenData.MarcarTokenComoUsadoAsync(tokenRec.Id);
            }
        }
        #endregion
    }
}
