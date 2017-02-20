using System;

namespace MapGenerator
{
    /// <summary>
    /// Generate a LandMass
    /// </summary>
    public class LandMassGenerator
    {
        Random rand = new Random();
        /// <summary>
        /// Generate a continent
        /// </summary>
        /// <param name="x">X start</param>
        /// <param name="y">Y start</param>
        /// <param name="clusters">Number of clusters</param>
        /// <param name="clustersize">distance between cluster center nodes</param>
        /// <param name="clusterpoints">Number of points in each cluster.</param>
        /// <returns>Continent</returns>
        public PointGroup GenerateContinent(int x, int y, int clusters, int clustersize, int clusterpoints)
        {
            PointGroupGenerator pgg = new PointGroupGenerator();
            PointGroup clusternodes = pgg.GenerateCluster(x, y, clusters, MapTerrain.Plains, clustersize);
            PointGroup landmass = new PointGroup();
            foreach(MapPoint mp in clusternodes)
                landmass.AddRange(pgg.GenerateCluster(mp.X, mp.Y, clusterpoints, MapTerrain.Plains, 5));
            return landmass;
        }
        /// <summary>
        /// Generate ocean
        /// </summary>
        /// <param name="sizex">Size of map</param>
        /// <param name="sizey">Size of map</param>
        /// <returns>Ocean PointGroup</returns>
        public PointGroup GenerateOcean(int sizex, int sizey)
        {
            PointGroupGenerator pgg = new PointGroupGenerator();
            return pgg.GenerateBox(0, 0, sizex, sizey,sizex + sizey / 2, MapTerrain.Ocean);
        }
        /// <summary>
        /// Generate the IceCaps
        /// </summary>
        /// <param name="sizex"></param>
        /// <param name="sizey"></param>
        /// <returns></returns>
        public PointGroup GenerateIceCaps(int sizex, int sizey)
        {
            int miny = sizey / 8;
            int maxy = sizey * 7 / 8;
            PointGroupGenerator pgg = new PointGroupGenerator();

            PointGroup caps = new PointGroup();
            caps.AddRange(pgg.GenerateBox(0, 0, sizex, miny, (sizex + sizey) * 2, MapTerrain.Ice));
            caps.AddRange(pgg.GenerateBox(0, maxy, sizex, sizey, (sizex + sizey) * 2, MapTerrain.Ice));

            foreach (MapPoint mp in caps)
                mp.Terrain = IceOrOcean(mp.Y, sizey);
            PointGroup newpointgroup = new PointGroup();
            newpointgroup.AddRangeAlpha(caps, MapTerrain.Ocean);
            return newpointgroup;
        }
        /// <summary>
        /// Determine Ice or Ocean for ice caps.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="sizey"></param>
        /// <returns></returns>
        private MapTerrain IceOrOcean(int y, int sizey)
        {
            int roll = rand.Next(sizey / 8);
            if (y < roll || y > sizey - roll)
                return MapTerrain.Ice;
            return MapTerrain.Ocean;
        }
        /// <summary>
        /// Generate a desert
        /// </summary>
        /// <param name="pg">LandMass desert belongs to.</param>
        /// <param name="points">Points in desert.</param>
        /// <param name="dist">distance between desert points</param>
        /// <returns>Desert PointGroup</returns>
        public PointGroup GenerateDesert(PointGroup pg, int points, int dist = 5)
        {
            MapPoint mp = pg.GetPointFromBounds();
            PointGroupGenerator pgg = new PointGroupGenerator();
            return pgg.GenerateCluster(mp.X, mp.Y, points, MapTerrain.Desert, dist);
        }
        /// <summary>
        /// Generate a swamp
        /// </summary>
        /// <param name="pg">LandMass swamp belongs to.</param>
        /// <param name="size">Size of swamp</param>
        /// <param name="dist">distance between swamp points</param>
        /// <returns>Swamp PointGroup</returns>
        public PointGroup GenerateSwamp(PointGroup pg, int size, int dist = 5)
        {
            MapPoint mp = pg.GetPointFromBounds();
            PointGroupGenerator pgg = new PointGroupGenerator();
            return pgg.GenerateCluster(mp.X, mp.Y, size, MapTerrain.Swamp, dist);
        }
    }
}
