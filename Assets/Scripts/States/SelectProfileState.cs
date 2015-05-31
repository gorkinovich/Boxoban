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
/// This class represents the select profile state.
/// </summary>
public class SelectProfileState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Texture2D profileButtonNormal;
    private Texture2D profileButtonHover;
    private Texture2D profileButtonActive;

    private Texture2D deleteButtonNormal;
    private Texture2D deleteButtonHover;
    private Texture2D deleteButtonActive;

    private Texture2D[] portraits;

    private Rect titleLblPos;
    private Rect[] namesLblPos;
    private Rect[] profileBtnPos;
    private Rect[] portraitsPos;
    private Rect[] deleteBtnPos;
    private Rect returnBtnPos;

    private GUIStyle titleStyle;
    private GUIStyle nameStyle;
    private GUIStyle profileButtonStyle;
    private GUIStyle deleteButtonStyle;
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

        profileButtonNormal = core.LoadMenuTexture("ProfileButton1");
        profileButtonHover = core.LoadMenuTexture("ProfileButton2");
        profileButtonActive = core.LoadMenuTexture("ProfileButton3");

        deleteButtonNormal = core.LoadMenuTexture("DeleteButton1");
        deleteButtonHover = core.LoadMenuTexture("DeleteButton2");
        deleteButtonActive = core.LoadMenuTexture("DeleteButton3");

        portraits = new Texture2D[ProfileData.MAX_AVATARS];
        for (int i = 0; i < ProfileData.MAX_AVATARS; i++) {
            portraits[i] = core.LoadMenuTexture("Portrait" + (i + 1));
        }

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));

        int pw = profileButtonNormal.width, ph = profileButtonNormal.height, th = 40;
        const int NXO = 220, NY = 170; int nx = 204;
        namesLblPos = new Rect[CoreManager.MAX_PROFILES];
        namesLblPos[0] = getScaledValue(new Rect(nx, NY, pw, th)); nx += NXO;
        namesLblPos[1] = getScaledValue(new Rect(nx, NY, pw, th)); nx += NXO;
        namesLblPos[2] = getScaledValue(new Rect(nx, NY, pw, th)); nx += NXO;
        namesLblPos[3] = getScaledValue(new Rect(nx, NY, pw, th));

        const int PXO = 220, PY = 220; int px = 204;
        profileBtnPos = new Rect[CoreManager.MAX_PROFILES];
        profileBtnPos[0] = getScaledValue(new Rect(px, PY, pw, ph)); px += PXO;
        profileBtnPos[1] = getScaledValue(new Rect(px, PY, pw, ph)); px += PXO;
        profileBtnPos[2] = getScaledValue(new Rect(px, PY, pw, ph)); px += PXO;
        profileBtnPos[3] = getScaledValue(new Rect(px, PY, pw, ph));

        int p2w = portraits[0].width, p2h = portraits[0].height;
        const int P2Y = 244; int p2x = 228;
        portraitsPos = new Rect[CoreManager.MAX_PROFILES];
        portraitsPos[0] = getScaledValue(new Rect(p2x, P2Y, p2w, p2h)); p2x += PXO;
        portraitsPos[1] = getScaledValue(new Rect(p2x, P2Y, p2w, p2h)); p2x += PXO;
        portraitsPos[2] = getScaledValue(new Rect(p2x, P2Y, p2w, p2h)); p2x += PXO;
        portraitsPos[3] = getScaledValue(new Rect(p2x, P2Y, p2w, p2h));

        int dw = deleteButtonNormal.width, dh = deleteButtonNormal.height;
        const int DXO = 220, DY = 442; int dx = 262;
        deleteBtnPos = new Rect[CoreManager.MAX_PROFILES];
        deleteBtnPos[0] = getScaledValue(new Rect(dx, DY, dw, dh)); dx += DXO;
        deleteBtnPos[1] = getScaledValue(new Rect(dx, DY, dw, dh)); dx += DXO;
        deleteBtnPos[2] = getScaledValue(new Rect(dx, DY, dw, dh)); dx += DXO;
        deleteBtnPos[3] = getScaledValue(new Rect(dx, DY, dw, dh));

        int bw = buttonNormal.width, bh = buttonNormal.height;
        returnBtnPos = getScaledValue(new Rect(460, 555, bw, bh));

        core.SelectedProfile = CoreManager.NO_PROFILE;

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        nameStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        profileButtonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            profileButtonNormal, profileButtonHover, profileButtonActive);
        deleteButtonStyle = GuiUtil.MakeButtonStyle(FONT_SIZE_X1, AtariPalette.Hue00Lum00,
            deleteButtonNormal, deleteButtonHover, deleteButtonActive);
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
        GUI.Label(titleLblPos, Strings.SELECT_PROFILE_TITLE, titleStyle);

        // The profile's buttons in the screen:
        for (int i = 0; i < CoreManager.MAX_PROFILES; i++) {
            if (core.UserProfiles[i].Empty) {
                // An empty profile:
                GUI.Label(namesLblPos[i], "???", nameStyle);
                if (GUI.Button(profileBtnPos[i], "", profileButtonStyle)) {
                    playButtonSound();
                    core.SelectedProfile = i;
                    core.GotoCreateProfile();
                }
            } else {
                // An user profile:
                GUI.Label(namesLblPos[i], core.UserProfiles[i].Name, nameStyle);
                if (GUI.Button(profileBtnPos[i], "", profileButtonStyle)) {
                    playButtonSound();
                    core.SelectedProfile = i;
                    core.GotoSelectChapter();
                }
                var avatar = core.UserProfiles[i].Avatar;
                if (0 <= avatar && avatar < portraits.Length) {
                    GUI.DrawTexture(portraitsPos[i], portraits[avatar]);
                }
                if (GUI.Button(deleteBtnPos[i], "", deleteButtonStyle)) {
                    playButtonSound();
                    core.SelectedProfile = i;
                    core.GotoDeleteProfile();
                }
            }
        }

        // The buttons in the screen:
        if (GUI.Button(returnBtnPos, Strings.SHARED_RETURN, buttonStyle)) {
            playButtonSound();
            core.GotoMenu();
        }

        // Other stuff:
        showVersion();
    }
}
