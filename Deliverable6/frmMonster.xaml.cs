// Ross Higley
// CS 1182
// 28 April 2016
// Monster window class for deliverable 6

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
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window
    {
        #region constructor
        /// <summary>
        /// Creates new window that shows monster's name. 
        /// Remember to call showDialog(). 
        /// </summary>
        /// <param name="monsterClicked">The monster that was clicked on, whose name will appear in the window.</param>
        public frmMonster(Monster monsterClicked)
        {
            InitializeComponent();
            Title = "A Monster! ";
            tbHeroName.Text = Game.Map.Hero.Name; // set hero's name in this window. 
            tbMonsterName.Text = monsterClicked.Name; // set monster's name in this window. 

            // set runaway to false so hero can attack again.
            Game.Map.Hero.IsRunningAway = false;
            showHP(monsterClicked);

        }
        #endregion

        #region events and methods
        /// <summary>
        /// Shows HP for hero and monster on the screen. 
        /// </summary>
        /// <param name="monsterClicked"></param>
        private void showHP(Monster monsterClicked)
        {
            tbHeroHP.Text = Game.Map.Hero.HP + "/" + Game.Map.Hero.MaxHP; // show hero's health. 
            tbMonsterHP.Text = monsterClicked.HP + "/" + monsterClicked.MaxHP; // show monster's health. 

            // also show hero's health in main window. 
            // citation: http://stackoverflow.com/questions/19647375/wpf-how-do-i-get-the-mainwindow-instance
            MainWindow main = (MainWindow) Application.Current.MainWindow; 
            main.tbHeroHP.Text = Game.Map.Hero.HP + "/" + Game.Map.Hero.MaxHP;
        }

        /// <summary>
        /// btnAttack click event. Causes hero and monster to attack each other. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            // let hero attack monster once. 
            Hero hero = Game.Map.Hero;
            Monster monster = Game.Map.HeroLocation.Monster;
           bool heroSurvives = hero + monster;

            if (!heroSurvives) // if hero dies, game is lost. 
            {
                Game.GameState = Game.Gamestate.Lost;
                this.Close(); // close this window.
            }
            if(monster.HP == 0) // if monster dies, remove monster.
            {
                Game.Map.HeroLocation.Monster = null;
                this.Close(); // close this window. 
            }
            // neither hero nor monster died, because this window is still open. 
            // keep window open and update the HP for hero and monster. 
            showHP(monster);

        }

        /// <summary>
        /// btnRunAway Click event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunAway_Click(object sender, RoutedEventArgs e)
        {
            Hero hero = Game.Map.Hero;// get hero and monster for convenience. 
            Monster monster = Game.Map.HeroLocation.Monster;

            hero.IsRunningAway = true; // set runaway property to true. Hero is fleeing. 
            bool heroSurvives = hero + monster; // monster attacks hero. 
            showHP(monster); // 
            if(!heroSurvives) // hero died? lose game and close this window.
            {
                Game.GameState = Game.Gamestate.Lost;
                
            }
            this.Close(); // close this window whether hero lives or not. 
        }
        #endregion

    }
}
