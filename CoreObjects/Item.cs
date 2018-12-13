// Ross Higley
// CS 1182
// 19 April 2016
// Item class for deliverable 6
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be seralized to binary or XML
    public abstract class Item
    {
        #region Private Variables
        String _Name;
        int _EffectStrength;
        Type _EffectType;
        #endregion

        #region Enums
        public enum Type
        {
            Weapon, Potion
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Create an Item. All properties can be changed at any time. 
        /// </summary>
        /// <param name="name">What to call the item, chosen from list of ItemTypes. Can be customized later.</param>
        /// <param name="strength">How effective the item is. Positive means it heals. Negative means it hurts. </param>
        public Item(String name, int strength)
        {
            Name = name; // remember what kind of item this is. 
            EffectStrength = strength;
            
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Consumes the item if potion, use to attack if weapon.
        /// </summary>
        /// <param name="target">The actor to attack or heal. Be careful not to attack yourself or heal your enemy. </param>
        public void Use(Actor target)
        {
           switch (_EffectType)
            {
                case Type.Weapon:
                    target.takeDamage(_EffectStrength);
                    break;
                case Type.Potion:
                    target.takeHeal(_EffectStrength);
                    break;
            }

        }
        #endregion

        #region Properties

        /// <summary>
        /// Controls the Name of the item. Can be anything you want. 
        /// </summary>
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = Actor.CapitolizeEachWord(value);
            }
        }

        /// <summary>
        /// Controls the Strength or effectiveness of the item. 
        /// Negative Values heal and become potions.
        /// Positive values hurt and become weapons.
        /// </summary>
        public int EffectStrength
        {
            get
            {
                return _EffectStrength;
            }
            set
            {
                if (value <= 0) // if negative, it's a potion. .
                {
                    _EffectStrength = value * -1; // save positive value.
                    _EffectType = Type.Potion; // set to healing/potion item. 
                }
                else // if positive, it's a weapon. 
                {
                    _EffectStrength = value; // save positive value. 
                    _EffectType = Type.Weapon; // set to hurting/weapon item. 
                }
            }
        }

        /// <summary>
        /// Returns either weapon or potion. 
        /// </summary>
        public Type EffectType
        {
            get
            {
                return _EffectType;
            }
        }

        #endregion

    }
}
