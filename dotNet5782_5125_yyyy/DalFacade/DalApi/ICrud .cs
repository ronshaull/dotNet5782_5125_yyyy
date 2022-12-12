 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi;

public interface ICrud<T>where T : struct
{
    int Add (T entity);
    void Update (T entity);
    void Delete (int ID);
    T Get (int id);
    IEnumerable<T?> GetAll (Func<T?,bool>? Select =null);
    /// <summary>
    /// special get function that retrive a certin 
    /// oobject with specific ID and that setisfay 
    /// a certin condition.
    /// </summary>
    /// <param name="ID">of the wanted object</param>
    /// <param name="Select">the condition imlemented by a boolean function.
    /// this function will be passed as a lamda expression.</param>
    /// <returns></returns>
    T Get(int ID,Func<T?, bool>? Select); 
}
