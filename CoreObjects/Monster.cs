// Ross Higley
// CS 1182
// 19 April 2016
// Monster class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class Monster:Actor, IRepeatable<Monster>
    {
        #region Private Variables
        int _AttackValue;

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a Monster object
        /// </summary>
        /// <param name="newName">Name to be used for Hero. </param>
        /// <param name="newTitle">Title to be used for Hero. prefix "the" will be added automatically when retrieving full name. </param>
        /// <param name="newHP">Hit Points or Health to start with. Will determine maximum health.</param>
        /// <param name="newAttackSpeed">How quick the Hero will react to threats and monsters. Will be halved when hero equips an item. </param>
        /// <param name="newXPos">Starting X coordinate. Try to keep within board size.</param>
        /// <param name="newYPos">Starting Y coordinate. Try to keep within board size.</param>
        /// <param name="newAttackValue">How many points of damage the monster can inflict on a hero.</param>
        public Monster(string newName, string newTitle, int newHP,
            int newAttackSpeed, int newXPos, int newYPos, 
            int newAttackValue)
            : base(newName, newTitle, newHP,
                 newAttackSpeed, newXPos, newYPos)
        {
            // All values passed to base constructor except for AttackDamage. save it. 
            _AttackValue = newAttackValue;
        }

        #endregion

        #region Interface Methods
        /// <summary>
        /// Performs deep copy to return identical, yet separate, monster.
        /// </summary>
        /// <returns>returns identical, yet separate, monster. </returns>
        public Monster CreateCopy()
        {
            Monster clone = new Monster(Name, _Title, HP, AttackSpeed, XPos, YPos, _AttackValue );
            
            return clone;
        }

        #endregion

        #region public Properties
        public int AttackValue
        {
            get
            {
                return _AttackValue;
            }
        }
        #endregion


    }
}
