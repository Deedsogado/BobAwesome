// Ross Higley
// CS 1182
// 19 April 2016
// Actor class for deliverable 6.
// citation: copied from deliverable 5.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreObjects
{
    [Serializable] // can be serialized to binary or XML.
    public abstract class Actor
    {
        #region Private Variables
        int _HP; // amount of health. 
        int _MaxHP; // amount of health he starts with, and is the highest it can ever get. 
        String _Name; // What the actor is called.  
        protected String _Title; // Can be placed after the actors name. Will always start with " the ".  
        // is protected so derived classes can access it. 

        int _AttackSpeed; // determines which actor will attack first. 
        int _XPos; // column coordinate in grid. 
        int _YPos; // row coordinate in grid. 

        int attackStrength = 10; // the default amount of damage Actor does. 

        string[] articles = { "The", "Of", "A", "An", "And", "From", "In", "As" }; // articles to leave lowercase in a name or title.  

        #endregion

        #region Enums

        /// <summary>
        /// Provides standard cardinal directions for movement. 
        /// </summary>
        public enum Direction
        {
            Up, Down, Left, Right
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates Actor object.  
        /// </summary>
        /// <param name="newName">Name to be used for Actor. </param>
        /// <param name="newTitle">Title to be used for actor. words "the" will be added automatically when retrieving full name. </param>
        /// <param name="newHP">Hit Points or Health to start with. Will determine maximum health.</param>
        /// <param name="newAttackSpeed">How quick the Actor will react to threats and monsters.</param>
        /// <param name="newXPos">Starting X coordinate. try to keep within board size.</param>
        /// <param name="newYPos">Starting Y coordinate. try to keep within board size.</param>
        public Actor(string newName, string newTitle, int newHP, int newAttackSpeed, int newXPos, int newYPos)
        {
             _Name = CapitolizeEachWord(newName);// Capitolize each word in the name. 
            _Title = AddPrefixToTitle(WordsToLowerCase(CapitolizeEachWord(newTitle), articles), articles); // Capitolize each word in title, except for articles. add "the " if it doesn't have it already. 

            _HP = newHP;
            _MaxHP = newHP;
            AttackSpeed = newAttackSpeed;
            _XPos = newXPos;
            _YPos = newYPos;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Returns only name of Actor. Read only. Cannot change once Actor is created. 
        /// </summary>
        public String Name
        {
            get
            {
                return _Name;
            }
        }


        /// <summary>
        /// Returns name and title of Actor, like "Bob the Awesome". Read only. Cannot change once actor is created.  
        /// </summary>
        public String FullName
        {
            get
            {
                return _Name + " " + _Title;
            }
        }


        /// <summary>
        /// Returns HP, or Hit Points. Read only. Change by using takeDamage() and takeHeal() methods. 
        /// </summary>
        public int HP
        {
            get
            {
                return _HP;
            }
        }

        /// <summary>
        /// Returns maximum HP for this actor. Read only. Cannot be changed once object is instantiated. 3
        /// Added in deliverable 6. 
        /// </summary>
        public int MaxHP
        {
            get
            {
                return _MaxHP;
            }
        }

        /// <summary>
        /// Get and Set attack speed. Only set between 0-999
        /// Is virtual so Hero can override, but Monster doesn't have to. 
        /// </summary>
        virtual public int AttackSpeed
        {
            get
            {
                return _AttackSpeed;
            }
            set
            {
                _AttackSpeed = value;
            }
        }


        /// <summary>
        /// Returns X, or column, coordinate of Actor. Read only. Can change using move(). 
        /// </summary>
        public int XPos
        {
            get
            {
                return _XPos;
            }
        }

        /// <summary>
        /// returns Y, or column, coordinates of Actor. Read only. Can change using move().
        /// </summary>
        public int YPos
        {
            get
            {
                return _YPos;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applys damage to targeted Monster.
        /// </summary>
        /// <param name="monster">Monster to attack</param>
        public void attack(Actor targetMonster)
        {
            // avoid targetting yourself. Citation: https://msdn.microsoft.com/en-us/library/system.object.referenceequals(v=vs.110).aspx
            if (!Object.ReferenceEquals(this, targetMonster)) // true if monster is not yourself. 
            {
                // monster is not yourself. go ahead and attack it.
                targetMonster.takeDamage(attackStrength); // make targetMonster take damage.
            }

        }


        /// <summary>
        /// Decreases Actor's HP, to a minimum of 0.
        /// </summary>
        /// <param name="amount">how much health Actor will lose. </param>
        public void takeDamage(int amount)
        {
            if (_HP != 0) // if Actor is alive, damage him.
            {
                _HP -= amount; // subtract amount from his HP. 

                if (_HP < 0) // if damage kills him, 
                    _HP = 0; // set health to 0. 
            }
        }


        /// <summary>
        /// Increases Actor's HP, to a maximum of full health. 
        /// </summary>
        /// <param name="amount">how much health Actor will gain. </param>
        public void takeHeal(int amount)
        {
            if (_HP != 0) // if Actor is alive, heal him.
            {
                _HP += amount; // add amount to his HP. 

                if (_HP > _MaxHP) // if heal has exceeded maximum,
                    _HP = _MaxHP; // set back to maximum. 
            }
        }


        /// <summary>
        /// Move Actor 1 space in the chosen direction.
        /// </summary>
        /// <param name="direction">Direction to move.  Use Actor.Direction.Up, Down, Left, or Right.</param>
        virtual public void move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                        _YPos--; // move him up. 
                    break;
                case Direction.Down:
                        _YPos++; // move him down. 
                    break;
                case Direction.Left:
                        _XPos--; // move him left. 
                    break;
                case Direction.Right:
                        _XPos++; // move him right. 
                    break;
            }
        }


        #endregion

        #region String Helping methods

        /// <summary>
        /// Converts a string to all lowercase, then capitalizes the first character of each word. 
        /// </summary>
        /// <param name="original">String to capitalize</param>
        /// <returns>returns string with changes.</returns>
        public static String CapitolizeEachWord(string original)
        {
            String lowercased = original.Trim().ToLower(); // Convert string to lowercase.
            String[] words = lowercased.Split(); // separate string into array of words, so we know where spaces are.
            StringBuilder result = new StringBuilder(); // use Stringbuilder to store result and to increase speed. 

            if (String.IsNullOrEmpty(lowercased)) { // if word is empty, or original passed in empty string, return empty. 
                return "";
            }

            for (int i = 0; i < words.Length; i++) // for each word
            {
                StringBuilder word = new StringBuilder(words[i]); // get the word as a stringbuilder.
                String firstChar = word[0].ToString().ToUpper();  // get the first character and capitalize it. 

                //replace first character from the word with it's Capital (since it still contains the lowercased char)
                word.Remove(0, 1);
                word.Insert(0, firstChar);

                // word is now capitolized. append onto result stringbuilder to return. 
                result.Append(word + " ");
            }

            // all words are capitalized and added to string. Return it. 
            return result.ToString().Trim();
        }


        /// <summary>
        /// Sets specified words in string to lowercase.  
        /// </summary>
        /// <param name="str">string to be edited.</param>
        /// <param name="words">words to set lowercase in str.</param>
        /// <returns></returns>
        public static String WordsToLowerCase(String str, params String[] words)
        {
            // keyword param in method header allows either one or many articles to be passed in. 
            // Citation: https://msdn.microsoft.com/en-us/library/ms228391(v=vs.90).aspx

            // break string into parts
            String[] parts = str.Split();
            StringBuilder result = new StringBuilder("");

            for(int i = 0; i < parts.Length; i++) // for all parts of the string, 
            {

                for(int j = 0; j< words.Length; j++) // for every word chosen to be lowercased, 
                {
                    if (parts[i].Equals(words[j])) // if the word is in the string, 
                    {
                        // set that word in string to lowercase. 
                        parts[i] = parts[i].ToLower();
                    }
                    // if the word is not in string, do nothing. 
                }
                // now that part has been checked against all chosen words, append onto result.
                result.Append(parts[i] + " ");
            }
             // all parts have been appended to result. return it 

            return result.ToString();
        }


        /// <summary>
        /// Attaches prefix "the" to title if it does not have one. 
        /// </summary>
        /// <param name="title">title to edit</param>
        /// <param name="prefixes">prefixes to look for</param>
        /// <returns></returns>
        public static String AddPrefixToTitle(String title, params String[] prefixes)
        {
            bool containsPrefix = false; // assume doesn't have one. try to prove otherwise. 
            foreach (string prefix in prefixes)
            {
                
                if (title.StartsWith(prefix) || title.StartsWith(prefix.ToLower()) ) // check both cases of prefixes. 
                {
                    containsPrefix = true; // if true, we found a prefix in string. 
                }
            }
            
            if (!containsPrefix) // if we did not find a prefix, append " the " to front of title. 
            {
                return "the " + title;
            }

            return title;
        }

        #endregion

        #region Unused Code
        // from 
        //// ignorecase by converting everything to lowercase
        //string strl = str.Trim().ToLower();

        //foreach (String pre in words) // test each words 
        //{
        //    string prefixl = pre.Trim().ToLower(); // convert each words to lowercase. 


        //    if (strl.StartsWith(prefixl)) // if the string starts with this words, 
        //    {
        //        str = str.Remove(0, prefixl.Length + 1); // remove the words, and the space next to it. 
        //    }

        //}
        //return str; // return string with articles removed. 

        
        #endregion
    }
}
