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
using System.Linq;

/// <summary>
/// This class represents a profile data.
/// </summary>
public class ProfileData {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    public const int NAME_MAX_LEN = 25;
    public const int MAX_AVATARS = 8;
    public const int NO_SCORE = -1;

    public const string CHARSET_FILE_BASE = "Charset";

    #endregion

    //--------------------------------------------------------------------------------------
    // Types:
    //--------------------------------------------------------------------------------------

    #region Types

    /// <summary>
    /// This structure represents the scores of the user in a chapter.
    /// </summary>
    public struct ChapterScore {
        public Score[] Scores;
        public ChapterScore(Score[] scores = null) {
            Scores = scores;
        }
    }

    /// <summary>
    /// This structure represents a score of the user.
    /// </summary>
    public struct Score {
        public int Steps;
        public int Seconds;
        public Score(int steps = 0, int seconds = 0) {
            Steps = steps;
            Seconds = seconds;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region bool Empty

    /// <summary>
    /// This is the profile is empty flag.
    /// </summary>
    private bool empty;

    /// <summary>
    /// Gets or sets if the profile is empty.
    /// </summary>
    public bool Empty {
        get {
            return empty;
        }
        set {
            if (value) {
                Clear();
            } else {
                empty = false;
            }
        }
    }

    #endregion

    #region string Name

    /// <summary>
    /// The name of the player.
    /// </summary>
    private string name;

    /// <summary>
    /// Gets or sets the name of the player.
    /// </summary>
    public string Name {
        get {
            return name;
        }
        set {
            name = value;
            if (name.Length > NAME_MAX_LEN) {
                name = name.Substring(0, NAME_MAX_LEN);
            }
        }
    }

    #endregion

    #region int Avatar

    /// <summary>
    /// The type of avatar of the player.
    /// </summary>
    private int avatar;

    /// <summary>
    /// Gets or sets the type of avatar of the player.
    /// </summary>
    public int Avatar {
        get {
            return avatar;
        }
        set {
            avatar = value;
            if (avatar < 0) {
                avatar = MAX_AVATARS - 1;
            } else if (avatar >= MAX_AVATARS) {
                avatar = 0;
            }
        }
    }

    #endregion

    #region string Charset

    /// <summary>
    /// Gets the charset used by the player.
    /// </summary>
    public string Charset {
        get { return GetCharset(avatar); }
    }

    #endregion

    #region int LastChapterUnlocked

    /// <summary>
    /// The last unlocked chapter of the player.
    /// </summary>
    private int lastChapterUnlocked;

    /// <summary>
    /// Gets or sets the last unlocked chapter of the player.
    /// </summary>
    public int LastChapterUnlocked {
        get {
            return lastChapterUnlocked;
        }
        set {
            lastChapterUnlocked = value;
            var chapters = CoreManager.Instance.Chapters;
            if (lastChapterUnlocked < 0) {
                lastChapterUnlocked = 0;
            } else if (lastChapterUnlocked >= chapters.Length) {
                lastChapterUnlocked = chapters.Length - 1;
            }
        }
    }

    #endregion

    #region int LastLevelUnlocked

    /// <summary>
    /// The last unlocked level of the player.
    /// </summary>
    private int lastLevelUnlocked;

    /// <summary>
    /// Gets or sets the last unlocked level of the player.
    /// </summary>
    public int LastLevelUnlocked {
        get {
            return lastLevelUnlocked;
        }
        set {
            lastLevelUnlocked = value;
            if (lastLevelUnlocked < 0) {
                lastLevelUnlocked = 0;
            } else {
                var chapters = CoreManager.Instance.Chapters;
                var levelsLength = chapters[lastChapterUnlocked].Levels.Length;
                if (lastLevelUnlocked >= levelsLength) {
                    if (lastChapterUnlocked + 1 >= chapters.Length) {
                        lastLevelUnlocked = levelsLength - 1;
                    } else {
                        lastLevelUnlocked = 0;
                        lastChapterUnlocked++;
                    }
                }
            }
        }
    }

    #endregion

    #region ChapterScore[] ChaptersScore

    /// <summary>
    /// Gets the chapters score of the player.
    /// </summary>
    public ChapterScore[] ChaptersScore { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void Clear()

    /// <summary>
    /// Clears the profile data.
    /// </summary>
    public void Clear() {
        empty = true;
        name = "PLAYER";
        avatar = 0;
        lastChapterUnlocked = 0;
        lastLevelUnlocked = 0;
        var chapters = CoreManager.Instance.Chapters;
        ChaptersScore = new ChapterScore[chapters.Length];
        for (int i = 0; i < chapters.Length; i++) {
            ChaptersScore[i].Scores = new Score[chapters[i].Levels.Length];
            for (int j = 0; j < chapters[i].Levels.Length; j++) {
                ChaptersScore[i].Scores[j] = new Score { Steps = NO_SCORE, Seconds = NO_SCORE };
            }
        }
    }

    #endregion

    #region int GetSteps(int, int)

    /// <summary>
    /// Gets the steps of the user in a level.
    /// </summary>
    /// <param name="chapter">The chapter of the level.</param>
    /// <param name="level">The number of the level.</param>
    /// <returns>The number of steps.</returns>
    public int GetSteps(int chapter, int level) {
        if (0 <= chapter && chapter < ChaptersScore.Length &&
            0 <= level && level < ChaptersScore[chapter].Scores.Length) {
                return ChaptersScore[chapter].Scores[level].Steps;
        }
        return NO_SCORE;
    }

    #endregion

    #region int GetSeconds(int, int)

    /// <summary>
    /// Gets the seconds of the user in a level.
    /// </summary>
    /// <param name="chapter">The chapter of the level.</param>
    /// <param name="level">The number of the level.</param>
    /// <returns>The number of seconds.</returns>
    public int GetSeconds(int chapter, int level) {
        if (0 <= chapter && chapter < ChaptersScore.Length &&
            0 <= level && level < ChaptersScore[chapter].Scores.Length) {
            return ChaptersScore[chapter].Scores[level].Seconds;
        }
        return NO_SCORE;
    }

    #endregion

    #region void SetScore(int, int, int, int)

    /// <summary>
    /// Sets the score of the user in a level.
    /// </summary>
    /// <param name="chapter">The chapter of the level.</param>
    /// <param name="level">The number of the level.</param>
    /// <param name="steps">The steps of the score.</param>
    /// <param name="seconds">The seconds of the score.</param>
    public void SetScore(int chapter, int level, int steps, int seconds) {
        if (0 <= chapter && chapter < ChaptersScore.Length &&
            0 <= level && level < ChaptersScore[chapter].Scores.Length) {
            var currentSteps = ChaptersScore[chapter].Scores[level].Steps;
            var currentSeconds = ChaptersScore[chapter].Scores[level].Seconds;
            if ((currentSteps <= NO_SCORE) ||
                (currentSeconds <= NO_SCORE) ||
                (currentSteps > steps) ||
                (currentSteps == steps && currentSeconds > seconds)) {
                ChaptersScore[chapter].Scores[level].Steps = steps;
                ChaptersScore[chapter].Scores[level].Seconds = seconds;
            }
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region ProfileData()

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public ProfileData() {
        Clear();
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Static):
    //--------------------------------------------------------------------------------------

    #region string GetCharset(int)

    /// <summary>
    /// Gets the charset used by a player.
    /// </summary>
    /// <param name="avatar">The avatar of the player.</param>
    /// <returns>The charset file name.</returns>
    public static string GetCharset(int avatar) {
        return CHARSET_FILE_BASE + (avatar + 1);
    }

    #endregion

    #region string GetRandomCharset()

    /// <summary>
    /// Gets a random charset.
    /// </summary>
    /// <returns>The charset file name.</returns>
    public static string GetRandomCharset() {
        return GetCharset(UnityEngine.Random.Range(0, MAX_AVATARS));
    }

    #endregion

    #region ProfileData[] Create(ProfilesXml)

    /// <summary>
    /// Creates a new profile data array.
    /// </summary>
    /// <param name="descriptor">The descriptor of the profiles.</param>
    /// <returns>Returns the created profile data array.</returns>
    public static ProfileData[] Create(ProfilesXml descriptor) {
        ProfileData[] profiles = new ProfileData[CoreManager.MAX_PROFILES];
        for (int i = 0; i < CoreManager.MAX_PROFILES; i++) {
            if (i < descriptor.Profile.Length) {
                profiles[i] = Create(descriptor.Profile[i]);
            } else {
                profiles[i] = new ProfileData();
            }
        }
        return profiles;
    }

    #endregion

    #region ProfileData Create(ProfileXml)

    /// <summary>
    /// Creates a new profile data.
    /// </summary>
    /// <param name="descriptor">The descriptor of the profile.</param>
    /// <returns>Returns the created profile data.</returns>
    public static ProfileData Create(ProfileXml descriptor) {
        ProfileData profiles = new ProfileData();
        profiles.empty = descriptor.Empty;
        profiles.name = descriptor.Name;
        profiles.avatar = descriptor.Avatar;
        profiles.lastChapterUnlocked = descriptor.LastChapterUnlocked;
        profiles.lastLevelUnlocked = descriptor.LastLevelUnlocked;
        if (descriptor.Scores != null &&
            descriptor.Scores.Length >= profiles.ChaptersScore.Length) {
            for (int i = 0; i < profiles.ChaptersScore.Length; i++) {
                if (i >= descriptor.Scores.Length) {
                    break;
                } else if (descriptor.Scores[i].Score != null) {
                    for (int j = 0; j < profiles.ChaptersScore[i].Scores.Length; j++) {
                        if (j >= descriptor.Scores[i].Score.Length) {
                            break;
                        } else {
                            profiles.ChaptersScore[i].Scores[j] = new Score {
                                Steps = descriptor.Scores[i].Score[j].Steps,
                                Seconds = descriptor.Scores[i].Score[j].Seconds
                            };
                        }
                    }
                }
            }
        }
        return profiles;
    }

    #endregion

    #region ProfilesXml CreateXml(ProfileData[])

    /// <summary>
    /// Creates a new profiles descriptor.
    /// </summary>
    /// <param name="data">The profiles.</param>
    /// <returns>Returns the created profiles descriptor.</returns>
    public static ProfilesXml CreateXml(ProfileData[] data) {
        ProfilesXml profiles = new ProfilesXml();
        profiles.Profile = new ProfileXml[data.Length];
        profiles.Profile = data.Select(x => CreateXml(x)).ToArray();
        return profiles;
    }

    #endregion

    #region ProfileXml CreateXml(ProfileData)

    /// <summary>
    /// Creates a new profile descriptor.
    /// </summary>
    /// <param name="data">The profile.</param>
    /// <returns>Returns the created profile descriptor.</returns>
    public static ProfileXml CreateXml(ProfileData data) {
        ProfileXml profile = new ProfileXml();
        profile.Empty = data.empty;
        profile.Name = data.name;
        profile.Avatar = data.avatar;
        profile.LastChapterUnlocked = data.lastChapterUnlocked;
        profile.LastLevelUnlocked = data.lastLevelUnlocked;
        Func<Score, ScoreXml> createScore = victim => {
            return new ScoreXml {
                Steps = victim.Steps,
                Seconds = victim.Seconds
            };
        };
        Func<Score[], ScoresXml> createScores = victim => {
            var scores = new ScoresXml();
            scores.Score = victim.Select(x => createScore(x)).ToArray();
            return scores;
        };
        profile.Scores = data.ChaptersScore.Select(x => createScores(x.Scores)).ToArray();
        return profile;
    }

    #endregion
}
