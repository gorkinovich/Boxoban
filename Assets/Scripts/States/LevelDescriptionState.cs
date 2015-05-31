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
using System;
using UnityEngine;

/// <summary>
/// This class represents the level description state.
/// </summary>
public class LevelDescriptionState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Texture2D preview;

    private Rect titleLblPos;
    private Rect previewPos;
    private Rect descriptionLblPos;
    private Rect scoreLblPos;
    private Rect playBtnPos;
    private Rect returnBtnPos;

    private GUIStyle titleStyle;
    private GUIStyle descriptionStyle;
    private GUIStyle scoreStyle;
    private GUIStyle buttonStyle;

    string score;

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

        preview = core.MakePreview();

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));
        previewPos = getScaledValue(new Rect(230, 120, 300, 350));
        descriptionLblPos = getScaledValue(new Rect(550, 120, 500, 350));
        scoreLblPos = getScaledValue(new Rect(INNER_BOX_X, 486, INNER_BOX_W, 40));

        const int BX1 = 256, BX2 = 664, BY1 = 555;
        int bw = buttonNormal.width, bh = buttonNormal.height;
        playBtnPos = getScaledValue(new Rect(BX1, BY1, bw, bh));
        returnBtnPos = getScaledValue(new Rect(BX2, BY1, bw, bh));

        var stepsScore = core.CurrentProfile.GetSteps(core.SelectedChapter, core.SelectedLevel);
        var secondsScore = core.CurrentProfile.GetSeconds(core.SelectedChapter, core.SelectedLevel);
        Func<int, string> getScoreString = (victim) => {
            return victim != ProfileData.NO_SCORE ? victim.ToString() : "---";
        };

        score = string.Format(Strings.LEVEL_DESCRIPTION_SCORE,
            Strings.LEVEL_DESCRIPTION_STEPS, getScoreString(stepsScore),
            Strings.LEVEL_DESCRIPTION_TIME, getScoreString(secondsScore));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        descriptionStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        scoreStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoSelectLevel();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, core.LevelDescriptor.Name, titleStyle);

        // The description in the screen:
        GUI.DrawTexture(previewPos, preview, ScaleMode.ScaleToFit);
        GUI.Label(descriptionLblPos, core.LevelDescriptor.Description, descriptionStyle);
        GUI.Label(scoreLblPos, score, scoreStyle);

        // The buttons in the screen:
        if (GUI.Button(playBtnPos, Strings.LEVEL_DESCRIPTION_PLAY, buttonStyle)) {
            playButtonSound();
            core.GotoLoadLevel();
        }
        if (GUI.Button(returnBtnPos, Strings.SHARED_RETURN, buttonStyle)) {
            playButtonSound();
            core.GotoSelectLevel();
        }

        // Other stuff:
        showVersion();
    }
}
