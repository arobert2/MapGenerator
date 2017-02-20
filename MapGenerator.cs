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

                    SmallContinentSize = 5;
                    MediumContinentClusterPoints = 7;
                    LargeContinentSize = 10;
                    HugeContinentSize = 15;

                    SmallContinentPoints = 3;
                    MediumContinentPoints = 5;
                    LargeContinentPoints = 7;
                    HugeContinentSize = 10;

                    SmallContinentClusterPoints = 4;
                    MediumContinentClusterPoints = 6;
                    LargeContinentClusterPoints = 8;
                    HugeContinentClusterPoints = 10;
                    break;
                case MapSize.medium:
                    SizeX = 84;
                    SizeY = 54;

                    SmallContinentSize = 7;
                    MediumContinentClusterPoints = 10;
                    LargeContinentSize = 13;
                    HugeContinentSize = 17;

                    SmallContinentPoints = 5;
                    MediumContinentPoints = 7;
                    LargeContinentPoints = 10;
                    HugeContinentSize = 13;

                    SmallContinentClusterPoints = 6;
                    MediumContinentClusterPoints = 8;
                    LargeContinentClusterPoints = 10;
                    HugeContinentClusterPoints = 12;
                    break;
                case MapSize.large:
                    SizeX = 96;
                    SizeY = 60;

                    SmallContinentSize = 10;
                    MediumContinentClusterPoints = 12;
                    LargeContinentSize = 15;
                    HugeContinentSize = 20;

                    SmallContinentPoints = 7;
                    MediumContinentPoints = 10;
                    LargeContinentPoints = 12;
                    HugeContinentSize = 15;

                    SmallContinentClusterPoints = 8;
                    MediumContinentClusterPoints = 10;
                    LargeContinentClusterPoints = 12;
                    HugeContinentClusterPoints = 14;
                    break;
                case MapSize.huge:
                    SizeX = 106;
                    SizeY = 66;

                    SmallContinentSize = 13;
                    MediumContinentClusterPoints = 15;
                    LargeContinentSize = 20;
                    HugeContinentSize = 23;

                    SmallContinentPoints = 10;
                    MediumContinentPoints = 12;
                    LargeContinentPoints = 15;
                    HugeContinentSize = 18;

                    SmallContinentClusterPoints = 10;
                    MediumContinentClusterPoints = 12;
                    LargeContinentClusterPoints = 14;
                    HugeContinentClusterPoints = 16;
                    break;
            }

            WType = wt;
        }

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
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY / 8, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //Canada
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY / 4, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //North America
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY / 2, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Central America
            Land.AddRange(lmg.GenerateContinent(SizeX / 4, SizeY * 3/ 4, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //South America
            //Right Side of Planet
            Land.AddRange(lmg.GenerateContinent(SizeX * 7 / 8, SizeY / 4, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //Asia
            Land.AddRange(lmg.GenerateContinent(SizeX * 3 / 4, SizeY / 3, MediumContinentPoints, MediumContinentSize, MediumContinentClusterPoints)); //Eastern Europe
            Land.AddRange(lmg.GenerateContinent(SizeX * 2 / 3, SizeY / 2, MediumContinentPoints, MediumContinentSize, MediumContinentClusterPoints)); //Europe
            Land.AddRange(lmg.GenerateContinent(SizeX * 2 / 3, SizeY * 3 / 4, HugeContinentPoints, HugeContinentSize, HugeContinentClusterPoints)); //Africa
            Land.AddRange(lmg.GenerateContinent(SizeX * 3 / 4, SizeY * 3 / 4, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Oceania
            //SouthPole
            Land.AddRange(lmg.GenerateContinent(SizeX / 2, SizeY, SmallContinentPoints, SmallContinentSize, SmallContinentClusterPoints)); //Antartica Land
            Land.AddRange(lmg.GenerateIceCaps(SizeX, SizeY));
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

            Land = mc.ConsolidateGroupPoints(new List<PointGroup>() { Ocean, Land, Desert, Swamp });
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
