// Ross Higley
// CS 1182
// 19 April 2016
// Weapon class for deliverable 6
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class Weapon: Item, IRepeatable<Weapon>
    {
        private int _AttackSpeed;

        #region Constructor
        /// <summary>
        /// Constructor. Creates a new weapon, which lowers attack speed of hero. 
        /// </summary>
        /// <param name="name">name of Weapon for example: sword, club, dagger</param>
        /// <param name="strength">Attack strength of weapon. How much damage this weapon inflicts. Be sure it's positive. </param>
        /// <param name="attackSpeed">Attack speed to be removed from actor's attack speed. </param>
        public Weapon(string name, int strength, int attackSpeed): base (name, strength)
        {
            _AttackSpeed = attackSpeed;   

        }

        #endregion

        #region Interface Methods

        /// <summary>
        /// Performs deep copy on weapon object to return new weapon.
        /// </summary>
        /// <returns>returns identical, yet separate, weapon object.</returns>
        public Weapon CreateCopy()
        {
            Weapon weapon = new Weapon(Name, EffectStrength, AttackSpeed);
            return weapon;
        }

        #endregion

        #region Properties

        /// <summary>
        /// gets attackSpeed, which is deducted from hero's attack speed. 
        /// </summary>
        public int AttackSpeed
        {
            get
            {
                return _AttackSpeed;
            }
        }

        #endregion

    }
}
