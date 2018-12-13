// Ross Higley
// CS 1182
// 28 April 2016
// Game Over Window class for deliverable 6

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
using System.Windows.Shapes;
using CoreObjects;

namespace Deliverable6
{
    /// <summary>
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window
    {
        #region Constructor
        /// <summary>
        /// Creates new Game over window and displays it on the screen. 
        /// </summary>
        public frmGameOver()
        {
            InitializeComponent();

            if (Game.Map.HeroLocation.HasItem &&
                Game.Map.HeroLocation.Item.GetType() == typeof(Door)) // if hero's cell has a door
            {
                Door door = (Door)Game.Map.HeroLocation.Item; // save the door. 
                if (Game.Map.Hero.EquippedKey != null &&       // if hero has a key 
                    door.compareKey(Game.Map.Hero.EquippedKey)) // and door and key match, change gamestate to won. 
                {
                    Game.GameState = Game.Gamestate.Won;
                }
                else // there is no doorkey, or key doesn't match door. tell user the door cannot be opened. 
                {
                    tbMessage.Text = "You do not have a doorKey, or the Key doesn't match the door. \nThis door cannot be opened.";
                }
            }
            checkGameState();
        }
        #endregion

        #region methods
        /// <summary>
        /// Controls the contents of the Game over window, depending on the gamestate. 
        /// </summary>
        private void checkGameState()
        {
            if (Game.GameState == Game.Gamestate.Won)
            {
                tbMessage.Text = "Congratulations!\nYou Have won the Game!"; // inform user they have won the game. 
                // make reset and exit buttons available
                stkGameOverButtons.Children.Clear();
                stkGameOverButtons.Children.Add(btnRestart);
                stkGameOverButtons.Children.Add(btnExit);

            } else if (Game.GameState == Game.Gamestate.Lost)
            {
                tbMessage.Text = "I am so sorry, you lost the game."; // inform user they have lost the game. 
                // make reset and exit buttons available
                stkGameOverButtons.Children.Clear();
                stkGameOverButtons.Children.Add(btnRestart);
                stkGameOverButtons.Children.Add(btnExit);
            } else
            {
                // game is still running. Make the OK button available. 
                stkGameOverButtons.Children.Clear();
                stkGameOverButtons.Children.Add(btnOkay);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Resets the game. Generates new random map, hero, items, etc...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            Game.resetGame(); // generate new randome map, hero, items, etc...
            this.Close(); // closes the game over window. 

            // redraw main window before hero tries to move. 
            // citation: http://stackoverflow.com/questions/19647375/wpf-how-do-i-get-the-mainwindow-instance
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.drawMap(Game.Map); 
        }

        /// <summary>
        /// Closes entire application. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // citation: taken from CaptainCrunch in class demonstration. 
            Application.Current.Shutdown(); // kill the entire application. 
        }

        /// <summary>
        /// closes current window and game continues.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
        #endregion
    }
}
