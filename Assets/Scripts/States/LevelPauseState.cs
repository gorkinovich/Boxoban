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
/// This class represents the pause level state.
/// </summary>
public class LevelPauseState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect titleLblPos;
    private Rect continueBtnPos;
    private Rect resetBtnPos;
    private Rect exitBtnPos;

    private GUIStyle titleStyle;
    private GUIStyle buttonStyle;

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the state.
    /// </summary>
    public override void Initialize() {
        buttonNormal = core.LoadMenuTexture("Button1");
        buttonHover = core.LoadMenuTexture("Button2");
        buttonActive = core.LoadMenuTexture("Button3");

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 80, INNER_BOX_W, 120));

        const int X2 = 460, Y1 = 231, Y2 = 383, Y3 = 535;
        int w = buttonNormal.width, h = buttonNormal.height;
        continueBtnPos = getScaledValue(new Rect(X2, Y1, w, h));
        resetBtnPos = getScaledValue(new Rect(X2, Y2, w, h));
        exitBtnPos = getScaledValue(new Rect(X2, Y3, w, h));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoLevel();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, core.LevelDescriptor.Name, titleStyle);

        // The buttons in the screen:
        if (GUI.Button(continueBtnPos, Strings.SHARED_CONTINUE, buttonStyle)) {
            playButtonSound();
            core.GotoLevel();
        }
        if (GUI.Button(resetBtnPos, Strings.LEVEL_PAUSE_RESET, buttonStyle)) {
            playButtonSound();
            core.GotoResetLevel();
        }
        if (GUI.Button(exitBtnPos, Strings.LEVEL_PAUSE_EXIT, buttonStyle)) {
            playButtonSound();
            core.GotoExitLevel();
        }

        // Other stuff:
        showVersion();
    }
}
