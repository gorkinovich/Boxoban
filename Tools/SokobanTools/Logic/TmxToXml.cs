//******************************************************************************************
// Boxoban: A retro pixel-art puzzle 2D game made with Unity 4.5
// Copyright (C) 2015  Gorka Suárez García
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//******************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Sokoban;

namespace SokobanTools.Logic {
    /// <summary>
    /// This static class represents the TMX to XML conversor.
    /// </summary>
    public static class TmxToXml {
        //--------------------------------------------------------------------------------------
        // Methods:
        //--------------------------------------------------------------------------------------

        /// <summary>
        /// Converts a TMX file to an XML level file.
        /// </summary>
        /// <param name="inputFileName">The input file name.</param>
        /// <param name="outputFileName">The output file name.</param>
        public static void Convert(string inputFileName, string outputFileName) {
            Shared.ShowTitle(inputFileName, outputFileName);

            // Get the basic data of the level:
            var level = new LevelXml();
            var map = XmlFile.LoadXml<Tiled.Map>(inputFileName);
            level.Name = findProperty(map, "Name", Shared.DEFAULT_NAME);
            level.Description = findProperty(map, "Description", Shared.DEFAULT_DESCRIPTION);
            level.Tileset = findTileset(map);

            Console.WriteLine("Basic level data converted...");

            // Get the terrain data of the level:
            var layers = map.Items.Select(x => x as Tiled.Layer)
                                  .Where(x => x != null).ToArray();
            var terrainLayer = findTerrainLayer(layers);
            level.World = new WorldXml();
            level.World.Width = terrainLayer.Width;
            level.World.Height = terrainLayer.Height;
            level.World.Content = "\n";

            int currentProgress = 0;
            int totalProgress = level.World.Width * level.World.Height;
            Console.Write("\rTerrain information into game level format... [" +
                          (currentProgress * 100 / totalProgress) + "%]");

            for (int y = 0, k = 0; y < level.World.Height; y++) {
                for (int x = 0; x < level.World.Width; x++, k++) {
                    var id = terrainLayer.Data.Tiles[k].Gid;
                    if (id > 0) {
                        id--;
                    }
                    level.World.Content += id.ToString("0000");
                    level.World.Content += " ";

                    Console.Write("\rTerrain information into game level format... [" +
                                  (currentProgress * 100 / totalProgress) + "%]");
                }
                level.World.Content += "\n";
            }

            Console.WriteLine("\rTerrain information into game level format... [100%]");

            // Get the entities data of the level:
            var entitiesLayer = findEntitiesLayer(layers);
            var entities = new List<EntityXml>();

            currentProgress = 0;
            Console.Write("\rEntities information into game level format... [" +
                          (currentProgress * 100 / totalProgress) + "%]");
            for (int y = 0, k = 0; y < entitiesLayer.Height; y++) {
                for (int x = 0; x < entitiesLayer.Width; x++, k++) {
                    var id = entitiesLayer.Data.Tiles[k].Gid;
                    if (id > 0) {
                        id--;
                        string type = "";
                        if (Shared.TERRAIN_ID_FIRST_BOX <= id && id <= Shared.TERRAIN_ID_LAST_BOX) {
                            var boxId = id - Shared.TERRAIN_ID_FIRST_BOX;
                            type = EntityType.BOX;
                            if (boxId > 0) {
                                type += boxId;
                            }
                        } else if (Shared.TERRAIN_ID_FIRST_DBOX <= id && id <= Shared.TERRAIN_ID_LAST_DBOX) {
                            var boxId = id - Shared.TERRAIN_ID_FIRST_DBOX;
                            type = EntityType.BOX;
                            if (boxId > 0) {
                                type += boxId;
                            }
                        } else if (Shared.TERRAIN_ID_LAST_DBOX < id) {
                            type = EntityType.PLAYER;
                        }
                        if (!string.IsNullOrEmpty(type)) {
                            var victim = new EntityXml();
                            victim.Type = type;
                            victim.X = x; victim.Y = y;
                            entities.Add(victim);
                        }
                    }

                    Console.Write("\rEntities information into game level format... [" +
                                  (currentProgress * 100 / totalProgress) + "%]");
                }
            }
            level.Entities = entities.ToArray();

            Console.WriteLine("\rEntities information into game level format... [100%]");

            // Save the level data in a file:
            XmlFile.Save(outputFileName, level);
            Console.WriteLine(outputFileName + " generated & saved...\n");
            Shared.ShowEnd();
        }

        /// <summary>
        /// Finds a property inside the TMX data.
        /// </summary>
        /// <param name="map">The TMX data.</param>
        /// <param name="field">The name of the property.</param>
        /// <param name="defaultValue">The default value if not found.</param>
        /// <returns>The property founded.</returns>
        private static string findProperty(Tiled.Map map, string field, string defaultValue) {
            var props = map.Properties.Items;
            if (props.Length > 0) {
                var value = props.Where(x => x.Name.ToLower() == field.ToLower())
                                 .Select(x => x.Value).FirstOrDefault();
                return string.IsNullOrEmpty(value) ? defaultValue : value;
            }
            return defaultValue;
        }

        /// <summary>
        /// Finds the tileset file name inside the TMX data.
        /// </summary>
        /// <param name="map">The TMX data.</param>
        /// <returns>The tileset file name founded.</returns>
        private static string findTileset(Tiled.Map map) {
            var tilesets = map.Tileset;
            if (tilesets.Length > 0) {
                return tilesets.Where(x => x.Image[0].Width == 512)
                               .Select(x => x.Name).First();
            }
            throw new Exception("No tileset file name inside the TMX file!");
        }

        /// <summary>
        /// Finds the terrain layer inside a layers array.
        /// </summary>
        /// <param name="layers">The layers array.</param>
        /// <returns>The founded layer.</returns>
        private static Tiled.Layer findTerrainLayer(Tiled.Layer[] layers) {
            const int LIMIT = Shared.TERRAIN_ID_FIRST_BOX + 1;
            foreach (var layer in layers) {
                var count = layer.Data.Tiles.Where(x => x.Gid >= LIMIT).Count();
                if (count <= 0) {
                    return layer;
                }
            }
            throw new Exception("No terrain layer inside the TMX file!");
        }

        /// <summary>
        /// Finds the entities layer inside a layers array.
        /// </summary>
        /// <param name="layers">The layers array.</param>
        /// <returns>The founded layer.</returns>
        private static Tiled.Layer findEntitiesLayer(Tiled.Layer[] layers) {
            const int LIMIT = Shared.TERRAIN_ID_FIRST_BOX + 1;
            foreach (var layer in layers) {
                var count = layer.Data.Tiles.Where(x => x.Gid >= LIMIT).Count();
                if (count > 0) {
                    return layer;
                }
            }
            throw new Exception("No terrain layer inside the TMX file!");
        }
    }
}
