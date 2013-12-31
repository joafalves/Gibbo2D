using ComponentFactory.Krypton.Toolkit;
using Gibbo.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gibbo.Editor
{
    public partial class ProjectSettings : KryptonForm
    {
        private bool locked = false;
        private GibboProject gameProject;
        private IniFile settings = new IniFile(SceneManager.GameProject.ProjectPath + "\\settings.ini");

        public ProjectSettings()
        {
            InitializeComponent();
        }

        private void ProjectSettings_Load(object sender, EventArgs e)
        {
            gameProject = SceneManager.GameProject;

            UnloadSettings();
        }

        private void UnloadSettings()
        {
            locked = true;
            projectNameTxt.Text = gameProject.ProjectName;
            showCollisionsCheckBox.Checked = gameProject.EditorSettings.ShowCollisions;
            highlightActiveTilesetEditorCheckBox.Checked = gameProject.ProjectSettings.HighlightActiveTilesetInEditor;
            highlightActiveTilesetGameCheckBox.Checked = gameProject.ProjectSettings.HighlightActiveTilesetInGame;
            debugCheckBox.Checked = gameProject.Debug;
            showGridCheckBox.Checked = gameProject.EditorSettings.ShowGrid;
            snapToGridCheckBox.Checked = gameProject.EditorSettings.SnapToGrid;
            gridSpacingNumeric.Value = gameProject.EditorSettings.GridSpacing;
            gridThicknessNumeric.Value = gameProject.EditorSettings.GridThickness;
            displayLinesNumeric.Value = gameProject.EditorSettings.GridNumberOfLines;
            colorPanel.BackColor = Color.FromArgb(gameProject.EditorSettings.GridColor.A, gameProject.EditorSettings.GridColor.R, gameProject.EditorSettings.GridColor.G, gameProject.EditorSettings.GridColor.B);
            showConsoleCheckBox.Checked = settings.IniReadValue("Console", "Visible").ToLower().Trim().Equals("true") ? true : false;
            mouseVisibleCheckBox.Checked = settings.IniReadValue("Mouse", "Visible").ToLower().Trim().Equals("true") ? true : false;
            screenWidthNumeric.Value = gameProject.Settings.ScreenWidth;
            screenHeightNumeric.Value = gameProject.Settings.ScreenHeight;
            startFullScreenCheckBox.Checked = settings.IniReadValue("Window", "StartFullScreen").ToLower().Trim().Equals("true") ? true : false;
            aNumeric.Value = colorPanel.BackColor.A;
            rNumeric.Value = colorPanel.BackColor.R;
            gNumeric.Value = colorPanel.BackColor.G;
            bNumeric.Value = colorPanel.BackColor.B;
            locked = false;
        }

        private void UploadSettings()
        {
            gameProject.ProjectName = projectNameTxt.Text;
            gameProject.EditorSettings.ShowCollisions = showCollisionsCheckBox.Checked;
            gameProject.ProjectSettings.HighlightActiveTilesetInEditor = highlightActiveTilesetEditorCheckBox.Checked;
            gameProject.ProjectSettings.HighlightActiveTilesetInGame = highlightActiveTilesetGameCheckBox.Checked;
            gameProject.Debug = debugCheckBox.Checked;
            gameProject.EditorSettings.ShowGrid = showGridCheckBox.Checked;
            gameProject.EditorSettings.SnapToGrid = snapToGridCheckBox.Checked;
            gameProject.EditorSettings.GridSpacing = (int)gridSpacingNumeric.Value;
            gameProject.EditorSettings.GridThickness = (int)gridThicknessNumeric.Value;
            gameProject.EditorSettings.GridNumberOfLines = (int)displayLinesNumeric.Value;
            gameProject.EditorSettings.GridColor = Microsoft.Xna.Framework.Color.FromNonPremultiplied(colorPanel.BackColor.R, colorPanel.BackColor.G, colorPanel.BackColor.B, colorPanel.BackColor.A);
            settings.IniWriteValue("Console", "Visible", " " + showConsoleCheckBox.Checked.ToString());
            settings.IniWriteValue("Mouse", "Visible", " " + mouseVisibleCheckBox.Checked.ToString());
            gameProject.Settings.ScreenWidth = (int)screenWidthNumeric.Value;
            gameProject.Settings.ScreenHeight = (int)screenHeightNumeric.Value;
            settings.IniWriteValue("Window", "StartFullScreen", " " + startFullScreenCheckBox.Checked);
        }

        private void colorPanel_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = colorPanel.BackColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colorPanel.BackColor = cd.Color;
                aNumeric.Value = cd.Color.A;
                rNumeric.Value = cd.Color.R;
                gNumeric.Value = cd.Color.G;
                bNumeric.Value = cd.Color.B;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UploadSettings();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UploadSettings();
            this.Close();
        }

        private void colorPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (!locked)
                colorPanel.BackColor = Color.FromArgb((int)aNumeric.Value, (int)rNumeric.Value, (int)gNumeric.Value, (int)bNumeric.Value);
        }

        private void showConsoleCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
