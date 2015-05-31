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
/// This class represents a moving action donne by an entity in the world.
/// </summary>
public class MovingAction {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    public const float MAX_TIME = 0.2f;

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region EntityController entity

    /// <summary>
    /// The entity controller.
    /// </summary>
    private EntityController entity;

    #endregion

    #region CellData destinationCell

    /// <summary>
    /// The destination cell.
    /// </summary>
    private CellData destinationCell;

    #endregion

    #region Vector3 originPoint

    /// <summary>
    /// The origin point.
    /// </summary>
    private Vector3 originPoint;

    #endregion

    #region Vector3 destinationPoint

    /// <summary>
    /// The destination point.
    /// </summary>
    private Vector3 destinationPoint;

    #endregion

    #region Vector3 offsetVector

    /// <summary>
    /// The offset vector.
    /// </summary>
    private Vector3 offsetVector;

    #endregion

    #region float currentTime

    /// <summary>
    /// The current time in the movement.
    /// </summary>
    private float currentTime;

    #endregion

    #region bool Finished

    /// <summary>
    /// Gets if the movement is finished or not.
    /// </summary>
    public bool Finished {
        get { return currentTime >= MAX_TIME; }
    }

    #endregion

    #region MoveDirection Direction

    /// <summary>
    /// Gets the movement direction.
    /// </summary>
    public MoveDirection Direction { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void Update()

    /// <summary>
    /// Updates the action.
    /// </summary>
    public void Update() {
        if (!Finished) {
            currentTime += Time.deltaTime;
            var delta = currentTime / MAX_TIME;
            var deltaVector = offsetVector * delta;
            var nextPoint = originPoint + deltaVector;
            SceneUtil.SetPosition(entity.gameObject, ref nextPoint);
        } else {
            SceneUtil.SetPosition(entity.gameObject, ref destinationPoint);
        }
    }

    #endregion

    #region void Finish()

    /// <summary>
    /// Finishes the action.
    /// </summary>
    public void Finish() {
        entity.EnterCell(destinationCell);
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region MovingAction(EntityController, CellData, MoveDirection)

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    /// <param name="controller">The entity controller.</param>
    /// <param name="destination">The destination cell.</param>
    /// <param name="direction">The movement direction.</param>
    public MovingAction(EntityController controller, CellData destination, MoveDirection direction) {
        entity = controller;
        destinationCell = destination;
        originPoint = entity.transform.localPosition;
        destinationPoint = destinationCell.TerrainObject.transform.localPosition;
        offsetVector = destinationPoint - originPoint;
        currentTime = 0.0f;
        Direction = direction;
    }

    #endregion
}
