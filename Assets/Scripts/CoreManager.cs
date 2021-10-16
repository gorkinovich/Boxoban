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
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// This singleton class represents the core manager.
/// </summary>
public class CoreManager {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    public const int NO_PROFILE = -1;
    public const int MAX_PROFILES = 4;

    public const int MAX_ITEMS = 10;
    public const int MAX_ITEMS_ROWS = 2;
    public const int MAX_ITEMS_COLS = 5;

    public const float PIXEL_TO_UNITS = 100.0f;

    public const int TILE_WIDTH = 16;
    public const int TILE_HEIGHT = 16;

    public const uint TERRAIN_ID_EMPTY = 0;
    public const uint TERRAIN_ID_FIRST_WALL = 1;
    public const uint TERRAIN_ID_LAST_WALL = 255;
    public const uint TERRAIN_ID_FIRST_FLOOR = 256;
    public const uint TERRAIN_ID_LAST_FLOOR = 511;
    public const uint TERRAIN_ID_FIRST_SFLOOR = 512;
    public const uint TERRAIN_ID_LAST_SFLOOR = 639;
    public const uint TERRAIN_ID_FIRST_DFLOOR = 640;
    public const uint TERRAIN_ID_LAST_DFLOOR = 767;
    public const uint TERRAIN_ID_FIRST_BOX = 768;
    public const uint TERRAIN_ID_LAST_BOX = 895;
    public const uint TERRAIN_ID_FIRST_DBOX = 896;
    public const uint TERRAIN_ID_LAST_DBOX = 1023;

    public const string CAMERA_GOBJ_NAME = "Camera";
    public const string WORLD_GOBJ_NAME = "World";
    public const string TERRAIN_GOBJ_NAME = "Terrain";
    public const string ENTITIES_GOBJ_NAME = "Entities";
    public const string MNUBACK_GOBJ_NAME = "MenuBackground";

    public const string TILE_GOBJ_NAME = "Tile";
    public const string PLAYER_GOBJ_NAME = "Player";
    public const string BOX_GOBJ_NAME = "Box";

    public const string FONT_NAME = "Fonts/GC Atari 2600 Basic";

    public const string MENU_SCENE_NAME = "Menu";
    public const string LEVEL_SCENE_NAME = "Level";
    public const string TUTORIAL_SCENE_NAME = "Tutorial";

    public const string MENU_TEXTURE_PATH = "Textures/Menu/";
    public const string GAME_TEXTURE_PATH = "Textures/Game/";

    public const string BASE_XML_PATH = "Data/";
    public const string CHAPTERS_FILE = "Chapters";
    public const string TUTORIAL_FILE = "Tutorial";
    public const string PROFILES_FILE = "profiles.sgd";

    public const string CHAPTERS_PATH = BASE_XML_PATH + CHAPTERS_FILE;
    public const string TUTORIAL_PATH = BASE_XML_PATH + TUTORIAL_FILE;
    
    public const string SOUND_VOLUME_KEY = "SOUND_VOLUME";
    public const string MUSIC_VOLUME_KEY = "MUSIC_VOLUME";

    public const string WALK_SOUND = "Sounds/Walk";
    public const string PUSH_SOUND = "Sounds/Push";
    public const string SETTLED_SOUND = "Sounds/Settled";

    public const string MAIN_MUSIC = "Music/Main";
    public const string VICTORY_MUSIC = "Music/Victory";

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (General):
    //--------------------------------------------------------------------------------------

    #region bool initialized

    /// <summary>
    /// The initialized flag.
    /// </summary>
    private bool initialized;

    #endregion

    #region Font TextFont

    /// <summary>
    /// Gets the default text font of the game.
    /// </summary>
    public Font TextFont { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (Options):
    //--------------------------------------------------------------------------------------

    #region float SoundVolume

    /// <summary>
    /// Gets the current sound volume.
    /// </summary>
    public float SoundVolume { get; private set; }

    #endregion

    #region float MusicVolume

    /// <summary>
    /// Gets the current music volume.
    /// </summary>
    public float MusicVolume { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (Scenes):
    //--------------------------------------------------------------------------------------

    #region bool IsMenuScene

    /// <summary>
    /// Gets if the current scene is the menu.
    /// </summary>
    public bool IsMenuScene {
        get {
            return Application.loadedLevelName == MENU_SCENE_NAME;
        }
    }

    #endregion

    #region bool IsLevelScene

    /// <summary>
    /// Gets if the current scene is the level.
    /// </summary>
    public bool IsLevelScene {
        get {
            return Application.loadedLevelName == LEVEL_SCENE_NAME;
        }
    }

    #endregion

    #region bool IsTutorialScene

    /// <summary>
    /// Gets if the current scene is the tutorial.
    /// </summary>
    public bool IsTutorialScene {
        get {
            return Application.loadedLevelName == TUTORIAL_SCENE_NAME;
        }
    }

    #endregion

    #region GameObject Camera

    /// <summary>
    /// Gets the camera of the scene.
    /// </summary>
    public GameObject Camera {
        get { return GameObject.Find(CAMERA_GOBJ_NAME); }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (States):
    //--------------------------------------------------------------------------------------

    #region IGameState State

    /// <summary>
    /// Gets the current game state.
    /// </summary>
    public IGameState State { get; private set; }

    #endregion

    #region IGameState nextState

    /// <summary>
    /// The next game state to set.
    /// </summary>
    private IGameState nextState;

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (Profiles):
    //--------------------------------------------------------------------------------------

    #region int SelectedProfile

    /// <summary>
    /// Gets the selected profile in the game.
    /// </summary>
    public int SelectedProfile { get; set; }

    #endregion

    #region ProfileData[] UserProfiles

    /// <summary>
    /// Gets all the user profiles of the game.
    /// </summary>
    public ProfileData[] UserProfiles { get; private set; }

    #endregion

    #region ProfileData CurrentProfile

    /// <summary>
    /// Gest the current loaded user profile.
    /// </summary>
    public ProfileData CurrentProfile {
        get {
            if (0 <= SelectedProfile && SelectedProfile < UserProfiles.Length) {
                return UserProfiles[SelectedProfile];
            } else {
                return null;
            }
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (Chapters):
    //--------------------------------------------------------------------------------------

    #region int SelectedChapter

    /// <summary>
    /// Gets the selected chapter in the game.
    /// </summary>
    public int SelectedChapter { get; set; }

    #endregion

    #region int SelectedLevel

    /// <summary>
    /// Gets the selected level in the chapter.
    /// </summary>
    public int SelectedLevel { get; set; }

    #endregion

    #region ChapterData[] Chapters

    /// <summary>
    /// Gets all the chapters of the game.
    /// </summary>
    public ChapterData[] Chapters { get; private set; }

    #endregion

    #region ProfileData CurrentChapter

    /// <summary>
    /// Gest the current selected chapter.
    /// </summary>
    public ChapterData CurrentChapter {
        get {
            if (0 <= SelectedChapter && SelectedChapter < Chapters.Length) {
                return Chapters[SelectedChapter];
            } else {
                return null;
            }
        }
    }

    #endregion

    #region string CurrentLevelFileName

    /// <summary>
    /// Gest the current selected level file name.
    /// </summary>
    public string CurrentLevelFileName {
        get {
            var chapter = CurrentChapter;
            if (chapter != null && 0 <= SelectedLevel &&
                SelectedLevel < chapter.Levels.Length) {
                return chapter.Levels[SelectedLevel];
            }
            return null;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties (Level):
    //--------------------------------------------------------------------------------------

    #region LevelXml LevelDescriptor

    /// <summary>
    /// Gets the current level descriptor.
    /// </summary>
    public LevelXml LevelDescriptor { get; private set; }

    #endregion

    #region LevelData Level

    /// <summary>
    /// Gets the current loaded level.
    /// </summary>
    public LevelData Level { get; private set; }

    #endregion

    #region LevelController levelController

    /// <summary>
    /// The level controller of the current level.
    /// </summary>
    private LevelController levelController;

    #endregion

    #region int Steps

    /// <summary>
    /// Gets the current number of steps of the player in the level.
    /// </summary>
    public int Steps { get; private set; }

    #endregion

    #region int Seconds

    /// <summary>
    /// Gets the current number of seconds of the player in the level.
    /// </summary>
    public int Seconds { get; private set; }

    #endregion

    #region string StepsMessage

    /// <summary>
    /// Gets the current number of steps of the player in the level.
    /// </summary>
    public string StepsMessage { get; private set; }

    #endregion

    #region string SecondsMessage

    /// <summary>
    /// Gets the current number of seconds of the player in the level.
    /// </summary>
    public string SecondsMessage { get; private set; }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (General):
    //--------------------------------------------------------------------------------------

    #region void Initialize()

    /// <summary>
    /// Initialize the manager.
    /// </summary>
    public void Initialize() {
        // Initialize this manager only once:
        if (initialized) return;

        // Create all the chapters data of the game:
        var chaptersDescriptor = LoadXmlFromResources<ChaptersXml>(CHAPTERS_PATH);
        if (chaptersDescriptor != null) {
            Chapters = ChapterData.Create(chaptersDescriptor);
        } else {
            throw new Exception("Can't load the chapters descriptor.");
        }

        // Load the font of the game:
        TextFont = Resources.Load<Font>(FONT_NAME);

        // Load the profiles & options of the game:
        LoadCurrentProfiles();
        LoadOptions();

        // Set the initial state of the game:
        State = new MenuState();
        State.Initialize();

        // Change the initialized flag:
        initialized = true;
    }

    #endregion

    #region void LoadOptions()

    /// <summary>
    /// Loads the options of the game.
    /// </summary>
    public void LoadOptions() {
        // Get a key from the players preferences or a default value:
        Func<string, float> getFloatKey = name => {
            if (PlayerPrefs.HasKey(name)) {
                return PlayerPrefs.GetFloat(name);
            }
            return 1.0f;
        };

        // Get the options from the player preferences:
        SoundVolume = getFloatKey(SOUND_VOLUME_KEY);
        MusicVolume = getFloatKey(MUSIC_VOLUME_KEY);

        // PlayerPrefs Reference:
        // http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
    }

    #endregion

    #region void SaveOptions(float, float)

    /// <summary>
    /// Saves the options of the game.
    /// </summary>
    /// <param name="soundVolume">The sound volume.</param>
    /// <param name="musicVolume">The music volume.</param>
    public void SaveOptions(float soundVolume, float musicVolume) {
        // Set the properties of the game:
        SoundVolume = Math.Max(0.0f, Math.Min(1.0f, soundVolume));
        MusicVolume = Math.Max(0.0f, Math.Min(1.0f, musicVolume));

        // Save the current options:
        PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, SoundVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, MusicVolume);
        PlayerPrefs.Save();

        // Change the volume of the current music:
        var audio = Camera.GetComponent<AudioSource>();
        if (audio != null && audio.isPlaying) {
            audio.volume = MusicVolume;
        }

        // PlayerPrefs Reference:
        // http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
    }

    #endregion

    #region void OnlyChangeSoundOptions(float, float)

    /// <summary>
    /// Changes the sound options of the game.
    /// </summary>
    /// <param name="soundVolume">The sound volume.</param>
    /// <param name="musicVolume">The music volume.</param>
    /// <remarks>This doesn't save the changed options.</remarks>
    public void ChangeSoundOptions(float soundVolume, float musicVolume) {
        // Set the properties of the game:
        SoundVolume = Math.Max(0.0f, Math.Min(1.0f, soundVolume));
        MusicVolume = Math.Max(0.0f, Math.Min(1.0f, musicVolume));

        // Change the volume of the current music:
        var audio = Camera.GetComponent<AudioSource>();
        if (audio != null && audio.isPlaying) {
            audio.volume = MusicVolume;
        }

        // PlayerPrefs Reference:
        // http://docs.unity3d.com/ScriptReference/PlayerPrefs.html
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Profile):
    //--------------------------------------------------------------------------------------

    #region void AssignCurrentProfile(ProfileData)

    /// <summary>
    /// Assigns a profile data to the current one.
    /// </summary>
    /// <param name="data">The data to assign.</param>
    public void AssignCurrentProfile(ProfileData data) {
        if (data != null && 0 <= SelectedProfile && SelectedProfile < UserProfiles.Length) {
            UserProfiles[SelectedProfile] = data;
            SaveCurrentProfiles();
        }
    }

    #endregion

    #region void DeleteCurrentProfile()

    /// <summary>
    /// Deletes the current profile.
    /// </summary>
    public void DeleteCurrentProfile() {
        if (0 <= SelectedProfile && SelectedProfile < UserProfiles.Length) {
            UserProfiles[SelectedProfile].Empty = true;
            SaveCurrentProfiles();
        }
    }

    #endregion

    #region void LoadCurrentProfiles()

    /// <summary>
    /// Loads the current profiles from a file.
    /// </summary>
    public void LoadCurrentProfiles() {
        // The on error action when loading the file:
        Action onError = () => {
            UserProfiles = new ProfileData[CoreManager.MAX_PROFILES] {
                new ProfileData(), new ProfileData(),
                new ProfileData(), new ProfileData()
            };
        };

        // Find if the profiles file exists:
        var dataPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, PROFILES_FILE));
        if (File.Exists(dataPath)) {
            // Try to load the descriptor:
            var descriptor = LoadXml<ProfilesXml>(dataPath);
            if (descriptor == null) {
                onError();
            } else {
                // Create the current profile data:
                UserProfiles = ProfileData.Create(descriptor);
            }
        } else {
            onError();
        }
    }

    #endregion

    #region void SaveCurrentProfiles()

    /// <summary>
    /// Saves the current profiles into a file.
    /// </summary>
    public void SaveCurrentProfiles() {
        var dataPath = Path.GetFullPath(Path.Combine(Application.persistentDataPath, PROFILES_FILE));
        var descriptor = ProfileData.CreateXml(UserProfiles);
        SaveXml(dataPath, descriptor);
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Scene):
    //--------------------------------------------------------------------------------------

    #region IEnumerator loadSceneCoroutine(string, float)

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="sceneName">The scene name.</param>
    /// <param name="waitTime">The time to wait.</param>
    /// <returns>Not used, this is a coroutine.</returns>
    private IEnumerator loadSceneCoroutine(string sceneName, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(sceneName);
    }

    #endregion

    #region void loadScene(string, float)

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="sceneName">The scene name.</param>
    /// <param name="waitTime">The time to wait.</param>
    private void loadScene(string sceneName, float waitTime = float.NaN) {
        var behaviour = Camera.GetComponent<InterfaceController>();
        if (behaviour != null && !float.IsNaN(waitTime)) {
            behaviour.StartCoroutine(loadSceneCoroutine(sceneName, waitTime));
        } else {
            Application.LoadLevel(sceneName);
        }
    }

    #endregion

    #region void LoadMenuScene(bool)

    /// <summary>
    /// Loads the menu scene.
    /// </summary>
    /// <param name="forceLoad">Forces the load.</param>
    public void LoadMenuScene(bool forceLoad = false) {
        if (!IsMenuScene) {
            loadScene(MENU_SCENE_NAME, 0.05f);
        } else if (forceLoad) {
            loadScene(MENU_SCENE_NAME);
        } else {
            Debug.LogWarning("Already at scene: " + MENU_SCENE_NAME);
        }
    }

    #endregion

    #region void LoadLevelScene(bool)

    /// <summary>
    /// Loads the level scene.
    /// </summary>
    /// <param name="forceLoad">Forces the load.</param>
    public void LoadLevelScene(bool forceLoad = false) {
        if (!IsMenuScene) {
            loadScene(LEVEL_SCENE_NAME, 0.05f);
        } else if (forceLoad) {
            loadScene(LEVEL_SCENE_NAME);
        } else {
            Debug.LogWarning("Already at scene: " + LEVEL_SCENE_NAME);
        }
    }

    #endregion

    #region void LoadTutorialScene(bool)

    /// <summary>
    /// Loads the tutorial scene.
    /// </summary>
    /// <param name="forceLoad">Forces the load.</param>
    public void LoadTutorialScene(bool forceLoad = false) {
        if (!IsMenuScene) {
            loadScene(TUTORIAL_SCENE_NAME, 0.05f);
        } else if (forceLoad) {
            loadScene(TUTORIAL_SCENE_NAME);
        } else {
            Debug.LogWarning("Already at scene: " + TUTORIAL_SCENE_NAME);
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (States):
    //--------------------------------------------------------------------------------------

    #region void ChangeNextState(IGameState)

    /// <summary>
    /// Changes the next state of the game.
    /// </summary>
    /// <param name="victim">The next state.</param>
    public void ChangeNextState(IGameState victim) {
        if (victim != null && victim != State) {
            nextState = victim;
        }
    }

    #endregion

    #region void ChangeState()

    /// <summary>
    /// Changes the current state.
    /// </summary>
    public void ChangeState() {
        if (nextState != null && nextState != State) {
            if (State != null) {
                State.Release();
            }
            State = nextState;
            State.Initialize();
            nextState = null;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Menu):
    //--------------------------------------------------------------------------------------

    #region void GotoMenu()

    /// <summary>
    /// Goes to the menu screen.
    /// </summary>
    public void GotoMenu() {
        ChangeNextState(new MenuState());
    }

    #endregion

    #region void GotoLoadTutorial()

    /// <summary>
    /// Goes to the load tutorial screen.
    /// </summary>
    public void GotoLoadTutorial() {
        if (LoadLevelDescriptor(TUTORIAL_PATH)) {
            ChangeNextState(new LoadTutorialState());
        } else {
            Debug.LogError("Can't load the level descriptor.");
        }
    }

    #endregion

    #region void GotoTutorial()

    /// <summary>
    /// Goes to the tutorial screen.
    /// </summary>
    public void GotoTutorial() {
        ChangeNextState(new TutorialState());
        HideMenuBackground();
        levelController.enabled = true;
    }

    #endregion

    #region void GotoTutorialExit()

    /// <summary>
    /// Goes to the tutorial pause screen.
    /// </summary>
    public void GotoTutorialExit() {
        levelController.enabled = false;
        levelController.ActivateSkipMouseUntilUp();
        ShowMenuBackground();
        ChangeNextState(new ExitTutorialState());
    }

    #endregion

    #region void GotoTutorialVictory()

    /// <summary>
    /// Goes to the tutorial victory screen.
    /// </summary>
    public void GotoTutorialVictory() {
        levelController.enabled = false;
        ChangeNextState(new TutorialVictoryState());
    }

    #endregion

    #region void GotoSelectProfile()

    /// <summary>
    /// Goes to the select profile screen.
    /// </summary>
    public void GotoSelectProfile() {
        ChangeNextState(new SelectProfileState());
    }

    #endregion

    #region void GotoCreateProfile()

    /// <summary>
    /// Goes to the create profile screen.
    /// </summary>
    public void GotoCreateProfile() {
        ChangeNextState(new CreateProfileState());
    }

    #endregion

    #region void GotoDeleteProfile()

    /// <summary>
    /// Goes to the delete profile screen.
    /// </summary>
    public void GotoDeleteProfile() {
        ChangeNextState(new DeleteProfileState());
    }

    #endregion

    #region void GotoSelectChapter()

    /// <summary>
    /// Goes to the select chapter screen.
    /// </summary>
    public void GotoSelectChapter() {
        ChangeNextState(new SelectChapterState());
    }

    #endregion

    #region void GotoSelectLevel()

    /// <summary>
    /// Goes to the select level screen.
    /// </summary>
    public void GotoSelectLevel() {
        ChangeNextState(new SelectLevelState());
    }

    #endregion

    #region void GotoLevelDescription()

    /// <summary>
    /// Goes to the level description screen.
    /// </summary>
    public void GotoLevelDescription() {
        if (CurrentLevelFileName != null &&
            LoadLevelDescriptor(BASE_XML_PATH + CurrentLevelFileName)) {
            ChangeNextState(new LevelDescriptionState());
        } else {
            Debug.LogError("Can't load the level descriptor.");
        }
    }

    #endregion

    #region void GotoLoadLevel()

    /// <summary>
    /// Goes to the load level screen.
    /// </summary>
    public void GotoLoadLevel() {
        ChangeNextState(new LoadLevelState());
    }

    #endregion

    #region void GotoLevel()

    /// <summary>
    /// Goes to the level screen.
    /// </summary>
    public void GotoLevel() {
        ChangeNextState(new LevelState());
        HideMenuBackground();
        levelController.enabled = true;
    }

    #endregion

    #region void GotoLevelPause()

    /// <summary>
    /// Goes to the level pause screen.
    /// </summary>
    public void GotoLevelPause() {
        levelController.enabled = false;
        levelController.ActivateSkipMouseUntilUp();
        ShowMenuBackground();
        ChangeNextState(new LevelPauseState());
    }

    #endregion

    #region void GotoExitLevel()

    /// <summary>
    /// Goes to the exit level screen.
    /// </summary>
    public void GotoExitLevel() {
        ChangeNextState(new ExitLevelState());
    }

    #endregion

    #region void GotoResetLevel()

    /// <summary>
    /// Goes to the reset level screen.
    /// </summary>
    public void GotoResetLevel() {
        ChangeNextState(new ResetLevelState());
    }

    #endregion

    #region void GotoVictory()

    /// <summary>
    /// Goes to the victory screen.
    /// </summary>
    public void GotoVictory() {
        levelController.enabled = false;
        ChangeNextState(new VictoryState());
    }

    #endregion

    #region void GotoOptions()

    /// <summary>
    /// Goes to the options screen.
    /// </summary>
    public void GotoOptions() {
        ChangeNextState(new OptionsState());
    }

    #endregion

    #region void GotoCredits()

    /// <summary>
    /// Goes to the credits screen.
    /// </summary>
    public void GotoCredits() {
        ChangeNextState(new CreditsState());
    }

    #endregion

    #region void GotoExit()

    /// <summary>
    /// Goes to the exit screen.
    /// </summary>
    public void GotoExit() {
        ChangeNextState(new ExitState());
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Level):
    //--------------------------------------------------------------------------------------

    #region bool LoadLevelDescriptor(string)

    /// <summary>
    /// Loads a level descriptor form an xml file.
    /// </summary>
    /// <param name="path">The path inside the resources folder.</param>
    /// <returns>If the descriptor is loaded or not.</returns>
    public bool LoadLevelDescriptor(string path) {
        LevelDescriptor = LoadXmlFromResources<LevelXml>(path);
        return LevelDescriptor != null;
    }

    #endregion

    #region bool LoadUserLevelDescriptor(string)

    /// <summary>
    /// Loads a level descriptor form an xml file.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <returns>If the descriptor is loaded or not.</returns>
    public bool LoadUserLevelDescriptor(string path) {
        LevelDescriptor = LoadXml<LevelXml>(path);
        return LevelDescriptor != null;
    }

    #endregion

    #region bool LoadLevel()

    /// <summary>
    /// Loads the current level with the loaded level descriptor.
    /// </summary>
    /// <returns>If the level is loaded or not.</returns>
    public bool LoadLevel() {
        if (LevelDescriptor != null) {
            // Set some logic data:
            changeSteps(0);
            changeSecond(0);

            // Clean unused assets:
            Resources.UnloadUnusedAssets();

            // Add the input controller to the world:
            var world = SceneUtil.Find(CoreManager.WORLD_GOBJ_NAME);
            levelController = world.AddComponent<LevelController>();
            levelController.enabled = true;

            // Change the layer of the terrain and entities nodes:
            Action<string, GameObject, float> updateNode = (name, parent, z) => {
                var victim = SceneUtil.Find(name);
                SceneUtil.SetParent(victim, parent);
                SceneUtil.SetPosition(victim, 0.0f, 0.0f, z);
            };

            updateNode(CoreManager.TERRAIN_GOBJ_NAME, world, 1.0f);
            updateNode(CoreManager.ENTITIES_GOBJ_NAME, world, 0.0f);

            // Create the current level:
            Level = new LevelData();
            Level.Create(LevelDescriptor);
            return Level.IsLoaded;
        }
        return false;
    }

    #endregion

    #region void AddStep()

    /// <summary>
    /// Adds one step to the player.
    /// </summary>
    public void AddStep() {
        changeSteps(Steps + 1);
    }

    #endregion

    #region void AddSecond()

    /// <summary>
    /// Adds one second to the player.
    /// </summary>
    public void AddSecond() {
        changeSecond(Seconds + 1);
    }

    #endregion

    #region void changeSteps(int)

    /// <summary>
    /// Changes the steps of the player.
    /// </summary>
    /// <param name="value">The value to set.</param>
    private void changeSteps(int value) {
        Steps = value;
        StepsMessage = Strings.LEVEL_STEPS + Steps;
    }

    #endregion

    #region void changeSecond(int)

    /// <summary>
    /// Changes the seconds of the player.
    /// </summary>
    /// <param name="value">The value to set.</param>
    private void changeSecond(int value) {
        Seconds = value;
        SecondsMessage = Strings.LEVEL_TIME + Seconds;
    }

    #endregion

    #region void CheckVictory()

    /// <summary>
    /// Checks if the victory have been reached.
    /// </summary>
    public bool CheckVictory() {
        // Find all the box controller components:
        var victims = GameObject.FindObjectsOfType<BoxController>();
        if (victims != null && victims.Length > 0) {
            // Check if every box is at the destination:
            bool victory = true;
            foreach (var item in victims) {
                if (!item.IsAtDestination) {
                    victory = false;
                    break;
                }
            }
            // If victory, exit the level:
            if (victory) {
                if (IsTutorialScene) {
                    GotoTutorialVictory();
                } else {
                    GotoVictory();
                }
                return true;
            }
        }
        return false;
    }

    #endregion

    #region void LevelFinished()

    /// <summary>
    /// Saves the last score and updates the profile.
    /// </summary>
    public void LevelFinished() {
        if (SelectedChapter == CurrentProfile.LastChapterUnlocked &&
            SelectedLevel == CurrentProfile.LastLevelUnlocked) {
            CurrentProfile.LastLevelUnlocked++;
        }
        CurrentProfile.SetScore(SelectedChapter, SelectedLevel, Steps, Seconds);
        SaveCurrentProfiles();
    }

    #endregion

    #region void HideMenuBackground()

    /// <summary>
    /// Hides the menu background in the level scene.
    /// </summary>
    public void HideMenuBackground() {
        if (IsLevelScene || IsTutorialScene) {
            var menuBackground = SceneUtil.Find(CoreManager.MNUBACK_GOBJ_NAME, false);
            var renderer = menuBackground.GetComponent<SpriteRenderer>();
            renderer.enabled = false;
        }
    }

    #endregion

    #region void ShowMenuBackground()

    /// <summary>
    /// Shows the menu background in the level scene.
    /// </summary>
    public void ShowMenuBackground() {
        if (IsLevelScene || IsTutorialScene) {
            var menuBackground = SceneUtil.Find(CoreManager.MNUBACK_GOBJ_NAME, false);
            var renderer = menuBackground.GetComponent<SpriteRenderer>();
            renderer.enabled = true;
        }
    }

    #endregion

    #region Texture2D MakePreview()

    /// <summary>
    /// Makes a preview texture of the current level descriptor loaded.
    /// </summary>
    /// <returns>The preview texture.</returns>
    public Texture2D MakePreview() {
        var terrain = LevelDescriptor.World.Terrain;
        var ww = LevelDescriptor.World.Width;
        var wh = LevelDescriptor.World.Height;
        var maxy = wh - 1;
        Texture2D preview = new Texture2D(ww, wh, TextureFormat.RGB24, false, false);
        // Paint the terrain:
        for (int i = 0, k = 0; i < wh; i++) {
            for (int j = 0; j < ww; j++, k++) {
                var id = terrain[k];
                Color color = AtariPalette.Hue00Lum00;
                if (TERRAIN_ID_FIRST_WALL <= id && id <= TERRAIN_ID_LAST_WALL) {
                    color = AtariPalette.Hue04Lum02;
                } else if (TERRAIN_ID_FIRST_FLOOR <= id && id <= TERRAIN_ID_LAST_FLOOR) {
                    color = AtariPalette.Hue00Lum06;
                } else if (TERRAIN_ID_FIRST_SFLOOR <= id && id <= TERRAIN_ID_LAST_SFLOOR) {
                    color = AtariPalette.Hue00Lum06;
                } else if (TERRAIN_ID_FIRST_DFLOOR <= id && id <= TERRAIN_ID_LAST_DFLOOR) {
                    color = AtariPalette.Hue01Lum10;
                }
                preview.SetPixel(j, maxy - i, color);
            }
        }
        // Paint the entities:
        foreach (var item in LevelDescriptor.Entities) {
            Color color = AtariPalette.Hue00Lum00;
            if (item.Type == EntityType.PLAYER) {
                color = AtariPalette.Hue10Lum02;
            } else {
                var id = terrain[item.X + item.Y * ww];
                if (TERRAIN_ID_FIRST_DFLOOR <= id && id <= TERRAIN_ID_LAST_DFLOOR) {
                    color = AtariPalette.Hue15Lum00;
                } else {
                    color = AtariPalette.Hue15Lum02;
                }
            }
            preview.SetPixel(item.X, maxy - item.Y, color);
        }
        // Return the texture:
        preview.filterMode = FilterMode.Point;
        preview.Apply();
        return preview;
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Textures):
    //--------------------------------------------------------------------------------------

    #region Texture2D LoadMenuTexture(string)

    /// <summary>
    /// Loads a menu texture.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The loaded texture.</returns>
    public Texture2D LoadMenuTexture(string fileName) {
        return Resources.Load<Texture2D>(MENU_TEXTURE_PATH + fileName);
    }

    #endregion

    #region Texture2D LoadGameTexture(string)

    /// <summary>
    /// Loads a game texture.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The loaded texture.</returns>
    public Texture2D LoadGameTexture(string fileName) {
        return Resources.Load<Texture2D>(GAME_TEXTURE_PATH + fileName);
    }

    #endregion

    #region Sprite[] LoadGameSprites(string)

    /// <summary>
    /// Loads a collection of game sprites.
    /// </summary>
    /// <param name="collectionName">The collection name.</param>
    /// <returns>The loaded sprites.</returns>
    public Sprite[] LoadGameSprites(string collectionName) {
        return Resources.LoadAll<Sprite>(GAME_TEXTURE_PATH + collectionName);
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Music):
    //--------------------------------------------------------------------------------------

    #region bool IsMusicPlaying()

    /// <summary>
    /// Checks if the music is playing or not.
    /// </summary>
    /// <returns>If the music is playing or not.</returns>
    public bool IsMusicPlaying() {
        var audio = Camera.GetComponent<AudioSource>();
        if (audio != null) {
            return audio.isPlaying;
        }
        return false;
    }

    #endregion

    #region void PlayMusic(string, bool)

    /// <summary>
    /// Plays a music file.
    /// </summary>
    /// <param name="path">The path of the file.</param>
    /// <param name="loop">The loop flag.</param>
    public void PlayMusic(string path, bool loop = true) {
        var audio = Camera.GetComponent<AudioSource>();
        if (audio != null) {
            audio.clip = Resources.Load<AudioClip>(path);
            audio.loop = loop;
            audio.volume = MusicVolume;
            audio.Play();
        }
    }

    #endregion

    #region void PlayMainMusic()

    /// <summary>
    /// Plays the main music.
    /// </summary>
    public void PlayMainMusic() {
        PlayMusic(MAIN_MUSIC);
    }

    #endregion

    #region void PlayVictoryMusic()

    /// <summary>
    /// Plays the victory music.
    /// </summary>
    public void PlayVictoryMusic() {
        PlayMusic(VICTORY_MUSIC, false);
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (XML):
    //--------------------------------------------------------------------------------------

    #region T LoadXmlFromResources<T>(string)

    /// <summary>
    /// Loads a xml file from the resources folder.
    /// </summary>
    /// <typeparam name="T">The type to be loaded.</typeparam>
    /// <param name="path">The path inside the resources folder.</param>
    /// <returns>The loaded object if success, otherwise null.</returns>
    public T LoadXmlFromResources<T>(string path) where T : class {
        try {
            var asset = Resources.Load<TextAsset>(path);
            var reader = new StringReader(asset.text);
            var serializer = new XmlSerializer(typeof(T));
            var stream = new XmlTextReader(reader);

            T victim = (T)serializer.Deserialize(stream);
            stream.Close();
            return victim;

        } catch (Exception e) {
            Log(e);
            return null;
        }
    }

    #endregion

    #region T LoadXml<T>(string)

    /// <summary>
    /// Loads a xml file into an object.
    /// </summary>
    /// <typeparam name="T">The type to be loaded.</typeparam>
    /// <param name="path">The path of the file.</param>
    /// <returns>The loaded object if success, otherwise null.</returns>
    public T LoadXml<T>(string path) where T : class {
        try {
            var serializer = new XmlSerializer(typeof(T));
            var stream = new XmlTextReader(path);

            T victim = (T)serializer.Deserialize(stream);
            stream.Close();
            return victim;

        } catch (Exception e) {
            Log(e);
            return null;
        }
    }

    #endregion

    #region void SaveXml<T>(string, T)

    /// <summary>
    /// Saves an object into a xml file.
    /// </summary>
    /// <typeparam name="T">The type to be saved.</typeparam>
    /// <param name="path">The path of the file.</param>
    /// <param name="victim">The objecto to be saved.</param>
    public void SaveXml<T>(string path, T victim) where T : class {
        try {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlTextWriter stream = new XmlTextWriter(path, Encoding.UTF8);

            stream.Formatting = Formatting.Indented;
            stream.Indentation = 1;
            stream.IndentChar = '\t';

            serializer.Serialize(stream, victim);
            stream.Close();

        } catch (Exception e) {
            Log(e);
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Debug):
    //--------------------------------------------------------------------------------------

    #region void Log(Exception)

    /// <summary>
    /// Shows some information inside the debug console.
    /// </summary>
    /// <param name="e">The exception to show.</param>
    public void Log(Exception e) {
        if (e != null) {
            var msg = "Message: " + e.Message + "\n\n" +
                      "Source: " + e.Source + "\n\n" +
                      "StackTrace: " + e.StackTrace + "\n\n" +
                      "TargetSite: " + e.TargetSite.Name + "\n\n" +
                      "HelpLink: " + e.HelpLink + "\n";
            Debug.LogError(msg);
            if (e.InnerException != null) {
                Log(e.InnerException);
            }
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Singleton ( https://en.wikipedia.org/wiki/Singleton_pattern ):
    //--------------------------------------------------------------------------------------

    #region CoreManager()

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    private CoreManager() {
        // General:
        initialized = false;
        TextFont = null;
        // Options:
        SoundVolume = 1.0f;
        MusicVolume = 1.0f;
        // Scenes:
        // States:
        State = null;
        nextState = null;
        // Profiles:
        SelectedProfile = NO_PROFILE;
        UserProfiles = null;
        // Chapters:
        SelectedChapter = 0;
        SelectedLevel = 0;
        Chapters = null;
        // Level:
        LevelDescriptor = null;
        Level = null;
        levelController = null;
        changeSteps(0);
        changeSecond(0);
    }

    #endregion

    #region CoreManager instance

    /// <summary>
    /// The main instance of the class.
    /// </summary>
    private static CoreManager instance = new CoreManager();

    #endregion

    #region CoreManager Instance

    /// <summary>
    /// Gets the main instance of the class.
    /// </summary>
    public static CoreManager Instance { get { return instance; } }

    #endregion
}
