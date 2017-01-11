using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiwiland.RouteTracker
{
    /// <summary>
    /// Dijkstra's shortest path algorithm implementation.
    /// </summary>
    public class ShortestPath
    {
        private Station _from;
        private TrainRouteGraph _graph;
        MinPQ<DirectRoute> pq = new MinPQ<DirectRoute>();
        private Dictionary<Station, int> _distTo = new Dictionary<Station, int>();
        private Dictionary<Station, DirectRoute> _edgeTo = new Dictionary<Station, DirectRoute>();
        private HashSet<DirectRoute> _allRoutes = new HashSet<DirectRoute>();

        public ShortestPath(TrainRouteGraph graph, Station from)
        {
            _graph = graph;
            _from = from;
            _distTo.Add(_from, 0);
            _edgeTo.Add(_from, null);
            foreach (var station in graph.Stations)
            {
                if (station != from)
                {
                    _distTo.Add(station, int.MaxValue);
                    _edgeTo.Add(station, null);
                }
            }

            DirectRoute r = new DirectRoute(from, from, 0);
            _allRoutes.Add(r);
            pq.Insert(r);
            while (!pq.IsEmpty())
            {
                DirectRoute v = pq.DelMin();
                var adjacentStationRoutes = graph.StationRoutes(v.To);
                foreach(var route in adjacentStationRoutes)
                {
                    Relax(route);
                    _allRoutes.Add(route);
                }

            }
        }

        private void Relax(DirectRoute route)
        {
            var from = route.From;
            var to = route.To;

            // if we are back to starting from node.. i.e. there is a cycle in the graph, loop back
            // in the cycle to calculate shortest 
            if (_distTo[to] > _distTo[from] + route.Distance ||(to == _from && _distTo[to] == 0))
            {
                _distTo[to] = _distTo[from] + route.Distance;
                var o = _edgeTo[to];
                _edgeTo[to] = route;
                if (pq.Contains(o))
                {
                    pq.ChangeKey(o, _edgeTo[to]);
                }
                else
                {
                    pq.Insert(_edgeTo[to]);
                }
            }

            
        }

        public int DistanceTo(Station to)
        {
            return _distTo[to]; 
        }

       
        public bool HasRouteTo(Station to)
        {
            return _edgeTo[to] != null;
        }
    }
}
