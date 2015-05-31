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
/// This class represents the interface controller behaviour.
/// </summary>
public class InterfaceController : MonoBehaviour {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region CoreManager core

    /// <summary>
    /// The core manager of the game.
    /// </summary>
    private CoreManager core = null;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Events):
    //--------------------------------------------------------------------------------------

    #region void Start()

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void Start() {
        core = CoreManager.Instance;
        core.Initialize();
        core.State.OnStart();
	}

    #endregion

    #region void Update()

    /// <summary>
    /// Updates the component. This is called once per frame.
    /// </summary>
    void Update() {
        core.State.OnUpdate();
        core.ChangeState();
	}

    #endregion

    #region void OnGUI()

    /// <summary>
    /// Updates the GUI.
    /// </summary>
    void OnGUI() {
        core.State.OnGUI();
    }

    #endregion
}
