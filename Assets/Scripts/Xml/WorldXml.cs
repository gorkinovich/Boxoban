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
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// This class represents the world descriptor inside a level xml.
/// </summary>
public class WorldXml {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the width of the world.
    /// </summary>
    [XmlAttribute("width")]
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the world.
    /// </summary>
    [XmlAttribute("height")]
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the terrain of the world.
    /// </summary>
    [XmlText()]
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the terrain of the world.
    /// </summary>
    [XmlIgnore()]
    public uint[] Terrain {
        get {
            List<uint> victims = new List<uint>();
            foreach (var chunk in Content.Split(' ')) {
                var victim = chunk.Trim();
                if (!String.IsNullOrEmpty(victim)) {
                    try {
                        victims.Add(uint.Parse(victim));
                    } catch (Exception) {
                        victims.Add(0);
                    }
                }
            }
            return victims.ToArray();
        }
        set {
            int i = 0;
            Content = value.Select(x => x.ToString()).Aggregate(
                (cur, nxt) => {
                    ++i;
                    if (i >= Width) {
                        i = 0;
                        return cur + "\n" + nxt; 
                    } else {
                        return cur + " " + nxt; 
                    }
                }
            );
        }
    }

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public WorldXml() {
        Width = 0;
        Height = 0;
        Content = null;
    }
}
