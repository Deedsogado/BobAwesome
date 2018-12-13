// Ross Higley
// CS 1182
// 19 April 2016
// Door class for deliverable 6
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class Door:Item
    {
        private string _Code;

        #region Constructors

        /// <summary>
        /// Creates a new Door object. Make sure to create a DoorKey for this door. 
        /// </summary>
        /// <param name="name">Name of door.</param>
        /// <param name="strength">Item effect strength. Can be set to 0.</param>
        /// <param name="code">String passcode to match with doorkey.</param>
        public Door(string name, int strength, string code):base(name, strength)
        {
            // may make strength 0, so the item.Type is sent to potion, but using will neither heal nor hurt. 
            _Code = code;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Tells whether a key works to unlock this door, based on the code field. 
        /// </summary>
        /// <param name="key">the Key to compare against.</param>
        /// <returns>returns true if the codes match, false if they don't.</returns>
        public bool compareKey(DoorKey key)
        {
            // return true if the key and door have the same code. 
            // return false if the codes are different. 
            return (this._Code == key.Code);
        }

        #endregion
    }
}
