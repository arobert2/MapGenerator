using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public enum MapSize { small, medium, large, huge }
    public enum WorldType { Terran, Desert, Volcanic, Swamp, Ice}
    /// <summary>
    /// MapGenerator
    /// </summary>
    public class MapGen
    {
        int SizeX { get; set; }
        int SizeY { get; set; }

        int SmallContinentSize { get; set; }
        int MediumContinentSize { get; set; }
        int LargeContinentSize { get; set; }
        int HugeContinentSize { get; set; }
        int IslandSize { get; set; }

        int SmallContinentPoints { get; set; }
        int MediumContinentPoints { get; set; }
        int LargeContinentPoints { get; set; }
        int HugeContinentPoints { get; set; }
        int IslandPoints { get; set; }

        int SmallContinentClusterPoints { get; set; }
        int MediumContinentClusterPoints { get; set; }
        int LargeContinentClusterPoints { get; set; }
        int HugeContinentClusterPoints { get; set; }

        WorldType WType { get; set; }

        public MapGen()
        { }

        public MapGen(MapSize ms, WorldType wt)
        {
            switch (ms)
            {
                case MapSize.small:
                    SizeX = 74;
                    SizeY = 46;

                    SmallContinentSize = 1;
                    MediumContinentClusterPoints = 2;
                    LargeContinentSize = 3;
                    HugeContinentSize = 4;
                    IslandSize = 2;

                    SmallContinentPoints = 1;
                    MediumContinentPoints = 2;
                    LargeContinentPoints = 3;
                    HugeContinentPoints = 4;

                    SmallContinentClusterPoints = 1;
                    MediumContinentClusterPoints = 2;
                    LargeContinentClusterPoints = 4;
                    HugeContinentClusterPoints = 6;
                    IslandPoints = 1;
                    break;
                case MapSize.medium:
                    SizeX = 84;
                    SizeY = 54;

                    SmallContinentSize = 2;
                    MediumContinentClusterPoints = 3;
                    LargeContinentSize = 4;
                    HugeContinentSize = 5;
                    IslandSize = 2;

                    SmallContinentPoints = 2;
                    MediumContinentPoints = 3;
                    LargeContinentPoints = 4;
                    HugeContinentPoints = 5;

                    SmallContinentClusterPoints = 1;
                    MediumContinentClusterPoints = 2;
                    LargeContinentClusterPoints = 3;
                    HugeContinentClusterPoints = 4;
                    IslandPoints = 2;
                    break;
                case MapSize.large:
                    SizeX = 96;
                    SizeY = 60;

                    SmallContinentSize = 3;
                    MediumContinentClusterPoints = 4;
                    LargeContinentSize = 5;
                    HugeContinentSize = 6;
                    IslandSize = 3;

                    SmallContinentPoints = 3;
                    MediumContinentPoints = 4;
                    LargeContinentPoints = 5;
                    HugeContinentPoints = 6;

                    SmallContinentClusterPoints = 2;
                    MediumContinentClusterPoints = 4;
                    LargeContinentClusterPoints = 6;
                    HugeContinentClusterPoints = 8;
                    IslandPoints = 3;
                    break;
                case MapSize.huge:
                    SizeX = 106;
                    SizeY = 66;

                    SmallContinentSize = 4;
                    MediumContinentClusterPoints = 5;
                    LargeContinentSize = 6;
                    HugeContinentSize = 7;
                    IslandSize = 3;

                    SmallContinentPoints = 4;
                    MediumContinentPoints = 5;
                    LargeContinentPoints = 6;
                    HugeContinentPoints = 7;

                    SmallContinentClusterPoints = 2;
                    MediumContinentClusterPoints = 4;
                    LargeContinentClusterPoints = 6;
                    HugeContinentClusterPoints = 8;
                    IslandPoints = 4;
                    break;
            }

            WType = wt;
        }
        /// <summary>
        /// Generate a new Map
        /// </summary>
        /// <returns>Generated Map</returns>
        public GameMap Generate()
        {
            switch(WType)
            {
                case WorldType.Terran:
                    return GenerateTerran();
                    break;
                default:
                    return GenerateTestTerran(256, 256);
            }
        }

        public GameMap GenerateTerran()
        {
            PointGroup Land = new PointGroup();
            PointGroup Desert = new PointGroup();
            PointGroup Swamp = new PointGroup();
            PointGroup Ocean = new PointGroup();

            LandMassGenerator lmg = new LandMassGenerator();
            MapConsolidator mc = new MapConsolidator();

            //Left Side of Planet
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY / 4, MediumContinentPoints, MediumContinentSize, MediumContinentClusterPoints)); //North America
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY / 2, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Central America
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY * 3/ 4, MediumContinentPoints, MediumContinentSize, MediumContinentClusterPoints)); //South America
            //Right Side of Planet
            Land.AddRange(lmg.GenerateContinent(SizeX * 7 / 8, SizeY / 4, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //Asia
            Land.AddRange(lmg.GenerateContinent(SizeX * 2 / 3, SizeY / 2, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Europe
            Land.AddRange(lmg.GenerateContinent(SizeX * 2 / 3, SizeY * 3 / 4, MediumContinentPoints, MediumContinentSize, MediumContinentClusterPoints)); //Africa
            Land.AddRange(lmg.GenerateContinent(SizeX * 3 / 4, SizeY * 3 / 4, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Oceania
            //Deserts
            Desert.AddRange(lmg.GenerateDesert(Land, IslandPoints));
            Desert.AddRange(lmg.GenerateDesert(Land, IslandPoints));
            Desert.AddRange(lmg.GenerateDesert(Land, IslandPoints));
            //Swamp
            Swamp.AddRange(lmg.GenerateSwamp(Land, IslandPoints));
            Swamp.AddRange(lmg.GenerateSwamp(Land, IslandPoints));
            Swamp.AddRange(lmg.GenerateSwamp(Land, IslandPoints));
            //Ocean
            Ocean.AddRange(lmg.GenerateOcean(SizeX, SizeY));
            //SouthPole
            Land.AddRange(lmg.GenerateContinent(SizeX / 2, SizeY, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Antartica Land
            Land.AddRange(lmg.GenerateIceCaps(SizeX, SizeY));

            Land = mc.ConsolidateGroupPoints(new List<PointGroup>() { Ocean, Land, Desert, Swamp });
            Land = MapWrap(Land, SizeX);
            return ColorMap(Land, SizeX, SizeY);
        }

        /// <summary>
        /// Generate a TestTerrain
        /// </summary>
        /// <param name="x">Size X</param>
        /// <param name="y">Size Y</param>
        /// <returns>Completed GameMap</returns>
        public GameMap GenerateTestTerran(int x, int y)
        {
            LandMassGenerator lmg = new LandMassGenerator();
            PointGroup ocean = lmg.GenerateOcean(x, y);
            PointGroup LandMass = new PointGroup();
            PointGroup IceCaps = new PointGroup();
            PointGroup Deserts = new PointGroup();
            PointGroup Swamp = new PointGroup();
            MapConsolidator mc = new MapConsolidator();
            //***LandMass***
            //right side of planet
            LandMass.AddRange(lmg.GenerateContinent(x / 4, y / 4, 10, 20, 20)); //Canada region
            LandMass.AddRange(lmg.GenerateContinent(x / 2, y / 4, 4, 10, 10)); //Greenland Region
            LandMass.AddRange(lmg.GenerateContinent(x / 4, y / 3, 7, 20, 30)); //North America region
            LandMass.AddRange(lmg.GenerateContinent(x / 4, y / 2, 4, 10, 10)); //Centeral America Region
            LandMass.AddRange(lmg.GenerateContinent(x / 4, y * 3 / 4, 7,20, 20)); //South America region
            //left side of planet
            LandMass.AddRange(lmg.GenerateContinent(x * 3 / 4, y / 4, 10, 20, 30)); //Siberia Region
            LandMass.AddRange(lmg.GenerateContinent(x * 2/3, y / 3, 4,10,10)); //Europe Region
            LandMass.AddRange(lmg.GenerateContinent(x * 2 / 3, y * 2 / 3, 7, 20, 20)); //Africa Region
            LandMass.AddRange(lmg.GenerateContinent(x * 3/4, y * 3/4, 3, 10, 10)); //Australia Region
            LandMass.AddRange(lmg.GenerateContinent(x * 7 / 8, y / 2, 7, 10, 10)); //Asian Region
            //South Pole
            LandMass.AddRange(lmg.GenerateContinent(x / 2, y, 3, 10, 10)); //South pole land
            IceCaps.AddRange(lmg.GenerateIceCaps(x, y)); //Ice Caps
            //***LandMass SubRegions***
            //Deserts
            Deserts.AddRange(lmg.GenerateDesert(LandMass, 20, 10));
            Deserts.AddRange(lmg.GenerateDesert(LandMass, 20, 10));
            //Swamps
            Swamp.AddRange(lmg.GenerateSwamp(LandMass, 20, 10));
            Swamp.AddRange(lmg.GenerateSwamp(LandMass, 20, 10));

            LandMass = MapWrap(LandMass, x);
            LandMass = mc.ConsolidateGroupPoints(new List<PointGroup> { ocean, LandMass, Deserts, Swamp });

            LandMass.AddRange(IceCaps);
            return ColorMap(LandMass, x, y);
            //return LandMass;
        }
        /// <summary>
        /// Return a colored GameMap object.
        /// </summary>
        /// <param name="pg">PointGroup to generate map from</param>
        /// <param name="x">Size of map</param>
        /// <param name="y">Size of map</param>
        /// <returns>new GameMap</returns>
        private GameMap ColorMap(PointGroup pg, int x, int y)
        {
            GameMap gamemap = new GameMap(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    MapPoint closest = pg.GetClosest(new MapPoint(i, j, MapTerrain.NONE));
                    gamemap[i, j] = new MapPoint(i, j, closest.Terrain);
                }
            }
            return gamemap;
        }
        /// <summary>
        /// Wrap MapPoints around world.
        /// </summary>
        /// <param name="pg">PointGroup to wrap.</param>
        /// <param name="sizex">XSize of map</param>
        /// <returns>Wrapped PointGroup</returns>
        private PointGroup MapWrap(PointGroup pg, int sizex)
        {
            foreach (MapPoint mp in pg)
            {
                if (mp.X < 0)
                    mp.X = mp.X + sizex;
                if (mp.X > sizex)
                    mp.X = mp.X - sizex;
            }
            return pg;
        }
    }
    /// <summary>
    /// GameMap
    /// </summary>
    public class GameMap
    {
        /// <summary>
        /// Map grid.
        /// </summary>
        public MapPoint[,] Map { get; set; }
        /// <summary>
        /// Indexer for GameMap object.
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <returns>MapPoint</returns>
        public MapPoint this[int x, int y]
        {
            get { return Map[x, y]; }
            set { Map[x, y] = value; }
        }

        public GameMap(int x, int y)
        {
            Map = new MapPoint[x, y];
        }
    }
}
