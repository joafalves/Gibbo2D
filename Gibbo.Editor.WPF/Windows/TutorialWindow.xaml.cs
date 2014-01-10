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
using Gibbo.Library;
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
    /// Interaction logic for TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        #region Constants

        private const int maxWidth = 640;
        private const int maxHeight = 340;

        #endregion

        #region Fields

        private string title = string.Empty;
        private List<Page> pages = new List<Page>();
        private int currentPage = 1;
        private string rootPath = string.Empty;

        #endregion

        #region Constructors

        public TutorialWindow()
        {
            InitializeComponent();
        }

        public TutorialWindow(string xmlPath, string rootPath, string title)
        {
            InitializeComponent();

            this.title = title;
            Title = title;

            if (!ReadTutorial(xmlPath))
            {
                EditorCommands.ShowOutputMessage("Error loading tutorial");
                this.Close();
            }

            this.rootPath = rootPath;
            RenderPage(0);
        }

        #endregion

        #region Methods

        private bool ReadTutorial(string xmlPath)
        {
            pages.Clear();

            XDocument doc = XDocument.Load(xmlPath);

            var tutorial = from al in doc.Element("Tutorial").Element("Pages").Descendants("Page") select al;

            foreach (var page in tutorial)
            {
                Page newPage = new Page(page.Attribute("Title").Value, page.Element("Description").Value, page.Element("Image").Value);
                this.pages.Add(newPage);
            }

            if (this.pages.Count <= 0)
                return false;

            return true;
        }

        private void RenderPage(int n)
        {
            EditorUtils.RenderPicture(ref PagePicture, this.rootPath + @"\Pictures\" + this.pages[n].PicturePath, maxWidth, maxHeight);
            if (PagePicture.Source == null)
            {
                this.Close();
            }
            RenderTitleAndDescription(n);
        }

        private void RenderTitleAndDescription(int n)
        {
            // Render Title
            this.TutorialTitle.Text = string.Format("{0} ({1}/{2})", this.pages[n].Subtitle, this.currentPage, this.pages.Count); // this.pages[n].Subtitle + " (" + this.currentPage + "/" + this.pages.Count + ")";

            // Render Description
            this.txtDescription.Text = this.pages[n].Description;
        }

        #endregion

        #region Events

        private void NextBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentPage < this.pages.Count)
                currentPage++;
            else
                currentPage = 1;

            RenderPage(currentPage-1);
        }

        private void PreviousBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
                currentPage--;
            else
                currentPage = this.pages.Count;

            RenderPage(currentPage-1);
        }

        #endregion

    }
}
