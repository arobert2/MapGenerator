using System;

namespace MapGenerator
{
    /// <summary>
    /// MapTerrain enum. Determins map texture.
    /// </summary>
    public enum MapTerrain { Ocean, Plains, Desert, Swamp, Ice, NONE }
    /// <summary>
    /// MapElevation enum, Determins map texture.
    /// </summary>
    public enum MapElevation { DeepOcean = -2, ShallowOcean = -1, Ground = 0, Hill = 1, Mountain = 2 }
    /// <summary>
    /// MapPoint object.
    /// </summary>
    public class MapPoint
    {
        /// <summary>
        /// X Location
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y Location
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Terrain Texture
        /// </summary>
        public MapTerrain Terrain { get; set; }
        /// <summary>
        /// Elevation Texture
        /// </summary>
        public MapElevation Elevation { get; set; } = MapElevation.Ground;

        public MapPoint()
        {

        }

        public MapPoint(int x, int y, MapTerrain mt)
        {
            X = x; Y = y; Terrain = mt;
        }
        /// <summary>
        /// Get the distance from another point.
        /// </summary>
        /// <param name="mp1"></param>
        /// <param name="mp2"></param>
        /// <returns></returns>
        public double GetDistance(MapPoint mp2)
        {
            return Math.Sqrt(Math.Pow(mp2.X - X, 2) + Math.Pow(mp2.Y - Y, 2));
        }
    }
}
