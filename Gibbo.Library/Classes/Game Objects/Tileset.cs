#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// Tile Structure
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class Tile
    {
        /// <summary>
        /// The source rectangle of the tile
        /// </summary>
        public Rectangle Source;

        /// <summary>
        /// Deep Copy 
        /// </summary>
        /// <returns>New tile based on this instance</returns>
        public Tile DeepCopy()
        {
            return new Tile()
            {
                Source = new Rectangle()
                {
                    X = this.Source.X,
                    Y = this.Source.Y,
                    Width = this.Source.Width,
                    Height = this.Source.Height
                }
            };
        }
    }

    /// <summary>
    /// Brush Object
    /// </summary>
#if WINDOWS
    [Serializable]
#endif
    [DataContract]
    public class Tileset : GameObject
    {
        #region fields

        [DataMember]
        private Tile[,] tiles = new Tile[20, 18];

        //private List<Tile> tiles = new List<Tile>();
        [DataMember]
        private string imagePath = string.Empty;
        //private bool checkForCollisions = false;
#if WINRT
        [DataMember]
#endif
        private int tileWidth = 32;
#if WINRT
        [DataMember]
#endif
        private int tileHeight = 32;

#if WINRT
        [DataMember]
#endif
        private Color dimColor;

#if WINDOWS
        [NonSerialized]
#endif
        private Texture2D texture;

#if WINDOWS
        [NonSerialized]
#endif
        private Color collisionBoundryColor;

        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Tile[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        /// <summary>
        /// The tile size (width)
        /// </summary>
#if WINDOWS
        [Category("Tileset Properties")]
        [DisplayName("Tile Width"), Description("The tile size (width)")]
#endif
        public int TileWidth
        {
            get { return tileWidth; }
            set
            {
                tileWidth = value;

                foreach (Tile tile in tiles)
                {
                    if (tile != null)
                        tile.Source.Width = value;
                }
            }
        }

        /// <summary>
        /// The tile size (height)
        /// </summary>
#if WINDOWS
        [Category("Tileset Properties")]
        [DisplayName("Tile Height"), Description("The tile size (height)")]
#endif
        public int TileHeight
        {
            get { return tileHeight; }
            set
            {
                tileHeight = value;

                foreach (Tile tile in tiles)
                {
                    // remove old offset and add the new value
                    if (tile != null)
                        tile.Source.Height = value;
                }
            }
        }

        /// <summary>
        /// The relative path to the image
        /// </summary>
#if WINDOWS
        [EditorAttribute(typeof(ContentBrowserEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Tileset Properties")]
        [DisplayName("Image Name"), Description("The relative path to the image")]
#endif
        public string ImageName
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                LoadTexture();
            }
        }

        ///// <summary>
        ///// Determine if the collision manager should check for collisions with the tiles
        ///// </summary>
        //[Category("Tileset Properties")]
        //[DisplayName("Check for Collisions"), Description("Determine if the collision manager should check for collisions with the tiles")]
        //public bool CheckForCollisions
        //{
        //    get { return checkForCollisions; }
        //    set { checkForCollisions = value; }
        //}

        /// <summary>
        /// The active texture
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Texture2D Texture
        {
            get { return this.texture; }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Tileset Properties")]
        [DisplayName("Width"), Description("Amount of tiles (width)")]
#endif
        public int Width
        {
            get
            {
                return tiles.GetLength(0);
            }
            set
            {
                if (value % 2 != 0)
                    value -= 1;

                if (value > 200)
                    value = 200;

                if (value > 0)
                    ResizeTileset(value, Height);
            }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Category("Tileset Properties")]
        [DisplayName("Height"), Description("Amount of tiles (height)")]
#endif
        public int Height
        {
            get
            {
                return tiles.GetLength(1);
            }
            set
            {
                if (value % 2 != 0)
                    value -= 1;

                if (value > 200)
                    value = 200;

                if (value > 0)
                    ResizeTileset(Width, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Rectangle VisibleTiles
        {
            get
            {
                Vector2 topLeft = Vector2.Transform(Vector2.Zero, Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));
                Vector2 bottomRight = Vector2.Transform(new Vector2(SceneManager.GraphicsDevice.Viewport.Width, SceneManager.GraphicsDevice.Viewport.Height), Matrix.Invert(SceneManager.ActiveCamera.TransformMatrix));

                // position to base the current tiles matrix
                Vector2 startPos = new Vector2()
                {
                    X = (float)Math.Floor((topLeft.X + ((tileWidth * Width) / 2)) / tileWidth) - tileWidth,
                    Y = (float)Math.Floor((topLeft.Y + ((tileHeight * Height) / 2)) / tileHeight) - tileHeight
                };

                Vector2 endPos = new Vector2()
                {
                    X = (float)Math.Floor((bottomRight.X + ((tileWidth * Width) / 2)) / tileWidth) + tileWidth,
                    Y = (float)Math.Floor((bottomRight.Y + ((tileHeight * Height) / 2)) / tileHeight) + tileHeight
                };

                // boundries verification
                if (startPos.X < 0)
                    startPos.X = 0;

                if (startPos.Y < 0)
                    startPos.Y = 0;

                if (endPos.X > Width)
                    endPos.X = Width;
                else if (endPos.X < 0)
                    endPos.X = 0;

                if (endPos.Y > Height)
                    endPos.Y = Height;
                else if (endPos.Y < 0)
                    endPos.Y = 0;

                return new Rectangle((int)startPos.X, (int)startPos.Y, (int)endPos.X - (int)startPos.X, (int)endPos.Y - (int)startPos.Y);
            }
        }

        /// <summary>
        /// Gets the near area of the matrix where the boundries are colliding
        /// </summary>
        /// <param name="boundries">Collision boundry to test</param>
        /// <returns></returns>
        public Rectangle CollisionZone(Rectangle boundries)
        {
            Vector2 startPos = TileMatrixPos(boundries.X - tileWidth, boundries.Y - tileHeight);
            Vector2 endPos = TileMatrixPos(boundries.X + boundries.Width + TileWidth, boundries.Y + boundries.Height + tileHeight);

            // boundries verification
            if (startPos.X < 0)
                startPos.X = 0;

            if (startPos.Y < 0)
                startPos.Y = 0;

            if (endPos.X > Width)
                endPos.X = Width;
            else if (endPos.X < 0)
                endPos.X = 0;

            if (endPos.Y > Height)
                endPos.Y = Height;
            else if (endPos.Y < 0)
                endPos.Y = 0;

            return new Rectangle((int)startPos.X, (int)startPos.Y, (int)endPos.X - (int)startPos.X, (int)endPos.Y - (int)startPos.Y);
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public Tileset()
        {
            Selectable = false;
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (texture == null && imagePath != null && imagePath.Trim() != "")
            {
                LoadTexture();
            }

            collisionBoundryColor = Color.FromNonPremultiplied(255, 64, 0, 120);
            dimColor = Color.FromNonPremultiplied(255, 255, 255, 150);
        }

        private void LoadTexture()
        {
            texture = TextureLoader.FromContent(imagePath);
        }

        /// <summary>
        /// Updates this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws this instance
        /// </summary>
        /// <param name="gameTime">The gametime</param>
        /// <param name="spriteBatch">The spriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
           
            if (texture != null && Visible)
            {
                Rectangle visibleTiles = VisibleTiles;

                if (visibleTiles != Rectangle.Empty)
                {
                    spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                    for (int x = visibleTiles.X; x < visibleTiles.X + visibleTiles.Width; x++)
                    {
                        for (int y = visibleTiles.Y; y < visibleTiles.Y + visibleTiles.Height; y++)
                        {
                            if (tiles[x, y] != null)
                            {
                                //  Console.WriteLine(TileWorldPos(x, y));
                                Vector2 worldPos = TileWorldPos(x, y);

                                // use highlight mode?
                                if (((SceneManager.IsEditor && SceneManager.GameProject.ProjectSettings.HighlightActiveTilesetInEditor) ||
                                    !SceneManager.IsEditor && SceneManager.GameProject.ProjectSettings.HighlightActiveTilesetInGame) &&
                                    SceneManager.ActiveTileset != null)
                                {
                                    if (SceneManager.ActiveTileset == this)
                                    {
                                        spriteBatch.Draw(this.texture, worldPos, tiles[x, y].Source, Color.White);
                                    }
                                    else
                                    {
                                        spriteBatch.Draw(this.texture, worldPos, tiles[x, y].Source, dimColor);
                                    }
                                }
                                else
                                {
                                    // normal mode (everything has the same focus)
                                    spriteBatch.Draw(this.texture, worldPos, tiles[x, y].Source, Color.White);
                                }
                            }
                        }
                    }

                    spriteBatch.End();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        public void ResizeTileset(int newWidth, int newHeight)
        {
            Tile[,] newTileset = new Tile[newWidth, newHeight];

            // smart update:
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    Vector2 relativePosition;

                    relativePosition.X = (newWidth < Width) ?
                        (x + (Width / 2 - newWidth / 2)) : (x - (newWidth / 2 - Width / 2));

                    relativePosition.Y = (newHeight < Height) ?
                        (y + (Height / 2 - newHeight / 2)) : (y - (newHeight / 2 - Height / 2));

                    if (ValidPosition(relativePosition))
                    {
                        newTileset[x, y] = tiles[(int)relativePosition.X, (int)relativePosition.Y];
                    }
                }
            }

            this.tiles = newTileset;
        }

        /// <summary>
        /// Gets the tile world position based on the matrix position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 TileWorldPos(int x, int y)
        {
            return new Vector2()
            {
                X = ((x * tileWidth) - ((float)(Width * tileWidth) / 2.0f)),
                Y = ((y * tileHeight) - ((float)(Height * tileHeight) / 2.0f))
            };
        }

        /// <summary>
        /// Gets the matrix position based on the world position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 TileMatrixPos(float x, float y)
        {
            return new Vector2()
            {
                X = (float)Math.Floor((x + ((tileWidth * Width) / 2)) / tileWidth),
                Y = (float)Math.Floor((y + ((tileHeight * Height) / 2)) / tileHeight)
            };
        }

        /// <summary>
        /// Places a matrix of tiles
        /// </summary>
        /// <param name="newTiles">The tiles to place</param>
        /// <param name="startingPosition">The starting position (world position)</param>
        public void PlaceTiles(Tile[,] newTiles, Vector2 startingPosition)
        {
            for (int x = 0; x < newTiles.GetLength(0); x++)
            {
                for (int y = 0; y < newTiles.GetLength(1); y++)
                {
                    if (newTiles[x, y] != null)
                    {
                        PlaceTile(
                            new Vector2(newTiles[x, y].Source.X, newTiles[x, y].Source.Y),
                            new Vector2(startingPosition.X + (x * tileWidth), startingPosition.Y + (y * tileHeight)));
                    }
                }
            }
        }

        /// <summary>
        /// Places tiles based on the source.
        /// This method automatically cuts the source overflow by dividing it in singular tiles.
        /// </summary>
        /// <param name="source">The area of the image that you want to use</param>
        /// <param name="worldPosition">The world position</param>
        public void PlaceTiles(Rectangle source, Vector2 worldPosition)
        {
            int cX, cY;
            cX = source.Width / tileWidth;
            cY = source.Height / tileHeight;

            for (int i = 0; i < cX; i++)
            {
                for (int l = 0; l < cY; l++)
                {
                    PlaceTile(new Vector2(source.X + (i * tileWidth), source.Y + (l * tileHeight)),
                        new Vector2(worldPosition.X + (i * tileWidth), worldPosition.Y + (l * tileHeight)));
                }
            }
        }

        /// <summary>
        /// Places tiles based on the source and area.
        /// This method automatically cuts the source overflow by dividing it in singular tiles.
        /// </summary>
        /// <param name="source">The area of the image that you want to use</param>
        /// <param name="area">The area of the map you want to use</param>
        public void PlaceTiles(Rectangle source, Rectangle area)
        {
            int sX, sY, cX = 0, cY = 0;
            sX = source.Width / tileWidth;
            sY = source.Height / tileHeight;

            int ax, ay;
            ax = area.Width / TileWidth;
            ay = area.Height / TileHeight;

            for (int x = 0; x < ax; x++)
            {
                cY = 0;

                for (int y = 0; y < ay; y++)
                {
                    PlaceTile(new Vector2(source.X + (cX * TileWidth), source.Y + (cY * TileHeight)),
                        new Vector2(area.X + (x * TileWidth), area.Y + (y * TileHeight)));

                    cY++;
                    if (cY >= sY)
                        cY = 0;
                }

                cX++;
                if (cX >= sX)
                    cX = 0;
            }
        }

        /// <summary>
        /// Places a tile at a desired position
        /// </summary>
        /// <param name="sourcePosition">The position of the image that you want to use</param>
        /// <param name="worldPosition">The world position</param>
        public void PlaceTile(Vector2 sourcePosition, Vector2 worldPosition)
        {
            Vector2 matrixPos = TileMatrixPos(worldPosition.X, worldPosition.Y);

            // Valid position?
            if (ValidPosition(matrixPos))
            {
                this.tiles[(int)matrixPos.X, (int)matrixPos.Y] = new Tile()
                {
                    Source = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, tileWidth, tileHeight)
                };
            }
        }

        /// <summary>
        /// Validates position
        /// </summary>
        /// <param name="position">Matrix position</param>
        /// <returns></returns>
        public bool ValidPosition(Vector2 position)
        {
            return (position.X >= 0 && position.X < tiles.GetLength(0) && position.Y >= 0 && position.Y < tiles.GetLength(1));
        }

        public bool ValidPositionEnd(Vector2 position)
        {
            return (position.X >= 0 && position.X <= tiles.GetLength(0) && position.Y >= 0 && position.Y <= tiles.GetLength(1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Rectangle WorldAreaToMatrix(Rectangle area)
        {
            Vector2 startPos = TileMatrixPos(area.X, area.Y);

            if (!ValidPosition(startPos))
            {
                if (startPos.X < 0)
                    startPos.X = 0;
                else
                    return Rectangle.Empty; // not a valid position

                if (startPos.Y < 0)
                    startPos.Y = 0;
                else
                    return Rectangle.Empty;
            }
            //Console.WriteLine(startPos);
            Vector2 endPos = TileMatrixPos(area.X + area.Width, area.Y + area.Height);

            if (!ValidPosition(endPos))
            {
                if (endPos.X >= Width)
                    endPos.X = Width;

                if (endPos.Y >= Height)
                    endPos.Y = Height;
            }

            return new Rectangle((int)startPos.X, (int)startPos.Y, (int)endPos.X - (int)startPos.X, (int)endPos.Y - (int)startPos.Y);
        }

        /// <summary>
        /// Removes tiles from a selected area (world position)
        /// </summary>
        /// <param name="area">The area (world position) you want to have tiles removed</param>
        /// <returns>The removed tiles</returns>
        public Tile[,] RemoveTiles(Rectangle area)
        {
            Rectangle _area;
            if ((_area = WorldAreaToMatrix(area)) == Rectangle.Empty)
                return null;

            //Console.WriteLine("valido {0}", endPos);
            Tile[,] result = new Tile[_area.Width, _area.Height];

            //Console.WriteLine(result.GetLength(0) + ":" + result.GetLength(1));

            for (int x = _area.X; x < _area.X + _area.Width; x++)
            {
                for (int y = _area.Y; y < _area.Y + _area.Height; y++)
                {
                    if (tiles[x, y] != null)
                        result[x - _area.X, y - _area.Y] = tiles[x, y].DeepCopy();

                    tiles[x, y] = null;
                }
            }

            return result;
        }

        /// <summary>
        /// Adds a column
        /// </summary>
        /// <param name="pointX">The column position (world coordinates)</param>
        public void AddColumn(int pointX)
        {
            Vector2 mPos = TileMatrixPos(pointX, 0);

            if (ValidPosition(mPos))
            {
                for (int x = Width - 1; x >= (int)mPos.X; x--)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (tiles[x, y] != null)
                        {
                            if (x + 1 < Width)
                            {
                                tiles[x + 1, y] = tiles[x, y];
                            }

                            tiles[x, y] = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds a row
        /// </summary>
        /// <param name="pointY">The row position (world coordinates)</param>
        public void AddRow(int pointY)
        {
            Vector2 mPos = TileMatrixPos(0, pointY);

            if (ValidPosition(mPos))
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = Height - 1; y >= (int)mPos.Y; y--)
                    {
                        if (tiles[x, y] != null)
                        {
                            if (y + 1 < Height)
                            {
                                tiles[x, y + 1] = tiles[x, y];
                            }

                            tiles[x, y] = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes a column
        /// </summary>
        /// <param name="pointX">The column position (world coordinates)</param>
        public void RemoveColumn(int pointX)
        {
            Vector2 mPos = TileMatrixPos(pointX, 0);

            if (ValidPosition(mPos) && mPos.X < Width - 1)
            {
                for (int x = (int)mPos.X + 1; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (tiles[x, y] != null)
                        {
                            tiles[x - 1, y] = tiles[x, y];
                            tiles[x, y] = null;
                        }
                        else
                        {
                            tiles[x - 1, y] = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes a row
        /// </summary>
        /// <param name="pointY">The row position (world coordinates)</param>
        public void RemoveRow(int pointY)
        {
            Vector2 mPos = TileMatrixPos(0, pointY);

            if (ValidPosition(mPos) && mPos.Y < Height - 1)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = (int)mPos.Y + 1; y < Height; y++)
                    {
                        if (tiles[x, y] != null)
                        {
                            tiles[x, y - 1] = tiles[x, y];
                            tiles[x, y] = null;
                        }
                        else
                        {
                            tiles[x, y - 1] = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Grabs all the tiles in the selected area
        /// </summary>
        /// <param name="area">The area to grab the tiles, must be multiple of the tile width and height.</param>
        /// <param name="clone">Determines if the result should use cloned tiles</param>
        /// <returns>The matrix of the tiles that were found. This result may contain null positions in case you select areas that aren't fully filled.</returns>
        public Tile[,] GrabTiles(Rectangle area, bool clone)
        {
            Rectangle _area;
            if ((_area = WorldAreaToMatrix(area)) == Rectangle.Empty)
                return null;

            Tile[,] result = new Tile[_area.Width, _area.Height];

            for (int x = _area.X; x < _area.X + _area.Width; x++)
            {
                for (int y = _area.Y; y < _area.Y + _area.Height; y++)
                {
                    if (tiles[x, y] != null)
                        result[x - _area.X, y - _area.Y] = tiles[x, y].DeepCopy();
                }
            }

            return result;
        }

        /// <summary>
        /// Shifts left (amount) positions
        /// </summary>
        /// <param name="amount">The amount of positions to shift</param>
        public void ShiftLeft(int amount)
        {
            if (amount == 0) return;

            for (int x = amount; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x - amount, y] = tiles[x, y];
                        tiles[x, y] = null;
                    }
                    else
                    {
                        tiles[x - amount, y] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Shifts right (amount) positions
        /// </summary>
        /// <param name="amount">The amount of positions to shift</param>
        public void ShiftRight(int amount)
        {
            if (amount == 0) return;

            for (int x = Width - amount - 1; x >= 0; x--)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x + amount, y] = tiles[x, y];
                        tiles[x, y] = null;
                    }
                    else
                    {
                        tiles[x + amount, y] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Shifts up (amount) positions
        /// </summary>
        /// <param name="amount">The amount of positions to shift</param>
        public void ShiftUp(int amount)
        {
            if (amount == 0) return;

            for (int x = 0; x < Width; x++)
            {
                for (int y = amount; y < Height; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x, y - amount] = tiles[x, y];
                        tiles[x, y] = null;
                    }
                    else
                    {
                        tiles[x, y - amount] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Shifts down (amount) positions
        /// </summary>
        /// <param name="amount">The amount of positions to shift</param>
        public void ShiftDown(int amount)
        {
            if (amount == 0) return;

            for (int x = 0; x < Width; x++)
            {
                for (int y = Height - amount - 1; y >= 0; y--)
                {
                    if (tiles[x, y] != null)
                    {
                        tiles[x, y + amount] = tiles[x, y];
                        tiles[x, y] = null;
                    }
                    else
                    {
                        tiles[x, y + amount] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Deep Copy
        /// </summary>
        /// <returns>A new array of the tiles in this tileset</returns>
        public Tile[,] DeepCopy()
        {
            Tile[,] result = new Tile[tiles.GetLength(0), tiles.GetLength(1)];

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    if (tiles[x, y] != null)
                        result[x, y] = tiles[x, y].DeepCopy();
                }
            }

            return result;
        }

        #endregion
    }
}