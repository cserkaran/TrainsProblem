using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiwiland.Models
{
    /// <summary>
    /// Represents the trips from one town to another town.
    /// Each trip is nothing but a collection of routes.
    /// </summary>
    public class Trips
    {
        #region Properties

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        public IReadOnlyCollection<IReadOnlyCollection<Route>> Routes { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Trips"/> class.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="count">The count.</param>
        public Trips(IReadOnlyCollection<IReadOnlyCollection<Route>> route)
        {
            Routes = route;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int count = 0;
            foreach (var routeCollection in Routes)
            {
                int i;
                Route route;
                for (i = 0; i < routeCollection.Count - 1; i++)
                {
                    route = routeCollection.ToList()[i];
                    sb.Append(route.From.Name + " - ");
                }

                route = routeCollection.ToList()[i];
                sb.Append(route.From.Name + " - " + route.To.Name);

                count++;

                if (count < Routes.Count)
                {
                    sb.Append(" , ");
                }
                else
                {
                    sb.Append(".");
                }
            }

            return sb.ToString();
        }


    }
}
