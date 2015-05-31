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
/// This class represents the pause tutorial state.
/// </summary>
public class ExitTutorialState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect msgLblPos;
    private Rect yesBtnPos;
    private Rect noBtnPos;

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

        msgLblPos = getScaledValue(new Rect(INNER_BOX_X, INNER_BOX_Y, INNER_BOX_W, 430));

        const int X1 = 256, X2 = 664, Y2 = 474;
        int w = buttonNormal.width, h = buttonNormal.height;
        yesBtnPos = getScaledValue(new Rect(X1, Y2, w, h));
        noBtnPos = getScaledValue(new Rect(X2, Y2, w, h));

        textStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoTutorial();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The message in the screen:
        GUI.Label(msgLblPos, Strings.EXIT_TUTORIAL_MSG, textStyle);

        // The buttons in the screen:
        if (GUI.Button(yesBtnPos, Strings.SHARED_YES, buttonStyle)) {
            playButtonSound();
            core.LoadMenuScene();
            core.GotoMenu();
        }
        if (GUI.Button(noBtnPos, Strings.SHARED_NO, buttonStyle)) {
            playButtonSound();
            core.GotoTutorial();
        }

        // Other stuff:
        showVersion();
    }
}
