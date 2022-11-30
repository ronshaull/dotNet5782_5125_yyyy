 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public interface ICrud<T>
{
    int Add (T entity);
    void Update (T entity);
    void Delete (int ID);
    T Get (int id);
    IEnumerable<T> GetAll ();
}
