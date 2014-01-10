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
