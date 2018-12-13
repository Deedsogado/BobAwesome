// Ross Higley
// CS 1182
// 19 April 2016
// Hero class for deliverable 5
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class Hero : Actor
    {
        #region Private Variables
        Weapon _EquippedWeapon;
        bool _IsRunningAway;
        DoorKey _EquippedKey;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates Hero Object. 
        /// </summary>
        /// <param name="newName">Name to be used for Hero. </param>
        /// <param name="newTitle">Title to be used for Hero. prefix "the" will be added automatically when retrieving full name. </param>
        /// <param name="newHP">Hit Points or Health to start with. Will determine maximum health.</param>
        /// <param name="newAttackSpeed">How quick the Hero will react to threats and monsters. Will be halved when hero equips an item. </param>
        /// <param name="newXPos">Starting X coordinate. Try to keep within board size.</param>
        /// <param name="newYPos">Starting Y coordinate. Try to keep within board size.</param>
        public Hero(string newName, string newTitle, int newHP,
            int newAttackSpeed, int newXPos, int newYPos)
            : base(newName, newTitle, newHP,
                 newAttackSpeed, newXPos, newYPos)
        {
            // All values have been passed to base constructor. Add any required changes here. 
        }
        #endregion

        #region Overridden  properties 

        /// <summary>
        /// Get attack speed of actor, minus attack speed of weapon, if equipped.  
        /// </summary>
        override public int AttackSpeed
        {
            get
            {
                if (HasWeapon)
                {
                    return (base.AttackSpeed - _EquippedWeapon.AttackSpeed);
                }
                else
                {
                    return base.AttackSpeed;
                }
            }

        }

        #endregion

        #region Overridden Methods
        /// <summary>
        /// Move the Hero. Behaves like actor for now, but will be changed later. 
        /// </summary>
        /// <param name="direction"></param>
        public override void move(Direction direction)
        {
            base.move(direction); // use Actor's move method. 

            // more uses will be coded here. 

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets whether or not the hero has a weapon.
        /// </summary>
        public bool HasWeapon
        {
            get
            {
                return (_EquippedWeapon != null); // if object exists, return true. else, return false. 
            }
        }

        /// <summary>
        /// gets and sets the weapon equipped to this actor. can set to null to remove weapon.
        /// </summary>
        public Weapon EquippedWeapon
        {
            get
            {
                return _EquippedWeapon;
            }

            set
            {
                _EquippedWeapon = value;
            }
        }

        /// <summary>
        /// gets and sets whether the hero is running away from a monster. 
        /// </summary>
        public bool IsRunningAway
        {
            get
            {
                return _IsRunningAway;
            }

            set
            {
                _IsRunningAway = value;
            }
        }

        /// <summary>
        /// Gets the DoorKey the hero is holding. 
        /// </summary>
        public DoorKey EquippedKey
        {
            get
            {
                return _EquippedKey;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets attack strength of Hero. If he has a weapon, he uses the weapon's strength. If unarmed, he attacks with 1. 
        /// </summary>
        /// <returns>returns 1 if hero is unarmed. returns the weapons attack strength if equipped.</returns>
        public int getAttackValue()
        {
            if (HasWeapon)
            {
                return EquippedWeapon.EffectStrength;
            }
            else
            {
                return 1;
            }
        }

        #endregion

        #region Overloaded methods

        /// <summary>
        /// Consumes potions, equips weapons, equips keys. 
        /// </summary>
        /// <param name="item">potion, weapon, or doorkey to be used or equipped.</param>
        /// <returns>returns previously equipped weapon or doorkey. returns null if no prior items, or when using potions.</returns>
        public Item applyItem(Item item)
        {
            // if potion, heal the hero. 
            if (item.GetType() == typeof(Potion))
            {
                item.Use(this); // heals if potion. 
                return null;
            }
            // if weapon, equip the new weapon and return the previously equipped weapon.
            else if (item.GetType() == typeof(Weapon))
            { // if no weapon was previously equipped, should return null automatically. 
                Weapon previous = EquippedWeapon;
                EquippedWeapon = (Weapon)item;
                return previous;
            }
            // if doorkey, equip the new doorkey and return the previously equipped doorkey
            else if (item.GetType() == typeof(DoorKey))
            {
                DoorKey previous = EquippedKey;
                _EquippedKey = (DoorKey)item;
                return previous;
            }
            // if any other type, just return it. 
            else
            {
                return item;
            }
        }

        /// <summary>
        /// Hero and monster attack each other. exact order depends on attack speeds. 
        /// </summary>
        /// <param name="hero">hero to attack monster.</param>
        /// <param name="monster">monster to attack hero.</param>
        /// <returns>Returns true if hero survives, false if hero dies.</returns>
        public static bool operator + (Hero hero, Monster monster)
        {
            if (!hero.IsRunningAway) // if hero is not running away, he will fight. 
            {
                // if hero's attack speed is greater than monster's, hero attacks first.
                if (hero.AttackSpeed > monster.AttackSpeed)
                {
                    monster.takeDamage(hero.getAttackValue());
                    // if monster is still alive, it will fight back.
                    if (monster.HP > 0)
                        hero.takeDamage(monster.AttackValue);
                }
                // if monster's attack speed is greater than hero's, monster attacks first. 
                else if (hero.AttackSpeed < monster.AttackSpeed)
                {
                    hero.takeDamage(monster.AttackValue);
                    // if hero is still alive, fight back. 
                    if (hero.HP > 0)
                        monster.takeDamage(hero.getAttackValue());
                }
                // if attack speeds are identical, both apply damage at the same time. 
                else
                {
                    hero.takeDamage(monster.AttackValue);
                    monster.takeDamage(hero.getAttackValue());
                }
            }
            else // if hero is running away, he cannot attack. 
            {
                // if hero's attack speed is greater than monster, he takes no damage, and applies no damage. 
                // if hero's attack speed is lesser than or equal to monster's, he gets hit by monster. 
                if (hero.AttackSpeed <= monster.AttackSpeed)
                    hero.takeDamage(monster.AttackValue);
            }
            // return true if hero is still alive after combat. return false if hero is dead. 
            return (hero.HP > 0);
        }


        #endregion

        #region Unused code


        ///// <summary>
        ///// Keeps the item on the actor for future use.  
        ///// </summary>
        ///// <param name="weapon">Weapon to keep.</param>
        //public void equip(Item item)
        //{
        //    //equipped = item;
        //    hasWeapon = true;
        //}

        //bool _HasWeapon; // tracks whether the hero is holding a weapon.  


        //#region Private Methods

        ///// <summary>
        ///// Equips a weapon to Hero. Hero's attack speed will be deducted by weapon's attack speed. 
        ///// </summary>
        //public void equip()
        //{
        //    _HasWeapon = true;
        //}

        //#endregion

        #endregion

    }
}
