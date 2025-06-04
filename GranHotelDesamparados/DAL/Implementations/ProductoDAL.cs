using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ProductoDAL : DALGenericoImpl<Producto>, IProductoDAL
    {
        GranHotelDesamparadosContext _context;
        public ProductoDAL(GranHotelDesamparadosContext context) : base(context)
        {
            _context = context;
        }   
    }
}
