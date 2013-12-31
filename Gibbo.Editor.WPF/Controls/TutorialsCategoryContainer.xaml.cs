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

namespace Gibbo.Editor.WPF
{
    /// <summary>
    /// Interaction logic for TutorialsCategoryContainer.xaml
    /// </summary>
    public partial class TutorialsCategoryContainer : UserControl
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public TutorialsCategoryContainer()
        {
            InitializeComponent();
        }

        public TutorialsCategoryContainer(string category)
        {
            InitializeComponent();

            CategoryTextBlock.Text = "● " + category;
        }

        #endregion

        #region Methods

        public bool AddTutorialPreview(string xmlPath, string rootPath)
        {
            TutorialContainer tutoPreview = new TutorialContainer(xmlPath, rootPath);
            if (tutoPreview.ReadInfo())
            {
                this.TutorialsWrapPanel.Children.Add(tutoPreview);
                return true;
            }

            return false;
        }

        #endregion
    }
}
