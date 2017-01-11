namespace Kiwiland.Models
{
    /// <summary>
    /// The Town node which represents a named town.
    /// </summary>
    public class Town 
    {
        #region Fields

        /// <summary>
        /// Gets the name of the town.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Town"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Town(string name)
        {
            Name = name;
        }

        #endregion

        #region Equal's Override 

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            // two town's are same if they have the same name.
            var otherStation = obj as Town;

            if (otherStation == null) return false;
            if (otherStation.Name.Equals(Name, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode(); ;
        }

        #endregion

        #region ToString Override for easy debugging

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Town " + Name;
        }

        #endregion
    }
}
