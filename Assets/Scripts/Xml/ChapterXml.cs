﻿//******************************************************************************************
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
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// This class represents a chapter descriptor inside a xml.
/// </summary>
public class ChapterXml {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the name of the chapter.
    /// </summary>
    [XmlAttribute("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the levels in the chapter.
    /// </summary>
    [XmlElement("level", typeof(string))]
    public string[] Levels { get; set; }

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public ChapterXml() {
        Name = "";
        Levels = null;
    }
}
