using ComponentFactory.Krypton.Toolkit;

namespace Gibbo.Editor
{
    public partial class OutputWindow : KryptonForm
    {
        public OutputWindow()
        {
            InitializeComponent();
        }

        private void OutputWindow_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == System.Windows.Forms.CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            kryptonDataGridView.Rows.Clear();
        }
    }
}
