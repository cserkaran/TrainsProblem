namespace Kiwiland.RouteTracker
{
    public class Station
    {
        public string Name { get; }

        public Station(string name)
        {
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var otherStation = obj as Station;

            if (otherStation == null) return false;
            if (otherStation.Name.Equals(Name, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.ToLowerInvariant().GetHashCode(); ;
        }

        public override string ToString()
        {
            return "Station " + Name;
        }
    }
}
