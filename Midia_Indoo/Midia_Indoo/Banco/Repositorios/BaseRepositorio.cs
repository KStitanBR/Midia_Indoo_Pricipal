using Midia_Indoo.Banco.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Midia_Indoo.Banco.Repositorios
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {

        protected readonly BancoContext DbContext;


        public BaseRepositorio()
        {
            DbContext = new BancoContext();
        }

        public void Add(TEntity entity)
        {

            var result = DbContext.Set<TEntity>().Add(entity);

            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
        }
        public TEntity GetById(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().ToList();
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                DbContext.Remove(entity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return false;
            }

        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
