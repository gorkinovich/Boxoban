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

/// <summary>
/// This static class contains the strings of the game.
/// </summary>
public static class Strings {
    // GENERAL:
    public const string VERSION = " VERSION 1.0";

    // Shared:
    public const string SHARED_YES = "YES";
    public const string SHARED_NO = "NO";
    public const string SHARED_RETURN = "RETURN";
    public const string SHARED_ACCEPT = "ACCEPT";
    public const string SHARED_CANCEL = "CANCEL";
    public const string SHARED_CONTINUE = "CONTINUE";
    public const string SHARED_LOADING = " LOADING...";

    // Menu State:
    public const string MENU_TITLE = "BOXOBAN";
    public const string MENU_PLAY = "PLAY";
    public const string MENU_TUTORIAL = "TUTORIAL";
    public const string MENU_OPTIONS = "OPTIONS";
    public const string MENU_CREDITS = "CREDITS";
    public const string MENU_EXIT = "EXIT";

    // Select Profile State:
    public const string SELECT_PROFILE_TITLE = "SELECT PROFILE";

    // Create Profile State:
    public const string CREATE_PROFILE_TITLE = "CREATE PROFILE";
    public const string CREATE_PROFILE_CREATE = "CREATE";

    // Delete Profile State:
    public const string DELETE_PROFILE_MSG =
        "DO YOU WANT TO DELETE\n" +
        "THIS PROFILE?";

    // Select Chapter State:
    public const string SELECT_CHAPTER_TITLE = "SELECT CHAPTER";

    // Level Description State:
    public const string LEVEL_DESCRIPTION_PLAY = "PLAY";
    public const string LEVEL_DESCRIPTION_STEPS = "STEPS:";
    public const string LEVEL_DESCRIPTION_TIME = "TIME:";
    public const string LEVEL_DESCRIPTION_SCORE = "{0} {1}   {2} {3}";

    // Level State:
    public const string LEVEL_STEPS = "STEPS: ";
    public const string LEVEL_TIME = "TIME: ";

    // Level Pause State:
    public const string LEVEL_PAUSE_RESET = "RESET";
    public const string LEVEL_PAUSE_EXIT = "EXIT";

    // Reset Level State:
    public const string RESET_LEVEL_MSG =
        "DO YOU WANT TO RESET\n" +
        "THE CURRENT LEVEL?";

    // Exit Level State:
    public const string EXIT_LEVEL_MSG =
        "DO YOU WANT TO EXIT\n" +
        "THE CURRENT LEVEL?";

    // Victory State:
    public const string VICTORY_TITLE = "VICTORY";
    public const string VICTORY_MSG =
        "CONGRATULATIONS!\n" +
        "LEVEL FINISHED!";
    public const string VICTORY_STEPS = "STEPS: ";
    public const string VICTORY_TIME = "TIME:  ";

    // Tutorial State:
    public const string TUTORIAL_MSG0 = "CLICK TO THE SOUTH TO MOVE";
    public const string TUTORIAL_MSG1 = "CLICK TO THE WEST TO MOVE";
    public const string TUTORIAL_MSG2 = "CLICK TO THE NORTH TO MOVE";
    public const string TUTORIAL_MSG3 = "CLICK TO THE EAST TO MOVE";
    public const string TUTORIAL_MSG4 = "PUSH THE BOX TO FINISH THE LEVEL";

    // Exit Tutorial State:
    public const string EXIT_TUTORIAL_MSG =
        "DO YOU WANT TO EXIT\n" +
        "THE TUTORIAL?";

    // Tutorial Victory State:
    public const string TUTORIAL_VICTORY_TITLE = "VICTORY";
    public const string TUTORIAL_VICTORY_MSG =
        "CONGRATULATIONS!\n" +
        "TUTORIAL FINISHED!";

    // Options State:
    public const string OPTIONS_TITLE = "OPTIONS";
    public const string OPTIONS_SOUND = "SOUND";
    public const string OPTIONS_MUSIC = "MUSIC";

    // Credits State:
    public const string CREDITS_TITLE = "CREDITS";
    public const string CREDITS_MSG =
        "Gorka Suárez García\n" +
        "PROGRAMMING, ART & DESIGN\n\n" +
        "Alberto Gil de la Fuente\n" +
        "ART & DESIGN\n\n" +
        "Iván Manuel Laclaustra Yebes\n" +
        "MUSIC & SOUND\n\n" +
        "Richard Jordan Cabana Ramírez\n" +
        "PRODUCTION";

    // Exit State:
    public const string EXIT_MSG =
        "DO YOU REALLY\n" +
        "WANT TO EXIT THIS\n" +
        "WONDERFUL GAME?";
}
