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

//#define SHOW_VERSION

using UnityEngine;

/// <summary>
/// This abstract class represents a generic game state.
/// </summary>
public abstract class GameState : IGameState {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    protected const int FONT_SIZE_X1 = 16;
    protected const int FONT_SIZE_X2 = 32;
    protected const int FONT_SIZE_X4 = 64;

    protected static readonly Rect BACKGROUND_RECT = new Rect(0, 0, 1280, 720);

    protected const int INNER_BOX_X = 192;
    protected const int INNER_BOX_Y = 36;
    protected const int INNER_BOX_W = 896;
    protected const int INNER_BOX_H = 648;

    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// The core manager of the game.
    /// </summary>
    protected CoreManager core = CoreManager.Instance;

    /// <summary>
    /// Gets the audio source of the menu.
    /// </summary>
    protected AudioSource audio {
        get {
            var menuBackground = GameObject.Find(CoreManager.MNUBACK_GOBJ_NAME);
            if (menuBackground != null) {
                return menuBackground.GetComponent<AudioSource>();
            }
            return null;
        }
    }

    //--------------------------------------------------------------------------------------
    // Methods (Scale):
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    protected int getScaledValue(int victim) {
        return GuiUtil.GetScaledValue(victim);
    }

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    protected float getScaledValue(float victim) {
        return GuiUtil.GetScaledValue(victim);
    }

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    protected Rect getScaledValue(Rect victim) {
        return GuiUtil.GetScaledValue(victim);
    }

    //--------------------------------------------------------------------------------------
    // Methods (GUI):
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Changes the label skin font.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="alignment">The text aligment.</param>
    protected void changeLabelFont(int fontSize, Color color,
        TextAnchor alignment = TextAnchor.UpperLeft) {
        GuiUtil.ChangeFont(GUI.skin.label, fontSize, color, alignment);
    }

    /// <summary>
    /// Changes the button skin font.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    protected void changeButtonFont(int fontSize, Color color) {
        GuiUtil.ChangeFont(GUI.skin.button, fontSize, color);
    }

    /// <summary>
    /// Changes the buton skin font & textures.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="normal">The normal state texture.</param>
    /// <param name="hover">The hover state texture.</param>
    /// <param name="active">The active state texture.</param>
    protected void changeButtonTextures(int fontSize, Color color,
        Texture2D normal, Texture2D hover, Texture2D active) {
        GuiUtil.ChangeFont(GUI.skin.button, fontSize, color);
        GuiUtil.ChangeTextures(GUI.skin.button, normal, hover, active);
    }

    /// <summary>
    /// Calculates the centered rectangle of a label.
    /// </summary>
    /// <param name="victim">The original rectangle.</param>
    /// <param name="msg">The text of the label.</param>
    /// <returns>The centered rectangle.</returns>
    protected Rect calcCenteredLabel(ref Rect victim, string msg) {
        var size = GUI.skin.label.CalcSize(new GUIContent(msg));
        return new Rect((Screen.width - size.x) * 0.5f,
            victim.y, size.x, size.y);
    }

    /// <summary>
    /// Shows the version of the game.
    /// </summary>
    protected void showVersion() {
#if SHOW_VERSION
        var height = GuiUtil.GetScaledValue(30);
        GuiUtil.ChangeFont(GUI.skin.label, FONT_SIZE_X1, AtariPalette.Hue01Lum12, TextAnchor.UpperLeft);
        GUI.Label(new Rect(0, Screen.height - height, 500, height), Strings.VERSION);
#endif
    }

    //--------------------------------------------------------------------------------------
    // Methods (audio):
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Plays the button sound.
    /// </summary>
    protected void playButtonSound() {
        var controller = audio;
        if (controller != null) {
            controller.volume = core.SoundVolume;
            controller.Play();
        }
    }

    //--------------------------------------------------------------------------------------
    // Methods (IGameState):
    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Initializes the state.
    /// </summary>
    public virtual void Initialize() {
    }

    /// <summary>
    /// Releases the state.
    /// </summary>
    public virtual void Release() {
    }

    /// <summary>
    /// This is called on the "start" event of the interface controller.
    /// </summary>
    public virtual void OnStart() {
    }

    /// <summary>
    /// This is called on the "update" event of the interface controller.
    /// </summary>
    public virtual void OnUpdate() {
    }

    /// <summary>
    /// This is called on the "GUI" event of the interface controller.
    /// </summary>
    public virtual void OnGUI() {
    }
}
