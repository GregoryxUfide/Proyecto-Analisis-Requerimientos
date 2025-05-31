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

        //public IProgramasDAL ProgramasDAL { get; set; }              REF CODIGO VIEJO
        //public IParametroDAL ParametroDAL { get; set; }

        private GranHotelDesamparadosContext _granHotelDesamparadosContext;

        public UnidadDeTrabajo(GranHotelDesamparadosContext granHotelDesamparadosContext)
            //, 
            //IProgramasDAL programasDAL, 
            //IParametroDAL parametroDAL)
            
        {
            this._granHotelDesamparadosContext = granHotelDesamparadosContext;
            //this.ProgramasDAL = programasDAL;
            //this.ParametroDAL = parametroDAL;

        }

        public bool Complete()
        {
            //throw new NotImplementedException();

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
            //throw new NotImplementedException();

            this._granHotelDesamparadosContext.Dispose();
        }
    }
}
