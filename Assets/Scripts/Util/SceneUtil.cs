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
/// This static class contains scene utility functions.
/// </summary>
public static class SceneUtil {
    //--------------------------------------------------------------------------------------
    // Methods (Search):
    //--------------------------------------------------------------------------------------

    #region GameObject Find(string, bool)

    /// <summary>
    /// Finds a game object inside the scene.
    /// </summary>
    /// <param name="name">The name of the object.</param>
    /// <param name="createIfNotExists">The create if not exists flag.</param>
    /// <returns>A game object inside the scene.</returns>
    public static GameObject Find(string name, bool createIfNotExists = true) {
        var victim = GameObject.Find(name);
        if (createIfNotExists && victim == null) {
            victim = new GameObject(name);
        }
        return victim;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Parent):
    //--------------------------------------------------------------------------------------

    #region void SetParent(GameObject, GameObject)

    /// <summary>
    /// Sets the parent of a game object.
    /// </summary>
    /// <param name="child">The game object to change.</param>
    /// <param name="parent">The parent game object.</param>
    public static void SetParent(GameObject child, GameObject parent) {
        child.transform.parent = parent.transform;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Local Position):
    //--------------------------------------------------------------------------------------

    #region void SetPosition(GameObject, float, float)

    /// <summary>
    /// Sets the position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public static void SetPosition(GameObject victim, float x, float y) {
        var position = victim.transform.localPosition;
        position.x = x;
        position.y = y;
        victim.transform.localPosition = position;
    }

    #endregion

    #region void SetPosition(GameObject, float, float, float)

    /// <summary>
    /// Sets the position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    public static void SetPosition(GameObject victim, float x, float y, float z) {
        var position = victim.transform.localPosition;
        position.x = x;
        position.y = y;
        position.z = z;
        victim.transform.localPosition = position;
    }

    #endregion

    #region void SetPosition(GameObject, ref Vector3)

    /// <summary>
    /// Sets the position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="destination">The destination position.</param>
    public static void SetPosition(GameObject victim, ref Vector3 destination) {
        victim.transform.localPosition = destination;
    }

    #endregion

    #region void SetPosition(GameObject, GameObject)

    /// <summary>
    /// Sets the position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="destination">The game object to copy the position.</param>
    public static void SetPosition(GameObject victim, GameObject destination) {
        victim.transform.localPosition = destination.transform.localPosition;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Global Position):
    //--------------------------------------------------------------------------------------

    #region void SetGlobalPosition(GameObject, float, float)

    /// <summary>
    /// Sets the global position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public static void SetGlobalPosition(GameObject victim, float x, float y) {
        var position = victim.transform.position;
        position.x = x;
        position.y = y;
        victim.transform.position = position;
    }

    #endregion

    #region void SetGlobalPosition(GameObject, float, float, float)

    /// <summary>
    /// Sets the global position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    public static void SetGlobalPosition(GameObject victim, float x, float y, float z) {
        var position = victim.transform.position;
        position.x = x;
        position.y = y;
        position.z = z;
        victim.transform.position = position;
    }

    #endregion

    #region void SetGlobalPosition(GameObject, ref Vector3)

    /// <summary>
    /// Sets the global position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="destination">The destination position.</param>
    public static void SetGlobalPosition(GameObject victim, ref Vector3 destination) {
        victim.transform.position = destination;
    }

    #endregion

    #region void SetGlobalPosition(GameObject, GameObject)

    /// <summary>
    /// Sets the global position of a game object.
    /// </summary>
    /// <param name="victim">The game object to change.</param>
    /// <param name="destination">The game object to copy the position.</param>
    public static void SetGlobalPosition(GameObject victim, GameObject destination) {
        victim.transform.position = destination.transform.position;
    }

    #endregion
}
