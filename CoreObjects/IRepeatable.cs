// Ross Higley
// CS 1182
// 19 April 2016
// Copy Interface for deliverable 6
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    interface IRepeatable<T>
    {
        /// <summary>
        /// Override this to perform a deep copy. Uses generic Types. 
        /// </summary>
        /// <returns>returns a generic type, which should be cast into the same object class that the interface is attached to.  </returns>
        T CreateCopy();
    }
}

#region Unused Code
//interface IRepeatable
//{
//    /// <summary>
//    /// Override this to perform a deep copy. 
//    /// </summary>
//    /// <returns>returns an object, which can be cast into an object of a derived class. </returns>
//    object CreateCopy();
//}
#endregion