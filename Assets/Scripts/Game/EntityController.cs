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
using UnityEngine;

/// <summary>
/// This class represents an abstract entity behaviour inside a game object.
/// </summary>
public abstract class EntityController : MonoBehaviour {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region int X

    /// <summary>
    /// Gets the column coordinate of the player inside the world.
    /// </summary>
    public int X { get; protected set; }

    #endregion

    #region int Y

    /// <summary>
    /// Gets the row coordinate of the player inside the world.
    /// </summary>
    public int Y { get; protected set; }

    #endregion

    #region CellData Cell

    /// <summary>
    /// Gets the cell where the entity is now.
    /// </summary>
    public CellData Cell {
        get {
            var level = CoreManager.Instance.Level;
            if (level != null) {
                return level.GetCell(X, Y);
            }
            return null;
        }
    }

    #endregion

    #region MovingAction movingAction

    /// <summary>
    /// The current moving action.
    /// </summary>
    protected MovingAction movingAction = null;

    #endregion

    #region bool IsMoving

    /// <summary>
    /// Gets if the player is moving or not.
    /// </summary>
    public bool IsMoving {
        get { return movingAction != null; }
    }

    #endregion

    #region Action onEnterCell

    /// <summary>
    /// The on enter cell event.
    /// </summary>
    protected Action onEnterCell = null;

    #endregion

    #region Action onExitCell

    /// <summary>
    /// The on exit cell event.
    /// </summary>
    protected Action onExitCell = null;

    #endregion

    #region Action onSetDestination

    /// <summary>
    /// The on set destination event.
    /// </summary>
    protected Action onSetDestination = null;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void EnterCell(CellData)

    /// <summary>
    /// Enters the entity into a cell inside the world.
    /// </summary>
    /// <param name="victim">The destination cell.</param>
    public void EnterCell(CellData victim) {
        if (victim.EntityObject == null) {
            victim.EntityObject = gameObject;
            X = victim.X;
            Y = victim.Y;
            SceneUtil.SetPosition(gameObject, victim.TerrainObject);
            if (onEnterCell != null) {
                onEnterCell();
            }
        }
    }

    #endregion

    #region void ExitCell()

    /// <summary>
    /// Exits from the current cell where the entity is in.
    /// </summary>
    public void ExitCell() {
        var previousCell = Cell;
        if (previousCell != null && previousCell.EntityObject == gameObject) {
            previousCell.EntityObject = null;
            if (onExitCell != null) {
                onExitCell();
            }
        }
    }

    #endregion

    #region void SetDestination(CellData, MoveDirection)

    /// <summary>
    /// Sets a new destination to the entity.
    /// </summary>
    /// <param name="victim">The destination cell.</param>
    /// <param name="direction">The movement direction.</param>
    public void SetDestination(CellData victim, MoveDirection direction) {
        ExitCell();
        movingAction = new MovingAction(this, victim, direction);
        if (onSetDestination != null) {
            onSetDestination();
        }
    }

    #endregion
}
