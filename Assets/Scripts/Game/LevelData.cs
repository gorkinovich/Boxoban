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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class represents the data of a level.
/// </summary>
public class LevelData {
    //--------------------------------------------------------------------------------------
    // Properties:
    //--------------------------------------------------------------------------------------

    #region bool IsLoaded

    /// <summary>
    /// Gets if the level is loaded.
    /// </summary>
    public bool IsLoaded { get; private set; }

    #endregion

    #region int Width

    /// <summary>
    /// Gets the width of the world.
    /// </summary>
    public int Width { get; private set; }

    #endregion

    #region int Height

    /// <summary>
    /// Gets the height of the world.
    /// </summary>
    public int Height { get; private set; }

    #endregion

    #region CellData[] cells

    /// <summary>
    /// The cells of the world.
    /// </summary>
    private CellData[] cells;

    #endregion

    //--------------------------------------------------------------------------------------
    // Methods:
    //--------------------------------------------------------------------------------------

    #region void Create(LevelXml)

    /// <summary>
    /// Create the level inside the current scene.
    /// </summary>
    /// <param name="descriptor">The level descriptor.</param>
    public void Create(LevelXml descriptor) {
        // Set the loaded flag to false:
        IsLoaded = false;

        // And then try to load the data in the scene:
        try {
            // Set the size of the world:
            Width = descriptor.World.Width;
            Height = descriptor.World.Height;

            // Check that the size of the world is correct:
            var data = descriptor.World.Terrain;
            if (data.Length != Width * Height) {
                throw new Exception("Invalid size inside the level descriptor for the world.");
            }

            // Load all the sprites from the selected tileset:
            var sprites = CoreManager.Instance.LoadGameSprites(descriptor.Tileset);

            // Get the world node inside the scene:
            var worldObject = GameObject.Find(CoreManager.TERRAIN_GOBJ_NAME);

            // This function creates a new tile game object in the scene:
            Func<uint, GameObject> createTile = id => {
                // Create the object and set the parent:
                var victim = new GameObject(CoreManager.TILE_GOBJ_NAME);
                SceneUtil.SetParent(victim, worldObject);
                // Create the sprite renderer component:
                var spriteRenderer = victim.AddComponent<SpriteRenderer>();
                var sprite = sprites.Where(x => x.name == descriptor.Tileset + "_" + id)
                                    .FirstOrDefault();
                spriteRenderer.sprite = sprite;
                // Return the object created:
                return victim;
            };

            // Set some data to calculate the position of the tiles:
            int widthInPixels = Width * CoreManager.TILE_WIDTH;
            int heightInPixels = Height * CoreManager.TILE_HEIGHT;
            const int PIVOT_OFFSET = 8;
            int startX = PIVOT_OFFSET - (widthInPixels / 2);
            int startY = (heightInPixels / 2) - PIVOT_OFFSET;

            // For each id inside the terrain array of the world inside the descriptor
            // we're going to create a new tile and add it to the array of cells:
            List<CellData> cellsList = new List<CellData>();

            for (int i = 0, k = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++, k++) {
                    // Create the tile and change its position:
                    var tile = createTile(data[k]);
                    SceneUtil.SetPosition(
                        tile,
                        (startX + CoreManager.TILE_WIDTH * j) / 100.0f,
                        (startY - CoreManager.TILE_HEIGHT * i) / 100.0f,
                        0.0f
                    );
                    // Create the current cell data:
                    cellsList.Add(new CellData(i, j, tile, data[k]));
                }
            }

            cells = cellsList.ToArray();

            // Get the entities node inside the scene:
            var entitiesObject = GameObject.Find(CoreManager.ENTITIES_GOBJ_NAME);

            // Create all the entities inside the world:
            foreach (var item in descriptor.Entities) {
                var created = PlayerController.Create(item, entitiesObject);
                if (!created) {
                    BoxController.Create(item, entitiesObject, descriptor.Tileset, sprites);
                }
            }

            // Finishing the load level process:
            IsLoaded = true;

        } catch (Exception e) {
            CoreManager.Instance.Log(e);
            Width = 0;
            Height = 0;
            cells = null;
        }
    }

    #endregion

    #region CellData GetCell(int, int)

    /// <summary>
    /// Gets a cell inside the level terrain.
    /// </summary>
    /// <param name="x">The column inside the world.</param>
    /// <param name="y">The row inside the world.</param>
    /// <returns>The selected cell if success, otherwise null.</returns>
    public CellData GetCell(int x, int y) {
        if (cells != null) {
            var i = y * Width + x;
            if (0 <= i && i < cells.Length) {
                return cells[i];
            }
        }
        return null;
    }

    #endregion

    #region CellData GetNextCell(int, int, MoveDirection)

    /// <summary>
    /// Gets the next cell inside the level terrain.
    /// </summary>
    /// <param name="x">The column inside the world.</param>
    /// <param name="y">The row inside the world.</param>
    /// <param name="direction">The direction to be moved.</param>
    /// <returns>The selected cell if success, otherwise null.</returns>
    public CellData GetNextCell(int x, int y, MoveDirection direction) {
        if (cells != null) {
            switch (direction) {
                case MoveDirection.North: return GetCell(x, y - 1);
                case MoveDirection.East: return GetCell(x + 1, y);
                case MoveDirection.South: return GetCell(x, y + 1);
                case MoveDirection.West: return GetCell(x - 1, y);
            }
        }
        return null;
    }

    #endregion

    #region bool IsInside(int, int)

    /// <summary>
    /// Checks if a coordinates are inside the world.
    /// </summary>
    /// <param name="x">The column inside the world.</param>
    /// <param name="y">The row inside the world.</param>
    /// <returns>If the coordinates are inside or not.</returns>
    public bool IsInside(int x, int y) {
        if (cells == null) {
            return false;
        } else {
            var idx = y * Width + x;
            return 0 <= idx && idx < cells.Length;
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------
    // Constructors:
    //--------------------------------------------------------------------------------------

    #region LevelData()

    /// <summary>
    /// Constructs a new object.
    /// </summary>
    public LevelData() {
        IsLoaded = false;
        Width = 0;
        Height = 0;
        cells = null;
    }

    #endregion
}
