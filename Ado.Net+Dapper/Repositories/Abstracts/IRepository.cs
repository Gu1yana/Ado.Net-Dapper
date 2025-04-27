using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net_Dapper.Repositories.Abstracts;

internal interface IRepository<T>
{
    List<T> GetAll();
    T GetById(int id);
    void Add(T model);
    void Update(int id,T model);
    void Delete(int id);
}
