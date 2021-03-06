﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace BMG
{
    public class Tiledata
    {

        [OnDeserialized]
        internal void Prepare(StreamingContext context)
        {
            foreach (var biome in biomes)
            {
                List<TileDefault> update = biome.defaults.ToList();

                foreach (var def in defaultBiome.defaults)
                {
                    bool ready = false;
                    foreach (var setting in biome.defaults)
                        if (def.tile == setting.tile)
                        {
                            ready = true; break;
                        }

                    if (!ready)
                        update.Add(def);
                }

                biome.defaults = update.ToArray();
            }    
        }

        public PresetOptions presetOptions { get; set; }
        public char[] ignoreTiles { get; set; }
        public Tile[] tiles { get; set; }
        public Biome[] biomes { get; set; }
        public Biome defaultBiome { get; set; }
        public Gamemode[] gamemodes { get; set; }

        public class Tile
        {
            public string tileName { get; set; }
            public char tileCode { get; set; }
            public TileType[] tileTypes { get; set; }
            public TileLink tileLinks { get; set; }
        }

        public class TileType
        {
            public TileParts tileParts { get; set; }
            public string color { get; set; }
            public bool detailed { get; set; }
            public bool visible { get; set; }
            public string other { get; set; }
            public string asset { get; set; }
            public int? orderHor { get; set; }
            public int? order { get; set; }
            public bool tileTransitions { get; set; } = false;
        }

        public class TileParts
        {
            public int top { get; set; }
            public int mid { get; set; }
            public int bot { get; set; }
            public int left { get; set; }
            public int right { get; set; }
        }

        public class TileDefault
        {
            public string tile { get; set; }
            public int type { get; set; }
        }

        public class Biome
        {
            public string name { get; set; }
            public string color1 { get; set; }
            public string color2 { get; set; }
            public TileDefault[] defaults { get; set; }
        }

        public Biome GetBiome(int index)
        {
            if (index <= biomes.Length - 1) return biomes[index];
            return defaultBiome;
        }

        public class TileLink
        {
            public TileLinkRule[] rules { get; set; }
            public bool multipleConditionsCouldApply { get; set; }
            public TileLinkDefault defaults { get; set; }
            public EdgeCase edgeCase { get; set; }
            public string assetFolder { get; set; }
        }

        public class TileLinkRule
        {
            public string condition { get; set; }
            public int? requiredBiome { get; set; }
            public string[] changeBinary { get; set; }
            public int? changeTileType { get; set; }
            public string changeAsset { get; set; }
            public string changeFolder { get; set; }
        }

        public class TileLinkDefault
        {
            public int tileType { get; set; }
            public string asset { get; set; }
        }

        public class Gamemode
        {
            public string name { get; set; }
            public SpecialTile[] specialTiles { get; set; }
            public TileDefault[] overrideBiome { get; set; }
            public MapMod[] mapModder { get; set; }
        }

        public class SpecialTile
        {
            public string tile { get; set; }
            public int type { get; set; }
            public string position { get; set; }
            public int drawOrder { get; set; }
        }

        public class MapMod
        {
            public string tile { get; set; }
            public string position { get; set; }
        }

        public enum EdgeCase
        {
            different, copies, mirror
        }

        public class PresetOptions
        {
            public int tileTransitionSize { get; set; }
        }

    }

}
