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
/// This static class contains GUI utility functions.
/// </summary>
public static class GuiUtil {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    private const int BASE_HEIGHT = 720;
    private const int EMPTY_FONT_SIZE = 8;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Scale):
    //--------------------------------------------------------------------------------------

    #region int GetScaledValue(int)

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static int GetScaledValue(int victim) {
        if (Screen.height == BASE_HEIGHT) {
            return victim;
        } else {
            return (victim * Screen.height) / BASE_HEIGHT;
        }
    }

    #endregion

    #region float GetScaledValue(float)

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static float GetScaledValue(float victim) {
        if (Screen.height == BASE_HEIGHT) {
            return victim;
        } else {
            return (victim * Screen.height) / BASE_HEIGHT;
        }
    }

    #endregion

    #region Rect GetScaledValue(Rect)

    /// <summary>
    /// Gets a scaled value.
    /// </summary>
    /// <param name="victim">The value to scale.</param>
    /// <returns>The scaled value.</returns>
    public static Rect GetScaledValue(Rect victim) {
        if (Screen.height == BASE_HEIGHT) {
            return victim;
        } else {
            var sw169 = (Screen.height / 9.0f) * 16.0f;
            var xoffset = (sw169 - Screen.width) * 0.5f;
            return new Rect(
                GetScaledValue(victim.x) + xoffset,
                GetScaledValue(victim.y),
                GetScaledValue(victim.width),
                GetScaledValue(victim.height)
            );
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Font):
    //--------------------------------------------------------------------------------------

    #region void ChangeFont(GUIStyle, int, Color)

    /// <summary>
    /// Changes a style skin font.
    /// </summary>
    /// <param name="victim">The style to change.</param>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    public static void ChangeFont(GUIStyle victim, int fontSize, Color color) {
        victim.font = CoreManager.Instance.TextFont;
        victim.fontSize = GetScaledValue(fontSize);
        victim.normal.textColor = color;
        victim.hover.textColor = color;
        victim.active.textColor = color;
        victim.focused.textColor = color;
        victim.onNormal.textColor = color;
        victim.onHover.textColor = color;
        victim.onActive.textColor = color;
        victim.onFocused.textColor = color;
    }

    #endregion

    #region void ChangeFont(GUIStyle, int, Color, TextAnchor)

    /// <summary>
    /// Changes a style skin font.
    /// </summary>
    /// <param name="victim">The style to change.</param>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="alignment">The text aligment.</param>
    public static void ChangeFont(GUIStyle victim, int fontSize, Color color, TextAnchor alignment) {
        victim.font = CoreManager.Instance.TextFont;
        victim.fontSize = GetScaledValue(fontSize);
        victim.normal.textColor = color;
        victim.hover.textColor = color;
        victim.active.textColor = color;
        victim.focused.textColor = color;
        victim.onNormal.textColor = color;
        victim.onHover.textColor = color;
        victim.onActive.textColor = color;
        victim.onFocused.textColor = color;
        victim.alignment = alignment;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Textures):
    //--------------------------------------------------------------------------------------

    #region void ChangeTextures(GUIStyle, Texture2D)

    /// <summary>
    /// Changes a style skin textures.
    /// </summary>
    /// <param name="victim">The style to change.</param>
    /// <param name="background">The background texture.</param>
    public static void ChangeTextures(GUIStyle victim, Texture2D background) {
        victim.normal.background = background;
        victim.hover.background = background;
        victim.active.background = background;
        victim.focused.background = background;
        victim.onNormal.background = background;
        victim.onHover.background = background;
        victim.onActive.background = background;
        victim.onFocused.background = background;
        victim.fixedWidth = GetScaledValue((float)background.width);
        victim.fixedHeight = GetScaledValue((float)background.height);
    }

    #endregion

    #region void ChangeTextures(GUIStyle, Texture2D, Texture2D, Texture2D)

    /// <summary>
    /// Changes a style skin textures.
    /// </summary>
    /// <param name="victim">The style to change.</param>
    /// <param name="normal">The normal state texture.</param>
    /// <param name="hover">The hover state texture.</param>
    /// <param name="active">The active state texture.</param>
    public static void ChangeTextures(GUIStyle victim, Texture2D normal,
        Texture2D hover, Texture2D active) {
        victim.normal.background = normal;
        victim.hover.background = hover;
        victim.active.background = active;
        victim.focused.background = normal;
        victim.onNormal.background = normal;
        victim.onHover.background = hover;
        victim.onActive.background = active;
        victim.onFocused.background = normal;
        victim.fixedWidth = GetScaledValue((float)normal.width);
        victim.fixedHeight = GetScaledValue((float)normal.height);
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Styles):
    //--------------------------------------------------------------------------------------

    #region GUIStyle MakeLabelStyle(int, Color, TextAnchor)

    /// <summary>
    /// Makes a new label style.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="alignment">The text aligment.</param>
    public static GUIStyle MakeLabelStyle(int fontSize, Color color,
        TextAnchor alignment = TextAnchor.UpperLeft) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, fontSize, color, alignment);
        victim.wordWrap = true;
        return victim;
    }

    #endregion

    #region GUIStyle MakeButtonStyle(Texture2D, Texture2D, Texture2D)

    /// <summary>
    /// Makes a new button style.
    /// </summary>
    /// <param name="normal">The normal state texture.</param>
    /// <param name="hover">The hover state texture.</param>
    /// <param name="active">The active state texture.</param>
    public static GUIStyle MakeButtonStyle(Texture2D normal, Texture2D hover, Texture2D active) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, EMPTY_FONT_SIZE, AtariPalette.Hue00Lum00, TextAnchor.MiddleCenter);
        ChangeTextures(victim, normal, hover, active);
        return victim;
    }

    #endregion

    #region GUIStyle MakeButtonStyle(int, Color, Texture2D, Texture2D, Texture2D)

    /// <summary>
    /// Makes a new button style.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="normal">The normal state texture.</param>
    /// <param name="hover">The hover state texture.</param>
    /// <param name="active">The active state texture.</param>
    public static GUIStyle MakeButtonStyle(int fontSize, Color color, Texture2D normal,
        Texture2D hover, Texture2D active) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, fontSize, color, TextAnchor.MiddleCenter);
        ChangeTextures(victim, normal, hover, active);
        return victim;
    }

    #endregion

    #region GUIStyle MakeHSliderStyle(Texture2D)

    /// <summary>
    /// Makes a new horizontal slider style.
    /// </summary>
    /// <param name="background">The background texture.</param>
    public static GUIStyle MakeHSliderStyle(Texture2D background) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, EMPTY_FONT_SIZE, AtariPalette.Hue00Lum00, TextAnchor.MiddleCenter);
        ChangeTextures(victim, background);
        return victim;
    }

    #endregion

    #region GUIStyle MakeHSliderThumbStyle(Texture2D, Texture2D, Texture2D)

    /// <summary>
    /// Makes a new horizontal slider thumb style.
    /// </summary>
    /// <param name="normal">The normal state texture.</param>
    /// <param name="hover">The hover state texture.</param>
    /// <param name="active">The active state texture.</param>
    public static GUIStyle MakeHSliderThumbStyle(Texture2D normal, Texture2D hover, Texture2D active) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, EMPTY_FONT_SIZE, AtariPalette.Hue00Lum00, TextAnchor.MiddleCenter);
        ChangeTextures(victim, normal, hover, active);
        return victim;
    }

    #endregion

    #region GUIStyle MakeTextFieldStyle(int, Color, Texture2D)

    /// <summary>
    /// Makes a new text field style.
    /// </summary>
    /// <param name="fontSize">The size of the font.</param>
    /// <param name="color">The color of the font.</param>
    /// <param name="background">The background texture.</param>
    public static GUIStyle MakeTextFieldStyle(int fontSize, Color color, Texture2D background) {
        GUIStyle victim = new GUIStyle();
        ChangeFont(victim, fontSize, color, TextAnchor.MiddleCenter);
        ChangeTextures(victim, background);
        return victim;
    }

    #endregion
}
