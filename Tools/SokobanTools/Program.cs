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
using System.IO;
using SokobanTools.Logic;

/* * * * * * * * * * * * * * * * * * * * * * * * * *
 * Properties -> Debug -> Command line arguments:  *
 *                                                 *
 *    -tmx Test.tmx                                *
 *    -tmx Test.tmx Test.xml                       *
 *                                                 *
 *    -txt2tmx Screen1.txt                         *
 *    -txt2tmx Screen1.txt Screen1.tmx             *
 *                                                 *
 * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace SokobanTools {
    /// <summary>
    /// This class represents the command.
    /// </summary>
    public class Program {
        /// <summary>
        /// The internal configuration of the command.
        /// </summary>
        private struct Config {
            public CommandMode Mode;
            public string InputFileName;
            public string OutputFileName;
            public Config(CommandMode m, string ifn = null, string ofn = null) {
                Mode = m;
                InputFileName = ifn;
                OutputFileName = ofn;
            }
        }

        /// <summary>
        /// The main entry of the command.
        /// </summary>
        /// <param name="args">The arguments of the command.</param>
        public static void Main(string[] args) {
            // Creates a configuration from 3 arguments:
            Func<CommandMode, string, string, Config> createConfig3 =
                (CommandMode mode, string inputFileName, string outputFileName) => {
                    if (File.Exists(inputFileName)) {
                        return new Config(mode, inputFileName, outputFileName);
                    }
                    return new Config(CommandMode.None);
                };

            // Creates a configuration from 2 arguments:
            Func<CommandMode, string, Config> createConfig2 =
                (CommandMode mode, string inputFileName) => {
                    string extension = "txt";
                    switch (mode) {
                        case CommandMode.TMX: extension = "xml"; break;
                        case CommandMode.TXT: extension = "xml"; break;
                        case CommandMode.TXT2TMX: extension = "tmx"; break;
                    }
                    string outputFileName = Path.GetFileNameWithoutExtension(inputFileName) + "." + extension;
                    return createConfig3(mode, inputFileName, outputFileName);
                };

            try {
                // Get the configuration of the command:
                Config cfg = new Config(CommandMode.None);
                if (args.Length == 3) {
                    if (args[0] == "-tmx") {
                        cfg = createConfig3(CommandMode.TMX, args[1], args[2]);
                    } else if (args[0] == "-txt") {
                        cfg = createConfig3(CommandMode.TXT, args[1], args[2]);
                    } else if (args[0] == "-txt2tmx") {
                        cfg = createConfig3(CommandMode.TXT2TMX, args[1], args[2]);
                    }
                } else if (args.Length == 2) {
                    if (args[0] == "-tmx") {
                        cfg = createConfig2(CommandMode.TMX, args[1]);
                    } else if (args[0] == "-txt") {
                        cfg = createConfig2(CommandMode.TXT, args[1]);
                    } else if (args[0] == "-txt2tmx") {
                        cfg = createConfig2(CommandMode.TXT2TMX, args[1]);
                    }
                }
                // Check the selected mode:
                switch (cfg.Mode) {
                    case CommandMode.TMX:
                        // Initiates the TMX to XML conversion:
                        TmxToXml.Convert(cfg.InputFileName, cfg.OutputFileName);
                        break;

                    case CommandMode.TXT:
                        // Initiates the TXT to XML conversion:
                        TxtToXml.Convert(cfg.InputFileName, cfg.OutputFileName);
                        break;

                    case CommandMode.TXT2TMX:
                        // Initiates the TXT to TMX conversion:
                        TxtToTmx.Convert(cfg.InputFileName, cfg.OutputFileName);
                        break;

                    default:
                        // Shows the help of the command:
                        Console.WriteLine("Sokoban Tools Help:");
                        Console.WriteLine();
                        Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " -tmx input.tmx output.xml");
                        Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " -txt input output.xml");
                        Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " -txt2tmx input output.tmx");
                        Console.WriteLine();
                        Console.WriteLine("-tmx = Converts a Tiled file format to the game level format.");
                        Console.WriteLine("-txt = Converts a classic file format to the game level format.");
                        Console.WriteLine("-txt2tmx = Converts a classic file format to the Tiled file format.");
                        Console.WriteLine();
                        Console.WriteLine("Classic format: http://www.sokobano.de/wiki/index.php?title=Level_format");
                        Console.WriteLine();
                        break;
                }
            } catch (Exception e) {
                Debug.Log(e);
            }
        }
    }
}
