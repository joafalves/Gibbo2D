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
