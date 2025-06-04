using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UbicacionProductoDAL : DALGenericoImpl<UbicacionProducto>, IUbicacionProductoDAL
    {
        GranHotelDesamparadosContext _context;
        public UbicacionProductoDAL(GranHotelDesamparadosContext context) : base(context)
        {
            _context = context;
        }
    }
}
