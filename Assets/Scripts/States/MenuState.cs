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
/// This class represents the game menu state.
/// </summary>
public class MenuState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Rect titleLblPos;
    private Rect playBtnPos;
    private Rect tutorialBtnPos;
    private Rect optionsBtnPos;
    private Rect creditsBtnPos;
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

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 81, INNER_BOX_W, 80));

        const int X1 = 256, X2 = 460, X3 = 664;
        const int Y1 = 231, Y2 = 393, Y3 = 555;
        int w = buttonNormal.width, h = buttonNormal.height;
        playBtnPos = getScaledValue(new Rect(X1, Y1, w, h));
        tutorialBtnPos = getScaledValue(new Rect(X3, Y1, w, h));
        optionsBtnPos = getScaledValue(new Rect(X1, Y2, w, h));
        creditsBtnPos = getScaledValue(new Rect(X3, Y2, w, h));
        exitBtnPos = getScaledValue(new Rect(X2, Y3, w, h));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X4, AtariPalette.Hue00Lum00,
            TextAnchor.UpperCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "start" event of the interface controller.
    /// </summary>
    public override void OnStart() {
        if (core.IsMenuScene) {
            core.PlayMainMusic();
        }
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (!core.IsMusicPlaying() && core.IsMenuScene) {
            core.PlayMainMusic();
        }
#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoExit();
        }
#endif
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, Strings.MENU_TITLE, titleStyle);

        // The buttons in the screen:
        if (GUI.Button(playBtnPos, Strings.MENU_PLAY, buttonStyle)) {
            playButtonSound();
            core.GotoSelectProfile();
        }
        if (GUI.Button(tutorialBtnPos, Strings.MENU_TUTORIAL, buttonStyle)) {
            playButtonSound();
            core.GotoLoadTutorial();
        }
        if (GUI.Button(optionsBtnPos, Strings.MENU_OPTIONS, buttonStyle)) {
            playButtonSound();
            core.GotoOptions();
        }
        if (GUI.Button(creditsBtnPos, Strings.MENU_CREDITS, buttonStyle)) {
            playButtonSound();
            core.GotoCredits();
        }
#if UNITY_STANDALONE
        if (GUI.Button(exitBtnPos, Strings.MENU_EXIT, buttonStyle)) {
            playButtonSound();
            core.GotoExit();
        }
#endif

        // Other stuff:
        showVersion();
    }
}
