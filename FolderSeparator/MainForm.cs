using System;
using System.IO;
using System.Windows.Forms;

namespace FolderSeparator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string SelectFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                // Open a new folder browser dialog and check the selected path
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }

            return null;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSourceFolderInfo.Text) && !string.IsNullOrEmpty(txtTargetFolderInfo.Text))
            {
                if (txtTargetFolderInfo.Text == txtSourceFolderInfo.Text)
                {
                    MessageBox.Show("Plese choose different folder!");
                    return;
                }

                // Source folder and target folder are both selected, they are different
                var sourceDirectoryInfo = new DirectoryInfo(txtSourceFolderInfo.Text);

                // Init check veriables
                var index = 1;
                long currentSize = 0;
                var maxSize = numUpDownSize.Value * 1024 * 1024 * 1024; // GB (MB KB B)
                string baseDesPath = txtTargetFolderInfo.Text + "\\" + sourceDirectoryInfo.Name + " - ";
                var actionExecutor = new ActionExecutor();

                // Create the folder in target folder
                var desFodlerInfo = Directory.CreateDirectory(baseDesPath + index);

                // Check and copy files
                foreach (var fileInfo in sourceDirectoryInfo.EnumerateFiles())
                {
                    currentSize += fileInfo.Length;
                    if (currentSize > maxSize)
                    {
                        // The size is going to out of the max
                        // Reset veriables and create a new folder
                        currentSize = fileInfo.Length;
                        index++;
                        desFodlerInfo = Directory.CreateDirectory(baseDesPath + index);
                    }

                    // Copy file
                    string destFileName = desFodlerInfo.FullName + "\\" + fileInfo.Name;
                    actionExecutor.AddActionToQueue(() =>
                    {
                        fileInfo.CopyTo(destFileName);
                    });
                }

                await actionExecutor.WaitForFinish();
                MessageBox.Show("Done");
            }
        }

        private void txtSourceFolderInfo_DoubleClick(object sender, EventArgs e)
        {
            txtSourceFolderInfo.Text = SelectFolder();
        }

        private void txtTargetFolderInfo_DoubleClick(object sender, EventArgs e)
        {
            txtTargetFolderInfo.Text = SelectFolder();
        }
    }
}
