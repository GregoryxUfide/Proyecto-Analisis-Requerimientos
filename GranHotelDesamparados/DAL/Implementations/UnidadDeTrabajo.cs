using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {

        public IProductoDAL ProductoDAL { get; set; }
        public IUbicacionProductoDAL UbicacionProductoDAL { get; set; }

        private GranHotelDesamparadosContext _granHotelDesamparadosContext;

        public UnidadDeTrabajo(GranHotelDesamparadosContext granHotelDesamparadosContext,
            IProductoDAL productoDAL,
            IUbicacionProductoDAL ubicacionProductoDAL)

        {
            this._granHotelDesamparadosContext = granHotelDesamparadosContext;
            this.ProductoDAL = productoDAL;
            this.UbicacionProductoDAL = ubicacionProductoDAL;
        }

        public bool Complete()
        {
            try
            {
                _granHotelDesamparadosContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            this._granHotelDesamparadosContext.Dispose();
        }
    }
}
