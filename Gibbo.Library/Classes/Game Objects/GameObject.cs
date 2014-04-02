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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;
using System.Reflection;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Gibbo.Library
{
    /// <summary>
    /// Game Object
    /// This class can be used to represent objects in the game. You can attach components to it.
    /// </summary>
#if WINDOWS
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract(IsReference = true)]
    //[KnownType(typeof(Sprite)), KnownType(typeof(Transform)), KnownType(typeof(BodyType)), KnownType(typeof(PropertyLabel))]
    public class GameObject : SystemObject, IDisposable
#if WINDOWS
, ICloneable
#endif
    {
        #region fields
#if WINDOWS
        [NonSerialized]
#endif
        internal PhysicalBody physicalBody;

        [DataMember]
        private string name = string.Empty;
        [DataMember]
        private bool visible = true;
        [DataMember]
        private bool selectable = true;
        //private CollisionModel collisionModel = new CollisionModel();
#if WINDOWS
        [NonSerialized]
#endif
        internal bool mouseOver;
#if WINDOWS
        [NonSerialized]
#endif
        internal bool mouseDown;

#if WINDOWS
        [NonSerialized]
#endif
        private Body body;

#if WINDOWS
        [NonSerialized]
#endif
        private Color collisionBoundryColor = Color.FromNonPremultiplied(255, 64, 0, 120);

        [DataMember]
        private Transform transform = new Transform();

        [DataMember]
        private GameObjectCollection children;

        [DataMember]
        private List<string> componentReferences = new List<string>();

        // <Component Name, <(Property Type Name, Name), Value>>
        [DataMember]
        private Dictionary<string, Dictionary<PropertyLabel, object>> componentValues = new Dictionary<string, Dictionary<PropertyLabel, object>>();

#if WINDOWS
        [NonSerialized]
#endif
        private List<ObjectComponent> components = new List<ObjectComponent>();

        [DataMember]
        private string tag = string.Empty;

        [DataMember]
        protected bool disabled = false;

        #endregion

        #region properties
#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Disabled"), Description("Determines if the object is disabled or not")]
#endif
        public virtual bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// The attached body  
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public Body Body
        {
            get { return body; }
            set
            {
                //only one body per object
                if (body != null)
                {
                    SceneManager.ActiveScene.World.RemoveBody(body);
                }

                body = value;
                if (body != null)
                {
                    body.Position = ConvertUnits.ToSimUnits(Transform.Position);
                    body.Rotation = Transform.Rotation;
                    body.gameObject = this;

                    if (!SceneManager.IsEditor)
                    {
                        body.OnCollision += body_OnCollision;
                        body.OnSeparation += body_OnSeparation;
                    }
                }
            }
        }

        /// <summary>
        /// The children game objects
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public GameObjectCollection Children
        {
            get { return children; }
            set { children = value; }
        }

        /// <summary>
        /// Gets the mouse over state in this object
        /// </summary>
#if WINDOWS
        [Browsable(false)]
#endif
        public bool MouseOver
        {
            get { return mouseOver; }
        }

        /// <summary>
        /// Game object tag.
        /// This can be used to reference the game object in the game.
        /// </summary>
#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Tag"), Description("The tag of the object")]
#endif
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        /// <summary>
        /// The collision model of this object
        /// </summary>
        //[Category("Object Properties")]
        //[DisplayName("Collision Model"), Description("Determines the collision boundries of the object")]
        //public CollisionModel CollisionModel
        //{
        //    get { return collisionModel; }
        //    set { collisionModel = value; }
        //}

        /// <summary>
        /// Determine if the object is to be drawn in the game.
        /// </summary>
#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Visible"), Description("Determines if this object is visible or not")]
#endif
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// Determine if the object can be selected
        /// </summary>
#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Selectable"), Description("Determines if this object is selected or not")]
#endif
        public bool Selectable
        {
            get { return selectable; }
            set { selectable = value; }
        }

        /// <summary>
        /// The name of the object
        /// </summary>
#if WINDOWS
        [Category("Object Properties"), ReadOnly(true)]
        [DisplayName("Name"), Description("The name of the object"), Browsable(false)]
#endif
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The object Transformation Matrix.
        /// </summary>
#if WINDOWS
        [Category("Object Properties")]
        [DisplayName("Transform"), Description("The transform of the object")]
#endif
        public Transform Transform
        {
            get
            {
                return transform;
            }
            set
            {
                transform = value;
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// The default constructor of this game object.
        /// </summary>
        public GameObject()
        {
            this.transform.GameObject = this;

            this.Initialize();

            // initialize collision model
            //collisionModel.Initialize(this.transform);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes this object.
        /// </summary>
        public virtual void Initialize()
        {
            if (Body != null)
                Body = null;

            transform.GameObject = this;

            this.components = new List<ObjectComponent>();
            foreach (string name in componentReferences)
            {
                //bool scriptsAssembly = true;

                Type _type = SceneManager.ScriptsAssembly.GetType(name);

                if (_type == null)
                {
#if WINDOWS
                    _type = Assembly.GetExecutingAssembly().GetType(name);
#elif WINRT
                    _type = typeof(GameObject).GetTypeInfo().Assembly.GetType(name);
#endif
                    //scriptsAssembly = false;
                }

                // The reference still exists?
                // (the user removed the component?)
                if (_type != null)
                {
                    try
                    {
                        ObjectComponent oc;
                        oc = (ObjectComponent)Activator.CreateInstance(_type);

                        // não apagar:
                        //if (scriptsAssembly)
                        //    oc = (ObjectComponent)SceneManager.ScriptsAssembly // .CreateInstance(name);
                        //else
                        //    oc = (ObjectComponent)typeof(GameObject).GetTypeInfo().Assembly.CreateInstance(name);

                        oc.Transform = this.Transform;

                        LoadComponentValues(oc);

                        if (oc != null)
                        {
                            if (!SceneManager.IsEditor)
                            {
                                oc.Initialize();
                            }
                            else if (SceneManager.IsEditor && oc is ExtendedObjectComponent)
                            {
                                oc.Initialize();
                            }
                        }

                        this.components.Add(oc);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            if (children == null)
                children = new GameObjectCollection(this);

            // Initialize Children game objects
            foreach (GameObject gameObject in children)
                gameObject.Initialize();

            collisionBoundryColor = Color.FromNonPremultiplied(255, 64, 0, 120);

            // initialize collision model
            //collisionModel.Initialize(this.transform);

            CheckAllAttributes();
        }

        /// <summary>
        /// Gets the components of this object.
        /// </summary>
        /// <returns></returns>
        public List<ObjectComponent> GetComponents()
        {
            return this.components;
        }

        private void CheckAllAttributes()
        {
            for (int i = components.Count - 1; i >= 0; i--)
            {
                CheckAttributes(components[i]);
            }
        }

        private bool CheckAttributes(ObjectComponent component)
        {
#if WINDOWS
            System.Reflection.MemberInfo info = component.GetType();
            object[] attributes = info.GetCustomAttributes(true);

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is RequireComponent)
                {
                    bool found = false;
                    int foundCount = 0;
                    string[] typeNames = (attributes[i] as RequireComponent).ComponentsTypeNames.Split('|');

                    foreach (string typeName in typeNames)
                    {
                        string typeNameTrim = typeName.Trim();

                        foreach (var cmp in components)
                        {
                            if (cmp.GetType().Name.Equals(typeNameTrim))
                            {
                                foundCount++;
                                break;
                            }
                        }
                    }

                    if ((foundCount == typeNames.Count() && (attributes[i] as RequireComponent).requireAll) ||
                        (foundCount > 0 && !(attributes[i] as RequireComponent).requireAll))
                        found = true;

                    if (!found && this.componentValues.ContainsKey(component.GetType().FullName))
                        RemoveComponent(component, false);

                    if (!found)
                        return false;
                }
            }
#endif
            return true;
        }

        /// <summary>
        /// Adds a component to this object.
        /// </summary>
        /// <param name="component"></param>
        public bool AddComponent(ObjectComponent component)
        {
#if WINDOWS
            if (!CheckAttributes(component))
                return false;

            // Check existing components attributes:
            foreach (var cmp in components)
            {
                System.Reflection.MemberInfo info = cmp.GetType();
                object[] attributes = info.GetCustomAttributes(true);

                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is Unique)
                    {
                        if ((attributes[i] as Unique).Options == Unique.UniqueOptions.Explicit)
                        {
                            if (component.GetType() == cmp.GetType())
                                return false;
                        }
                        else
                        {
                            if (component.GetType().IsAssignableFrom(cmp.GetType()) || cmp.GetType().IsAssignableFrom(component.GetType()))
                                return false;

                            var baseA = component.GetType().BaseType;
                            var baseB = cmp.GetType().BaseType;

                            while (true)
                            {
                                if (baseA == typeof(ExtendedObjectComponent) || baseB == typeof(ExtendedObjectComponent) ||
                                    baseA == typeof(ObjectComponent) || baseB == typeof(ObjectComponent))
                                {
                                    break;
                                }
                                else if (baseA == baseB)
                                {
                                    return false;
                                }
                                else if (baseA.IsAssignableFrom(baseB) || baseB.IsAssignableFrom(baseA))
                                {
                                    return false;
                                }

                                baseA = baseA.BaseType;
                                baseB = baseB.BaseType;
                            }
                        }
                    }
                }
            }
#endif
            // This component is already assigned?
            if (!this.componentValues.ContainsKey(component.GetType().FullName))
            {
                this.componentValues[component.GetType().FullName] = new Dictionary<PropertyLabel, object>(new PropertyLabel.EqualityComparer());
            }
            else
            {
                // Component already added, nothing to do here, return.
                return false;
            }

            component.Transform = this.transform;
            component.Name = component.GetType().Name;

            this.components.Add(component);
            this.componentReferences.Add(component.GetType().FullName);

            // Get through all the properties in the component and assign them
#if WINDOWS
            List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetProperties());
#elif WINRT
            List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetRuntimeProperties());
#endif
            foreach (PropertyInfo propInfo in props)
            {
                PropertyLabel label = new PropertyLabel(propInfo.PropertyType.FullName, propInfo.Name);
                this.componentValues[component.GetType().FullName][label] = propInfo.GetValue(component, null);
            }

            if (!SceneManager.IsEditor)
            {
                component.Initialize();
            }
            else if (SceneManager.IsEditor && component is ExtendedObjectComponent)
            {
                component.Initialize();
            }

            return true;
        }

        /// <summary>
        /// Remove a component from this object
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(ObjectComponent component)
        {
            RemoveComponent(component, true);
        }

        private void RemoveComponent(ObjectComponent component, bool checkAttributes)
        {
            component.Removed();

            this.components.Remove(component);
            this.componentReferences.Remove(component.GetType().FullName);
            this.componentValues.Remove(component.GetType().FullName);

            if (checkAttributes)
                CheckAllAttributes();
        }

        /// <summary>
        /// Remove all components from this object
        /// </summary>
        public void RemoveAllComponents()
        {
            for (int i = this.components.Count - 1; i >= 0; i--)
            {
                this.components[i].Removed();
                RemoveComponent(this.components[i]);
            }

            foreach (GameObject child in children)
                child.RemoveAllComponents();
        }

        private void LoadComponentValues(ObjectComponent component)
        {
            // The component is assigned?
            if (!componentValues.ContainsKey(component.GetType().FullName)) return;

#if WINDOWS
            List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetProperties());
            //foreach (PropertyInfo pinfo in props)
            //{
            //    Console.WriteLine("c:" + component.Transform.gameObject + " b: " + pinfo.Name);
            //}
#elif WINRT
            List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetRuntimeProperties());
            //foreach (PropertyInfo pinfo in props)
            //{
            //    Debug.WriteLine("cc: " + component.GetType().Name + " c:" + component.Transform.gameObject + " b: " + pinfo.Name);
            //    Debug.WriteLine("VALUE: " + pinfo.GetValue(component, null));
            //}
#endif
            foreach (PropertyInfo propInfo in props)
            {
                try
                {
#if WINRT
                    bool found = false;
                    foreach (var item in componentValues[component.GetType().FullName])
                    {
                        if (item.Key.Name == propInfo.Name && item.Key.TypeName == propInfo.PropertyType.FullName)
                        {
                            propInfo.SetValue(component, componentValues[component.GetType().FullName][item.Key], null);
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        PropertyLabel label = new PropertyLabel(propInfo.PropertyType.FullName, propInfo.Name);
                        this.componentValues[component.GetType().FullName][label] = propInfo.GetValue(component, null);
                    }
#elif WINDOWS
                    // dummy label
                    PropertyLabel label = new PropertyLabel(propInfo.PropertyType.FullName, propInfo.Name);

                    // There is a place to store the component value?
                    if (!componentValues[component.GetType().FullName].ContainsKey(label))
                    {
                        this.componentValues[component.GetType().FullName][label] = propInfo.GetValue(component, null);
                    }
                    else
                    {
                        propInfo.SetValue(component, componentValues[component.GetType().FullName][label], null);
                    }
#endif
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Property not loaded: " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Save the current state of this object components.
        /// </summary>
        public void SaveComponentValues()
        {
            foreach (ObjectComponent component in this.components)
            {
#if WINDOWS
                List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetProperties());
#elif WINRT
                List<PropertyInfo> props = new List<PropertyInfo>(component.GetType().GetRuntimeProperties());
#endif
                foreach (PropertyInfo propInfo in props)
                {
                    PropertyLabel label = new PropertyLabel(propInfo.PropertyType.FullName, propInfo.Name);

                    //if(propInfo.GetValue(component, null).GetType().IsSerializable)
                    componentValues[component.GetType().FullName][label] = propInfo.GetValue(component, null);
                }
            }

            foreach (GameObject gameObject in children)
                gameObject.SaveComponentValues();
        }

        /// <summary>
        /// Updates the logic of this game object.
        /// Components and children are notified to \.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            try
            {
                // Update this object children
                for (int i = 0; i < children.Count; i++)
                    if (!children[i].Disabled)
                        children[i].Update(gameTime);

                // Update this object components
                foreach (ObjectComponent component in components)
                {
                    if (component.Disabled) continue;

                    if (SceneManager.IsEditor && component is ExtendedObjectComponent)
                    {
                        component.Update(gameTime);
                    }
                    else if (!SceneManager.IsEditor)
                    {
                        component.Update(gameTime);
                    }
                }

                //if (!SceneManager.IsEditor && body != null && !transform.physicsPositionReached)
                //{
                //    MoveBodyToPosition(transform.desiredPosition);
                //}

                if (mouseOver)
                {
                    Fixture detected = SceneManager.ActiveScene.World.TestPoint(ConvertUnits.ToSimUnits(GameInput.MousePosition));
                    if (detected == null || detected.Body.GameObject != this && !SceneManager.IsEditor)
                    {
                        this.mouseOver = false;
                        this.OnMouseOut();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace.ToString());
            }
        }

        //private void MoveBodyToPosition(Vector2 destination)
        //{
        //    if (body != null)
        //    {
        //        Vector2 positionDelta = body.Position - destination;
        //        Vector2 velocity = (Vector2)((positionDelta / (1f / 60f)) * 1f);
        //        body.LinearVelocity = -velocity;
        //        velocity = new Vector2((int)velocity.X, (int)velocity.Y);

        //        if (velocity == Vector2.Zero)
        //            transform.physicsPositionReached = true;
        //    }
        //}

        /// <summary>
        /// Draws the game object.
        /// Components and children are notified to draw.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            try
            {
                // Draw this object components
                foreach (ObjectComponent component in components)
                {
                    if (component.Disabled) continue;

                    if (SceneManager.IsEditor && component is ExtendedObjectComponent)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }
                    else if (!SceneManager.IsEditor)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }
                }

                if (visible)
                {
                    // Draw this object children
                    for (int i = 0; i < children.Count; i++)
                        if (!children[i].Disabled)
                            children[i].Draw(gameTime, spriteBatch);

                    //if (CollisionModel.CollisionBoundry.Intersects(SceneManager.ActiveCamera.BoundingBox))
                    //{
                    //    if (SceneManager.IsEditor && SceneManager.GameProject.EditorSettings.ShowCollisions)
                    //    {
                    //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, SceneManager.ActiveCamera.TransformMatrix);

                    //        Primitives.DrawBoxFilled(spriteBatch, collisionModel.CollisionBoundry, collisionBoundryColor);

                    //        spriteBatch.End();

                    //        spriteBatch.Begin();

                    //        if (CollisionModel.BlockTexture != null)
                    //        {
                    //            Vector2 _pos = Vector2.Transform(transform.Position, SceneManager.ActiveCamera.TransformMatrix);
                    //            spriteBatch.Draw(CollisionModel.BlockTexture, _pos, null, Color.FromNonPremultiplied(255, 255, 255, 120), transform.Rotation, new Vector2(CollisionModel.BlockTexture.Width / 2, CollisionModel.BlockTexture.Height / 2), transform.Scale * SceneManager.ActiveCamera.Zoom, SpriteEffects.None, 1);
                    //        }

                    //        spriteBatch.End();
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace.ToString());
            }
        }

        /// <summary>
        /// Removes the gameobject in the active scene by its name
        /// </summary>
        /// <param name="src">object name</param>
        /// <returns>null if not found</returns>
        public static GameObject Remove(string src)
        {
            if (SceneManager.ActiveScene == null) return null;

            foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            {
                if (gameObject.name == src)
                {
                    SceneManager.ActiveScene.GameObjects.Remove(gameObject);
                    return gameObject;
                }

                GameObject result = RemoveNext(gameObject, src);

                // The game object was found?
                if (result != null)
                {
                    return result;
                }
            }

            // Not found
            return null;
        }

        private static GameObject RemoveNext(GameObject gameObject, string src)
        {
            foreach (GameObject _gameObject in gameObject.Children)
            {
                if (_gameObject.name == src)
                {
                    gameObject.Children.Remove(gameObject);
                    return gameObject;
                }

                GameObject result = RemoveNext(_gameObject, src);

                // The game object was found?
                if (result != null)
                {
                    _gameObject.Children.Remove(result);
                    return result;
                }
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Removes a game object by it's reference
        /// </summary>
        /// <param name="gameObject">The object to remove</param>
        /// <returns></returns>
        public static GameObject Remove(GameObject gameObject)
        {
            gameObject.Remove();

            return gameObject;
        }

        /// <summary>
        /// Searches for the gameobject in the active scene 
        /// </summary>
        /// <param name="src">search parameter</param>
        /// <param name="searchOption">what to look for</param>
        /// <returns>null if not found</returns>
        public static GameObject Find(string src, SearchOptions searchOption = SearchOptions.Name)
        {
            if (SceneManager.ActiveScene == null) return null;

            foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            {
                GameObject result = FindNext(gameObject, src, searchOption);

                // The game object was found?
                if (result != null)
                    return result;
            }

            // Not found
            return null;
        }

        private static GameObject FindNext(GameObject gameObject, string src, SearchOptions searchOption)
        {
            if (FindComparer(gameObject, src, searchOption) != null) return gameObject;

            foreach (GameObject _gameObject in gameObject.Children)
            {
                GameObject result = FindNext(_gameObject, src, searchOption);

                // The game object was found?
                if (result != null)
                    return result;
            }

            // Not found
            return null;
        }

        private static GameObject FindComparer(GameObject gameObject, string src, SearchOptions searchOption)
        {
            switch (searchOption)
            {
                case SearchOptions.Hash:
                    if (gameObject.GetHashCode() == Convert.ToInt32(src))
                        return gameObject;
                    break;
                case SearchOptions.Name:
                    if (gameObject.name == src)
                        return gameObject;
                    break;
            }

            return null;
        }

        /// <summary>
        /// Gets all gameobjects
        /// </summary>
        /// <returns>A list with all the game objects of the current scene</returns>
        public static List<GameObject> GetAllGameObjects()
        {
            if (SceneManager.ActiveScene == null) return null;

            List<GameObject> gameObjectsBuffer = new List<GameObject>();

            foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            {
                gameObjectsBuffer.Add(gameObject);
                LoadNext(gameObject, gameObjectsBuffer);
            }

            return gameObjectsBuffer;
        }

        private static void LoadNext(GameObject _gameObject, List<GameObject> gameObjectsBuffer)
        {
            foreach (GameObject gameObject in _gameObject.Children)
            {
                gameObjectsBuffer.Add(gameObject);
                LoadNext(gameObject, gameObjectsBuffer);
            }
        }

        internal void Delete()
        {
            if (this.transform.Parent == null)
                SceneManager.ActiveScene.GameObjects.Delete(this);
            else
                this.transform.Parent.GameObject.Children.Delete(this);
        }

        /// <summary>
        /// Removes this object from the hierarchy
        /// </summary>
        public void Remove()
        {
            if (this.transform.Parent == null)
                SceneManager.ActiveScene.GameObjects.Remove(this);
            else
                this.transform.Parent.GameObject.Children.Remove(this);
        }

        /// <summary>
        /// Event thrown when there is a mouse down collision
        /// </summary>
        public void OnMouseDown(MouseEventButton buttonPressed)
        {
            // send notification to components that this object has a mouse moving
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseDown(buttonPressed);
        }

        /// <summary>
        /// Event thrown when there is a mouse click collision
        /// </summary>
        /// <param name="buttonPressed">The button that was pressed</param>
        public void OnMouseClick(MouseEventButton buttonPressed)
        {
            // Send notification to components that this object was clicked
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseClick(buttonPressed);
        }

        /// <summary>
        /// Event thrown when there is a mouse move collision
        /// </summary>
        public void OnMouseMove()
        {
            // send notification to components that this object has a mouse moving
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseMove();
        }

        /// <summary>
        /// Event thrown when the mouse enters in collision with the game object
        /// </summary>
        public void OnMouseEnter()
        {
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseEnter();
        }

        /// <summary>
        /// Event thrown when the mouse is not over the object
        /// </summary>
        public void OnMouseUp()
        {
            // send notification to components that this object has a mouse moving
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseUp();
        }

        /// <summary>
        /// Event thrown when the mouse is not over the object
        /// </summary>
        public void OnMouseOut()
        {
            // send notification to components that this object has a mouse moving
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnMouseOut();
        }

        /// <summary>
        /// Event thrown when another object collides with this.
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(GameObject other)
        {
            // send notification to components that this object collided with other
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled)
                        component.OnCollisionEnter(other);
        }

        /// <summary>
        /// Event thrown in a frame where there is no collision with other objects
        /// </summary>
        public void OnCollisionFree()
        {
            // send notification to components
            foreach (ObjectComponent component in components)
                if ((SceneManager.IsEditor && component is ExtendedObjectComponent) || !SceneManager.IsEditor)
                    if (!component.Disabled) 
                        component.OnCollisionFree();
        }

        /// <summary>
        /// Saves the game object
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            GibboHelper.SerializeObject(filename, this);
        }

        /// <summary>
        /// Clone the game object.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            //return this.MemberwiseClone(); // old not functional way
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                var obj = (object)formatter.Deserialize(stream);
                (obj as GameObject).Initialize(); // important
                return obj;
            }
        }

        /// <summary>
        /// Clone de game object 
        /// </summary>
        /// <returns></returns>
        public GameObject Copy()
        {
            return (GameObject)Clone();
        }

        /// <summary>
        /// To String.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            foreach (GameObject obj in Children)
                obj.Dispose();

            GC.SuppressFinalize(this);
        }

        public virtual RotatedRectangle MeasureDimension()
        {
            return new RotatedRectangle(new Rectangle((int)transform.position.X, (int)transform.position.Y, 1, 1));
        }

        #endregion

        #region events

        void body_OnSeparation(Fixture fixtureA, Fixture fixtureB)
        {
            OnCollisionFree();
        }

        bool body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            OnCollisionEnter(fixtureB.Body.GameObject);
            return true;
        }

        #endregion
    }
}
