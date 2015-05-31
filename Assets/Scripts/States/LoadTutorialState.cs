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
/// This class represents the load tutorial state.
/// </summary>
public class LoadTutorialState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D background;
    private Rect backTexPos;
    private Rect msgLblPos;

    private GUIStyle textStyle;

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the state.
    /// </summary>
    public override void Initialize() {
        background = core.LoadMenuTexture("BlackBackground");
        backTexPos = new Rect(0, 0, Screen.width, Screen.height);

        var height = GuiUtil.GetScaledValue(30);
        msgLblPos = new Rect(0, Screen.height - height, 500, height);

        core.LoadTutorialScene(true);
        core.HideMenuBackground();

        textStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum14);
    }

    /// <summary>
    /// This is called on the "start" event of the interface controller.
    /// </summary>
    public override void OnStart() {
        if (!core.IsTutorialScene) return;
        if (core.LoadLevel()) {
            Tutorial.Start();
            core.GotoTutorial();
        } else {
            Debug.LogError("Can't load the level.");
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        GUI.DrawTexture(backTexPos, background);
        GUI.Label(msgLblPos, Strings.SHARED_LOADING, textStyle);
    }
}
