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
/// This class represents the select level state.
/// </summary>
public class SelectLevelState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Texture2D itemNotActive;
    private Texture2D itemButtonNormal;
    private Texture2D itemButtonHover;
    private Texture2D itemButtonActive;

    private Rect titleLblPos;
    private Rect[] itemBtnPos;
    private Rect returnBtnPos;

    private string[] itemText;

    private GUIStyle titleStyle;
    private GUIStyle itemButtonStyle;
    private GUIStyle itemTextStyle;
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

        itemNotActive = core.LoadMenuTexture("ItemButton0");
        itemButtonNormal = core.LoadMenuTexture("ItemButton1");
        itemButtonHover = core.LoadMenuTexture("ItemButton2");
        itemButtonActive = core.LoadMenuTexture("ItemButton3");

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));

        int iw = itemButtonNormal.width, ih = itemButtonNormal.height;
        const int BX = 236, BY = 164, OX = 170, OY = 192;
        itemBtnPos = new Rect[CoreManager.MAX_ITEMS];
        itemText = new string[CoreManager.MAX_ITEMS];
        var destination = new Rect(BX, BY, iw, ih);
        for (int i = 0, k = 0; i < CoreManager.MAX_ITEMS_ROWS; i++) {
            for (int j = 0; j < CoreManager.MAX_ITEMS_COLS; j++, k++) {
                itemText[k] = (k + 1).ToString();
                itemBtnPos[k] = getScaledValue(destination);
                destination.x += OX;
            }
            destination.x = BX;
            destination.y += OY;
        }

        int bw = buttonNormal.width, bh = buttonNormal.height;
        returnBtnPos = getScaledValue(new Rect(460, 555, bw, bh));

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        itemButtonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            itemButtonNormal, itemButtonHover, itemButtonActive);
        itemTextStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoSelectChapter();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // The title in the screen:
        GUI.Label(titleLblPos, core.CurrentChapter.Name, titleStyle);

        // The chapter's buttons in the screen:
        var lastChapterUnlocked = core.CurrentProfile != null ?
            core.CurrentProfile.LastChapterUnlocked : -1;
        var lastLevelUnlocked = core.CurrentProfile != null ?
            core.CurrentProfile.LastLevelUnlocked : -1;
        for (int i = 0; i < CoreManager.MAX_ITEMS && i < core.Chapters.Length; i++) {
            if (core.SelectedChapter < lastChapterUnlocked ||
                (core.SelectedChapter == lastChapterUnlocked && i <= lastLevelUnlocked)) {
                if (GUI.Button(itemBtnPos[i], itemText[i], itemButtonStyle)) {
                    playButtonSound();
                    core.SelectedLevel = i;
                    core.GotoLevelDescription();
                }
            } else {
                GUI.DrawTexture(itemBtnPos[i], itemNotActive);
                GUI.Label(itemBtnPos[i], itemText[i], itemTextStyle);
            }
        }

        // The buttons in the screen:
        if (GUI.Button(returnBtnPos, Strings.SHARED_RETURN, buttonStyle)) {
            playButtonSound();
            core.GotoSelectChapter();
        }

        // Other stuff:
        showVersion();
    }
}
