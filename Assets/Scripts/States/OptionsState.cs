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
/// This class represents the options state.
/// </summary>
public class OptionsState : GameState {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    private Texture2D buttonNormal;
    private Texture2D buttonHover;
    private Texture2D buttonActive;

    private Texture2D sliderBackground;
    private Texture2D sliderNormal;
    private Texture2D sliderHover;
    private Texture2D sliderActive;

    private Rect titleLblPos;
    private Rect soundLblPos;
    private Rect soundSldPos;
    private Rect musicLblPos;
    private Rect musicSldPos;
    private Rect acceptBtnPos;
    private Rect cancelBtnPos;

    private float soundVolume;
    private float musicVolume;

    private float savedSoundVolume;
    private float savedMusicVolume;

    private GUIStyle titleStyle;
    private GUIStyle textStyle;
    private GUIStyle sliderStyle;
    private GUIStyle thumbStyle;
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

        sliderBackground = core.LoadMenuTexture("Slider0");
        sliderNormal = core.LoadMenuTexture("Slider1");
        sliderHover = core.LoadMenuTexture("Slider2");
        sliderActive = core.LoadMenuTexture("Slider3");

        titleLblPos = getScaledValue(new Rect(INNER_BOX_X, 50, INNER_BOX_W, 50));

        const int SX1 = 264, SX2 = 504, SY1 = 200, SY2 = 350;
        int sw = sliderBackground.width, sh = sliderBackground.height;
        soundLblPos = getScaledValue(new Rect(SX1, SY1, SX2 - SX1, sh));
        soundSldPos = getScaledValue(new Rect(SX2, SY1, sw, sh));
        musicLblPos = getScaledValue(new Rect(SX1, SY2, SX2 - SX1, sh));
        musicSldPos = getScaledValue(new Rect(SX2, SY2, sw, sh));

        const int BX1 = 256, BX2 = 664, BY1 = 555;
        int bw = buttonNormal.width, bh = buttonNormal.height;
        acceptBtnPos = getScaledValue(new Rect(BX1, BY1, bw, bh));
        cancelBtnPos = getScaledValue(new Rect(BX2, BY1, bw, bh));

        savedSoundVolume = soundVolume = core.SoundVolume;
        savedMusicVolume = musicVolume = core.MusicVolume;

        titleStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.UpperCenter);
        textStyle = GuiUtil.MakeLabelStyle(FONT_SIZE_X2, AtariPalette.Hue00Lum00,
            TextAnchor.MiddleCenter);
        sliderStyle = GuiUtil.MakeHSliderStyle(sliderBackground);
        thumbStyle = GuiUtil.MakeHSliderThumbStyle(sliderNormal, sliderHover, sliderActive);
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
        GUI.Label(titleLblPos, Strings.OPTIONS_TITLE, titleStyle);

        // The sliders in the screen:
        GUI.Label(soundLblPos, Strings.OPTIONS_SOUND, textStyle);
        GUI.Label(musicLblPos, Strings.OPTIONS_MUSIC, textStyle);
        soundVolume = GUI.HorizontalSlider(soundSldPos, soundVolume, 0.0f, 1.0f, sliderStyle, thumbStyle);
        musicVolume = GUI.HorizontalSlider(musicSldPos, musicVolume, 0.0f, 1.0f, sliderStyle, thumbStyle);
        core.ChangeSoundOptions(soundVolume, musicVolume);

        // The buttons in the screen:
        if (GUI.Button(acceptBtnPos, Strings.SHARED_ACCEPT, buttonStyle)) {
            playButtonSound();
            core.SaveOptions(soundVolume, musicVolume);
            core.GotoMenu();
        }
        if (GUI.Button(cancelBtnPos, Strings.SHARED_CANCEL, buttonStyle)) {
            playButtonSound();
            core.ChangeSoundOptions(savedSoundVolume, savedMusicVolume);
            core.GotoMenu();
        }

        // Other stuff:
        showVersion();
    }
}
