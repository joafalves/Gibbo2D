#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

The license applies to all versions of the software, both newer and older than the one listed, unless a newer copy 
of the license is available, in which case the most recent copy of the license supercedes all others.

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
