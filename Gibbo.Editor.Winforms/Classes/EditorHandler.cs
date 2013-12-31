using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gibbo.Library;
using System.Windows.Forms;
using System.IO;

namespace Gibbo.Editor
{
    static class EditorHandler
    {
        internal static OutputWindow OutputWindow = new OutputWindow();

        /// <summary>
        /// Changes the property grid and property label to the input value
        /// </summary>
        /// <param name="value">The object</param>
        public static void ChangeSelectedObject(object value)
        {
            if (value != null)
            {
                PropertyGrid.SelectedObject = value;

                string text = value.ToString();

                if (text.Contains("."))
                {
                    text = Path.GetExtension(text).Replace(".", string.Empty);
                }

                PropertyPage.Text = "Object Name: " + text + "\n";
            }
        }

        /// <summary>
        /// Changes the property grid and property label to the selected objects
        /// </summary>
        public static void ChangeSelectedObjects()
        {
            if (SelectedGameObjects != null)
            {
                if (SelectedGameObjects.Count == 1)
                {
                    ChangeSelectedObject(SelectedGameObjects[0]);

                    if (SelectedGameObjects[0] is Tileset)
                    {
                        EditorHandler.BrushControl.ChangeSelectionSize((SelectedGameObjects[0] as Tileset).TileWidth, (SelectedGameObjects[0] as Tileset).TileHeight);
                        EditorHandler.BrushControl.ChangeImageSource((SelectedGameObjects[0] as Tileset).ImagePath);
                        EditorHandler.EditorSplitterContainer.Panel2Collapsed = false;

                        SceneManager.ActiveTileset = (Tileset)SelectedGameObjects[0];
                    }
                    else
                    {
                        EditorHandler.EditorSplitterContainer.Panel2Collapsed = true;
                        SceneManager.ActiveTileset = null;
                    }
                }
                else if (SelectedGameObjects.Count == 0)
                {
                    SceneManager.ActiveTileset = null;
                    PropertyPage.Text = "No object selected";
                    PropertyGrid.SelectedObject = null;
                    EditorHandler.EditorSplitterContainer.Panel2Collapsed = true;
                }
                else
                {
                    PropertyGrid.SelectedObjects = SelectedGameObjects.ToArray();
                    PropertyPage.Text = "Multiple Game Objects";

                    EditorHandler.EditorSplitterContainer.Panel2Collapsed = true;
                } 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static SplitContainer EditorSplitterContainer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static BrushControl BrushControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IniFile Settings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static ToolStripStatusLabel StatusLabel { get; set; }

        /// <summary>
        /// Gets or sets the active PropertyGrid.
        /// </summary>
        public static PropertyGrid PropertyGrid { get; set; }

        /// <summary>
        /// Gets or sets the active PropertyLabel.
        /// </summary>
        public static TabPage PropertyPage { get; set; }

        /// <summary>
        /// Gets or sets the active SceneTreeView.
        /// </summary>
        public static SceneTreeViewControl SceneTreeView { get; set; }

        /// <summary>
        /// Gets or sets the selected GameObjects.
        /// </summary>
        public static List<GameObject> SelectedGameObjects { get; set; }

        /// <summary>
        /// Gets or sets the active UnDoRedo.
        /// </summary>
        public static UndoRedo UnDoRedo { get; set; }
    }
}
