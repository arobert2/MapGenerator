using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    /// <summary>
    /// Consolidate MapPoints
    /// </summary>
    class MapConsolidator
    {
        /// <summary>
        /// Consolidate MapPoints, destroyes overlap.
        /// </summary>
        /// <param name="lpg">List of PointGroups to consolidate.</param>
        /// <returns>Consolidated MapPoints.</returns>
        public PointGroup ConsolidateGroupPoints(List<PointGroup> lpg)
        {
            PointGroup ConsolidatedPoints = new PointGroup();
            PointGroup removegroup = new PointGroup();

            foreach (PointGroup pg in lpg)
            {
                for (int i = 0; i < ConsolidatedPoints.Count; i++)
                    if (pg.InBounds(ConsolidatedPoints[i], 5))
                        removegroup.Add(ConsolidatedPoints[i]);
                foreach (MapPoint mp in removegroup)
                    ConsolidatedPoints.Remove(mp);
                removegroup.Clear();
                ConsolidatedPoints.AddRange(pg);
            }
            return ConsolidatedPoints;
        }
    }
}
