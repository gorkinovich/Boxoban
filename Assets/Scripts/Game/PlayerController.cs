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
using UnityEngine;

/// <summary>
/// This class represents the player behaviour inside a game object.
/// </summary>
public class PlayerController : EntityController {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    public const string IDLE_EXT = "_0";
    public const string WALK0_EXT = "_1";
    public const string WALK1_EXT = "_2";
    public const string WALK2_EXT = "_3";
    public const string WALK3_EXT = "_4";
    public const string WALK4_EXT = "_5";
    public const string WALK5_EXT = "_6";
    public const string PUSH0_EXT = "_7";
    public const string PUSH1_EXT = "_8";
    public const string PUSH2_EXT = "_9";
    public const string PUSH3_EXT = "_10";
    public const string PUSH4_EXT = "_11";
    public const string PUSH5_EXT = "_12";

    #endregion

    //--------------------------------------------------------------------------------------
    // Types:
    //--------------------------------------------------------------------------------------

    #region Types

    /// <summary>
    /// This enumeration represents the player animation state.
    /// </summary>
    private enum AnimationState {
        Idle, Walk, Push
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region SpriteRenderer spriteRenderer

    /// <summary>
    /// The sprite renderer of the entity.
    /// </summary>
    private SpriteRenderer spriteRenderer = null;

    #endregion

    #region AnimationState animationState

    /// <summary>
    /// The animation state of the entity.
    /// </summary>
    private AnimationState animationState = AnimationState.Idle;

    #endregion

    #region Sprite idleSprite

    /// <summary>
    /// The idle sprite of the entity.
    /// </summary>
    private Sprite idleSprite;

    #endregion

    #region Sprite[] walkSprite

    /// <summary>
    /// The walk animation sprites of the entity.
    /// </summary>
    private Sprite[] walkSprite;

    #endregion

    #region Sprite[] pushSprite

    /// <summary>
    /// The push animation sprites of the entity.
    /// </summary>
    private Sprite[] pushSprite;

    #endregion

    #region Timer animationTimer

    /// <summary>
    /// The animation sprite of the entity.
    /// </summary>
    private Timer animationTimer;

    #endregion

    #region int currentFrame

    /// <summary>
    /// The current frame in the animation of the entity.
    /// </summary>
    private int currentFrame;

    #endregion

    #region AudioClip walkSound

    /// <summary>
    /// The walk sound of the entity.
    /// </summary>
    private AudioClip walkSound;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void configure(string, Sprite[])

    private void configure(string charset, Sprite[] sprites) {
        // Get the sprite renderer of the player:
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Change the animation state:
        animationState = AnimationState.Idle;

        // Load the sprites to use in the animations:
        Func<string, Sprite> getSprite = ext => {
            return sprites.Where(x => x.name == charset + ext).FirstOrDefault();
        };
        idleSprite = getSprite(IDLE_EXT);
        walkSprite = new Sprite[] {
            getSprite(WALK1_EXT),
            getSprite(WALK4_EXT)
        };
        pushSprite = new Sprite[] {
            getSprite(PUSH1_EXT),
            getSprite(PUSH4_EXT)
        };
        //walkSprite = new Sprite[] {
        //    getSprite(WALK0_EXT), getSprite(WALK1_EXT),
        //    getSprite(WALK2_EXT), getSprite(WALK3_EXT),
        //    getSprite(WALK4_EXT), getSprite(WALK5_EXT)
        //};
        //pushSprite = new Sprite[] {
        //    getSprite(PUSH0_EXT), getSprite(PUSH1_EXT),
        //    getSprite(PUSH2_EXT), getSprite(PUSH3_EXT),
        //    getSprite(PUSH4_EXT), getSprite(PUSH5_EXT)
        //};

        // Creates the timer to control the animation:
        animationTimer = new Timer();
        currentFrame = 0;

        // Load the sound to use in the entity:
        var core = CoreManager.Instance;
        walkSound = Resources.Load<AudioClip>(CoreManager.WALK_SOUND);

        // Set the "on exit cell" event handler:
        onExitCell = () => {
            GetComponent<AudioSource>().PlayOneShot(walkSound, core.SoundVolume);
        };

        // Set the "on enter cell" event handler:
        onEnterCell = () => {
            // Stop the walk sound:
            if (GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().loop = false;
            }
            // Changes the animation state of the player:
            ShowIdle();
        };

        // Set the "on set destination" event handler:
        onSetDestination = () => {
            // Sets the rotation of the sprite when moving:
            gameObject.transform.rotation = new Quaternion();
            if (movingAction.Direction == MoveDirection.North) {
                gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
            } else if (movingAction.Direction == MoveDirection.West) {
                gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
            } else if (movingAction.Direction == MoveDirection.South) {
                gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, 270.0f));
            }
        };
    }

    #endregion

    #region void ShowIdle()

    /// <summary>
    /// Show the idle animation of the player.
    /// </summary>
    public void ShowIdle() {
        animationState = AnimationState.Idle;
        spriteRenderer.sprite = idleSprite;
        animationTimer.Enable = false;
    }

    #endregion

    #region void ShowWalk()

    /// <summary>
    /// Show the walk animation of the player.
    /// </summary>
    public void ShowWalk() {
        animationState = AnimationState.Walk;
        currentFrame = 0;
        updateSprite();
        float interval = MovingAction.MAX_TIME / (float)walkSprite.Length;
        animationTimer.SetAndEnable(interval, owner => {
            currentFrame = (currentFrame + 1) % walkSprite.Length;
            updateSprite();
        });
    }

    #endregion

    #region void ShowPush()

    /// <summary>
    /// Show the push animation of the player.
    /// </summary>
    public void ShowPush() {
        animationState = AnimationState.Push;
        currentFrame = 0;
        updateSprite();
        float interval = MovingAction.MAX_TIME / (float)walkSprite.Length;
        animationTimer.SetAndEnable(interval, owner => {
            currentFrame = (currentFrame + 1) % pushSprite.Length;
            updateSprite();
        });
    }

    #endregion

    #region void updateSprite()

    /// <summary>
    /// Updates the current sprite to show.
    /// </summary>
    private void updateSprite() {
        switch (animationState) {
            case AnimationState.Idle:
                spriteRenderer.sprite = idleSprite;
                break;
            case AnimationState.Walk:
                spriteRenderer.sprite = walkSprite[currentFrame];
                break;
            case AnimationState.Push:
                spriteRenderer.sprite = pushSprite[currentFrame];
                break;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Events):
    //--------------------------------------------------------------------------------------

    #region void Start()

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void Start() {
    }

    #endregion

    #region void Update()

    /// <summary>
    /// Updates the component. This is called once per frame.
    /// </summary>
    void Update() {
        if (IsMoving) {
            if (movingAction.Finished) {
                movingAction.Finish();
                movingAction = null;
            } else {
                movingAction.Update();
                animationTimer.Update();
            }
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Static):
    //--------------------------------------------------------------------------------------

    #region bool Create(EntityXml, GameObject)

    /// <summary>
    /// Creates a new player entity.
    /// </summary>
    /// <param name="descriptor">The descriptor of the entity.</param>
    /// <param name="parent">The parent game object.</param>
    /// <returns>Returns if the entity have been created or not.</returns>
    public static bool Create(EntityXml descriptor, GameObject parent) {
        // Check the type of the descriptor:
        if (descriptor.Type.ToLower() != EntityType.PLAYER) return false;

        // Check if the entity will be inside the world:
        var core = CoreManager.Instance;
        var level = core.Level;
        var cell = level.GetCell(descriptor.X, descriptor.Y);
        if (cell == null || cell.EntityObject != null) return false;

        // Load all the sprites from the selected charset:
        var charset = core.CurrentProfile != null ?
            core.CurrentProfile.Charset :
            ProfileData.GetRandomCharset();
        var sprites = core.LoadGameSprites(ProfileData.CHARSET_FILE_BASE);

        // Create the player node inside the scene:
        var victim = new GameObject(CoreManager.PLAYER_GOBJ_NAME);
        SceneUtil.SetParent(victim, parent);
        SceneUtil.SetPosition(victim, cell.TerrainObject);

        // Create the sprite renderer component:
        var spriteRenderer = victim.AddComponent<SpriteRenderer>();
        var sprite = sprites.Where(x => x.name == charset + IDLE_EXT).FirstOrDefault();
        spriteRenderer.sprite = sprite;

        // Create the audio source component:
        victim.AddComponent<AudioSource>();

        // Create the player behaviour component:
        var playerController = victim.AddComponent<PlayerController>();
        playerController.configure(charset, sprites);
        playerController.EnterCell(cell);

        // Everything is alright:
        return true;
    }

    #endregion
}
