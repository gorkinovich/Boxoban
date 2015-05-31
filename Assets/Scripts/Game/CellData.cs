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
using UnityEngine;

/// <summary>
/// This class represents the data of a cell.
/// </summary>
public class CellData {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region int X

    /// <summary>
    /// Gets the column coordinate.
    /// </summary>
    public int X { get; private set; }

    #endregion

    #region int Y

    /// <summary>
    /// Gets the row coordinate.
    /// </summary>
    public int Y { get; private set; }

    #endregion

    #region GameObject TerrainObject

    /// <summary>
    /// Gets the current terrain inside the cell.
    /// </summary>
    public GameObject TerrainObject { get; private set; }

    #endregion

    #region GameObject EntityObject

    /// <summary>
    /// Gets or sets the current entity inside the cell.
    /// </summary>
    public GameObject EntityObject { get; set; }

    #endregion

    #region uint Terrain

    /// <summary>
    /// Gets the type of terrain of the cell.
    /// </summary>
    public uint Terrain { get; private set; }

    #endregion

    #region bool IsEmtpy

    /// <summary>
    /// Gets if the cell is an empty type.
    /// </summary>
    public bool IsEmtpy {
        get {
            return Terrain == CoreManager.TERRAIN_ID_EMPTY;
        }
    }

    #endregion

    #region bool IsWall

    /// <summary>
    /// Gets if the cell is a wall type.
    /// </summary>
    public bool IsWall {
        get {
            return CoreManager.TERRAIN_ID_FIRST_WALL <= Terrain &&
                   Terrain <= CoreManager.TERRAIN_ID_LAST_WALL;
        }
    }

    #endregion

    #region bool IsFloor

    /// <summary>
    /// Gets if the cell is a floor type.
    /// </summary>
    public bool IsFloor {
        get {
            return CoreManager.TERRAIN_ID_FIRST_FLOOR <= Terrain &&
                   Terrain <= CoreManager.TERRAIN_ID_LAST_FLOOR;
        }
    }

    #endregion

    #region bool IsSpecialFloor

    /// <summary>
    /// Gets if the cell is a special floor type.
    /// </summary>
    public bool IsSpecialFloor {
        get {
            return CoreManager.TERRAIN_ID_FIRST_SFLOOR <= Terrain &&
                   Terrain <= CoreManager.TERRAIN_ID_LAST_SFLOOR;
        }
    }

    #endregion

    #region bool IsDestinationFloor

    /// <summary>
    /// Gets if the cell is a destination floor type.
    /// </summary>
    public bool IsDestinationFloor {
        get {
            return CoreManager.TERRAIN_ID_FIRST_DFLOOR <= Terrain &&
                   Terrain <= CoreManager.TERRAIN_ID_LAST_DFLOOR;
        }
    }

    #endregion

    #region bool IsWalkable

    /// <summary>
    /// Gets if the cell is walkable or not.
    /// </summary>
    public bool IsWalkable {
        get {
            return CoreManager.TERRAIN_ID_FIRST_FLOOR <= Terrain &&
                   Terrain <= CoreManager.TERRAIN_ID_LAST_DFLOOR;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region CellData(int, int, GameObject, uint)

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    /// <param name="row">The row of the cell.</param>
    /// <param name="column">The column of the cell.</param>
    /// <param name="terrainObject">The current terrain inside the cell.</param>
    /// <param name="terrain">The type of terrain of the cell.</param>
    public CellData(int row, int column, GameObject terrainObject, uint terrain) {
        X = column;
        Y = row;
        TerrainObject = terrainObject;
        EntityObject = null;
        Terrain = terrain;
    }

    #endregion
}
