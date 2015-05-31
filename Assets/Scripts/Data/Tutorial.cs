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
/// This static class contains the tutorial of the game.
/// </summary>
public static class Tutorial {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets the current message to the player.
    /// </summary>
    public static string Message { get; private set; }

    /// <summary>
    /// The current step in the tutorial.
    /// </summary>
    private static int step = 0;

    /// <summary>
    /// The current player controler in the tutorial.
    /// </summary>
    private static PlayerController player = null;

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Starts a new tutorial.
    /// </summary>
    public static void Start() {
        Message = Strings.TUTORIAL_MSG0;
        step = 0;
        player = null;
    }

    /// <summary>
    /// Updates the current tutorial.
    /// </summary>
    public static void Update() {
        switch (step) {
            case 0:
                if (player == null) {
                    player = GameObject.FindObjectOfType<PlayerController>();
                }
                if (player.Cell.X == 4 && player.Cell.Y == 7) {
                    Message = Strings.TUTORIAL_MSG1;
                    step++;
                }
                break;

            case 1:
                if (player.Cell.X == 2 && player.Cell.Y == 7) {
                    Message = Strings.TUTORIAL_MSG2;
                    step++;
                }
                break;

            case 2:
                if (player.Cell.X == 2 && player.Cell.Y == 3) {
                    Message = Strings.TUTORIAL_MSG3;
                    step++;
                }
                break;

            case 3:
                if (player.Cell.X == 6 && player.Cell.Y == 3) {
                    Message = Strings.TUTORIAL_MSG4;
                    step++;
                }
                break;
        }
    }
}
