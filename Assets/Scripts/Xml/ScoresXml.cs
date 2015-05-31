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
/// This class represents a scores descriptor inside a xml.
/// </summary>
public class ScoreXml {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the steps of the score.
    /// </summary>
    [XmlAttribute("steps")]
    public int Steps { get; set; }

    /// <summary>
    /// Gets or sets the seconds of the score.
    /// </summary>
    [XmlAttribute("seconds")]
    public int Seconds { get; set; }

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public ScoreXml() {
        Steps = ProfileData.NO_SCORE;
        Seconds = ProfileData.NO_SCORE;
    }
}

/// <summary>
/// This class represents a scores descriptor inside a xml.
/// </summary>
public class ScoresXml {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets the scores of the profile.
    /// </summary>
    [XmlElement("score", typeof(ScoreXml))]
    public ScoreXml[] Score { get; set; }

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public ScoresXml() {
        Score = null;
    }
}
