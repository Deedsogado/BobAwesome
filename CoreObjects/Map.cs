// Ross Higley
// CS 1182
// 19 April 2016
// Map class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media; // remember to include "PresentationCore" in project references

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML
    public class Map
    {
        #region Private variables
        MapCell[,] _GameBoard; // all of the MapCells that make up the gameboard.

        List<Item> _Items; // list of possible unique items (potions, weapons)
        List<Monster> _Monsters; // list of possible unique monsters.
        Hero _Hero; // the hero for our game. 
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Creates new Map, including gameboard and lists of potions, weapons, and monsters. 
        /// </summary>
        /// <param name="columns">How many vertical columns the gameboard will have.</param>
        /// <param name="rows">How many horizontal rows the gameboard will have. </param>
        public Map(int columns, int rows)
        {
            // create grid of mapcells. 
            _GameBoard = new MapCell[rows, columns];

            for (int row = 0; row <= GameBoard.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= GameBoard.GetUpperBound(1); col++)
                {
                    // create new mapcell in gameboard
                    _GameBoard[row, col] = new MapCell();
                }
            }

            fillItems(); // populate list of Items

            fillMonsters(); // populate list of Monsters

            fillMapCells(); // randomly give each mapcell either an item, a monster, or nothing. 

            createDoorAndKey("private key value");
        }
        #endregion


        #region Methods
        /// <summary>
        /// fills list of possible monsters. 
        /// </summary>
        private void fillMonsters()
        {
            // fill collection of monsters
            // Varied the strengths, speeds, and healths of monsters for Deliverable 6. 
            _Monsters = new List<Monster>();
            _Monsters.Add(new Monster("orc", "", 200, 5, 0, 0, 10));
            _Monsters.Add(new Monster("goblin", "", 100, 9, 0, 0, 9));
            _Monsters.Add(new Monster("slug", "", 80, 5, 0, 0, 5));
            _Monsters.Add(new Monster("rat", "", 80, 10, 0, 0, 7));
            _Monsters.Add(new Monster("skeleton", "", 100, 10, 0, 0, 8));
        }

        /// <summary>
        /// fills list of possible Items, including potions and weapons. 
        /// </summary>
        private void fillItems()
        {
            // fill collection of items (weapons and potions only)
            _Items = new List<Item>();
            _Items.Add(new Potion("small healing potion", 25, Potion.colors.LightSeaGreen));
            _Items.Add(new Potion("medium healing potion", 50, Potion.colors.Blue));
            _Items.Add(new Potion("large healing potion", 100, Potion.colors.BlueViolet));
            _Items.Add(new Potion("extreme healing potion", 200, Potion.colors.Maroon));
            _Items.Add(new Weapon("dagger", 10, 1));
            _Items.Add(new Weapon("club", 10, 1));
            _Items.Add(new Weapon("sword", 20, 2));
            _Items.Add(new Weapon("claymore", 30, 3));
        }

        /// <summary>
        /// Fill mapcell objects with random potion, weapon, or monster. 
        /// Not every mapcell gets a potion, weapon, or monster. 
        /// </summary>
        private void fillMapCells()
        {
            Random rand = new Random(); 

            foreach (MapCell mapcell in GameBoard) // for every cell on the map
            {
                int kindOfObject = rand.Next(3);// decide to put Item, monster, or nothing in mapcell. 
                int index = 0;  // will be used to copy an object from a list. 
                switch (kindOfObject)
                {
                    case 0: index = rand.Next(_Items.Count); // get random number representing index of item.
                        if (_Items[index].GetType() == typeof(Potion))
                            mapcell.Item = ((Potion)_Items[index]).CreateCopy(); // deep copy object into mapcell. 
                                                                                 // note: items don't have deep copy method, but potions and weapons do. cast to them. 
                        if (_Items[index].GetType() == typeof(Weapon))
                            mapcell.Item = ((Weapon)_Items[index]).CreateCopy();
                        break;
                    case 1: index = rand.Next(_Monsters.Count); // get random number representing index of monster
                        mapcell.Monster = _Monsters[index].CreateCopy();
                        break;
                    default: break; // kindOfObject is not item or monster. don't fill the mapcell. 
                }
            }
        }

        /// <summary>
        /// Creates a matching door and key, and puts them in empty cells.
        /// </summary>
        /// <param name="passcode">code to be used to match door with key. </param>
        private void createDoorAndKey(string passcode)
        {
            // create matching door and key. 
            Door door = new Door("Door", 0, passcode);
            DoorKey key = new DoorKey("Key", 0, passcode);

            // put door in random empty cell. 
            putItemInRandomEmptyCell(door);

            // put key in random empty cell. 
            putItemInRandomEmptyCell(key);
            
        }

        /// <summary>
        /// Places the item in a random, empty cell (no items, potions, weapons, doors, keys or monsters)
        /// </summary>
        /// <param name="item">item to be placed in empty cell. Can be any child object of item.</param>
        private void putItemInRandomEmptyCell(Item item)
        {
            // put item in mapcell with no items or monsters. 
            bool itemPlaced = false;

            while (!itemPlaced)
            {
                // get random mapcell, and determine if it is empty (no items or monsters)
                Random rand = new Random();
                int xPos = rand.Next(GameBoard.GetUpperBound(0)); // get random x coordinate in gameboard. 
                int yPos = rand.Next(GameBoard.GetUpperBound(1)); // get randome y coordinate in gameboard. 

                MapCell cell = GameBoard[xPos, yPos]; // get cell from grid using random coordinates. 
                if (!(cell.HasItem || cell.HasMonster)) // if cell is empty (no items or monsters)
                {
                    cell.Item = item;  // put item in the cell. 
                    itemPlaced = true; // mark item as placed, so it only appears once. 
                }
            }
            
            // while loop is over. return. 
        }


        /// <summary>
        /// Moves the hero one cell in the specified direction, unless that movement would move him off the gameboard.
        /// </summary>
        /// <param name="direction">The direction the hero will move. Use Actor.Direction or Hero.Direction enums.</param>
        /// <returns>returns true if the hero needs to act (new cell has monster or item). return false if the cell is empty. </returns>
        public bool moveHero(Actor.Direction direction)
        {
            // mark old cell as discovered and visible, before hero leaves it. 
            HeroLocation.IsVisible = true;

            int x = Hero.XPos;
            int y = Hero.YPos; 
            if (direction == Actor.Direction.Up && y - 1 >= 0) // if told to go up, and hero is not at top of gameboard,
            {
              Hero.move(Actor.Direction.Up); // move hero up. 
            } else if (direction == Actor.Direction.Down && y + 1 <= GameBoard.GetUpperBound(0))
            { // if told to go down, and hero is not at bottom of gameboard, move hero down. 
                Hero.move(Actor.Direction.Down); 
            } else if (direction == Actor.Direction.Left && x - 1 >= 0)
            { // if told to go left, and hero is not at left edge of gameboard, move hero left. 
                Hero.move(Actor.Direction.Left);
            } else if (direction == Actor.Direction.Right && x + 1 <= GameBoard.GetUpperBound(1))
            { // if told to go right, and hero is not at right edge of gameboard, move hero right. 
                Hero.move(Actor.Direction.Right);
            }
            //// hero has been moved. mark new cell as discovered and visible. 
            //HeroLocation.IsVisible = true;

            // return true if hero needs to act (new location has monster or item). return false if the cell is empty. 
            return (HeroLocation.HasItem || HeroLocation.HasMonster);
        }

        #endregion

        #region Properties
        // public accessors for Monsters, Potions, and Weapons have been removed and are in the Unused Code Region. 

        /// <summary>
        /// Get 2 dimensional array of MapCells. 
        /// </summary>
        public MapCell[,] GameBoard
        {
            get
            {
                return _GameBoard;
            }
        }

        /// <summary>
        /// Gets and sets hero on map. 
        /// </summary>
        public Hero Hero
        {
            get
            {
                return _Hero;
            }

            set
            {
                _Hero = value;
            }
        }

        /// <summary>
        /// Gets the cell that the hero is in. 
        /// </summary>
        public MapCell HeroLocation
        {
            get
            {
                return GameBoard[Hero.YPos, Hero.XPos];
            }
        }
        
        #endregion

        #region Unused Code
        //List<Potion> _Potions; // list of possible unique potions. 
        //List<Weapon> _Weapons; // list of possible unique weapons. 

        // public map() 

        //// fill collection of potions
        //_Potions = new List<Potion>();
        //_Potions.Add(new Potion("small healing potion", 25, Potion.colors.LightSeaGreen));
        //_Potions.Add(new Potion("medium healing potion", 50, Potion.colors.Blue));
        //_Potions.Add(new Potion("large healing potion", 100, Potion.colors.BlueViolet));
        //_Potions.Add(new Potion("extreme healing potion", 200, Potion.colors.Maroon));


        //// fill collection of weapons
        //_Weapons = new List<Weapon>();
        //_Weapons.Add(new Weapon("dagger", 10, 1));
        //_Weapons.Add(new Weapon("club", 10, 1));
        //_Weapons.Add(new Weapon("sword", 20, 2));
        //_Weapons.Add(new Weapon("claymore", 30, 3));


        ///// <summary>
        ///// Get list of possible unique monsters.
        ///// </summary>
        //public List<Monster> Monsters
        //{
        //    get
        //    {
        //        return _Monsters;
        //    }
        //}


        ///// <summary>
        ///// Get list of possible unique potions.
        ///// </summary>
        //public List<Potion> Potions
        //{
        //    get
        //    {
        //        return _Potions;
        //    }
        //}

        ///// <summary>
        ///// Get list of possible unique weapons. 
        ///// </summary>
        //public List<Weapon> Weapons
        //{
        //    get
        //    {
        //        return _Weapons;
        //    }
        //}

        #endregion
    }
}
