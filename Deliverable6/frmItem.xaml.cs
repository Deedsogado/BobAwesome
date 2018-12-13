// Ross Higley
// CS 1182
// 28 April 2016
// Item Window class for deliverable 6

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
    /// Interaction logic for frmItem.xaml
    /// </summary>
    public partial class frmItem : Window
    {
        #region Constructor
        /// <summary>
        /// Creates new window that shows item's name. 
        /// Remember to call showDialog(). 
        /// </summary>
        /// <param name="itemClicked">The item that was clicked on, whose name will appear in the window.</param>
        public frmItem(Item itemClicked)
        {
            InitializeComponent();
            Title = "Yay, an item! ";
            tbItemName.Text = itemClicked.Name; // set textblock to item's name. 
            tbItemStrength.Text = "Strength: " + itemClicked.EffectStrength;
            
            if (itemClicked.GetType() == typeof(Weapon)) // if item is weapon, show it's stats. 
            {
                Weapon weaponClicked = (Weapon)itemClicked;
                tbItemPenalty.Text = "Attack Speed Penalty: " + weaponClicked.AttackSpeed;
                
            }
            else // item is not weapon. remove penalty field. 
            {
                stkItemDetails.Children.Remove(tbItemPenalty);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Equips item in cell to hero and drops the previously equipped item into the cell. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItemYes_Click(object sender, RoutedEventArgs e)
        {
            MapCell cell = Game.Map.HeroLocation; // get current cell. 
            Hero hero = Game.Map.Hero; // get hero for convenience.

            // apply cell's item to hero. remember that this consumes potions, 
            // equips weapons and keys, and returns previously equipped weapons or keys. 
            cell.Item = hero.applyItem(cell.Item);  // drop whatever is returned into the cell. 

            this.Close(); // close the Item window. 
        }

        /// <summary>
        /// Event for clicking the No button. Does not equip or remove anything. Closes Item window. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItemNo_Click(object sender, RoutedEventArgs e)
        {
            // don't make any changes. just close the window. 
            this.Close();
        }
        #endregion

    }
}
