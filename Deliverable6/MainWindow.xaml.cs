// Ross Higley
// CS 1182
// 28 April 2016
// Main Window for deliverable 6, A.K.A frmMain.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreObjects;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.IO;

namespace Deliverable6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private variables 
        int _BoardHeight = 10;
        int _BoardWidth = 10;
        String _AllowedFileFormats = "Game Map object|*.map"; // used in SaveFileDialog and OpenFileDialog.
        #endregion

        #region constructor
        /// <summary>
        /// Creates new Main window. 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Game.resetGame(_BoardHeight, _BoardWidth);
            drawMap(Game.Map); // draw map on screen. 
            displayHeroDetails(Game.Map); // draw hero's name, HP, weapon, and key on screen. 
        }
        #endregion

        #region Map Drawing Methods

        /// <summary>
        /// Populates grid with controls to show Items, Monsters, Doors, Keys, and Hero. Discovered empty cells marked with X. 
        /// </summary>
        public void drawMap(Map map)
        {
            createGridDefinitions(map); // create the same number of rows and columns in grdMap as in the map object. 

            // empty grid of old textblocks
            grdMap.Children.Clear();

            // create textblock for each mapcell to show items, monsters, doors, keys, or empty cells.  
            for (int row = 0; row <= map.GameBoard.GetUpperBound(0); row++) // for each row in gameboard
            {
                for (int col = 0; col <= map.GameBoard.GetUpperBound(1); col++) // for each column in gameboard
                {
                    TextBlock textblock = new TextBlock();
                    MapCell cell = map.GameBoard[row, col];
                    if (cell.IsVisible)
                    {
                        if (cell.HasItem) // if cell has an item, display the item's name.
                        {
                            displayItem(textblock, cell);
                        }
                        else if (cell.HasMonster) // if cell has a monster, display the monster's name. 
                        {
                            displayMonster(textblock, cell);
                        } 
                    }
                    
                    else  // if cell is not visible, color it black. 
                    {
                       displayItemBlack(textblock, cell);
                    }

                    // add textblock to grid, with specified coordinates. 
                    grdMap.Children.Add(textblock);
                    Grid.SetRow(textblock, row);
                    Grid.SetColumn(textblock, col);
                }
            }
            displayHero(map); // show hero in his cell. 
        }

        /// <summary>
        /// Displays colored textblock in the grid to represent a monster.
        /// </summary>
        /// <param name="textblock">Textblock which will appear in grid, to apply formatting to.</param>
        /// <param name="cell">cell to retrieve monster from. </param>
        private static void displayMonster(TextBlock textblock, MapCell cell)
        {
            textblock.Text = cell.Monster.Name;
            textblock.Background = new SolidColorBrush(Colors.Red);
            textblock.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Displays colored textblock in the grid, colored based on type of item. 
        /// </summary>
        /// <param name="textblock">Textblock to color and fill text based on item.</param>
        /// <param name="cell">cell to retrieve item from.</param>
        private static void displayItem(TextBlock textblock, MapCell cell)
        {
            textblock.Text = cell.Item.Name;
            if (cell.Item.GetType() == typeof(Potion))
            {
                Potion potion = (Potion)cell.Item;
                if (potion.Color == Potion.colors.Blue)
                    textblock.Background = new SolidColorBrush(Colors.Blue);

                else if (potion.Color == Potion.colors.BlueViolet)
                    textblock.Background = new SolidColorBrush(Colors.BlueViolet);

                else if (potion.Color == Potion.colors.LightSeaGreen)
                    textblock.Background = new SolidColorBrush(Colors.LightSeaGreen);

                else if (potion.Color == Potion.colors.Maroon)
                    textblock.Background = new SolidColorBrush(Colors.Maroon);
            }
            else if (cell.Item.GetType() == typeof(Door)) {
                textblock.Background = new SolidColorBrush(Colors.SandyBrown);
            } 
            else if (cell.Item.GetType() == typeof(DoorKey)) {
                textblock.Background = new SolidColorBrush(Colors.Yellow);
            }
            else if (cell.Item.GetType() == typeof(Weapon))
            {
                textblock.Background = new SolidColorBrush(Colors.DarkGray);
            }
        }

 
        /// <summary>
        /// Sets background of the chosen cell to black. 
        /// </summary>
        /// <param name="textblock">The textblock in the grid corresponding to this cell</param>
        /// <param name="cell">The cell to be colored black. </param>
        private static void displayItemBlack(TextBlock textblock, MapCell cell)
        {
            textblock.Background = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Shows hero in grdMap
        /// </summary>
        /// <param name="map">map to pull hero from.</param>
        private void displayHero(Map map)
        {
            // show hero in Grid.
            TextBlock HeroTextBlock = new TextBlock();
            HeroTextBlock.Text = map.Hero.FullName;
            HeroTextBlock.Background = new SolidColorBrush(Colors.DarkBlue);
            HeroTextBlock.Foreground = new SolidColorBrush(Colors.White);

            grdMap.Children.Add(HeroTextBlock);
            Grid.SetRow(HeroTextBlock, map.Hero.YPos);
            Grid.SetColumn(HeroTextBlock, map.Hero.XPos);
        }

        /// <summary>
        /// Shows hero's name, health, weapon, and key on screen. 
        /// </summary>
        /// <param name="map"></param>
        private void displayHeroDetails(Map map)
        {
            Hero hero = map.Hero;// get Hero from map
            tbHeroName.Text = hero.FullName; // put hero's name and title on screen. 
            tbHeroHP.Text = hero.HP + "/" + hero.MaxHP;
            if (hero.HasWeapon) // if hero has weapon
                tbHeroWeapon.Text = hero.EquippedWeapon.Name; // use weapon's name on screen. 
            else
                tbHeroWeapon.Text = "None"; // hero is not holding weapon, say "None"
            if (hero.EquippedKey != null) // if hero has key, 
                tbHeroKey.Text = hero.EquippedKey.Name; // use key's name on screen. 
            else
                tbHeroKey.Text = "None"; // hero is not holding key, say "None"
        }

        /// <summary>
        /// Creates same number of rows and columns in window's grid as in map.GameBoard.
        /// </summary>
        /// <param name="map"></param>
        private void createGridDefinitions(Map map)
        {
            // reset grid to zero columns and rows.
            grdMap.RowDefinitions.Clear();
            grdMap.ColumnDefinitions.Clear();

            for (int row = 0; row <= map.GameBoard.GetUpperBound(0); row++) // for each row in gameboard
            {
                grdMap.RowDefinitions.Add(new RowDefinition()); // create row definition. 
            }
            for (int col = 0; col <= map.GameBoard.GetUpperBound(1); col++) // for each column in gameboard
            {
                grdMap.ColumnDefinitions.Add(new ColumnDefinition()); // create column definition.    
            }
        }
        #endregion

        #region Button Click and Key Press Events
        /// <summary>
        /// Click event for btnLeft. Moves hero left one cell and redraws grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            bool needsToAct = Game.Map.moveHero(Actor.Direction.Left);
            commonMovementMethods(needsToAct); // call methods common to all movement methods. 
        }
        

        /// <summary>
        /// Click event for btnUp. Moves hero up one cell and redraws grid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            bool needsToAct = Game.Map.moveHero(Actor.Direction.Up);
            commonMovementMethods(needsToAct); // call methods common to all movement methods. 
        }

        /// <summary>
        /// Click event for btnDown. Moves hero down one cell and redraws grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            bool needsToAct = Game.Map.moveHero(Actor.Direction.Down);
            commonMovementMethods(needsToAct); // call methods common to all movement methods. 
        }

        /// <summary>
        /// Click event for btnRight. Moves hero right one cell and redraws grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            bool needsToAct = Game.Map.moveHero(Actor.Direction.Right);
            commonMovementMethods(needsToAct); // call methods common to all movement methods. 
        }

        /// <summary>
        /// updates map, displays forms, handles game state, and display's hero's details.
        /// </summary>
        /// <param name="needsToAct"></param>
        private void commonMovementMethods(bool needsToAct)
        {
            // methods common to each movement event are kept here to reduce redundant code. 
            // any changes here will affect all movement methods. 

            drawMap(Game.Map); // update map in window's grid. 
            displayForm(needsToAct); // if hero's cell contains an item or monster, display frmItem or frmMonster.
            handleGameState(); //handle game status. 
            displayHeroDetails(Game.Map); //update hero's details.
        }

        /// <summary>
        /// Click event for btnRefreshMap. Refills the map with a new game. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshMap_Click(object sender, RoutedEventArgs e)
        {
            Game.resetGame(_BoardHeight, _BoardWidth);
            drawMap(Game.Map);
        }

        /// <summary>
        /// KeyPress event for window. Listens for Up, down, left, and right arrow keys, and fires corresponding btn_Click events. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                btnLeft_Click(null,null);

            if (e.Key == Key.Up)
                btnUp_Click(null, null);

            if (e.Key == Key.Right)
                btnRight_Click(null, null);

            if (e.Key == Key.Down)
                btnDown_Click(null, null);
        }
        #endregion

        #region game logic methods
        /// <summary>
        /// Displays frmItem if hero's location contains an item, or frmMonster if hero's location contains a monster.
        /// </summary>
        /// <param name="needsToAct">Whether or not hero needs to act in this cell. Get from Game.Map.moveHero().</param>
        private static void displayForm(bool needsToAct)
        {
            if (needsToAct) // if hero's cell contains an item or monster, display frmItem or frmMonster.
            {
                if (Game.Map.HeroLocation.HasItem) // if the cell contains an item, disply frmItem. 
                {
                   
                    if (Game.Map.HeroLocation.Item.GetType() == typeof(Door)) // but, if the item is a door, display the Game Over screen.
                    {
                        frmGameOver frmGameOver = new frmGameOver();
                        frmGameOver.ShowDialog();
                    }
                    else { // cell has item, which is not a door. display frmItem. 
                    frmItem frmItem = new frmItem(Game.Map.HeroLocation.Item);
                    frmItem.ShowDialog();

                    }

                }
                else // cell doesn't have item, but does need attention.  display frmMonster.
                {
                    // open new monster form window.
                    frmMonster frmMonster = new frmMonster(Game.Map.HeroLocation.Monster);
                    frmMonster.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Displays game over window if game is lost. 
        /// </summary>
        private static void handleGameState()
        {
            if (Game.GameState == Game.Gamestate.Lost) // if the current gamestate is lost, display the frmGameOver window. 
            {
                frmGameOver frmGameOver = new frmGameOver();
                frmGameOver.ShowDialog();
            }
        }

        #endregion

        #region Menu button events
        /// <summary>
        /// Saves Game.Map as binary file using serialization. Uses .map extension. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miSaveGame_Click(object sender, RoutedEventArgs e)
        {
            // citation: borrowed the series of steps from Jon's CaptainCrunch in class demonstration.
            SaveFileDialog saveLocation = new SaveFileDialog();
            saveLocation.Filter = _AllowedFileFormats; // allow user to save as specified types (.map only). 
            bool? chosen = saveLocation.ShowDialog(); // returns true if they choose a file, false if they did not. 

            if(chosen == true) // if user chose a file, Serialize the object into that file. 
            {
                String fileName = saveLocation.FileName;
                BinaryFormatter binFormat = new BinaryFormatter(); // will convert objects to binary files. 
                FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate); // opens file on hard drive.
                binFormat.Serialize(fileStream, Game.Map); // writes object into the file on hard drive.
                fileStream.Close();  // closes the file on hard drive. 
                
            } // user did not chose file. do nothing. 

        }

        /// <summary>
        /// Loads Game.map object from .map binary file using deserialization. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miLoadGame_Click(object sender, RoutedEventArgs e)
        {
            // citation: borrowed the series of steps from Jon's CaptainCrunch in class demonstration.
            OpenFileDialog openLocation = new OpenFileDialog();
            openLocation.Filter = _AllowedFileFormats; // allows user to open specified types (.map only).
            if (openLocation.ShowDialog() == true) // if user chose a file to open, open it.
            {
                String fileName = openLocation.FileName; // get name of file they chose.
                BinaryFormatter binForm = new BinaryFormatter(); // will convert binary files to objects.
                FileStream fileStream = new FileStream(fileName, FileMode.Open); // open file on hard drive. 
                Game.Map = (Map)binForm.Deserialize(fileStream); // read from file on hard drive, save to Game.Map. 
                fileStream.Close(); // close the file on the hard drive. 
                drawMap(Game.Map); // show on the GUI! 


            } // else, user did not choose a file to open. do nothing. 
        }
        #endregion
    }
}
