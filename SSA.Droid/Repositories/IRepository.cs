using System.Collections.Generic;

namespace SSA.Droid.Repositories
{
    interface IRepository<T>
    {
        T Save(T item);
        T Get(int id);
        int Delete(int id);
        int DeleteAll();
        List<T> GetAll();
    }
}