using System.Collections;
using System.Collections.Generic;

namespace Kiwiland.RouteTracker
{
    public class StationRoutes  : IEnumerable<DirectRoute>
    {
        private Station _origin;
        private LinkedList<DirectRoute> _routes = new LinkedList<DirectRoute>();

        public StationRoutes(Station station)
        {
            _origin = station;
        }

        public void Add(DirectRoute route)
        {
            _routes.AddLast(route);
        }

        public IEnumerator<DirectRoute> GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        public DirectRoute DirectRouteToDestination(Station destination)
        {
            foreach(var route in _routes)
            {
                if (route.To.Equals(destination))
                {
                    return route;
                }
            }
            return null;
        }

    }
}
