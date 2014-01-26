using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using Xceed.Wpf.AvalonDock;
using System.Collections;

namespace Gibbo.Editor.WPF
{
    static class LayoutHelper
    {
        // [alterar] colocar o field na região correspondente
        private const string layoutPath = @".\Layout\";
        private const string layoutExtension = ".layout";

        public static string LayoutPath { get { return layoutPath; } }

        public static string LayoutExtension { get { return layoutExtension; } }

        public static DockingManager DockManager { get; set; }

        // Attemps to Load a Layout
        public static bool LoadLayout(string layoutName)
        {
            if (!LayoutExists(layoutName)) return false;

            try
            {
                var serializer = new XmlLayoutSerializer(DockManager);
                using (var stream = new StreamReader(string.Format(layoutPath + "{0}" + layoutExtension, layoutName)))
                    serializer.Deserialize(stream);

                Properties.Settings.Default.Layout = layoutName;
            }
            catch (Exception)
            {
                return false;
            }
            // loaded successfully
            return true;
        }

        // Attemps to load Gibbo's default layout
        public static void LoadGibbsoDefaultLayout()
        {
            EditorCommands.ShowOutputMessage("Attempting to load Gibbo's default layout");
            if (LoadLayout("Default"))
                EditorCommands.ShowOutputMessage("Gibbo's default layout has been loaded successfully");
            else
                EditorCommands.ShowOutputMessage("Gibbo's saved layout load attempt has failed");
        }

        // Attemps to create a new Layout
        public static bool CreateNewLayout(string layoutName)
        {
            EditorCommands.ShowOutputMessage("Attempting to create a new layout");

            if (layoutName.Equals("Default"))
            {
                EditorCommands.ShowOutputMessage("Cannot override Gibbo's Default Layout");
                return false;
            }
            
            if (RemoveLayout(layoutName))
                EditorCommands.ShowOutputMessage("Attempting to replace layout");

            try
            {
                EditorCommands.ShowOutputMessage("Attempting to save the layout");
                var serializer = new Xceed.Wpf.AvalonDock.Layout.Serialization.XmlLayoutSerializer(DockManager);
                using (var stream = new StreamWriter(layoutPath + layoutName + LayoutExtension))
                    serializer.Serialize(stream);

                EditorCommands.ShowOutputMessage("Layout saved successfully");
            }
            catch (Exception)
            {
                EditorCommands.ShowOutputMessage("Layout saving attempt has failed");
                return false;
            }

            Properties.Settings.Default.Layout = layoutName;
            return true;
        }

        public static bool RenameLayout(string newLayoutName)
        {
            if (!newLayoutName.Equals("Default") && !Properties.Settings.Default.Layout.Equals("Default"))
            {
                // Attemps to remove the current layout
                if (RemoveLayout(Properties.Settings.Default.Layout))
                    // Attempts to create a new layout with a new name
                    if (CreateNewLayout(newLayoutName))
                        return true; // renamed successfully
            }

            return false;
        }

        public static bool RemoveLayout(string layoutName)
        {
            EditorCommands.ShowOutputMessage("Attempting to remove layout");
            if (layoutName.Equals("Default"))
            {
                EditorCommands.ShowOutputMessage("Cannot remove Gibbo's Default Layout");
                return false;
            }

            if (LayoutExists(layoutName))
                try { System.IO.File.Delete(layoutPath + layoutName + layoutExtension); }
                catch { EditorCommands.ShowOutputMessage("Attempt to remove layout has failed"); return false; }

            return true;
        }

        public static bool LayoutExists(string layoutName)
        {
            layoutName = layoutName.Trim();
            string path = layoutPath + layoutName + layoutExtension;
            if (System.IO.File.Exists(path))
                return true;
            return false;
        }

        public static List<string> GetLayouts()
         {
             List<string> Layouts = new List<string>();

             foreach (var layout in System.IO.Directory.GetFiles(layoutPath, "*.layout"))
             {
                 string name = System.IO.Path.GetFileNameWithoutExtension(layout);
                 if (!name.Equals("Default"))
                     Layouts.Add(name);
             }

             return Layouts;
         }

    }
}
