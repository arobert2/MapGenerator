using System;

namespace MapGenerator
{
    class PointGenerator
    {
        Random rand = new Random();
        /// <summary>
        /// Generate a random MapPoint within boundaries.
        /// </summary>
        /// <param name="minx">Minimum X Boundary</param>
        /// <param name="miny">Minmum Y Boundary</param>
        /// <param name="maxx">Maximum X Boundary</param>
        /// <param name="maxy">Maximum Y Boundary</param>
        /// <returns>new point.</returns>
        public MapPoint GeneratePoint(int minx, int miny, int maxx, int maxy, MapTerrain mt)
        {
            int X = rand.Next(minx, maxx);
            int Y = rand.Next(miny, maxy);
            return new MapPoint(X, Y, mt);
        }
        /// <summary>
        /// Generate a point adjacent to another point.
        /// </summary>
        /// <param name="mt">Reference point</param>
        /// <param name="dist">Distance from point</param>
        /// <param name="rad"></param>
        /// <returns>AdjacentPoint</returns>
        public MapPoint GenerateAdjacentPoint(MapPoint mt, int dist, int angl, MapTerrain mter)
        {
            Random rand = new Random();
            double angle = Math.PI * angl / 180;
            double xnew = dist * Math.Cos(angle) - dist * Math.Sin(angle);
            double ynew = dist * Math.Sin(angle) + dist * Math.Cos(angle);
            return new MapPoint((int)xnew + mt.X, (int)ynew + mt.Y, mter);
        }
        
    }
}
