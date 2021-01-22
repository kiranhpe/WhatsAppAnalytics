using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Features
{
    public interface IGenericRepository<T>
    {
        List<T> GetData(T filter);

        List<T> InsertData(List<T> data);
    }
}
