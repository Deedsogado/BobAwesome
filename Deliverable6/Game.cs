// Ross Higley
// CS 1182
// 28 April 2016
// Game class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreObjects;

namespace Deliverable6
{
   static class Game
    {
        #region private variables 
        static Gamestate _GameState;
        static Map _Map;
        private static int _GameBoardHeight = 10;
        private static int _GameBoardWidth = 10;

        public enum Gamestate
        {
            Running, Lost, Won
        }
        #endregion

        #region game logic methods
        /// <summary>
        /// Creates a new map and places a hero at a random, empty position on the map. 
        /// </summary>
        /// <param name="height">how many cells high to make the gameboard.</param>
        /// <param name="width">how many cells wide to make the gameboard. </param>
        public static void resetGame(int height, int width)
        {
            // remember height and width
            _GameBoardHeight = height;
            _GameBoardWidth = width;

            // set GameState to running
            _GameState = Gamestate.Running;

            // create a new map object
            _Map = new Map(width, height);

            // find empty cell (no items or monsters) to put hero in
            Random rand = new Random();
            bool emptyCellFound = false;
            while (!emptyCellFound)
            {
                int x = rand.Next(width);
                int y = rand.Next(height);
                // if cell does not have a monster or item, it's empty. 
                if (!(_Map.GameBoard[y,x].HasItem || _Map.GameBoard[y,x].HasMonster))
                {
                    // create hero in this position. 
                    _Map.Hero = new Hero("Bob", "Awesome", 100, 10, x, y);
                    // mark cell found to end while loop. 
                    emptyCellFound = true; 
                }
            }

        }

        /// <summary>
        /// Creates a new map and places a hero at a random, empty position on the map.
        /// Uses previously set height and width.
        /// </summary>
        public static void resetGame()
        {
            resetGame(_GameBoardHeight, _GameBoardWidth);
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets and sets the state of the game from enum Game.GameState. Will be GameState.Lost if hero ever dies. 
        /// </summary>
        public static Gamestate GameState
        {
            get
            {
                if (_Map.Hero.HP == 0) // if hero is dead, return Lost.
                {
                    return Gamestate.Lost;
                } else // hero is still alive, then the game is still running. 
                {
                    return _GameState;
                } 
            }

            set
            {
                _GameState = value;
            }
        }

        /// <summary>
        /// gets and sets the map field. 
        /// </summary>
        public static Map Map
        {
            get
            {
                return _Map;
            }

            set
            {
                _Map = value;
            }
        }
        #endregion
    }
}
