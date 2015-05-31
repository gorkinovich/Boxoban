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

/*
 * Atari TIA (Atari 2600)
 * hue / luminance
 *
 *       00      02      04      06      08      10      12      14
 * 00: 000000, 404040, 6C6C6C, 909090, B0B0B0, C8C8C8, DCDCDC, ECECEC
 * 01: 444400, 646410, 848424, A0A034, B8B840, D0D050, E8E85C, FCFC68
 * 02: 702800, 844414, 985C28, AC783C, BC8C4C, CCA05C, DCB468, ECC878
 * 03: 841800, 983418, AC5030, C06848, D0805C, E09470, ECA880, FCBC94
 * 04: 880000, 9C2020, B03C3C, C05858, D07070, E08888, ECA0A0, FCB4B4
 * 05: 78005C, 8C2074, A03C88, B0589C, C070B0, D084C0, DC9CD0, ECB0E0
 * 06: 480078, 602090, 783CA4, 8C58B8, A070CC, B484DC, C49CEC, D4B0FC
 * 07: 140084, 302098, 4C3CAC, 6858C0, 7C70D0, 9488E0, A8A0EC, BCB4FC
 * 08: 000088, 1C209C, 3840B0, 505CC0, 6874D0, 7C8CE0, 90A4EC, A4B8FC
 * 09: 00187C, 1C3890, 3854A8, 5070BC, 6888CC, 7C9CDC, 90B4EC, A4C8FC
 * 10: 002C5C, 1C4C78, 386890, 5084AC, 689CC0, 7CB4D4, 90CCE8, A4E0FC
 * 11: 003C2C, 1C5C48, 387C64, 509C80, 68B494, 7CD0AC, 90E4C0, A4FCD4
 * 12: 003C00, 205C20, 407C40, 5C9C5C, 74B474, 8CD08C, A4E4A4, B8FCB8
 * 13: 143800, 345C1C, 507C38, 6C9850, 84B468, 9CCC7C, B4E490, C8FCA4
 * 14: 2C3000, 4C501C, 687034, 848C4C, 9CA864, B4C078, CCD488, E0EC9C
 * 15: 442800, 644818, 846830, A08444, B89C58, D0B46C, E8CC7C, FCE08C
 */

/// <summary>
/// This static class contains the Atari 2600 palette colors.
/// </summary>
public static class AtariPalette {
    //--------------------------------------------------------------------------------------
    // Colors:
    //--------------------------------------------------------------------------------------

    #region Colors

    // 000000, 404040, 6C6C6C, 909090, B0B0B0, C8C8C8, DCDCDC, ECECEC
    public static readonly Color Hue00Lum00 = new Color(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue00Lum02 = new Color(0x40 / 255.0f, 0x40 / 255.0f, 0x40 / 255.0f);
    public static readonly Color Hue00Lum04 = new Color(0x6C / 255.0f, 0x6C / 255.0f, 0x6C / 255.0f);
    public static readonly Color Hue00Lum06 = new Color(0x90 / 255.0f, 0x90 / 255.0f, 0x90 / 255.0f);
    public static readonly Color Hue00Lum08 = new Color(0xB0 / 255.0f, 0xB0 / 255.0f, 0xB0 / 255.0f);
    public static readonly Color Hue00Lum10 = new Color(0xC8 / 255.0f, 0xC8 / 255.0f, 0xC8 / 255.0f);
    public static readonly Color Hue00Lum12 = new Color(0xDC / 255.0f, 0xDC / 255.0f, 0xDC / 255.0f);
    public static readonly Color Hue00Lum14 = new Color(0xEC / 255.0f, 0xEC / 255.0f, 0xEC / 255.0f);

    // 444400, 646410, 848424, A0A034, B8B840, D0D050, E8E85C, FCFC68
    public static readonly Color Hue01Lum00 = new Color(0x44 / 255.0f, 0x44 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue01Lum02 = new Color(0x64 / 255.0f, 0x64 / 255.0f, 0x10 / 255.0f);
    public static readonly Color Hue01Lum04 = new Color(0x84 / 255.0f, 0x84 / 255.0f, 0x24 / 255.0f);
    public static readonly Color Hue01Lum06 = new Color(0xA0 / 255.0f, 0xA0 / 255.0f, 0x34 / 255.0f);
    public static readonly Color Hue01Lum08 = new Color(0xB8 / 255.0f, 0xB8 / 255.0f, 0x40 / 255.0f);
    public static readonly Color Hue01Lum10 = new Color(0xD0 / 255.0f, 0xD0 / 255.0f, 0x50 / 255.0f);
    public static readonly Color Hue01Lum12 = new Color(0xE8 / 255.0f, 0xE8 / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue01Lum14 = new Color(0xFC / 255.0f, 0xFC / 255.0f, 0x68 / 255.0f);

    // 702800, 844414, 985C28, AC783C, BC8C4C, CCA05C, DCB468, ECC878
    public static readonly Color Hue02Lum00 = new Color(0x70 / 255.0f, 0x28 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue02Lum02 = new Color(0x84 / 255.0f, 0x44 / 255.0f, 0x14 / 255.0f);
    public static readonly Color Hue02Lum04 = new Color(0x98 / 255.0f, 0x5C / 255.0f, 0x28 / 255.0f);
    public static readonly Color Hue02Lum06 = new Color(0xAC / 255.0f, 0x78 / 255.0f, 0x3C / 255.0f);
    public static readonly Color Hue02Lum08 = new Color(0xBC / 255.0f, 0x8C / 255.0f, 0x4C / 255.0f);
    public static readonly Color Hue02Lum10 = new Color(0xCC / 255.0f, 0xA0 / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue02Lum12 = new Color(0xDC / 255.0f, 0xB4 / 255.0f, 0x68 / 255.0f);
    public static readonly Color Hue02Lum14 = new Color(0xEC / 255.0f, 0xC8 / 255.0f, 0x78 / 255.0f);

    // 841800, 983418, AC5030, C06848, D0805C, E09470, ECA880, FCBC94
    public static readonly Color Hue03Lum00 = new Color(0x84 / 255.0f, 0x18 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue03Lum02 = new Color(0x98 / 255.0f, 0x34 / 255.0f, 0x18 / 255.0f);
    public static readonly Color Hue03Lum04 = new Color(0xAC / 255.0f, 0x50 / 255.0f, 0x30 / 255.0f);
    public static readonly Color Hue03Lum06 = new Color(0xC0 / 255.0f, 0x68 / 255.0f, 0x48 / 255.0f);
    public static readonly Color Hue03Lum08 = new Color(0xD0 / 255.0f, 0x80 / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue03Lum10 = new Color(0xE0 / 255.0f, 0x94 / 255.0f, 0x70 / 255.0f);
    public static readonly Color Hue03Lum12 = new Color(0xEC / 255.0f, 0xA8 / 255.0f, 0x80 / 255.0f);
    public static readonly Color Hue03Lum14 = new Color(0xFC / 255.0f, 0xBC / 255.0f, 0x94 / 255.0f);

    // 880000, 9C2020, B03C3C, C05858, D07070, E08888, ECA0A0, FCB4B4
    public static readonly Color Hue04Lum00 = new Color(0x88 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue04Lum02 = new Color(0x9C / 255.0f, 0x20 / 255.0f, 0x20 / 255.0f);
    public static readonly Color Hue04Lum04 = new Color(0xB0 / 255.0f, 0x3C / 255.0f, 0x3C / 255.0f);
    public static readonly Color Hue04Lum06 = new Color(0xC0 / 255.0f, 0x58 / 255.0f, 0x58 / 255.0f);
    public static readonly Color Hue04Lum08 = new Color(0xD0 / 255.0f, 0x70 / 255.0f, 0x70 / 255.0f);
    public static readonly Color Hue04Lum10 = new Color(0xE0 / 255.0f, 0x88 / 255.0f, 0x88 / 255.0f);
    public static readonly Color Hue04Lum12 = new Color(0xEC / 255.0f, 0xA0 / 255.0f, 0xA0 / 255.0f);
    public static readonly Color Hue04Lum14 = new Color(0xFC / 255.0f, 0xB4 / 255.0f, 0xB4 / 255.0f);

    // 78005C, 8C2074, A03C88, B0589C, C070B0, D084C0, DC9CD0, ECB0E0
    public static readonly Color Hue05Lum00 = new Color(0x78 / 255.0f, 0x00 / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue05Lum02 = new Color(0x8C / 255.0f, 0x20 / 255.0f, 0x74 / 255.0f);
    public static readonly Color Hue05Lum04 = new Color(0xA0 / 255.0f, 0x3C / 255.0f, 0x88 / 255.0f);
    public static readonly Color Hue05Lum06 = new Color(0xB0 / 255.0f, 0x58 / 255.0f, 0x9C / 255.0f);
    public static readonly Color Hue05Lum08 = new Color(0xC0 / 255.0f, 0x70 / 255.0f, 0xB0 / 255.0f);
    public static readonly Color Hue05Lum10 = new Color(0xD0 / 255.0f, 0x84 / 255.0f, 0xC0 / 255.0f);
    public static readonly Color Hue05Lum12 = new Color(0xDC / 255.0f, 0x9C / 255.0f, 0xD0 / 255.0f);
    public static readonly Color Hue05Lum14 = new Color(0xEC / 255.0f, 0xB0 / 255.0f, 0xE0 / 255.0f);

    // 480078, 602090, 783CA4, 8C58B8, A070CC, B484DC, C49CEC, D4B0FC
    public static readonly Color Hue06Lum00 = new Color(0x48 / 255.0f, 0x00 / 255.0f, 0x78 / 255.0f);
    public static readonly Color Hue06Lum02 = new Color(0x60 / 255.0f, 0x20 / 255.0f, 0x90 / 255.0f);
    public static readonly Color Hue06Lum04 = new Color(0x78 / 255.0f, 0x3C / 255.0f, 0xA4 / 255.0f);
    public static readonly Color Hue06Lum06 = new Color(0x8C / 255.0f, 0x58 / 255.0f, 0xB8 / 255.0f);
    public static readonly Color Hue06Lum08 = new Color(0xA0 / 255.0f, 0x70 / 255.0f, 0xCC / 255.0f);
    public static readonly Color Hue06Lum10 = new Color(0xB4 / 255.0f, 0x84 / 255.0f, 0xDC / 255.0f);
    public static readonly Color Hue06Lum12 = new Color(0xC4 / 255.0f, 0x9C / 255.0f, 0xEC / 255.0f);
    public static readonly Color Hue06Lum14 = new Color(0xD4 / 255.0f, 0xB0 / 255.0f, 0xFC / 255.0f);

    // 140084, 302098, 4C3CAC, 6858C0, 7C70D0, 9488E0, A8A0EC, BCB4FC
    public static readonly Color Hue07Lum00 = new Color(0x14 / 255.0f, 0x00 / 255.0f, 0x84 / 255.0f);
    public static readonly Color Hue07Lum02 = new Color(0x30 / 255.0f, 0x20 / 255.0f, 0x98 / 255.0f);
    public static readonly Color Hue07Lum04 = new Color(0x4C / 255.0f, 0x3C / 255.0f, 0xAC / 255.0f);
    public static readonly Color Hue07Lum06 = new Color(0x68 / 255.0f, 0x58 / 255.0f, 0xC0 / 255.0f);
    public static readonly Color Hue07Lum08 = new Color(0x7C / 255.0f, 0x70 / 255.0f, 0xD0 / 255.0f);
    public static readonly Color Hue07Lum10 = new Color(0x94 / 255.0f, 0x88 / 255.0f, 0xE0 / 255.0f);
    public static readonly Color Hue07Lum12 = new Color(0xA8 / 255.0f, 0xA0 / 255.0f, 0xEC / 255.0f);
    public static readonly Color Hue07Lum14 = new Color(0xBC / 255.0f, 0xB4 / 255.0f, 0xFC / 255.0f);

    // 000088, 1C209C, 3840B0, 505CC0, 6874D0, 7C8CE0, 90A4EC, A4B8FC
    public static readonly Color Hue08Lum00 = new Color(0x00 / 255.0f, 0x00 / 255.0f, 0x88 / 255.0f);
    public static readonly Color Hue08Lum02 = new Color(0x1C / 255.0f, 0x20 / 255.0f, 0x9C / 255.0f);
    public static readonly Color Hue08Lum04 = new Color(0x38 / 255.0f, 0x40 / 255.0f, 0xB0 / 255.0f);
    public static readonly Color Hue08Lum06 = new Color(0x50 / 255.0f, 0x5C / 255.0f, 0xC0 / 255.0f);
    public static readonly Color Hue08Lum08 = new Color(0x68 / 255.0f, 0x74 / 255.0f, 0xD0 / 255.0f);
    public static readonly Color Hue08Lum10 = new Color(0x7C / 255.0f, 0x8C / 255.0f, 0xE0 / 255.0f);
    public static readonly Color Hue08Lum12 = new Color(0x90 / 255.0f, 0xA4 / 255.0f, 0xEC / 255.0f);
    public static readonly Color Hue08Lum14 = new Color(0xA4 / 255.0f, 0xB8 / 255.0f, 0xFC / 255.0f);

    // 00187C, 1C3890, 3854A8, 5070BC, 6888CC, 7C9CDC, 90B4EC, A4C8FC
    public static readonly Color Hue09Lum00 = new Color(0x00 / 255.0f, 0x18 / 255.0f, 0x7C / 255.0f);
    public static readonly Color Hue09Lum02 = new Color(0x1C / 255.0f, 0x38 / 255.0f, 0x90 / 255.0f);
    public static readonly Color Hue09Lum04 = new Color(0x38 / 255.0f, 0x54 / 255.0f, 0xA8 / 255.0f);
    public static readonly Color Hue09Lum06 = new Color(0x50 / 255.0f, 0x70 / 255.0f, 0xBC / 255.0f);
    public static readonly Color Hue09Lum08 = new Color(0x68 / 255.0f, 0x88 / 255.0f, 0xCC / 255.0f);
    public static readonly Color Hue09Lum10 = new Color(0x7C / 255.0f, 0x9C / 255.0f, 0xDC / 255.0f);
    public static readonly Color Hue09Lum12 = new Color(0x90 / 255.0f, 0xB4 / 255.0f, 0xEC / 255.0f);
    public static readonly Color Hue09Lum14 = new Color(0xA4 / 255.0f, 0xC8 / 255.0f, 0xFC / 255.0f);

    // 002C5C, 1C4C78, 386890, 5084AC, 689CC0, 7CB4D4, 90CCE8, A4E0FC
    public static readonly Color Hue10Lum00 = new Color(0x00 / 255.0f, 0x2C / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue10Lum02 = new Color(0x1C / 255.0f, 0x4C / 255.0f, 0x78 / 255.0f);
    public static readonly Color Hue10Lum04 = new Color(0x38 / 255.0f, 0x68 / 255.0f, 0x90 / 255.0f);
    public static readonly Color Hue10Lum06 = new Color(0x50 / 255.0f, 0x84 / 255.0f, 0xAC / 255.0f);
    public static readonly Color Hue10Lum08 = new Color(0x68 / 255.0f, 0x9C / 255.0f, 0xC0 / 255.0f);
    public static readonly Color Hue10Lum10 = new Color(0x7C / 255.0f, 0xB4 / 255.0f, 0xD4 / 255.0f);
    public static readonly Color Hue10Lum12 = new Color(0x90 / 255.0f, 0xCC / 255.0f, 0xE8 / 255.0f);
    public static readonly Color Hue10Lum14 = new Color(0xA4 / 255.0f, 0xE0 / 255.0f, 0xFC / 255.0f);

    // 003C2C, 1C5C48, 387C64, 509C80, 68B494, 7CD0AC, 90E4C0, A4FCD4
    public static readonly Color Hue11Lum00 = new Color(0x00 / 255.0f, 0x3C / 255.0f, 0x2C / 255.0f);
    public static readonly Color Hue11Lum02 = new Color(0x1C / 255.0f, 0x5C / 255.0f, 0x48 / 255.0f);
    public static readonly Color Hue11Lum04 = new Color(0x38 / 255.0f, 0x7C / 255.0f, 0x64 / 255.0f);
    public static readonly Color Hue11Lum06 = new Color(0x50 / 255.0f, 0x9C / 255.0f, 0x80 / 255.0f);
    public static readonly Color Hue11Lum08 = new Color(0x68 / 255.0f, 0xB4 / 255.0f, 0x94 / 255.0f);
    public static readonly Color Hue11Lum10 = new Color(0x7C / 255.0f, 0xD0 / 255.0f, 0xAC / 255.0f);
    public static readonly Color Hue11Lum12 = new Color(0x90 / 255.0f, 0xE4 / 255.0f, 0xC0 / 255.0f);
    public static readonly Color Hue11Lum14 = new Color(0xA4 / 255.0f, 0xFC / 255.0f, 0xD4 / 255.0f);

    // 003C00, 205C20, 407C40, 5C9C5C, 74B474, 8CD08C, A4E4A4, B8FCB8
    public static readonly Color Hue12Lum00 = new Color(0x00 / 255.0f, 0x3C / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue12Lum02 = new Color(0x20 / 255.0f, 0x5C / 255.0f, 0x20 / 255.0f);
    public static readonly Color Hue12Lum04 = new Color(0x40 / 255.0f, 0x7C / 255.0f, 0x40 / 255.0f);
    public static readonly Color Hue12Lum06 = new Color(0x5C / 255.0f, 0x9C / 255.0f, 0x5C / 255.0f);
    public static readonly Color Hue12Lum08 = new Color(0x74 / 255.0f, 0xB4 / 255.0f, 0x74 / 255.0f);
    public static readonly Color Hue12Lum10 = new Color(0x8C / 255.0f, 0xD0 / 255.0f, 0x8C / 255.0f);
    public static readonly Color Hue12Lum12 = new Color(0xA4 / 255.0f, 0xE4 / 255.0f, 0xA4 / 255.0f);
    public static readonly Color Hue12Lum14 = new Color(0xB8 / 255.0f, 0xFC / 255.0f, 0xB8 / 255.0f);

    // 143800, 345C1C, 507C38, 6C9850, 84B468, 9CCC7C, B4E490, C8FCA4
    public static readonly Color Hue13Lum00 = new Color(0x14 / 255.0f, 0x38 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue13Lum02 = new Color(0x34 / 255.0f, 0x5C / 255.0f, 0x1C / 255.0f);
    public static readonly Color Hue13Lum04 = new Color(0x50 / 255.0f, 0x7C / 255.0f, 0x38 / 255.0f);
    public static readonly Color Hue13Lum06 = new Color(0x6C / 255.0f, 0x98 / 255.0f, 0x50 / 255.0f);
    public static readonly Color Hue13Lum08 = new Color(0x84 / 255.0f, 0xB4 / 255.0f, 0x68 / 255.0f);
    public static readonly Color Hue13Lum10 = new Color(0x9C / 255.0f, 0xCC / 255.0f, 0x7C / 255.0f);
    public static readonly Color Hue13Lum12 = new Color(0xB4 / 255.0f, 0xE4 / 255.0f, 0x90 / 255.0f);
    public static readonly Color Hue13Lum14 = new Color(0xC8 / 255.0f, 0xFC / 255.0f, 0xA4 / 255.0f);

    // 2C3000, 4C501C, 687034, 848C4C, 9CA864, B4C078, CCD488, E0EC9C
    public static readonly Color Hue14Lum00 = new Color(0x2C / 255.0f, 0x30 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue14Lum02 = new Color(0x4C / 255.0f, 0x50 / 255.0f, 0x1C / 255.0f);
    public static readonly Color Hue14Lum04 = new Color(0x68 / 255.0f, 0x70 / 255.0f, 0x34 / 255.0f);
    public static readonly Color Hue14Lum06 = new Color(0x84 / 255.0f, 0x8C / 255.0f, 0x4C / 255.0f);
    public static readonly Color Hue14Lum08 = new Color(0x9C / 255.0f, 0xA8 / 255.0f, 0x64 / 255.0f);
    public static readonly Color Hue14Lum10 = new Color(0xB4 / 255.0f, 0xC0 / 255.0f, 0x78 / 255.0f);
    public static readonly Color Hue14Lum12 = new Color(0xCC / 255.0f, 0xD4 / 255.0f, 0x88 / 255.0f);
    public static readonly Color Hue14Lum14 = new Color(0xE0 / 255.0f, 0xEC / 255.0f, 0x9C / 255.0f);

    // 442800, 644818, 846830, A08444, B89C58, D0B46C, E8CC7C, FCE08C
    public static readonly Color Hue15Lum00 = new Color(0x44 / 255.0f, 0x28 / 255.0f, 0x00 / 255.0f);
    public static readonly Color Hue15Lum02 = new Color(0x64 / 255.0f, 0x48 / 255.0f, 0x18 / 255.0f);
    public static readonly Color Hue15Lum04 = new Color(0x84 / 255.0f, 0x68 / 255.0f, 0x30 / 255.0f);
    public static readonly Color Hue15Lum06 = new Color(0xA0 / 255.0f, 0x84 / 255.0f, 0x44 / 255.0f);
    public static readonly Color Hue15Lum08 = new Color(0xB8 / 255.0f, 0x9C / 255.0f, 0x58 / 255.0f);
    public static readonly Color Hue15Lum10 = new Color(0xD0 / 255.0f, 0xB4 / 255.0f, 0x6C / 255.0f);
    public static readonly Color Hue15Lum12 = new Color(0xE8 / 255.0f, 0xCC / 255.0f, 0x7C / 255.0f);
    public static readonly Color Hue15Lum14 = new Color(0xFC / 255.0f, 0xE0 / 255.0f, 0x8C / 255.0f);

    #endregion

    //--------------------------------------------------------------------------------------
    // Indexes:
    //--------------------------------------------------------------------------------------

    #region Indexes

    public const int Hue00Lum00Index = 0;
    public const int Hue00Lum02Index = 1;
    public const int Hue00Lum04Index = 2;
    public const int Hue00Lum06Index = 3;
    public const int Hue00Lum08Index = 4;
    public const int Hue00Lum10Index = 5;
    public const int Hue00Lum12Index = 6;
    public const int Hue00Lum14Index = 7;

    public const int Hue01Lum00Index = 8;
    public const int Hue01Lum02Index = 9;
    public const int Hue01Lum04Index = 10;
    public const int Hue01Lum06Index = 11;
    public const int Hue01Lum08Index = 12;
    public const int Hue01Lum10Index = 13;
    public const int Hue01Lum12Index = 14;
    public const int Hue01Lum14Index = 15;

    public const int Hue02Lum00Index = 16;
    public const int Hue02Lum02Index = 17;
    public const int Hue02Lum04Index = 18;
    public const int Hue02Lum06Index = 19;
    public const int Hue02Lum08Index = 20;
    public const int Hue02Lum10Index = 21;
    public const int Hue02Lum12Index = 22;
    public const int Hue02Lum14Index = 23;

    public const int Hue03Lum00Index = 24;
    public const int Hue03Lum02Index = 25;
    public const int Hue03Lum04Index = 26;
    public const int Hue03Lum06Index = 27;
    public const int Hue03Lum08Index = 28;
    public const int Hue03Lum10Index = 29;
    public const int Hue03Lum12Index = 30;
    public const int Hue03Lum14Index = 31;

    public const int Hue04Lum00Index = 32;
    public const int Hue04Lum02Index = 33;
    public const int Hue04Lum04Index = 34;
    public const int Hue04Lum06Index = 35;
    public const int Hue04Lum08Index = 36;
    public const int Hue04Lum10Index = 37;
    public const int Hue04Lum12Index = 38;
    public const int Hue04Lum14Index = 39;

    public const int Hue05Lum00Index = 40;
    public const int Hue05Lum02Index = 41;
    public const int Hue05Lum04Index = 42;
    public const int Hue05Lum06Index = 43;
    public const int Hue05Lum08Index = 44;
    public const int Hue05Lum10Index = 45;
    public const int Hue05Lum12Index = 46;
    public const int Hue05Lum14Index = 47;

    public const int Hue06Lum00Index = 48;
    public const int Hue06Lum02Index = 49;
    public const int Hue06Lum04Index = 50;
    public const int Hue06Lum06Index = 51;
    public const int Hue06Lum08Index = 52;
    public const int Hue06Lum10Index = 53;
    public const int Hue06Lum12Index = 54;
    public const int Hue06Lum14Index = 55;

    public const int Hue07Lum00Index = 56;
    public const int Hue07Lum02Index = 57;
    public const int Hue07Lum04Index = 58;
    public const int Hue07Lum06Index = 59;
    public const int Hue07Lum08Index = 60;
    public const int Hue07Lum10Index = 61;
    public const int Hue07Lum12Index = 62;
    public const int Hue07Lum14Index = 63;

    public const int Hue08Lum00Index = 64;
    public const int Hue08Lum02Index = 65;
    public const int Hue08Lum04Index = 66;
    public const int Hue08Lum06Index = 67;
    public const int Hue08Lum08Index = 68;
    public const int Hue08Lum10Index = 69;
    public const int Hue08Lum12Index = 70;
    public const int Hue08Lum14Index = 71;

    public const int Hue09Lum00Index = 72;
    public const int Hue09Lum02Index = 73;
    public const int Hue09Lum04Index = 74;
    public const int Hue09Lum06Index = 75;
    public const int Hue09Lum08Index = 76;
    public const int Hue09Lum10Index = 77;
    public const int Hue09Lum12Index = 78;
    public const int Hue09Lum14Index = 79;

    public const int Hue10Lum00Index = 80;
    public const int Hue10Lum02Index = 81;
    public const int Hue10Lum04Index = 82;
    public const int Hue10Lum06Index = 83;
    public const int Hue10Lum08Index = 84;
    public const int Hue10Lum10Index = 85;
    public const int Hue10Lum12Index = 86;
    public const int Hue10Lum14Index = 87;

    public const int Hue11Lum00Index = 88;
    public const int Hue11Lum02Index = 89;
    public const int Hue11Lum04Index = 90;
    public const int Hue11Lum06Index = 91;
    public const int Hue11Lum08Index = 92;
    public const int Hue11Lum10Index = 93;
    public const int Hue11Lum12Index = 94;
    public const int Hue11Lum14Index = 95;

    public const int Hue12Lum00Index = 96;
    public const int Hue12Lum02Index = 97;
    public const int Hue12Lum04Index = 98;
    public const int Hue12Lum06Index = 99;
    public const int Hue12Lum08Index = 100;
    public const int Hue12Lum10Index = 101;
    public const int Hue12Lum12Index = 102;
    public const int Hue12Lum14Index = 103;

    public const int Hue13Lum00Index = 104;
    public const int Hue13Lum02Index = 105;
    public const int Hue13Lum04Index = 106;
    public const int Hue13Lum06Index = 107;
    public const int Hue13Lum08Index = 108;
    public const int Hue13Lum10Index = 109;
    public const int Hue13Lum12Index = 110;
    public const int Hue13Lum14Index = 111;

    public const int Hue14Lum00Index = 112;
    public const int Hue14Lum02Index = 113;
    public const int Hue14Lum04Index = 114;
    public const int Hue14Lum06Index = 115;
    public const int Hue14Lum08Index = 116;
    public const int Hue14Lum10Index = 117;
    public const int Hue14Lum12Index = 118;
    public const int Hue14Lum14Index = 119;

    public const int Hue15Lum00Index = 120;
    public const int Hue15Lum02Index = 121;
    public const int Hue15Lum04Index = 122;
    public const int Hue15Lum06Index = 123;
    public const int Hue15Lum08Index = 124;
    public const int Hue15Lum10Index = 125;
    public const int Hue15Lum12Index = 126;
    public const int Hue15Lum14Index = 127;

    #endregion

    //--------------------------------------------------------------------------------------
    // Array:
    //--------------------------------------------------------------------------------------

    #region Array

    public const int NumberOfColors = 128;

    public static readonly Color[] Colors = new Color[NumberOfColors] {
        Hue00Lum00, Hue00Lum02, Hue00Lum04, Hue00Lum06, Hue00Lum08, Hue00Lum10, Hue00Lum12, Hue00Lum14,
        Hue01Lum00, Hue01Lum02, Hue01Lum04, Hue01Lum06, Hue01Lum08, Hue01Lum10, Hue01Lum12, Hue01Lum14,
        Hue02Lum00, Hue02Lum02, Hue02Lum04, Hue02Lum06, Hue02Lum08, Hue02Lum10, Hue02Lum12, Hue02Lum14,
        Hue03Lum00, Hue03Lum02, Hue03Lum04, Hue03Lum06, Hue03Lum08, Hue03Lum10, Hue03Lum12, Hue03Lum14,
        Hue04Lum00, Hue04Lum02, Hue04Lum04, Hue04Lum06, Hue04Lum08, Hue04Lum10, Hue04Lum12, Hue04Lum14,
        Hue05Lum00, Hue05Lum02, Hue05Lum04, Hue05Lum06, Hue05Lum08, Hue05Lum10, Hue05Lum12, Hue05Lum14,
        Hue06Lum00, Hue06Lum02, Hue06Lum04, Hue06Lum06, Hue06Lum08, Hue06Lum10, Hue06Lum12, Hue06Lum14,
        Hue07Lum00, Hue07Lum02, Hue07Lum04, Hue07Lum06, Hue07Lum08, Hue07Lum10, Hue07Lum12, Hue07Lum14,
        Hue08Lum00, Hue08Lum02, Hue08Lum04, Hue08Lum06, Hue08Lum08, Hue08Lum10, Hue08Lum12, Hue08Lum14,
        Hue09Lum00, Hue09Lum02, Hue09Lum04, Hue09Lum06, Hue09Lum08, Hue09Lum10, Hue09Lum12, Hue09Lum14,
        Hue10Lum00, Hue10Lum02, Hue10Lum04, Hue10Lum06, Hue10Lum08, Hue10Lum10, Hue10Lum12, Hue10Lum14,
        Hue11Lum00, Hue11Lum02, Hue11Lum04, Hue11Lum06, Hue11Lum08, Hue11Lum10, Hue11Lum12, Hue11Lum14,
        Hue12Lum00, Hue12Lum02, Hue12Lum04, Hue12Lum06, Hue12Lum08, Hue12Lum10, Hue12Lum12, Hue12Lum14,
        Hue13Lum00, Hue13Lum02, Hue13Lum04, Hue13Lum06, Hue13Lum08, Hue13Lum10, Hue13Lum12, Hue13Lum14,
        Hue14Lum00, Hue14Lum02, Hue14Lum04, Hue14Lum06, Hue14Lum08, Hue14Lum10, Hue14Lum12, Hue14Lum14,
        Hue15Lum00, Hue15Lum02, Hue15Lum04, Hue15Lum06, Hue15Lum08, Hue15Lum10, Hue15Lum12, Hue15Lum14
    };

    #endregion
}
