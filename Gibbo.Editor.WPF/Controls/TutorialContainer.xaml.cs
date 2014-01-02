using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for TutorialContainer.xaml
    /// </summary>
    public partial class TutorialContainer : UserControl
    {
        #region Fields

        private string xmlPath = string.Empty;
        private string rootPath = string.Empty;
        private string imagePath = string.Empty;
        private string title = string.Empty;

        #endregion

        #region Constructors

        public TutorialContainer()
        {
            InitializeComponent();
        }

        public TutorialContainer(string xmlPath, string rootPath)
        {
            InitializeComponent();
            this.xmlPath = xmlPath;
            this.rootPath = rootPath;
        }

        #endregion

        #region Methods

        public bool ReadInfo()
        {
            XDocument doc = XDocument.Load(xmlPath);

            imagePath = doc.Element("Tutorial").Element("Info").Element("Image").Value;

            EditorUtils.RenderPicture(ref containerPicture, this.rootPath + @"\Pictures\" + imagePath, 200, 180);
            if (containerPicture.Source == null)
                return false;

            containerPicture.Width = 200;
            containerPicture.Height = 180;

            this.title = doc.Element("Tutorial").Element("Info").Element("Title").Value;
            
            TitleTextBlock.Text = this.title;
            AuthorTextBlock.Text = doc.Element("Tutorial").Element("Info").Element("Author").Value;
            DescriptionTextBlock.Text = doc.Element("Tutorial").Element("Info").Element("Description").Value;

            return true;
        }

        #endregion

        #region Events

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TutorialWindow tutorial = new TutorialWindow(xmlPath, rootPath, this.title);
            tutorial.ShowDialog();
        }

        #endregion


    }
}
