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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Concurrent;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Gibbo.Library
//{
//    /// <summary>
//    /// The default collision engine
//    /// </summary>
//    public class CollisionManager
//    {
//        #region fields

//        private List<GameObject> gameObjectsBuffer;

//        private const int delay = 1;
//        private int currentFrame = 0;

//        private object obj = new object();

//        private Color[] mouseData;

//        #endregion

//        #region properties

//        /// <summary>
//        /// The game objects buffer
//        /// This is automatically generated everytime a scene loads
//        /// </summary>
//        public List<GameObject> GameObjectsBuffer
//        {
//            get { return gameObjectsBuffer; }
//        }

//        #endregion

//        #region methods

//        /// <summary>
//        /// Initializes the collision manager
//        /// </summary>
//        public void Initialize()
//        {
//            mouseData = new Color[1];
//            mouseData[0] = Color.White;

//            LoadGameObjectsToBuffer();
//        }

//        private void LoadGameObjectsToBuffer()
//        {
//            gameObjectsBuffer = GameObject.GetAllGameObjects();
//        }


//        /// <summary>
//        /// Update the collision manager and look for collisions in the scene
//        /// </summary>
//        public void Update()
//        {
//            //LoadGameObjectsToBuffer();

//            //if (gameObjectsBuffer == null || gameObjectsBuffer.Count == 0) return;

//            //if (currentFrame >= delay)
//            //{
//            //    // Check for collision in separated threads
//            //    //Parallel.ForEach(Partitioner.Create(0, gameObjectsBuffer.Count), (range) =>
//            //    //{
//            //    //for (int i = range.Item1; i < range.Item2; i++)
//            //    int collisionCount = 0;
//            //    for (int i = 0; i < gameObjectsBuffer.Count(); i++)
//            //    {
//            //        // passtrough 
//            //        if (!gameObjectsBuffer[i].Visible || gameObjectsBuffer[i] is Tileset) continue;

//            //        // The object has collision properties?
//            //        if (gameObjectsBuffer[i].CollisionModel.CollisionBoundry != Rectangle.Empty)
//            //        {
//            //            /* The mouse is colliding with the game object? */
//            //            if (GameInput.MouseBoundingBox.Intersects(gameObjectsBuffer[i].CollisionModel.CollisionBoundry))
//            //            {
//            //                gameObjectsBuffer[i].mouseOver = true;

//            //                bool blockTexturePass = true;

//            //                // The object has a block texture?
//            //                if (gameObjectsBuffer[i].CollisionModel.BlockTextureData != null)
//            //                {
//            //                    blockTexturePass = ComplexPixelsIntersection(
//            //                        gameObjectsBuffer[i].CollisionModel.BlockTextureTransform, gameObjectsBuffer[i].CollisionModel.BlockTexture.Width, gameObjectsBuffer[i].CollisionModel.BlockTexture.Height, gameObjectsBuffer[i].CollisionModel.BlockTextureData, 
//            //                        GameInput.MouseTransform, 1, 1, mouseData);
//            //                }

//            //                if (blockTexturePass)
//            //                {
//            //                    gameObjectsBuffer[i].OnMouseMove();

//            //                    bool mouseDown = false;

//            //                    /* left button down? */
//            //                    if (GameInput.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseDown(MouseEventButton.LeftButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    /* right button down? */
//            //                    if (GameInput.MouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseDown(MouseEventButton.RightButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    /* middle button down? */
//            //                    if (GameInput.MouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseDown(MouseEventButton.MiddleButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    /* left button click? */
//            //                    if (GameInput.IsMouseKeyPressed(MouseEventButton.LeftButton))
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseClick(MouseEventButton.LeftButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    /* right button click? */
//            //                    if (GameInput.IsMouseKeyPressed(MouseEventButton.RightButton))
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseClick(MouseEventButton.RightButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    /* middle button click? */
//            //                    if (GameInput.IsMouseKeyPressed(MouseEventButton.MiddleButton))
//            //                    {
//            //                        gameObjectsBuffer[i].OnMouseClick(MouseEventButton.MiddleButton);
//            //                        mouseDown = true;
//            //                    }

//            //                    if (mouseDown)
//            //                    {
//            //                        gameObjectsBuffer[i].mouseDown = true;
//            //                    }
//            //                    else
//            //                    {
//            //                        if (gameObjectsBuffer[i].mouseDown)
//            //                        {
//            //                            gameObjectsBuffer[i].mouseDown = false;
//            //                            gameObjectsBuffer[i].OnMouseUp();
//            //                        }
//            //                    }
//            //                }
//            //            }
//            //            else
//            //            {
//            //                gameObjectsBuffer[i].mouseOver = false;
//            //                gameObjectsBuffer[i].OnMouseOut();
//            //            }

//            //            collisionCount = ObjectCollision(gameObjectsBuffer[i], false, null).Count;

//            //            if (collisionCount == 0)
//            //            {
//            //                // This object is not colliding with anything
//            //                gameObjectsBuffer[i].OnCollisionFree();
//            //            }
//            //        }
//            //    }
//            //    //});

//            //    // initialize counter
//            //    currentFrame = 0;
//            //}
//            //else
//            //{
//            //    currentFrame++;
//            //}
//        }

//        /// <summary>
//        /// Collision between objects
//        /// </summary>
//        /// <param name="_gameObject"></param>
//        /// <param name="simulation"></param>
//        /// <param name="exception"></param>
//        /// <returns>The gameobjects in the collision</returns>
//        internal List<GameObject> ObjectCollision(GameObject _gameObject, bool simulation, GameObject exception )
//        {
//           List<GameObject> collidedGameObjects = new List<GameObject>();

//           // /* Check for collision with other objects */
//           //// foreach (GameObject gameObject in gameObjectsBuffer)
//           // for(int i = 0; i < gameObjectsBuffer.Count; i++)
//           // {
//           //     if (gameObjectsBuffer[i] is Tileset)
//           //     {
//           //         if ((gameObjectsBuffer[i] as Tileset).CheckForCollisions)
//           //         {
//           //             Rectangle tileArea = (gameObjectsBuffer[i] as Tileset).CollisionZone(_gameObject.CollisionModel.CollisionBoundry);

//           //             for (int x = tileArea.X; x < tileArea.X + tileArea.Width; x++)
//           //             {
//           //                 for (int y = tileArea.Y; y < tileArea.Y + tileArea.Height; y++)
//           //                 {
//           //                     if ((gameObjectsBuffer[i] as Tileset).Tiles[x, y] != null)
//           //                     {
//           //                         Vector2 _pos = (gameObjectsBuffer[i] as Tileset).TileWorldPos(x, y);
//           //                         Rectangle boundries = new Rectangle()
//           //                         {
//           //                             X = (int)_pos.X,
//           //                             Y = (int)_pos.Y,
//           //                             Width = (gameObjectsBuffer[i] as Tileset).TileWidth,
//           //                             Height = (gameObjectsBuffer[i] as Tileset).TileHeight
//           //                         };

//           //                         if(boundries.Intersects(_gameObject.CollisionModel.CollisionBoundry))
//           //                         {
//           //                             GameObject dummy = new GameObject();
//           //                             dummy.Name = "Dummy";
//           //                             dummy.Transform.Position = _pos;

//           //                             dummy.CollisionModel.Width = (gameObjectsBuffer[i] as Tileset).TileWidth;
//           //                             dummy.CollisionModel.Height = (gameObjectsBuffer[i] as Tileset).TileHeight;

//           //                             if (!simulation)
//           //                                 _gameObject.OnCollisionEnter(dummy);

//           //                             collidedGameObjects.Add(dummy);
//           //                         }
//           //                     }
//           //                 }
//           //             }
//           //         }
//           //     }
//           //     else
//           //     {
//           //         // The object has collision properties and is not the object being processed?
//           //         if (gameObjectsBuffer[i].CollisionModel.CollisionBoundry != Rectangle.Empty && 
//           //             gameObjectsBuffer[i].CollisionModel.CollisionBoundry.Width > 1 && 
//           //             gameObjectsBuffer[i].CollisionModel.CollisionBoundry.Height > 1 &&
//           //             gameObjectsBuffer[i] != _gameObject && gameObjectsBuffer[i].Visible && 
//           //             (exception == null || exception !=  gameObjectsBuffer[i]))
//           //         {
//           //             // first collision test (simple boundry test)
//           //             if (gameObjectsBuffer[i].CollisionModel.CollisionBoundry.Intersects(_gameObject.CollisionModel.CollisionBoundry))
//           //             {
//           //                 // both game objects have block textures?         
//           //                 if (gameObjectsBuffer[i].CollisionModel.BlockTextureData != null && _gameObject.CollisionModel.BlockTextureData != null)
//           //                 {
//           //                     //gameObjectsBuffer[i].OnCollisionEnter(gameObject);
//           //                     // perform complex type (pixel per pixel) collision
//           //                     if (ComplexPixelsIntersection(_gameObject.CollisionModel.BlockTextureTransform, _gameObject.CollisionModel.BlockTexture.Width,
//           //                                         _gameObject.CollisionModel.BlockTexture.Height, _gameObject.CollisionModel.BlockTextureData,
//           //                                         gameObjectsBuffer[i].CollisionModel.BlockTextureTransform, gameObjectsBuffer[i].CollisionModel.BlockTexture.Width,
//           //                                         gameObjectsBuffer[i].CollisionModel.BlockTexture.Height, gameObjectsBuffer[i].CollisionModel.BlockTextureData))
//           //                     {
//           //                         // Notify object that has been a collision
//           //                         if (!simulation)
//           //                             _gameObject.OnCollisionEnter(gameObjectsBuffer[i]);

//           //                         collidedGameObjects.Add(gameObjectsBuffer[i]);
//           //                         // Console.WriteLine("complex collision " + DateTime.Now);
//           //                     }
//           //                 }
//           //                 // the processing object has a block texture?
//           //                 else if (_gameObject.CollisionModel.BlockTextureData != null &&
//           //                     gameObjectsBuffer[i].CollisionModel.BlockData != null)
//           //                 {
//           //                     // TODO:
//           //                     // perform half complex type (pixel per pixel) collision
//           //                     // with the processed object                              
//           //                     if (ComplexPixelsIntersection(_gameObject.CollisionModel.BlockTextureTransform, _gameObject.CollisionModel.BlockTexture.Width,
//           //                                      _gameObject.CollisionModel.BlockTexture.Height, _gameObject.CollisionModel.BlockTextureData,
//           //                                      gameObjectsBuffer[i].CollisionModel.BlockTransform, gameObjectsBuffer[i].CollisionModel.Width,
//           //                                      gameObjectsBuffer[i].CollisionModel.Height, gameObjectsBuffer[i].CollisionModel.BlockData))
//           //                     {
//           //                         // Notify object that has been a collision
//           //                         if (!simulation)
//           //                             _gameObject.OnCollisionEnter(gameObjectsBuffer[i]);

//           //                         collidedGameObjects.Add(gameObjectsBuffer[i]);
//           //                         // Console.WriteLine("complex collision " + DateTime.Now);
//           //                     }
//           //                 }
//           //                 // the sub routine object has a block texture?
//           //                 else if (gameObjectsBuffer[i].CollisionModel.BlockTextureData != null &&
//           //                     _gameObject.CollisionModel.BlockData != null)
//           //                 {
//           //                     // TODO:
//           //                     // perform half complex type (pixel per pixel) collision
//           //                     // with the  sub routine object
//           //                     //gameObjectsBuffer[i].OnCollisionEnter(gameObject);
//           //                     // perform complex type (pixel per pixel) collision
//           //                     if (ComplexPixelsIntersection(_gameObject.CollisionModel.BlockTransform, _gameObject.CollisionModel.Width,
//           //                                         _gameObject.CollisionModel.Height, _gameObject.CollisionModel.BlockData,
//           //                                         gameObjectsBuffer[i].CollisionModel.BlockTextureTransform, gameObjectsBuffer[i].CollisionModel.BlockTexture.Width,
//           //                                         gameObjectsBuffer[i].CollisionModel.BlockTexture.Height, gameObjectsBuffer[i].CollisionModel.BlockTextureData))
//           //                     {
//           //                         // Notify object that has been a collision
//           //                         if (!simulation)
//           //                             _gameObject.OnCollisionEnter(gameObjectsBuffer[i]);

//           //                         collidedGameObjects.Add(gameObjectsBuffer[i]);
//           //                         // Console.WriteLine("complex collision " + DateTime.Now);
//           //                     }
//           //                 }
//           //                 else
//           //                 {
//           //                     // Notify object that has been a collision
//           //                     if (!simulation)
//           //                         _gameObject.OnCollisionEnter(gameObjectsBuffer[i]);

//           //                     collidedGameObjects.Add(gameObjectsBuffer[i]);
//           //                     //Console.WriteLine("simple collision " + DateTime.Now);
//           //                 }
//           //             }
//           //         }
//           //     }
//           // }

//            return collidedGameObjects;
//        }

//        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
//                            Rectangle rectangleB, Color[] dataB)
//        {
//            // Find the bounds of the rectangle intersection
//            int top = Math.Max(rectangleA.Top, rectangleB.Top);
//            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
//            int left = Math.Max(rectangleA.Left, rectangleB.Left);
//            int right = Math.Min(rectangleA.Right, rectangleB.Right);

//            // Check every point within the intersection bounds
//            for (int y = top; y < bottom; y++)
//            {
//                for (int x = left; x < right; x++)
//                {
//                    // Get the color of both pixels at this point
//                    Color colorA = dataA[(x - rectangleA.Left) +
//                                         (y - rectangleA.Top) * rectangleA.Width];
//                    Color colorB = dataB[(x - rectangleB.Left) +
//                                         (y - rectangleB.Top) * rectangleB.Width];

//                    // If both pixels are not completely transparent,
//                    if (colorA.A != 0 && colorB.A != 0)
//                    {
//                        // then an intersection has been found
//                        return true;
//                    }
//                }
//            }

//            // No intersection found
//            return false;
//        }

//        /// <summary>
//        /// Determines if there is overlap of the non-transparent pixels between two texture blocks.
//        /// </summary>
//        /// <param name="transformA">World transform of the first texture block.</param>
//        /// <param name="widthA">Width of the first texture block's texture.</param>
//        /// <param name="heightA">Height of the first texture block's texture.</param>
//        /// <param name="dataA">Pixel color data of the first texture block.</param>
//        /// <param name="transformB">World transform of the second texture block.</param>
//        /// <param name="widthB">Width of the second texture block's texture.</param>
//        /// <param name="heightB">Height of the second texture block's texture.</param>
//        /// <param name="dataB">Pixel color data of the second texture block.</param>
//        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
//        public static bool ComplexPixelsIntersection(
//                            Matrix transformA, int widthA, int heightA, Color[] dataA,
//                            Matrix transformB, int widthB, int heightB, Color[] dataB)
//        {
//            // Calculate a matrix which transforms from A's local space into
//            // world space and then into B's local space
//            Matrix transformAToB = transformA * Matrix.Invert(transformB);

//            // When a point moves in A's local space, it moves in B's local space with a
//            // fixed direction and distance proportional to the movement in A.
//            // This algorithm steps through A one pixel at a time along A's X and Y axes
//            // Calculate the analogous steps in B:
//            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
//            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

//            // Calculate the top left corner of A in B's local space
//            // This variable will be reused to keep track of the start of each row
//            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

//            // For each row of pixels in A
//            for (int yA = 0; yA < heightA; yA++)
//            {
//                // Start at the beginning of the row
//                Vector2 posInB = yPosInB;

//                // For each pixel in this row
//                for (int xA = 0; xA < widthA; xA++)
//                {
//                    //Console.WriteLine("calculating");
//                    // Round to the nearest pixel
//                    int xB = (int)Math.Round(posInB.X);
//                    int yB = (int)Math.Round(posInB.Y);

//                    // If the pixel lies within the bounds of B
//                    if (0 <= xB && xB < widthB &&
//                        0 <= yB && yB < heightB)
//                    {
//                        // Get the colors of the overlapping pixels
//                        Color colorA = dataA[xA + yA * widthA];
//                        Color colorB = dataB[xB + yB * widthB];

//                        // If both pixels are not completely transparent,
//                        if (colorA.A != 0 && colorB.A != 0)
//                        {
//                            // then an intersection has been found
//                            return true;
//                        }
//                    }

//                    // Move to the next pixel in the row
//                    posInB += stepX;
//                }

//                // Move to the next row
//                yPosInB += stepY;
//            }

//            // No intersection found
//            return false;
//        }

//        #endregion
//    }
//}
