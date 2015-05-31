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
/// This class represents the credits state.
/// </summary>
public class CreditsState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect titleLblPos;
    private Rect msgLblPos;
    private Rect returnBtnPos;

    private GUIStyle titleStyle;
    private GUIStyle textStyle;
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

        int w = buttonNormal.width, h = buttonNormal.height;
        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));
        msgLblPos = getScaledValue(new Rect(INNER_BOX_X, 100, INNER_BOX_W, 400));
        returnBtnPos = getScaledValue(new Rect(460, 555, w, h));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.UpperCenter);
        textStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoMenu();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, Strings.CREDITS_TITLE, titleStyle);

        // The message in the screen:
        GUI.Label(msgLblPos, Strings.CREDITS_MSG, textStyle);

        // The buttons in the screen:
        if (GUI.Button(returnBtnPos, Strings.SHARED_RETURN, buttonStyle)) {
            playButtonSound();
            core.GotoMenu();
        }

        // Other stuff:
        showVersion();
    }
}
