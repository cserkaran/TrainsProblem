using Kiwiland.Models;
using System;
using System.Collections.Generic;

namespace Kiwiland.Algorithms
{
    public class RouteDistanceCalculator
    {
        /// <summary>
        /// Total Distance between the list of specified towns starting from first town the in 
        /// the list to last town if exists, else throws exception.
        /// </summary>
        /// <param name="towns">The towns.</param>
        /// <param name="railNetwork">The rail network.</param>
        /// <returns>total distance</returns>
        /// <exception cref="System.InvalidOperationException">NO SUCH ROUTE</exception>
        public static int Distance(IList<Town> towns,RailNetwork railNetwork)
        {
            var distance = 0;
            
            for(int i = 0; i < towns.Count - 1; i++)
            {
                var town = towns[i];
                var nextTown = towns[i + 1];

                var route = railNetwork.Routes(town).RouteToDestination(nextTown);
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
