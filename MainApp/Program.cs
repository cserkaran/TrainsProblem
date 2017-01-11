using Kiwiland.Algorithms;
using Kiwiland.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter input file path:");
            var filePath = Console.ReadLine();

            try
            {
                var input = File.ReadAllText(filePath);

                // assuming a 3 letter comma separated values.
                var routes = input.Split(',');

                RailNetworkCreator creator = new RailNetworkCreator(routes);
                RailNetwork railNetwork = creator.Create();
               
               
                Town A = railNetwork.Towns.Find(item => item.Name == "A");
                Town B = railNetwork.Towns.Find(item => item.Name == "B");
                Town C = railNetwork.Towns.Find(item => item.Name == "C");
                Town D = railNetwork.Towns.Find(item => item.Name == "D");
                Town E = railNetwork.Towns.Find(item => item.Name == "E");

                Console.WriteLine(Environment.NewLine + Environment.NewLine + "=============================================");

                Console.WriteLine(Environment.NewLine + Environment.NewLine);

                // now answer the questions.
                //1.The distance of the route A - B - C.
                List <Town> abc = new List<Town> { A, B, C };
                Console.WriteLine(" 1. The distance of the route A - B - C : " + RouteDistanceCalculator.Distance(abc, railNetwork).ToString());

                //2.The distance of the route A - D.
                List<Town> ad = new List<Town> { A, D };
                Console.WriteLine(" 2. The distance of the route A - D : " + RouteDistanceCalculator.Distance(ad, railNetwork).ToString());

                //3.The distance of the route A - D - C.
                List<Town> adc = new List<Town> { A, D, C };
                Console.WriteLine(" 3. The distance of the route A - D - C : " + RouteDistanceCalculator.Distance(adc, railNetwork).ToString());

                //4.The distance of the route A-E-B-C-D.
                List<Town> aebcd = new List<Town> { A, E, B, C, D };
                Console.WriteLine(" 4. The distance of the route A - E -  B - C - D : " + RouteDistanceCalculator.Distance(aebcd, railNetwork).ToString());

                //5.The distance of the route A-E-D.
                try
                {
                    List<Town> aed = new List<Town> { A, E, D };
                    Console.WriteLine(" 5. The distance of the route A - E - D : " + RouteDistanceCalculator.Distance(aed, railNetwork).ToString());
                }
                catch(Exception e)
                {
                    Console.WriteLine(" 5. The distance of the route A - E - D : " + e.Message);
                }

                //6.The number of trips starting at C and ending at C with a maximum of 3 stops.
                var CtoCTrips = railNetwork.TripCountForMaxNumberOfHops(C, C,3);
                Console.WriteLine(" 6. The number of trips starting at C and ending at C with a maximum of 3 stops : " 
                    + CtoCTrips.Routes.Count + ".The trips are " + CtoCTrips.ToString());

                //7. The number of trips starting at A and ending at C with exactly 4 stops.
                var AtoCTrips = railNetwork.TripCountForExactHops(A, C,4);
                Console.WriteLine(" 7. The number of trips starting at A and ending at C with exactly 4 stops : " 
                    + AtoCTrips.Routes.Count + ".The trips are " + AtoCTrips.ToString());

                //8.The length of the shortest route (in terms of distance to travel) from A to C.
                ShortestPath fromA = new ShortestPath(railNetwork, A);
                Console.WriteLine(" 8. The length of the shortest route (in terms of distance to travel) from A to C : " + fromA.DistanceTo(C));

                //9.The length of the shortest route (in terms of distance to travel) from B to B.
                ShortestPath fromB = new ShortestPath(railNetwork, B);
                Console.WriteLine(" 9. The length of the shortest route (in terms of distance to travel) from B to B : " + fromB.DistanceTo(B));

                //10.The number of different routes from C to C with a distance of less than 30
                var CToCMaxDistanceTrips = railNetwork.TripsCountForMaxDistance(C, C, 30);
                Console.WriteLine(" 10. The number of different routes from C to C with a distance of less than 30 : "
                    + CToCMaxDistanceTrips.Routes.Count + ".The trips are " + CToCMaxDistanceTrips.ToString());

                Console.WriteLine(Environment.NewLine + Environment.NewLine + "=============================================");


            }
            catch(Exception e)
            {
                Console.WriteLine("Provide a valid input file!\n" + e.Message);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
