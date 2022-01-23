using System;
using System.Collections.Generic;
using System.Text;

namespace Midia_Indoo.Banco.Contratos
{
    public interface IBaseRepositorio<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
