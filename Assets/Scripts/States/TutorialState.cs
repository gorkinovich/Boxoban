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
/// This class represents the play tutorial state.
/// </summary>
public class TutorialState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D menuBarLeft;
    private Texture2D menuBarMiddle;
    private Texture2D menuBarRight;

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect menuBarLeftPos;
    private Rect menuBarMiddlePos;
    private Rect menuBarRightPos;

    private Rect pauseBtnPos;
    private Rect msgLblPos;

    private GUIStyle buttonStyle;
    private GUIStyle msgStyle;

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the state.
    /// </summary>
    public override void Initialize() {
        menuBarLeft = core.LoadMenuTexture("MenuBar1");
        menuBarMiddle = core.LoadMenuTexture("MenuBar2");
        menuBarRight = core.LoadMenuTexture("MenuBar3");

        buttonNormal = core.LoadMenuTexture("PauseButton1");
        buttonHover = core.LoadMenuTexture("PauseButton2");
        buttonActive = core.LoadMenuTexture("PauseButton3");

        menuBarLeftPos = getScaledValue(new Rect(0, 0, menuBarLeft.width, menuBarLeft.height));
        menuBarMiddlePos = getScaledValue(new Rect(0, 0, menuBarMiddle.width, menuBarMiddle.height));
        menuBarRightPos = getScaledValue(new Rect(0, 0, menuBarRight.width, menuBarRight.height));

        menuBarMiddlePos.x = menuBarLeftPos.x + menuBarLeftPos.width;
        menuBarRightPos.x = Screen.width - menuBarRightPos.width;
        menuBarMiddlePos.width = menuBarRightPos.x - menuBarMiddlePos.x;

        int w = buttonNormal.width, h = buttonNormal.height;
        pauseBtnPos = getScaledValue(new Rect(16, 16, w, h));

        msgLblPos = new Rect();
        msgLblPos.x = menuBarMiddlePos.x;
        msgLblPos.width = Screen.width - msgLblPos.x;
        msgLblPos.height = menuBarMiddlePos.height;

        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
        msgStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoTutorialExit();
        } else {
            Tutorial.Update();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The menu bar in the screen:
        GUI.DrawTexture(menuBarLeftPos, menuBarLeft);
        GUI.DrawTexture(menuBarMiddlePos, menuBarMiddle, ScaleMode.StretchToFill);
        GUI.DrawTexture(menuBarRightPos, menuBarRight);

        // The buttons in the screen:
        if (GUI.Button(pauseBtnPos, "", buttonStyle)) {
            playButtonSound();
            core.GotoTutorialExit();
        }

        // The message in the screen:
        GUI.Label(msgLblPos, Tutorial.Message, msgStyle);

        // Other stuff:
        showVersion();
    }
}
