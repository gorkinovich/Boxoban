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

namespace SokobanTools.Logic {
    /// <summary>
    /// This static class represents the TXT to XML conversor.
    /// </summary>
    public static class TxtToXml {
        /// <summary>
        /// Converts a TXT file to an XML level file.
        /// </summary>
        /// <param name="inputFileName">The input file name.</param>
        /// <param name="outputFileName">The output file name.</param>
        public static void Convert(string inputFileName, string outputFileName) {
            Shared.ShowTitle(inputFileName, outputFileName);
            string[] mapLines = File.ReadAllLines(inputFileName);
            var level = Shared.ConvertFromTextMap(mapLines);
            XmlFile.Save(outputFileName, level);
            Console.WriteLine(outputFileName + " generated & saved...\n");
            Shared.ShowEnd();
        }
    }
}
