// Ross Higley
// CS 1182
// 19 April 2016
// DoorKey class for deliverable 5
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be seralized to binary or XML
    public class DoorKey: Item
    {
        private string _Code;

        #region Constructors

        /// <summary>
        /// Creates a new DoorKey object. Make sure to create a Door for this key.
        /// </summary>
        /// <param name="name">Name of doorkey.</param>
        /// <param name="strength">Item effect strength of the key. Can be set to 0.</param>
        /// <param name="code">string passcode to match with Door.</param>
        public DoorKey(string name, int strength, string code):base(name, strength)
        {
            // may make strength 0, so the item.Type is sent to potion, but using will neither heal nor hurt. 
            _Code = code;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Returns the code feild to be compared with a Door's code. 
        /// </summary>
        public string Code
        {
            get
            {
                return _Code;
            }
        }
        #endregion

    }
}
