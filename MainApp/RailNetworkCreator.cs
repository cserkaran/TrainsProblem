using Kiwiland.Models;
using System.Collections.Generic;

namespace MainApp
{
    internal class RailNetworkCreator
    {
        private string[] _routes;
        private Dictionary<string, Town> _towns = new Dictionary<string, Town>();

        public RailNetworkCreator(string[] routes)
        {
            _routes = routes;
        }

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
    }
}
