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
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SokobanTools.Logic {
    /// <summary>
    /// This static class represents xml file operations.
    /// </summary>
    public static class XmlFile {
        /// <summary>
        /// Loads a xml file into an object.
        /// </summary>
        /// <typeparam name="T">The type to be loaded.</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <returns>The loaded object if success, otherwise null.</returns>
        public static T LoadXml<T>(string path) where T : class {
            try {
                var serializer = new XmlSerializer(typeof(T));
                var stream = new XmlTextReader(path);

                T victim = (T)serializer.Deserialize(stream);
                stream.Close();
                return victim;

            } catch (Exception e) {
                Debug.Log(e);
                return null;
            }
        }

        /// <summary>
        /// Saves an object into a xml file.
        /// </summary>
        /// <typeparam name="T">The type to be saved.</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <param name="victim">The objecto to be saved.</param>
        public static void Save<T>(string path, T victim) where T : class {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextWriter stream = new XmlTextWriter(path, Encoding.UTF8);

                stream.Formatting = Formatting.Indented;
                stream.Indentation = 1;
                stream.IndentChar = '\t';

                serializer.Serialize(stream, victim);
                stream.Close();

            } catch (Exception e) {
                Debug.Log(e);
            }
        }
    }
}
