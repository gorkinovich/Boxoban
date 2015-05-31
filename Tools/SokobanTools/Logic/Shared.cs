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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Sokoban;

namespace SokobanTools.Logic {
    public static class Shared {
        //--------------------------------------------------------------------------------------
        // Constants:
        //--------------------------------------------------------------------------------------

        public const string DEFAULT_NAME = "{TODO:LEVEL_NAME}";
        public const string DEFAULT_DESCRIPTION = "{TODO:LEVEL_DESCRIPTION}";
        public const string DEFAULT_TILESET = "TilesetWorld1";

        public const char WALL = '#';
        public const char GOAL = '.';
        public const char FLOOR = ' ';
        public const char PLAYER_ON_GOAL = '+';
        public const char BOX_ON_GOAL = '*';
        public const char PLAYER = '@';
        public const char BOX = '$';
        public const char VOID = '?';

        private const char ZONE_UNKNOWN = '?';
        private const char ZONE_VISITED = 'V';
        private const char ZONE_EMPTY = 'E';

        public const int TERRAIN_ID_EMPTY = 0;
        public const int TERRAIN_ID_FIRST_WALL = 1;
        public const int TERRAIN_ID_LAST_WALL = 255;
        public const int TERRAIN_ID_FIRST_FLOOR = 256;
        public const int TERRAIN_ID_LAST_FLOOR = 511;
        public const int TERRAIN_ID_FIRST_SFLOOR = 512;
        public const int TERRAIN_ID_LAST_SFLOOR = 639;
        public const int TERRAIN_ID_FIRST_DFLOOR = 640;
        public const int TERRAIN_ID_LAST_DFLOOR = 767;
        public const int TERRAIN_ID_FIRST_BOX = 768;
        public const int TERRAIN_ID_LAST_BOX = 895;
        public const int TERRAIN_ID_FIRST_DBOX = 896;
        public const int TERRAIN_ID_LAST_DBOX = 1023;

        public const int PLAYER_GID = 1025;

        //--------------------------------------------------------------------------------------
        // Methods (Show):
        //--------------------------------------------------------------------------------------

        /// <summary>
        /// Shows the title message of the command.
        /// </summary>
        /// <param name="inputFileName">The input file name.</param>
        /// <param name="outputFileName">The output file name.</param>
        public static void ShowTitle(string inputFileName, string outputFileName) {
            ShowMessageInBox("SOKOBAN TOOLS", '|', '=');
            Console.WriteLine();
            Console.WriteLine("Input file:  " + inputFileName);
            Console.WriteLine("Output file: " + outputFileName);
            Console.WriteLine();
        }

        /// <summary>
        /// Shows the end message of the command.
        /// </summary>
        public static void ShowEnd() {
            Console.WriteLine("Conversion finished... ^_^");
        }

        /// <summary>
        /// Shows a message inside a box in the console.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <param name="hside">The horizontal line character.</param>
        /// <param name="vside">The vertical line character.</param>
        public static void ShowMessageInBox(string message, char hside, char vside) {
            var lines = message.Split('\n');
            int length = lines.Select(l => l.Length).Max();
            var vbar = new String(vside, length + 2);
            Console.WriteLine(hside + vbar + hside);
            foreach (var line in lines) {
                var msg = " " + line + " ";
                int spacesLength = length - line.Length;
                if (spacesLength > 0) {
                    int leftSpaces = spacesLength / 2;
                    int rightSpaces = leftSpaces;
                    if (spacesLength % 2 != 0) {
                        rightSpaces++;
                    }
                    var left = new String(' ', leftSpaces);
                    var right = new String(' ', rightSpaces);
                    msg = left + msg + right;
                }
                Console.WriteLine(hside + msg + hside);
            }
            Console.WriteLine(hside + vbar + hside);
        }

        /// <summary>
        /// Shows a text map in the console.
        /// </summary>
        /// <param name="mapLines">The text map.</param>
        public static void ShowTextMap(string[] mapLines) {
            foreach (var item in mapLines) {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        //--------------------------------------------------------------------------------------
        // Methods (Convert):
        //--------------------------------------------------------------------------------------

        /// <summary>
        /// Converts a text map level into a level xml descriptor.
        /// </summary>
        /// <param name="mapLines">The text map.</param>
        /// <returns>The converted map.</returns>
        public static LevelXml ConvertFromTextMap(string[] mapLines) {
            // Validate the input data:
            if (mapLines == null || mapLines.Length <= 0) {
                throw new Exception("No input map lines to convert!");
            }
            if (mapLines.Length > 1 && string.IsNullOrEmpty(mapLines[mapLines.Length - 1])) {
                var ml = new string[mapLines.Length - 1];
                for (int i = 0; i < ml.Length; i++) {
                    ml[i] = mapLines[i];
                }
                mapLines = ml;
            }
            if (mapLines.Select(l => string.IsNullOrEmpty(l)).Aggregate((cur, nxt) => cur || nxt)) {
                throw new Exception("Some of the map lines are empty!");
            }

            // Transform the input data into something more cleaner:
            ShowMessageInBox("Raw input map", '|', '-');
            Console.WriteLine();
            ShowTextMap(mapLines);
            mapLines = transform(mapLines);
            ShowMessageInBox("Final input map", '|', '-');
            Console.WriteLine();
            ShowTextMap(mapLines);

            // Initialize the basic data of the level:
            var level = new LevelXml();
            level.Name = DEFAULT_NAME;
            level.Description = DEFAULT_DESCRIPTION;
            level.Tileset = DEFAULT_TILESET;

            // Set the terrain & entities data of the level:
            level.World = new WorldXml();
            level.World.Width = mapLines[0].Length;
            level.World.Height = mapLines.Length;
            level.World.Content = "\n";

            int x = 0, y = 0;
            var entities = new List<EntityXml>();
            Action<string> addEntity = (type) => {
                var victim = new EntityXml();
                victim.Type = type;
                victim.X = x; victim.Y = y;
                entities.Add(victim);
            };

            int currentProgress = 0;
            int totalProgress = level.World.Width * level.World.Height;
            Console.Write("\rText map into game level format... [" +
                          (currentProgress * 100 / totalProgress) + "%]");

            foreach (var line in mapLines) {
                foreach (var c in line) {
                    var id = TERRAIN_ID_EMPTY;
                    switch (c) {
                        case WALL:  id = TERRAIN_ID_FIRST_WALL;   break;
                        case GOAL:  id = TERRAIN_ID_FIRST_DFLOOR; break;
                        case FLOOR: id = TERRAIN_ID_FIRST_FLOOR;  break;

                        case PLAYER_ON_GOAL:
                            id = TERRAIN_ID_FIRST_DFLOOR;
                            addEntity(EntityType.PLAYER);
                            break;

                        case BOX_ON_GOAL:
                            id = TERRAIN_ID_FIRST_DFLOOR;
                            addEntity(EntityType.BOX);
                            break;

                        case PLAYER:
                            id = TERRAIN_ID_FIRST_FLOOR;
                            addEntity(EntityType.PLAYER);
                            break;

                        case BOX:
                            id = TERRAIN_ID_FIRST_FLOOR;
                            addEntity(EntityType.BOX);
                            break;
                    }
                    level.World.Content += id.ToString("0000");
                    level.World.Content += " ";
                    ++x;

                    Console.Write("\rText map into game level format... [" +
                                  (currentProgress * 100 / totalProgress) + "%]");
                }
                level.World.Content += "\n";
                x = 0;
                ++y;
            }

            Console.WriteLine("\rText map into game level format... [100%]");

            level.Entities = entities.ToArray();
            return level;
        }

        /// <summary>
        /// Converts a level xml descriptor into a TMX xml text.
        /// </summary>
        /// <param name="level">The level descriptor</param>
        /// <returns>The TMX xml text.</returns>
        public static string[] ConvertToTmxText(LevelXml level) {
            var lines = new List<string>();
            int width = level.World.Width;
            int height = level.World.Height;

            // Set the root:
            lines.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            lines.Add("<map version=\"1.0\" orientation=\"orthogonal\" renderorder=\"right-down\" width=\"" +
                      width + "\" height=\"" + height + "\" tilewidth=\"16\" tileheight=\"16\">");

            Console.WriteLine("TMX root set...");

            // Set the properties:
            lines.Add(" <properties>");
            lines.Add("  <property name=\"Name\" value=\"" + level.Name + "\"/>");
            lines.Add("  <property name=\"Description\" value=\"" + level.Description + "\"/>");
            lines.Add(" </properties>");

            Console.WriteLine("TMX properties set...");

            // Set the tilesets:
            lines.Add(" <tileset firstgid=\"1\" name=\"" + level.Tileset + "\" tilewidth=\"16\" tileheight=\"16\">");
            lines.Add("  <image source=\"" + level.Tileset + ".png\" width=\"512\" height=\"512\"/>");
            lines.Add(" </tileset>");
            lines.Add(" <tileset firstgid=\"1025\" name=\"Charset\" tilewidth=\"16\" tileheight=\"16\">");
            lines.Add("  <image source=\"Charset.png\" width=\"256\" height=\"256\"/>");
            lines.Add(" </tileset>");

            Console.WriteLine("TMX tilesets set...");

            // Set the terrain layer:
            lines.Add(" <layer name=\"Terrain\" width=\"" + width + "\" height=\"" + height + "\">");
            lines.Add("  <data>");

            int currentProgress = 0;
            int totalProgress = level.World.Width * level.World.Height;
            Console.Write("\rTerrain information into TMX level format... [" +
                          (currentProgress * 100 / totalProgress) + "%]");

            var terrain = level.World.Terrain;
            for (int i = 0, k = 0; i < height; i++) {
                for (int j = 0; j < width; j++, k++) {
                    var gid = terrain[k] + 1;
                    lines.Add("   <tile gid=\"" + gid + "\"/>");

                    Console.Write("\rTerrain information into TMX level format... [" +
                                  (currentProgress * 100 / totalProgress) + "%]");
                }
            }

            Console.WriteLine("\rTerrain information into TMX level format... [100%]");

            lines.Add("  </data>");
            lines.Add(" </layer>");

            // Set the entities layer:
            lines.Add(" <layer name=\"Entities\" width=\"" + width + "\" height=\"" + height + "\">");
            lines.Add("  <data>");

            currentProgress = 0;
            Console.Write("\rEntities information into TMX level format... [" +
                          (currentProgress * 100 / totalProgress) + "%]");

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    int gid = 0;
                    var entity = level.Entities.Where(v => v.X == j && v.Y == i).FirstOrDefault();
                    if (entity != null) {
                        if (entity.Type == EntityType.BOX) {
                            gid = TERRAIN_ID_FIRST_BOX + 1;
                        } else if (entity.Type == EntityType.PLAYER) {
                            gid = PLAYER_GID;
                        }
                    }
                    lines.Add("   <tile gid=\"" + gid + "\"/>");

                    Console.Write("\rEntities information into TMX level format... [" +
                                  (currentProgress * 100 / totalProgress) + "%]");
                }
            }
            lines.Add("  </data>");
            lines.Add(" </layer>");

            Console.WriteLine("\rEntities information into TMX level format... [100%]");

            // Set the end:
            lines.Add("</map>");

            return lines.ToArray();
        }

        //--------------------------------------------------------------------------------------
        // Methods (Transform):
        //--------------------------------------------------------------------------------------

        /// <summary>
        /// Converts the raw text map into something more cleaner to parse.
        /// </summary>
        /// <param name="mapLines">The raw text map.</param>
        /// <returns>The cleaner text map.</returns>
        private static string[] transform(string[] mapLines) {
            // Initialize the table:
            int height = mapLines.Length;
            int width = mapLines.Select(l => l.Length).Max();
            char[,] table = new char[height, width];
            char[,] zone = new char[height, width];
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    if (j < mapLines[i].Length) {
                        table[i, j] = mapLines[i][j];
                    } else {
                        table[i, j] = VOID;
                    }
                    zone[i, j] = ZONE_UNKNOWN;
                }
            }

            // Find empty zones:
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    findEmptyZone(i, j, height, width, table, zone);
                    if (zone[i, j] == ZONE_EMPTY) {
                        table[i, j] = VOID;
                    }
                }
            }


            // Set the final result:
            string[] result = new string[height];
            for (int i = 0; i < height; i++) {
                var item = "";
                for (int j = 0; j < width; j++) {
                    item += table[i, j];
                }
                result[i] = item;
            }
            return result;
        }

        /// <summary>
        /// Finds an empty zone.
        /// </summary>
        /// <param name="i">The current row.</param>
        /// <param name="j">The current column.</param>
        /// <param name="h">The height of the map.</param>
        /// <param name="w">The width of the map.</param>
        /// <param name="table">The map.</param>
        /// <param name="zone">The zone flags.</param>
        private static void findEmptyZone(int i, int j, int h, int w, char[,] table, char[,] zone) {
            if (zone[i, j] == ZONE_UNKNOWN) {
                if (table[i, j] == WALL) {
                    zone[i, j] = ZONE_VISITED;
                } else if (table[i, j] == VOID) {
                    zone[i, j] = ZONE_EMPTY;
                } else {
                    if (isEmptyZone(i, j, h, w, table, zone)) {
                        setEmptyZone(i, j, h, w, table, zone);
                    } else {
                        visitNotEmptyZone(i, j, h, w, table, zone);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a zone is empty.
        /// </summary>
        /// <param name="i">The current row.</param>
        /// <param name="j">The current column.</param>
        /// <param name="h">The height of the map.</param>
        /// <param name="w">The width of the map.</param>
        /// <param name="table">The map.</param>
        /// <param name="zone">The zone flags.</param>
        /// <returns></returns>
        private static bool isEmptyZone(int i, int j, int h, int w, char[,] table, char[,] zone) {
            if (i < 0 || i >= h || j < 0 || j >= w || zone[i, j] != ZONE_UNKNOWN) {
                // The coordinates are inside the map or the cell have been visited:
                return true;
            } else {
                if (table[i, j] == FLOOR) {
                    // If the cell is a floor type, we'll visit the neighbours:
                    zone[i, j] = ZONE_VISITED;
                    var result = isEmptyZone(i - 1, j, h, w, table, zone) &&
                                 isEmptyZone(i + 1, j, h, w, table, zone) &&
                                 isEmptyZone(i, j - 1, h, w, table, zone) &&
                                 isEmptyZone(i, j + 1, h, w, table, zone);
                    // If this isn't an empty zone we'll mark the cell as unknown:
                    if (!result) {
                        zone[i, j] = ZONE_UNKNOWN;
                    }
                    return result;
                } else {
                    // Anything that isn't the void or a wall is something inside
                    // the zone, in that case the zone wouldn't be empty:
                    return table[i, j] == VOID || table[i, j] == WALL;
                }
            }
        }

        /// <summary>
        /// Marks as empty a visited zone.
        /// </summary>
        /// <param name="i">The current row.</param>
        /// <param name="j">The current column.</param>
        /// <param name="h">The height of the map.</param>
        /// <param name="w">The width of the map.</param>
        /// <param name="table">The map.</param>
        /// <param name="zone">The zone flags.</param>
        private static void setEmptyZone(int i, int j, int h, int w, char[,] table, char[,] zone) {
            // We'll only mark as "void" zones inside the map, that has been
            // visited and they're floor type cells:
            if (0 <= i && i < h && 0 <= j && j < w &&
                zone[i, j] == ZONE_VISITED && table[i, j] == FLOOR) {
                // If so, we'll mark the zone flags and the map:
                zone[i, j] = ZONE_EMPTY;
                table[i, j] = VOID;
                // And then visit the neighbours:
                setEmptyZone(i - 1, j, h, w, table, zone);
                setEmptyZone(i + 1, j, h, w, table, zone);
                setEmptyZone(i, j - 1, h, w, table, zone);
                setEmptyZone(i, j + 1, h, w, table, zone);
            }
        }

        /// <summary>
        /// Marks as visited a non empty zone.
        /// </summary>
        /// <param name="i">The current row.</param>
        /// <param name="j">The current column.</param>
        /// <param name="h">The height of the map.</param>
        /// <param name="w">The width of the map.</param>
        /// <param name="table">The map.</param>
        /// <param name="zone">The zone flags.</param>
        private static void visitNotEmptyZone(int i, int j, int h, int w, char[,] table, char[,] zone) {
            // We'll only mark as "visited" zones inside the map, that hasn't
            // been visited and they aren't walls or void type cells:
            if (0 <= i && i < h && 0 <= j && j < w && zone[i, j] == ZONE_UNKNOWN &&
                table[i, j] != WALL && table[i, j] != VOID) {
                // If so, we'll mark the zone flags:
                zone[i, j] = ZONE_VISITED;
                // And then visit the neighbours:
                visitNotEmptyZone(i - 1, j, h, w, table, zone);
                visitNotEmptyZone(i + 1, j, h, w, table, zone);
                visitNotEmptyZone(i, j - 1, h, w, table, zone);
                visitNotEmptyZone(i, j + 1, h, w, table, zone);
            }
        }

        /// <summary>
        /// Shows in the console a table.
        /// </summary>
        /// <param name="height">The height of the table.</param>
        /// <param name="width">The width of the table.</param>
        /// <param name="table">The table.</param>
        private static void showTable(int height, int width, char[,] table) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    Console.Write(table[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
