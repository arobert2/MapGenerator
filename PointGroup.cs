using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class PointGroup : List<MapPoint>
    {
        Random rand = new Random();
        /// <summary>
        /// Check to see if a point is within the bounds of this MapPointCollection
        /// </summary>
        /// <param name="mp">Point to check boundaries.</param>
        /// <returns>bool, is it in bounds?</returns>
        public bool InBounds(MapPoint mp, int bound)
        {
            foreach (MapPoint m in this)
            {
                if (m.GetDistance(mp) < bound)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Get a point of the average of all points in the PointCollection
        /// </summary>
        /// <returns>Average Point</returns>
        public MapPoint GetAverage()
        {
            int xtotal = 0, ytotal = 0;
            foreach (MapPoint ip in this)
            {
                xtotal += ip.X;
                ytotal += ip.Y;
            }
            return new MapPoint(xtotal / Count, ytotal / Count, MapTerrain.NONE);
        }
        /// <summary>
        /// Get the closest point
        /// </summary>
        /// <param name="mp">MapPoint</param>
        /// <returns>Closest MapPoint</returns>
        public MapPoint GetClosest(MapPoint mp)
        {
            double dist = 999999999;
            MapPoint closest = new MapPoint(-1,-1,MapTerrain.NONE);
            foreach (MapPoint m in this)
                if (mp.GetDistance(m) < dist)
                {
                    dist = mp.GetDistance(m);
                    closest = m;
                }
            return closest;
        }
        /// <summary>
        /// Add a range of MapPoints but exclude points of a certain terrain.
        /// </summary>
        /// <param name="pg">PointGroup to add</param>
        /// <param name="mt">MapTerrain to ignore.</param>
        public void AddRangeAlpha(PointGroup pg, MapTerrain mt)
        {
            foreach (MapPoint mp in pg)
                if (mp.Terrain != mt)
                    this.Add(mp);
        }

        /// <summary>
        /// Return a point in the object.
        /// </summary>
        /// <returns>Rand MapPoint from group.</returns>
        public MapPoint GetPointFromBounds()
        {
            return this[rand.Next(Count - 1)];
        }
    }
}
