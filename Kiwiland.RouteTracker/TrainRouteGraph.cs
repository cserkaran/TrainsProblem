using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiwiland.RouteTracker
{
    public class TrainRouteGraph
    {
        Dictionary<Station, StationRoutes> routes = new Dictionary<Station, StationRoutes>();
        HashSet<Station> stations = new HashSet<Station>();
        public TrainRouteGraph()
        {

        }

        public IEnumerable<Station> Stations
        {
            get
            {
                return stations;
            }
        }

        public void AddRoute(Station origin,Station destination,int distance)
        {
            if(origin == destination)
            {
                throw new InvalidOperationException("starting and ending town cannot be the same town!");
            }

            DirectRoute route = new DirectRoute(origin, destination, distance);

            if (routes.ContainsKey(origin))
            {
                routes[origin].Add(route);
            }
            else
            {
                StationRoutes list = new StationRoutes(origin);
                list.Add(route);
                routes.Add(origin, list);
            }

            if (!routes.ContainsKey(destination))
            {
                StationRoutes list = new StationRoutes(destination);
                routes.Add(destination, list);
            }

            stations.Add(origin);
            stations.Add(destination);
        }

        public StationRoutes StationRoutes(Station station)
        {
            if (routes.ContainsKey(station))
            {
                return routes[station];
            }
            else
            {
                return null;
            }
        }

        public IReadOnlyCollection<IReadOnlyCollection<DirectRoute>> Paths(Station from,Station to)
        {
            var paths = new List<List<DirectRoute>>();
            var routes = StationRoutes(from);

            HashSet<DirectRoute> traversed = new HashSet<DirectRoute>();

            foreach (var route in routes)
            {
                Paths(route, paths, to, traversed);
                traversed.Clear();
            }

            return paths;
        }

        public void Paths(DirectRoute r, List<List<DirectRoute>> paths, Station to, HashSet<DirectRoute> traversed)
        {
            if (!traversed.Contains(r))
            {
                traversed.Add(r);

                if (r.To == to)
                {
                    paths.Add(traversed.ToList());
                }

                var routes = StationRoutes(r.To);
                foreach (var route in routes)
                {
                    Paths(route, paths, to, traversed);
                }
            }
        }
    }
}
