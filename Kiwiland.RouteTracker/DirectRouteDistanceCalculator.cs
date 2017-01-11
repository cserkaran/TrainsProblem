using System;
using System.Collections.Generic;

namespace Kiwiland.RouteTracker
{
    public class DirectRouteDistanceCalculator
    {
        public static int Distance(List<Station> nodes,TrainRouteGraph graph)
        {
            var distance = 0;
            
            for(int i = 0; i < nodes.Count - 1; i++)
            {
                var node = nodes[i];
                var nextNode = nodes[i + 1];

                var route = graph.StationRoutes(node).DirectRouteToDestination(nextNode);
                if(route != null)
                {
                    distance += route.Distance;
                }
                else
                {
                    throw new InvalidOperationException("NO SUCH ROUTE");
                }
            }

            return distance;
        }
    }
}
