using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    public partial class CompilerForm : KryptonForm
    {
        public CompilerForm()
        {
            InitializeComponent();
        }

        private void CompilerForm_Shown(object sender, EventArgs e)
        {
            label2.Text = "Gathering data and setting parameters to compile...";

            if (ScriptsBuilder.ReloadScripts())
            {
                // Sucess:

                progressBar1.Value = 100;
                label2.Text = "Success!";

                System.Threading.ThreadPool.QueueUserWorkItem(delegate
                {
                    Thread.Sleep(500);
                    SafeClose();
                }, null);

                EditorCommands.ShowOutputMessage("Scripts compiled with sucess");
            }
            else
            {
                progressBar1.Value = 80;
                label2.Text = "Failure!";

                EditorCommands.ShowOutputMessage("Error while compiling the scripts");
            }

            //Thread.Sleep(2000);
            //this.Close();
        }

        public void SafeClose()
        {
            // Make sure we're running on the UI thread
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action(SafeClose));
                return;
            }

            // Close the form now that we're running on the UI thread
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            Close();

        }

        private void CompilerForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
