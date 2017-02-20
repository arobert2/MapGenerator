using System;

namespace MapGenerator
{
    public class PointGroupGenerator
    {
        Random rand = new Random();
        /// <summary>
        /// Generate random points within a box
        /// </summary>
        /// <param name="minx">Minimum X Bound</param>
        /// <param name="miny">Minimum Y Bound</param>
        /// <param name="maxx">Maximum X Bound</param>
        /// <param name="maxy">Maximum Y Bound</param>
        /// <param name="points">Number of points to draw.</param>
        /// <returns>generated MapPointGroup</returns>
        public PointGroup GenerateBox(int minx, int miny, int maxx, int maxy, int points, MapTerrain mt)
        {
            PointGenerator pg = new PointGenerator();
            PointGroup mpc = new PointGroup();
            for (int i = 0; i < points; i++)
                mpc.Add(pg.GeneratePoint(minx, miny, maxx, maxy, mt));
            return mpc;
        }
        /// <summary>
        /// Generates a chain that deviates slightly by a supplied angle
        /// </summary>
        /// <param name="x1">X Start</param>
        /// <param name="y1">Y Start</param>
        /// <param name="points">Number of points to draw.</param>
        /// <param name="dist">Maximum distance of point.</param>
        /// <param name="startangle">Angle to start in</param>
        /// <param name="anglebound">+- angle to deviate from.</param>
        /// <returns>generated PointGroup</returns>
        public PointGroup GenerateChain(int x1, int y1, int points, int dist, int startangle, int anglebound, MapTerrain mt )
        {
            PointGenerator pg = new PointGenerator();
            PointGroup mtc = new PointGroup();
            mtc.Add(new MapPoint(x1, y1, mt));
            for (int i = 1; i < points; i++)
                mtc.Add(pg.GenerateAdjacentPoint((MapPoint)mtc[mtc.Count - 1], rand.Next(1, dist), rand.Next(-anglebound, anglebound),mt));
            return mtc;
        }
        /// <summary>
        /// Generate a PointGroup with points adjacent to another PointGroup.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="distance"></param>
        /// <param name="angle"></param>
        /// <param name="startbox"></param>
        /// <returns>Adjacent point collection.</returns>
        public PointGroup GenerateAdjacentBox(int size, int distance, int angle, PointGroup startbox, MapTerrain mt)
        {
            PointGenerator pg = new PointGenerator();
            double radangle = angle * Math.PI / 180;
            MapPoint mp = pg.GenerateAdjacentPoint(startbox.GetAverage(), distance, angle, mt);
            return GenerateBox(mp.X - size / 2, mp.Y - size / 2, mp.X + size / 2, mp.Y + size / 2, startbox.Count, mt);
        }
        /// <summary>
        /// Generate a cluster of points that are adjacent to one another.
        /// </summary>
        /// <param name="x">Start X</param>
        /// <param name="y">Start Y</param>
        /// <param name="points">Number of points in cluster</param>
        /// <param name="mt">MapTerrain to color point.</param>
        /// <returns>new PointGroup</returns>
        public PointGroup GenerateCluster(int x, int y, int points, MapTerrain mt, int dist)
        {
            PointGroup newpg = new PointGroup();
            PointGenerator pg = new PointGenerator();
            for(int i = 0;i < points;i++)
            {
                if (i == 0)
                    newpg.Add(new MapPoint(x, y, mt));
                else
                    newpg.Add(pg.GenerateAdjacentPoint(newpg[rand.Next(newpg.Count - 1)], dist, rand.Next(360), mt));        
            }
            return newpg;
        }
    }
}
