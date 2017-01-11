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
            TrainRouteGraph graph = new TrainRouteGraph();

            Station A = new Station("A");
            Station B = new Station("B");
            Station C = new Station("C");
            Station D = new Station("D");
            Station E = new Station("E");

            //AB5
            graph.AddRoute(A, B, 5);
            // BC4,
            graph.AddRoute(B, C, 4);
            //CD8
            graph.AddRoute(C, D, 8);
            // DC8 
            graph.AddRoute(D, C, 8);
            // DE6 
            graph.AddRoute(D, E, 6);
            // AD5 
            graph.AddRoute(A, D, 5);
            // CE2
            graph.AddRoute(C, E, 2);
            // EB3
            graph.AddRoute(E, B, 3);
            // AE7
            graph.AddRoute(A, E, 7);

            //1.A - B - C.
            List <Station> abc = new List<Station> { A, B, C };
            Assert.Equal(9, DirectRouteDistanceCalculator.Distance(abc, graph));

            //2.A - D
            List<Station> ad = new List<Station> { A, D };
            Assert.Equal(5, DirectRouteDistanceCalculator.Distance(ad, graph));

            //3.A-D-C
            List<Station> adc = new List<Station> { A, D ,C};
            Assert.Equal(13, DirectRouteDistanceCalculator.Distance(adc, graph));

            //4.A-E-B-C-D
            List<Station> aebcd = new List<Station> { A, E, B, C, D };
            Assert.Equal(22, DirectRouteDistanceCalculator.Distance(aebcd, graph));


            //5.A - E - D.
            List<Station> aed = new List<Station> { A, E, D};
            Exception ex = Assert.Throws<InvalidOperationException>(() => DirectRouteDistanceCalculator.Distance(aed, graph));
            Assert.Equal("NO SUCH ROUTE", ex.Message);

            //6.The number of trips starting at C and ending at C with a maximum of 3 stops.
         
            var pathsToC = graph.Paths(C,C);

            //8.The length of the shortest route (in terms of distance to travel) from A to C.
            ShortestPath fromA = new ShortestPath(graph, A);
            Assert.Equal(9, fromA.DistanceTo(C));
            var pathsFromAtoC = graph.Paths(A,C);

            //9.The length of the shortest route (in terms of distance to travel) from B to B.
            ShortestPath fromB = new ShortestPath(graph, B);
            Assert.Equal(9, fromB.DistanceTo(B));

        }
    }
}
