using System;

namespace Kiwiland.RouteTracker
{
    public class DirectRoute : IComparable<DirectRoute>
    {
        public Station From { get; }
        public Station To { get; }
        public int Distance { get; }

        public DirectRoute(Station origin,Station destination,int distance)
        {
            this.From = origin;
            this.To = destination;
            this.Distance = distance;
        }

        public override string ToString()
        {
            return From.Name + " " + To.Name + " " + Distance;
        }

        public int Compare(DirectRoute x, DirectRoute y)
        {
            return x.Distance.CompareTo(y.Distance);
        }

        public int CompareTo(DirectRoute other)
        {
           return Distance.CompareTo(other.Distance);
        }
    }
}
