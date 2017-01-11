using Kiwiland.Algorithms;
using Kiwiland.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Kiwiland.RouteTracker.Tests
{
    public class RouteTrackerTests
    {
        [Fact]
        public void CreateTrainRouteTests()
        {
            RailNetwork railNetwork = new RailNetwork();

            Town A = new Town("A");
            Town B = new Town("B");
            Town C = new Town("C");
            Town D = new Town("D");
            Town E = new Town("E");

            //AB5
            railNetwork.AddRoute(A, B, 5);
            // BC4,
            railNetwork.AddRoute(B, C, 4);
            //CD8
            railNetwork.AddRoute(C, D, 8);
            // DC8 
            railNetwork.AddRoute(D, C, 8);
            // DE6 
            railNetwork.AddRoute(D, E, 6);
            // AD5 
            railNetwork.AddRoute(A, D, 5);
            // CE2
            railNetwork.AddRoute(C, E, 2);
            // EB3
            railNetwork.AddRoute(E, B, 3);
            // AE7
            railNetwork.AddRoute(A, E, 7);

            //1.A - B - C.
            List <Town> abc = new List<Town> { A, B, C };
            Assert.Equal(9, RouteDistanceCalculator.Distance(abc, railNetwork));

            //2.A - D
            List<Town> ad = new List<Town> { A, D };
            Assert.Equal(5, RouteDistanceCalculator.Distance(ad, railNetwork));

            //3.A-D-C
            List<Town> adc = new List<Town> { A, D ,C};
            Assert.Equal(13, RouteDistanceCalculator.Distance(adc, railNetwork));

            //4.A-E-B-C-D
            List<Town> aebcd = new List<Town> { A, E, B, C, D };
            Assert.Equal(22, RouteDistanceCalculator.Distance(aebcd, railNetwork));


            //5.A - E - D.
            List<Town> aed = new List<Town> { A, E, D};
            Exception ex = Assert.Throws<InvalidOperationException>(() => RouteDistanceCalculator.Distance(aed, railNetwork));
            Assert.Equal("NO SUCH ROUTE", ex.Message);

            //6.The number of trips starting at C and ending at C with a maximum of 3 stops.
         
            var pathsToC = railNetwork.Trips(C,C);

            //8.The length of the shortest route (in terms of distance to travel) from A to C.
            ShortestPath fromA = new ShortestPath(railNetwork, A);
            Assert.Equal(9, fromA.DistanceTo(C));

            var pathsFromAtoC = railNetwork.Trips(A,C);

            //9.The length of the shortest route (in terms of distance to travel) from B to B.
            ShortestPath fromB = new ShortestPath(railNetwork, B);
            Assert.Equal(9, fromB.DistanceTo(B));

        }
    }
}
