using Kiwiland.Models;
using System.Collections.Generic;

namespace MainApp
{
    /// <summary>
    /// Recieves the parsed input from file and constructs the rail network.
    /// </summary>
    internal class RailNetworkCreator
    {
        #region Fields

        /// <summary>
        /// The routes array.
        /// </summary>
        private string[] _routes;

        /// <summary>
        /// created towns mapped to their name.
        /// </summary>
        private Dictionary<string, Town> _towns = new Dictionary<string, Town>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RailNetworkCreator"/> class.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public RailNetworkCreator(string[] routes)
        {
            _routes = routes;
        }

        #endregion

        #region Create the rail network.

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public RailNetwork Create()
        {
            RailNetwork railNetwork = new RailNetwork();

            // parse thru input and construct the rail network.
            foreach (var route in _routes)
            {
                var r = route.Trim();
                var fromTownName = r[0].ToString();
                var toTownName = r[1].ToString();
                var routeDistance = int.Parse(r[2].ToString());

                Town from = GetTown(fromTownName);
                Town to = GetTown(toTownName);

                railNetwork.AddRoute(from, to, routeDistance);
            }

            return railNetwork;
        }

        #endregion

        #region Get the town.

        /// <summary>
        /// Gets the town.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private Town GetTown(string name)
        {
            if (_towns.ContainsKey(name))
            {
                return _towns[name];
            }

            Town town = new Town(name);
            _towns.Add(name,town);
            return town;

        }

        #endregion
    }
}
