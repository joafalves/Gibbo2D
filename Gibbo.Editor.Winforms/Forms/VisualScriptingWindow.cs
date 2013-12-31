using ComponentFactory.Krypton.Toolkit;
using Gibbo.Library;
using Gibbo.Library.VisualScripting.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public partial class VisualScriptingWindow : KryptonForm
    {
        #region fields

        // combo box item index - visual script key 
        private Dictionary<int, int> mapping = new Dictionary<int, int>();

        #endregion

        #region constructors

        public VisualScriptingWindow()
        {
            InitializeComponent();

            ResetVSComboBox();
        }

        #endregion

        #region methods

        protected override bool ProcessKeyPreview(ref Message m)
        {
            visualScripting1.ProcessKeyMessage(ref m);
            return base.ProcessKeyPreview(ref m);
        }

        private void ResetVSComboBox()
        {
            visualScriptsComboBox.Text = string.Empty;
            visualScriptsComboBox.Items.Clear();
            visualScriptsComboBox.SelectedIndex = -1;
            mapping = new Dictionary<int, int>();
            int index = 0;

            foreach (int key in SceneManager.GameProject.VisualScriptManager.VisualScripts.Keys)
            {
                visualScriptsComboBox.Items.Add(SceneManager.GameProject.VisualScriptManager.VisualScripts[key]);

                if (SceneManager.GameProject.VisualScriptManager.VisualScripts[key] == visualScripting1.ActiveVisualScript)
                {
                    visualScriptsComboBox.SelectedIndex = index;
                }

                mapping[index] = key;
                index++;
            }

            if (visualScriptsComboBox.Items.Count > 0 && visualScriptsComboBox.SelectedIndex == -1)
                visualScriptsComboBox.SelectedIndex = 0;
        }

        #endregion

        #region events

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (visualScripting1.ActiveVisualScript == null)
                e.Cancel = true;
        }

        private void visualScripting1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                visualScripting1.LeftMouseKeyDown = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                visualScripting1.MiddleMouseKeyDown = false;
            }
        }

        private void visualScripting1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                visualScripting1.LeftMouseKeyDown = true;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                visualScripting1.MiddleMouseKeyDown = true;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            visualScripting1.Camera.Position = Vector2.Zero;
        }

        private void visualScriptsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            visualScripting1.ActiveVisualScript = (VisualScript)visualScriptsComboBox.SelectedItem;
        }

        private void visualScriptsComboBox_TextChanged(object sender, EventArgs e)
        {
            if (visualScripting1.ActiveVisualScript != null && visualScriptsComboBox.Text != string.Empty)
            {
                visualScripting1.ActiveVisualScript.Name = visualScriptsComboBox.Text;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            VisualScript vs = new VisualScript() { Name = "New Visual Script" };
            SceneManager.GameProject.VisualScriptManager.AddVisualScript(vs);
            ResetVSComboBox();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (visualScriptsComboBox.SelectedItem != null)
            {
                SceneManager.GameProject.VisualScriptManager.VisualScripts.Remove(mapping[visualScriptsComboBox.SelectedIndex]);
                visualScripting1.ActiveVisualScript = null;
                ResetVSComboBox();
            }
        }

        private void VisualScriptingWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            visualScripting1.ReleaseDevice();
        }

        private void onSceneStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            visualScripting1.ActiveVisualScript.Nodes.Add(
               new Event()
               {
                   EventType = EventTypes.OnSceneStart
               });
        }

        #endregion
    }
}
