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
/// This class represents the box behaviour inside a game object.
/// </summary>
public class BoxController : EntityController {
    //--------------------------------------------------------------------------------------
    // Constants:
    //--------------------------------------------------------------------------------------

    #region Constants

    public const int TYPE_INVALID = -1;
    public const int TYPE_BOX = 0;
    public const int MAX_BOX_TYPES = 128;

    #endregion

    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region int boxType

    /// <summary>
    /// The type of the box.
    /// </summary>
    private int boxType;

    #endregion

    #region SpriteRenderer spriteRenderer

    /// <summary>
    /// The sprite renderer component.
    /// </summary>
    private SpriteRenderer spriteRenderer = null;

    #endregion

    #region Sprite normalSprite

    /// <summary>
    /// The normal sprite asset.
    /// </summary>
    private Sprite normalSprite = null;

    #endregion

    #region Sprite destinationSprite

    /// <summary>
    /// The destination sprite asset.
    /// </summary>
    private Sprite destinationSprite = null;

    #endregion

    #region bool IsAtDestination

    /// <summary>
    /// Gets the on destination flag.
    /// </summary>
    public bool IsAtDestination { get; private set; }

    #endregion

    #region AudioClip pushSound

    /// <summary>
    /// The push sound of the entity.
    /// </summary>
    private AudioClip pushSound;

    #endregion

    #region AudioClip settledSound

    /// <summary>
    /// The settled sound of the entity.
    /// </summary>
    private AudioClip settledSound;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void configure(int, Sprite, Sprite)

    /// <summary>
    /// Configures the controller.
    /// </summary>
    /// <param name="type">The type of the box.</param>
    /// <param name="nspr">The normal sprite asset.</param>
    /// <param name="dspr">The destination sprite asset.</param>
    private void configure(int type, Sprite nspr, Sprite dspr) {
        // Set the members of the component:
        boxType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalSprite = nspr;
        destinationSprite = dspr;
        IsAtDestination = false;

        // Set the sounds of the component:
        pushSound = Resources.Load<AudioClip>(CoreManager.PUSH_SOUND);
        settledSound = Resources.Load<AudioClip>(CoreManager.SETTLED_SOUND);

        // Set the entity events:
        var core = CoreManager.Instance;
        onEnterCell = () => {
            checkCurrentCell();
        };
        onExitCell = () => {
            GetComponent<AudioSource>().PlayOneShot(pushSound, core.SoundVolume);
            IsAtDestination = false;
            spriteRenderer.sprite = normalSprite;
        };
    }

    #endregion

    #region void checkCurrentCell()

    /// <summary>
    /// Checks the current cell.
    /// </summary>
    private void checkCurrentCell() {
        var cell = Cell;
        if (cell != null && CoreManager.TERRAIN_ID_FIRST_DFLOOR <= cell.Terrain &&
            cell.Terrain <= CoreManager.TERRAIN_ID_LAST_DFLOOR) {
            if (boxType != TYPE_BOX) {
                // Check cell for specific type of boxes:
                var destFloorType = cell.Terrain - CoreManager.TERRAIN_ID_FIRST_DFLOOR;
                if (destFloorType == boxType) {
                    var core = CoreManager.Instance;
                    IsAtDestination = true;
                    spriteRenderer.sprite = destinationSprite;
                    if (!core.CheckVictory()) {
                        GetComponent<AudioSource>().PlayOneShot(settledSound, core.SoundVolume);
                    }
                } else {
                    IsAtDestination = false;
                    spriteRenderer.sprite = normalSprite;
                }
            } else {
                // Check cell for standard type of box that fits all:
                var core = CoreManager.Instance;
                IsAtDestination = true;
                spriteRenderer.sprite = destinationSprite;
                if (!core.CheckVictory()) {
                    GetComponent<AudioSource>().PlayOneShot(settledSound, core.SoundVolume);
                }
            }
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
            }
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods (Static):
    //--------------------------------------------------------------------------------------

    #region bool Create(EntityXml, GameObject, string, Sprite[])

    /// <summary>
    /// Creates a new box entity.
    /// </summary>
    /// <param name="descriptor">The descriptor of the entity.</param>
    /// <param name="parent">The parent game object.</param>
    /// <param name="tileset">The name of the tileset to use.</param>
    /// <param name="sprites">The loaded sprites to use.</param>
    /// <returns>Returns if the entity have been created or not.</returns>
    public static bool Create(EntityXml descriptor, GameObject parent, string tileset, Sprite[] sprites) {
        // Check the type of the descriptor:
        var type = GetType(descriptor);
        if (type == TYPE_INVALID) return false;

        // Check if the entity will be inside the world:
        var level = CoreManager.Instance.Level;
        var cell = level.GetCell(descriptor.X, descriptor.Y);
        if (cell == null || cell.EntityObject != null) return false;

        // Create the player node inside the scene:
        var victim = new GameObject(CoreManager.BOX_GOBJ_NAME);
        SceneUtil.SetParent(victim, parent);
        SceneUtil.SetPosition(victim, cell.TerrainObject);

        // Find the sprites:
        var normalSpriteName = GetNormalSpriteName(tileset, type);
        var destinationSpriteName = GetDestinationSpriteName(tileset, type);
        var normalSprite = sprites.Where(x => x.name == normalSpriteName).FirstOrDefault();
        var destinationSprite = sprites.Where(x => x.name == destinationSpriteName).FirstOrDefault();

        // Create the sprite renderer component:
        var spriteRenderer = victim.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;

        // Create the audio source component:
        victim.AddComponent<AudioSource>();

        // Create the player behaviour component:
        var boxController = victim.AddComponent<BoxController>();
        boxController.configure(type, normalSprite, destinationSprite);
        boxController.EnterCell(cell);

        // Everything is alright:
        return true;
    }

    #endregion

    #region int GetType(EntityXml)

    /// <summary>
    /// Gets the type of box entity.
    /// </summary>
    /// <param name="descriptor">The descriptor of the entity.</param>
    /// <returns>The type of box if valid, otherwise -1.</returns>
    public static int GetType(EntityXml descriptor) {
        var type = descriptor.Type.ToLower();
        if (type == EntityType.BOX) {
            return TYPE_BOX;
        } else if (type.StartsWith(EntityType.BOX)) {
            try {
                var boxId = int.Parse(type.Substring(EntityType.BOX.Length));
                return TYPE_BOX + boxId;
            } catch (Exception) {
            }
        }
        return -1;
    }

    #endregion

    #region string GetNormalSpriteName(string, int)

    /// <summary>
    /// Gets the normal box sprite name.
    /// </summary>
    /// <param name="tileset">The name of the tileset to use.</param>
    /// <param name="type">The type of box.</param>
    /// <returns>The name of the sprite.</returns>
    public static string GetNormalSpriteName(string tileset, int type) {
        var id = CoreManager.TERRAIN_ID_FIRST_BOX;
        if (TYPE_BOX <= type && type < MAX_BOX_TYPES) {
            id += (uint)type;
        }
        return tileset + "_" + id;
    }

    #endregion

    #region string GetDestinationSpriteName(string, int)

    /// <summary>
    /// Gets the destination box sprite name.
    /// </summary>
    /// <param name="tileset">The name of the tileset to use.</param>
    /// <param name="type">The type of box.</param>
    /// <returns>The name of the sprite.</returns>
    public static string GetDestinationSpriteName(string tileset, int type) {
        var id = CoreManager.TERRAIN_ID_FIRST_DBOX;
        if (TYPE_BOX <= type && type < MAX_BOX_TYPES) {
            id += (uint)type;
        }
        return tileset + "_" + id;
    }

    #endregion
}
