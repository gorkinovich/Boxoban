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
/// This class represents the level controller behaviour inside the world.
/// </summary>
public class LevelController : MonoBehaviour {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region bool mouseButtonDown

    /// <summary>
    /// The mouse button down flag, used to change the world position.
    /// </summary>
    private bool mouseButtonDown = false;

    #endregion

    #region Vector3 lastMousePosition

    /// <summary>
    /// The last mouse position to control where to move the world.
    /// </summary>
    private Vector3 lastMousePosition = Vector3.zero;

    #endregion

    #region Vector3 offsetAccumulated

    /// <summary>
    /// The offset accumulated when we try to move the world.
    /// </summary>
    private Vector3 offsetAccumulated = Vector3.zero;

    #endregion

    #region Vector3 worldStartPosition

    /// <summary>
    /// The start position of the world when we press down the mouse button.
    /// </summary>
    private Vector3 worldStartPosition = Vector3.zero;

    #endregion

    #region bool worldLimitsInitialized

    /// <summary>
    /// The world limits initialized flag.
    /// </summary>
    private bool worldLimitsInitialized = false;

    #endregion

    #region Vector3 worldMinPosition

    /// <summary>
    /// The minimum position of the world when we try to move it.
    /// </summary>
    private Vector3 worldMinPosition = Vector3.zero;

    #endregion

    #region Vector3 worldMaxPosition

    /// <summary>
    /// The maximum position of the world when we try to move it.
    /// </summary>
    private Vector3 worldMaxPosition = Vector3.zero;

    #endregion

    #region Timer secondTimer

    /// <summary>
    /// The timer that controls each second
    /// </summary>
    private Timer secondTimer = new Timer();

    #endregion

    #region CoreManager core

    /// <summary>
    /// The core manager of the game.
    /// </summary>
    private CoreManager core = CoreManager.Instance;

    #endregion

    #region bool skipMouseUntilUp

    /// <summary>
    /// The "skip mouse until up" flag.
    /// </summary>
    private bool skipMouseUntilUp = false;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region bool worldMoved()

    /// <summary>
    /// Checks if the world have been moved enough.
    /// </summary>
    /// <returns>Returns if the world have been moved enough.</returns>
    private bool worldMoved() {
        const float MIN_OFFSET = 4.0f;
        return Math.Abs(offsetAccumulated.x) >= MIN_OFFSET ||
               Math.Abs(offsetAccumulated.y) >= MIN_OFFSET;
    }

    #endregion

    #region void checkAndChangeWorldNextPosition(ref Vector3)

    /// <summary>
    /// Checks if the world can be moved to a position, if can't be moved
    /// the position will be changed with the current limits.
    /// </summary>
    /// <param name="nextPosition">The next position of the world.</param>
    private void checkAndChangeWorldNextPosition(ref Vector3 nextPosition) {
        if (!worldLimitsInitialized) {
            // Get the current level data:
            var level = CoreManager.Instance.Level;
            if (level == null || !level.IsLoaded) return;
            // Calculate some distances:
            float widthInUnits = (level.Width * CoreManager.TILE_WIDTH) / CoreManager.PIXEL_TO_UNITS;
            float heightInUnits = (level.Height * CoreManager.TILE_HEIGHT) / CoreManager.PIXEL_TO_UNITS;
            float halfWIU = widthInUnits * 0.5f;
            float halfHIU = heightInUnits * 0.5f;
            // Set the limits:
            worldMinPosition = new Vector3(-halfWIU, -halfHIU);
            worldMaxPosition = new Vector3(halfWIU, halfHIU);
            // Change the initalization flag:
            worldLimitsInitialized = true;
        }
        // Check the limits with the next position:
        nextPosition.x = Math.Max(nextPosition.x, worldMinPosition.x);
        nextPosition.x = Math.Min(nextPosition.x, worldMaxPosition.x);
        nextPosition.y = Math.Max(nextPosition.y, worldMinPosition.y);
        nextPosition.y = Math.Min(nextPosition.y, worldMaxPosition.y);
    }

    #endregion

    #region void findDirectionAndMove()

    /// <summary>
    /// Finds the direction to move the planer and makes the move.
    /// </summary>
    private void findDirectionAndMove() {
        try {
            // Get the camera and the player from the scene:
            var camera = GameObject.Find(CoreManager.CAMERA_GOBJ_NAME);
            var player = GameObject.Find(CoreManager.PLAYER_GOBJ_NAME);
            // Get the centered coordinates:
            var playerPosition = player.transform.position;
            playerPosition.x += (CoreManager.TILE_WIDTH / 2.0f) / CoreManager.PIXEL_TO_UNITS;
            playerPosition.y -= (CoreManager.TILE_WIDTH / 2.0f) / CoreManager.PIXEL_TO_UNITS;
            // Get the offset between the player and the mouse coordinates:
            Vector3 playerScreenPosition = camera.camera.WorldToScreenPoint(playerPosition);
            var offset = lastMousePosition - playerScreenPosition;
            // Check wich side have been selected:
            MoveDirection direction = MoveDirection.None;
            var absX = Math.Abs(offset.x);
            var absY = Math.Abs(offset.y);
            if (offset.x < 0) {
                if (offset.y < 0) {
                    // This is the left-down side:
                    direction = absX < absY ? MoveDirection.South : MoveDirection.West;
                } else {
                    // This is the left-up side:
                    direction = absX < absY ? MoveDirection.North : MoveDirection.West;
                }
            } else {
                if (offset.y < 0) {
                    // This is the right-down side:
                    direction = absX < absY ? MoveDirection.South : MoveDirection.East;
                } else {
                    // This is the right-up side:
                    direction = absX < absY ? MoveDirection.North : MoveDirection.East;
                }
            }
            // Make the move action:
            makeMove(player, direction);
        } catch (Exception e) {
            CoreManager.Instance.Log(e);
        }
    }

    #endregion

    #region void makeMove(GameObject, MoveDirection)

    /// <summary>
    /// Makes a move with the player entity.
    /// </summary>
    /// <param name="player">The player game object.</param>
    /// <param name="direction">The direction to be moved.</param>
    private void makeMove(GameObject player, MoveDirection direction) {
        // Get the player controller from the game object:
        var playerController = player.GetComponent<PlayerController>();
        if (playerController.IsMoving) return;
        // Get the next cell where we're going to move:
        var level = CoreManager.Instance.Level;
        var nextCell = level.GetNextCell(playerController.X, playerController.Y, direction);
        if (nextCell != null && nextCell.IsWalkable) {
            // Check if there is no entity inside the cell:
            if (nextCell.EntityObject == null) {
                playerController.SetDestination(nextCell, direction);
                playerController.ShowWalk();
                core.AddStep();
            } else {
                // If there is an entity inside the cell, check the next one:
                var next2Cell = level.GetNextCell(nextCell.X, nextCell.Y, direction);
                if (next2Cell != null && next2Cell.IsWalkable && next2Cell.EntityObject == null) {
                    var boxController = nextCell.EntityObject.GetComponent<BoxController>();
                    boxController.SetDestination(next2Cell, direction);
                    playerController.SetDestination(nextCell, direction);
                    playerController.ShowPush();
                    core.AddStep();
                }
            }
        }
    }

    #endregion

    #region bool keyboardInputUsed()

    /// <summary>
    /// Checks if the keyboard input is used.
    /// </summary>
    /// <returns>Returns true if the keyboard is used.</returns>
    private bool keyboardInputUsed() {
        bool keyPressed = false;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            CoreManager.Instance.GotoLevelPause();
            keyPressed = true;

        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            makeMove(GameObject.Find(CoreManager.PLAYER_GOBJ_NAME), MoveDirection.North);
            keyPressed = true;

        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            makeMove(GameObject.Find(CoreManager.PLAYER_GOBJ_NAME), MoveDirection.East);
            keyPressed = true;

        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            makeMove(GameObject.Find(CoreManager.PLAYER_GOBJ_NAME), MoveDirection.South);
            keyPressed = true;

        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            makeMove(GameObject.Find(CoreManager.PLAYER_GOBJ_NAME), MoveDirection.West);
            keyPressed = true;
        }

        if (keyPressed) {
            mouseButtonDown = false;
        }

        return keyPressed;
    }

    #endregion

    #region void ActivateSkipMouseUntilUp()

    /// <summary>
    /// Activates the "skip mouse until up" flag.
    /// </summary>
    public void ActivateSkipMouseUntilUp() {
        skipMouseUntilUp = true;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Events):
    //--------------------------------------------------------------------------------------

    #region void Start()

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void Start() {
        secondTimer.SetAndEnable(1.0f, (timer) => core.AddSecond());
    }

    #endregion

    #region void Update()

    /// <summary>
    /// Updates the component. This is called once per frame.
    /// </summary>
    void Update() {
        // Update the timer:
        secondTimer.Update();

        // Update the input:
        if (!keyboardInputUsed()) {
            if (skipMouseUntilUp) {
                // Check if the left mouse button is up to unlock the mouse input:
                if (!Input.GetMouseButton(0)) {
                    mouseButtonDown = false;
                    skipMouseUntilUp = false;
                }
            } else {
                // Check if the left mouse button have been pressed down:
                if (Input.GetMouseButtonDown(0)) {
                    worldStartPosition = gameObject.transform.position;
                    lastMousePosition = Input.mousePosition;
                    offsetAccumulated = Vector3.zero;
                    mouseButtonDown = true;
                }
                // Check if the left mouse button have been pressed up:
                if (Input.GetMouseButtonUp(0)) {
                    if (!worldMoved()) {
                        gameObject.transform.position = worldStartPosition;
                        findDirectionAndMove();
                    }
                    mouseButtonDown = false;
                }
                // Check if the left mouse button is down to move the world: 
                if (mouseButtonDown) {
                    // Calculate the offset of the mouse position:
                    var mousePosition = Input.mousePosition;
                    var offset = mousePosition - lastMousePosition;
                    offsetAccumulated += offset;
                    offset /= CoreManager.PIXEL_TO_UNITS;
                    // Get the next position of the world and check if we can move it:
                    var position = gameObject.transform.position + offset;
                    checkAndChangeWorldNextPosition(ref position);
                    gameObject.transform.position = position;
                    // Set the new last mouse position:
                    lastMousePosition = mousePosition;
                }
            }
        }
    }

    #endregion
}
