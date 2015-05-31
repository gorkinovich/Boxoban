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
using System.Xml;
using System.Xml.Serialization;

namespace Tiled {

    [Serializable, XmlRoot("map", Namespace = "", IsNullable = false)]
    public class Map {
        [XmlElement("properties")]
        public Properties Properties;

        [XmlElement("tileset")]
        public Tileset[] Tileset;

        [XmlElement("layer", typeof(Layer)),
         XmlElement("objectgroup", typeof(ObjectGroup))]
        public MapItem[] Items;

        [XmlAttribute("orientation")]
        public OrientationType Orientation;

        [XmlAttribute("renderorder")]
        public RenderOrderType RenderOrder;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;

        [XmlAttribute("tilewidth")]
        public int TileWidth;

        [XmlAttribute("tileheight")]
        public int TileHeight;
    }

    //--------------------------------------------------------------------------------------

    public class Properties {
        [XmlElement("property")]
        public Property[] Items;
    }

    //--------------------------------------------------------------------------------------

    public class Property {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("value")]
        public string Value;
    }

    //--------------------------------------------------------------------------------------

    public class Tileset {
        [XmlElement("image")]
        public TilesetImage[] Image;

        [XmlElement("tile")]
        public TilesetTile[] Tile;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("firstgid")]
        public int FirstGid;

        [XmlAttribute("source")]
        public int Source;

        [XmlAttribute("tilewidth")]
        public int TileWidth;

        [XmlAttribute("tileheight")]
        public int TileHeight;

        [XmlAttribute("spacing")]
        public int Spacing;

        [XmlAttribute("margin")]
        public int Margin;
    }

    //--------------------------------------------------------------------------------------

    public class TilesetImage {
        [XmlAttribute("source")]
        public string Source;

        [XmlAttribute("trans")]
        public string Trans;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;
    }

    //--------------------------------------------------------------------------------------

    public class TilesetTile {
        [XmlAttribute("id")]
        public string Id;

        [XmlElement("properties")]
        public Properties Properties;
    }

    //--------------------------------------------------------------------------------------

    public class MapItem {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;
    }

    //--------------------------------------------------------------------------------------

    public class Layer : MapItem {
        [XmlAttribute("opacity")]
        public float Opacity;

        [XmlElement("properties")]
        public Properties Properties;

        [XmlElement("data")]
        public LayerData Data;
    }

    //--------------------------------------------------------------------------------------

    public class LayerData {
        [XmlElement("tile")]
        public LayerDataTile[] Tiles;
    }

    //--------------------------------------------------------------------------------------

    public class LayerDataTile {
        [XmlAttribute("gid")]
        public int Gid;
    }

    //--------------------------------------------------------------------------------------

    public class ObjectGroup : MapItem {
        [XmlElement("object")]
        public MapObject[] Objects;
    }

    //--------------------------------------------------------------------------------------

    public class MapObject {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("type")]
        public string Type;

        [XmlAttribute("x")]
        public int X;

        [XmlAttribute("y")]
        public int Y;

        [XmlAttribute("width")]
        public int Width;

        [XmlAttribute("height")]
        public int Height;

        [XmlElement("properties")]
        public Properties Properties;
    }

    //--------------------------------------------------------------------------------------

    public enum OrientationType {
        orthogonal,
        isometric,
        hexagonal,
        shifted,
    }

    //--------------------------------------------------------------------------------------

    public enum RenderOrderType {
        [XmlEnum("right-down")]
        rightdown,

        [XmlEnum("right-up")]
        rightup,

        [XmlEnum("left-down")]
        leftdown,

        [XmlEnum("left-up")]
        leftup,
    }
}
