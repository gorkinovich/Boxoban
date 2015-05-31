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
/// This class represents the create profile state.
/// </summary>
public class CreateProfileState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Texture2D leftButtonNormal;
    private Texture2D leftButtonHover;
    private Texture2D leftButtonActive;

    private Texture2D rightButtonNormal;
    private Texture2D rightButtonHover;
    private Texture2D rightButtonActive;

    private Texture2D portraitFrame;
    private Texture2D[] portraits;
    private Texture2D textField;

    private Rect titleLblPos;
    private Rect portraitFramePos;
    private Rect portraitPos;
    private Rect leftBtnPos;
    private Rect rightBtnPos;
    private Rect nameTxtPos;
    private Rect createBtnPos;
    private Rect cancelBtnPos;

    private ProfileData victim;

    private GUIStyle titleStyle;
    private GUIStyle leftButtonStyle;
    private GUIStyle rightButtonStyle;
    private GUIStyle textFieldStyle;
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

        leftButtonNormal = core.LoadMenuTexture("LeftButton1");
        leftButtonHover = core.LoadMenuTexture("LeftButton2");
        leftButtonActive = core.LoadMenuTexture("LeftButton3");

        rightButtonNormal = core.LoadMenuTexture("RightButton1");
        rightButtonHover = core.LoadMenuTexture("RightButton2");
        rightButtonActive = core.LoadMenuTexture("RightButton3");

        portraitFrame = core.LoadMenuTexture("PortaitFrame");
        portraits = new Texture2D[ProfileData.MAX_AVATARS];
        for (int i = 0; i < ProfileData.MAX_AVATARS; i++) {
            portraits[i] = core.LoadMenuTexture("Portrait" + (i + 1));
        }
        textField = core.LoadMenuTexture("TextField");

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));

        int pfw = portraitFrame.width, pfh = portraitFrame.height;
        int pw = portraits[0].width, ph = portraits[0].height;
        const int PFX = 534, PFY = 200, POF = 24;
        portraitFramePos = getScaledValue(new Rect(PFX, PFY, pfw, pfh));
        portraitPos = getScaledValue(new Rect(PFX + POF, PFY + POF, pw, ph));

        const int LX = 418, RX = 766, LRY = PFY + 58;
        int lbw = leftButtonNormal.width, lbh = leftButtonNormal.height;
        int rbw = rightButtonNormal.width, rbh = rightButtonNormal.height;
        leftBtnPos = getScaledValue(new Rect(LX, LRY, lbw, lbh));
        rightBtnPos = getScaledValue(new Rect(RX, LRY, rbw, rbh));

        nameTxtPos = getScaledValue(new Rect(240, 430, textField.width, textField.height));

        const int X1 = 256, X2 = 664, Y2 = 555;
        int w = buttonNormal.width, h = buttonNormal.height;
        createBtnPos = getScaledValue(new Rect(X1, Y2, w, h));
        cancelBtnPos = getScaledValue(new Rect(X2, Y2, w, h));

        victim = new ProfileData();
        victim.Empty = false;

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        leftButtonStyle = GuiUtil.MakeButtonStyle(leftButtonNormal, leftButtonHover,
            leftButtonActive);
        rightButtonStyle = GuiUtil.MakeButtonStyle(rightButtonNormal, rightButtonHover,
            rightButtonActive);
        textFieldStyle = GuiUtil.MakeTextFieldStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            textField);
        buttonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            buttonNormal, buttonHover, buttonActive);
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            core.GotoSelectProfile();
        }
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public override void OnGUI() {
        // Change some settings of the GUI:
        GUI.skin.settings.cursorColor = AtariPalette.Hue00Lum00;
        GUI.skin.settings.selectionColor = AtariPalette.Hue01Lum12;

        // The title in the screen:
        GUI.Label(titleLblPos, Strings.CREATE_PROFILE_TITLE, titleStyle);

        // The profile's portait in the screen:
        GUI.DrawTexture(portraitFramePos, portraitFrame);
        if (0 <= victim.Avatar && victim.Avatar < portraits.Length) {
            GUI.DrawTexture(portraitPos, portraits[victim.Avatar]);
        }
        if (GUI.Button(leftBtnPos, "", leftButtonStyle)) {
            playButtonSound();
            victim.Avatar--;
        }
        if (GUI.Button(rightBtnPos, "", rightButtonStyle)) {
            playButtonSound();
            victim.Avatar++;
        }

        // The profile's name in the screen:
        victim.Name = GUI.TextField(nameTxtPos, victim.Name, textFieldStyle);

        // The buttons in the screen:
        if (GUI.Button(createBtnPos, Strings.CREATE_PROFILE_CREATE, buttonStyle)) {
            playButtonSound();
            core.AssignCurrentProfile(victim);
            core.GotoSelectProfile();
        }
        if (GUI.Button(cancelBtnPos, Strings.SHARED_CANCEL, buttonStyle)) {
            playButtonSound();
            core.GotoSelectProfile();
        }

        // Other stuff:
        showVersion();
    }
}
