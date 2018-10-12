using System;
using System.Collections.Generic;

namespace MessageConsumer.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        T Create(T item);
        void Delete(Guid id);
        T Get(Guid id);
        void SaveChanges();
    }
}
