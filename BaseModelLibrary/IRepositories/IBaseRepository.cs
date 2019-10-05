using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T model);
        List<T> GetAll();
        T Get(int id);
        void Remove(int id);
        void Remove(T model);
    }
}
