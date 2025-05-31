using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
    {
        private GranHotelDesamparadosContext _granHotelDesamparadosContext;

        public DALGenericoImpl(GranHotelDesamparadosContext granHotelDesamparadosContext)
        {
            _granHotelDesamparadosContext = granHotelDesamparadosContext;
        }

        public bool Add(TEntity entity)
        {

            try
            {
                _granHotelDesamparadosContext.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TEntity Get(int id)
        {

            return _granHotelDesamparadosContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {

            return _granHotelDesamparadosContext.Set<TEntity>().ToList();
        }

        public bool Remove(TEntity entity)
        {

            try
            {
                _granHotelDesamparadosContext.Set<TEntity>().Attach(entity);
                _granHotelDesamparadosContext.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {

            try
            {
                _granHotelDesamparadosContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
