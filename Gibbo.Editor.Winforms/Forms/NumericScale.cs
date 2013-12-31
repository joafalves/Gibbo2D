using ComponentFactory.Krypton.Toolkit;
using Gibbo.Library;
using System;

namespace Gibbo.Editor
{
    public partial class NumericScale : KryptonForm
    {
        #region constructors

        public NumericScale()
        {
            InitializeComponent();
        }

        #endregion

        #region methods


        #endregion

        #region events

        private void NumericScale_Load(object sender, System.EventArgs e)
        {
            if (EditorHandler.SelectedGameObjects.Count > 1)
            {
                maskedTextBox1.Text = "100";
            }
            else
            {
                int convert = (int)(EditorHandler.SelectedGameObjects[0].Transform.Scale * 100.0f);
                maskedTextBox1.Text = convert.ToString();
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            float convert = float.Parse(maskedTextBox1.Text) / 100.0f;

            foreach (GameObject gameObject in EditorHandler.SelectedGameObjects)
            {
                gameObject.Transform.Scale = convert;
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
