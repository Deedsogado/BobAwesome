// Ross Higley
// CS 1182
// 19 April 2016
// MapCell class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class MapCell
    {
        #region Private Variables
        bool _IsVisible;
        Item item;
        Monster monster;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a mapcell without a monster, or an item. 
        /// </summary>
        public MapCell()
        {
            IsVisible = false;
            
        }



        #endregion

        #region Properties
        
        /// <summary>
        /// Get or set whether the cell has been discovered, and therefore should be visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return _IsVisible;
            }
            set
            {
                _IsVisible = value;
            }
        }

        /// <summary>
        /// Tells whether this cell contains a Monster.
        /// </summary>
        public bool HasMonster
        {
            get
            {
                return (monster != null); // return true if monster exists, false if it doesn't.
            }
        }

        /// <summary>
        /// Tells whether this cell contains an Item. 
        /// </summary>
        public bool HasItem
        {
            get
            {
                return (item != null); // return true if item exists, false if it doesn't.
            }
        }

        /// <summary>
        /// Gets and sets item in this MapCell. Setting item will remove a monster if it exists. 
        /// </summary>
        public Item Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
                monster = null; //Mapcell can have either item or monster. 
            }
        }

        /// <summary>
        /// Gets and sets monster in this MapCell. Setting monster will remove an item if it exists. 
        /// </summary>
        public Monster Monster
        {
            get
            {
                return monster;
            }

            set
            {
                monster = value;
                item = null; // Mapcell can have either item or monster.  
            }
        }

        #endregion

        #region Unused code

        //bool _HasMonster;
        //bool _HasItem;

        ///// <summary>
        ///// Creates a mapcell with either no contents, a monster, or an item. 
        ///// </summary>
        //public MapCell()
        //{

        //    // decide if cell has monster by generating random number. 
        //    Random generator = new Random();
        //    int choice = generator.Next(6); // get number from 0 to 6

        //    if (choice == 1) // if number is 1, the cell has a monster.
        //    {
        //        _HasMonster = true;
        //        _HasItem = false;
        //    }
        //    else if (choice == 2) // if number is 2, the cell has an item. 
        //    {
        //        _HasItem = true;
        //        _HasMonster = false;
        //    }
        //    else  // any other number, the cell is empty. 
        //    {
        //        _HasItem = false;
        //        _HasMonster = false;
        //    }
        //}

        ///// <summary>
        ///// Creates a mapcell with the specified contents. Cannot contain both monster and item. 
        ///// If two trues are passed in, it will default to creating an item. 
        ///// </summary>
        ///// <param name="createMonster">true to create a monster in this cell. </param>
        ///// <param name="createItem">true to create item in this cell. </param>
        //public MapCell(bool createMonster, bool createItem)
        //{
        //    _IsVisible = false; // start out invisible, or undiscovered by player. 

        //    if (createMonster && createItem) // passed in both monster and item. default to only item. 
        //    {
        //        _HasMonster = false;
        //        _HasItem = true;
        //    }
        //    else
        //    {
        //        // either one or neither, but not both, were passed in. Use them.
        //        _HasMonster = createMonster;
        //        _HasItem = createItem;
        //    }

        //}


        #endregion

    }
}
