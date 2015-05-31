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
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// This class represents an entity descriptor inside a level xml.
/// </summary>
public class EntityXml {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the type of the entity.
    /// </summary>
    [XmlAttribute("type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the column of the entity's position.
    /// </summary>
    [XmlAttribute("x")]
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the row of the entity's position.
    /// </summary>
    [XmlAttribute("y")]
    public int Y { get; set; }

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public EntityXml() {
        Type = "";
        X = 0;
        Y = 0;
    }
}
