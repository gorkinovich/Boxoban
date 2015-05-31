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
/// This class represents the victory state.
/// </summary>
public class VictoryState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect titleLblPos;
    private Rect msgLblPos;
    private Rect stepsLblPos;
    private Rect timeLblPos;
    private Rect continueBtnPos;

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
        core.LoadMenuScene();

        buttonNormal = core.LoadMenuTexture("Button1");
        buttonHover = core.LoadMenuTexture("Button2");
        buttonActive = core.LoadMenuTexture("Button3");

        const int Y0 = 170, Y1 = 320, Y2 = 380;
        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 80));
        msgLblPos = getScaledValue(new Rect(INNER_BOX_X, Y0, INNER_BOX_W, 100));
        stepsLblPos = getScaledValue(new Rect(INNER_BOX_X, Y1, INNER_BOX_W, 50));
        timeLblPos = getScaledValue(new Rect(INNER_BOX_X, Y2, INNER_BOX_W, 50));

        int w = buttonNormal.width, h = buttonNormal.height;
        continueBtnPos = getScaledValue(new Rect(460, 555, w, h));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X4, AtariPalette.Hue00Lum00,
            TextAnchor.UpperCenter);
        textStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.UpperCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "start" event of the interface controller.
    /// </summary>
    public override void OnStart() {
        if (core.IsMenuScene) {
            core.PlayVictoryMusic();
        }
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.LevelFinished();
            core.GotoSelectLevel();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, Strings.VICTORY_TITLE, titleStyle);

        // The message in the screen:
        GUI.Label(msgLblPos, Strings.VICTORY_MSG, textStyle);

        // The score in the screen:
        GUI.Label(stepsLblPos, Strings.VICTORY_STEPS + string.Format("{0,5:####0}", core.Steps), textStyle);
        GUI.Label(timeLblPos, Strings.VICTORY_TIME + string.Format("{0,5:####0}", core.Seconds), textStyle);

        // The buttons in the screen:
        if (GUI.Button(continueBtnPos, Strings.SHARED_CONTINUE, buttonStyle)) {
            playButtonSound();
            core.LevelFinished();
            core.GotoSelectLevel();
        }

        // Other stuff:
        showVersion();
    }
}
