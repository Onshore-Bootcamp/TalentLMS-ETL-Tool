namespace EverymanETL.DataProviders
{
    using System.Collections.Generic;

    public interface IDataProvider<T>
    {
        bool Create(T source);

        HashSet<T> ViewAll();

        T ViewById(int source);

        bool Update(T source);

        bool Delete(T source);
    }
}
