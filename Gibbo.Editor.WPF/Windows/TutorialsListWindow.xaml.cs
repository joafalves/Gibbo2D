using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for TutorialsListWindow.xaml
    /// </summary>
    public partial class TutorialsListWindow : Window
    {
        #region Fields

        private List<FileInfo> tutorialFiles = new List<FileInfo>();

        private Dictionary<string, TutorialsCategoryContainer> tutorialsSections =
            new Dictionary<string, TutorialsCategoryContainer>();

        System.Windows.Forms.Timer visualEffectsTimer;

        #endregion

        public TutorialsListWindow()
        {
            InitializeComponent();

            if (!RenderList())
                this.Close();
        }

        private bool RenderList()
        {
            string tutorialsPath = AppDomain.CurrentDomain.BaseDirectory + @"\Tutorials";
            if (!Directory.Exists(tutorialsPath))
            {
                EditorCommands.ShowOutputMessage("Tutorials folder not found");
                return false;
            }

            DirectoryInfo directory = new DirectoryInfo(tutorialsPath);
            if (!getTutorials(directory))
            {
                EditorCommands.ShowOutputMessage("Error loading tutorials");
                return false;
            }

            foreach (FileInfo file in tutorialFiles)
            {
                string xmlFullPath = file.FullName;
                XDocument doc = XDocument.Load(xmlFullPath);
                string category = doc.Element("Tutorial").Element("Info").Element("Category").Value;

                if (!tutorialsSections.ContainsKey(category))
                {
                    tutorialsSections.Add(category, new TutorialsCategoryContainer(category));
                    DockPanel.SetDock(tutorialsSections[category], Dock.Top);
                    this.ContainersDockPanel.Children.Add(tutorialsSections[category]);
                }

                if (!(tutorialsSections[category] as TutorialsCategoryContainer).AddTutorialPreview(xmlFullPath, file.DirectoryName))
                    return false;

            }

            return true;
        }

        #region Methods

        private bool getTutorials(DirectoryInfo dir)
        {
            tutorialFiles.Clear();

            try
            {
                foreach (FileInfo file in dir.GetFiles("*.xml", SearchOption.AllDirectories))
                {
                    Console.WriteLine("File {0}", file.FullName);
                    tutorialFiles.Add(file);
                }
            }
            catch
            {
                Console.WriteLine("Directory {0}  \n could not be accessed!!!!", dir.FullName);
                return false;
            }

            return true;
        }

        #endregion

    }
}
