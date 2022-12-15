using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

/// <summary>
/// in case we try to either add item when there is no space for it.
/// as was defiend in project description.
/// </summary>
public class OutOfRangeEx : Exception
{
    public string message= "Exception thrown: insufficient space in that list.";
}
/// <summary>
/// in case we ask an object (to display/change etc) and it was not found in list.
/// </summary>
public class ObjectNotFoundEx :Exception
{
    public string message= "Exception thrown: Object was not found in list.";
}
/// <summary>
/// in case we want to create a new object with an excisting id.
/// </summary>
public class IdExistEx : Exception
{
    public string message= "Exception thrown: ID already excisit.";
}

public class EmptyListEx: Exception
{
    public string message = "list was empty!.";
}
/// <summary>
/// when trying to laod the correct dal lyer, if an exception occurs 
/// then this instance is used.
/// </summary>
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
