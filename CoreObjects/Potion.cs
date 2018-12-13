// Ross Higley
// CS 1182
// 19 April 2016
// Potion class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

/* Citation: http://stackoverflow.com/questions/3154198/cant-find-system-windows-media-namepspace 
    added PresentationCore to CoreObjects references to be able to import System.Windows.media */

namespace CoreObjects
{
    [Serializable] // can serialize to binary or XML
   public class Potion : Item, IRepeatable<Potion>
    {
        #region private variables
        colors _Color; 
        #endregion

        #region Enums
        /// <summary>
        /// List of available colors a potion can be. 
        /// </summary>
        public enum colors
        {
            LightSeaGreen, Blue, BlueViolet, Maroon
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a Potion object, when heals Actor when used. 
        /// </summary>
        /// <param name="newName">Name to call potion.</param>
        /// <param name="newStrength">How many HP it heals.</param>
        /// <param name="newColor">What color to display. Choose from Potion.colors. </param>
        public Potion (String newName, int newStrength, colors newColor): base (newName, newStrength * -1)
        {
            // newStrength * -1 goes to Item class constructor to set item type to potion. 
            _Color = newColor;
        }
        #endregion

        #region Interface Methods
        /// <summary>
        /// Performs deep copy on potion object.
        /// </summary>
        /// <returns>Returns identical, but separate, potion object. </returns>
        public Potion CreateCopy()
        {
            Potion clone = new Potion(Name, EffectStrength, _Color);
            return clone;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Controls the color of the potion. Returns an Enum, not a System.Media.Color. 
        /// Set to Potion.colors. 
        /// </summary>
        public colors Color
        {
            // Used to be chosen from System.Windows.Media.Colors, but is now an enum. 
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;
            }
        }
        #endregion
    }
}
