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
using System.Collections.Generic;

/// <summary>
/// This class represents a chapter data.
/// </summary>
public class ChapterData {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region string Name

    /// <summary>
    /// Gets the name of the chapter.
    /// </summary>
    public string Name { get; private set; }

    #endregion

    #region string[] Levels

    /// <summary>
    /// Gets the levels of the chapter.
    /// </summary>
    public string[] Levels { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region ChapterData(string, string[])

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    /// <param name="name">The name of the chapter.</param>
    /// <param name="levels">The levels of the chapter.</param>
    public ChapterData(string name, string[] levels) {
        Name = name;
        Levels = levels;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Static):
    //--------------------------------------------------------------------------------------

    #region ChapterData[] Create(ChaptersXml)

    /// <summary>
    /// Creates a new chapter data array.
    /// </summary>
    /// <param name="descriptor">The descriptor of the chapters.</param>
    /// <returns>Returns the created chapter data array.</returns>
    public static ChapterData[] Create(ChaptersXml descriptor) {
        if (descriptor == null) {
            return null;
        } else {
            var chapters = new List<ChapterData>();
            foreach (var item in descriptor.Chapters) {
                chapters.Add(Create(item));
            }
            return chapters.ToArray();
        }
    }

    #endregion

    #region ChapterData Create(ChapterXml)

    /// <summary>
    /// Creates a new chapter data.
    /// </summary>
    /// <param name="descriptor">The descriptor of the chapter.</param>
    /// <returns>Returns the created chapter data.</returns>
    public static ChapterData Create(ChapterXml descriptor) {
        return descriptor == null ? null : new ChapterData(descriptor.Name, descriptor.Levels);
    }

    #endregion
}
