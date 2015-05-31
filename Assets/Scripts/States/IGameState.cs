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

/// <summary>
/// This interface represents a generic game state.
/// </summary>
public interface IGameState {
    /// <summary>
    /// Initializes the state.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Releases the state.
    /// </summary>
    void Release();

    /// <summary>
    /// This is called on the "start" event of the interface controller.
    /// </summary>
    void OnStart();

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    void OnUpdate();

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    void OnGUI();
}
