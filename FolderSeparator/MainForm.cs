using System;
using System.IO;
using System.Windows.Forms;

namespace FolderSeparator
{
    public partial class MainForm : Form
    {
        private decimal _MaxSize { get; set; }

        private string _BaseDesPath { get; set; }

        private ActionExecutor _actionExecutor = null;

        private ActionExecutor _ActionExecutor
        {
            get
            {
                if (_actionExecutor == null)
                {
                    _actionExecutor = new ActionExecutor();
                }
                return _actionExecutor;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Select a folder
        /// </summary>
        /// <returns>Path of selected folder</returns>
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
                _MaxSize = numUpDownSize.Value * 1024 * 1024 * 1024; // (B KB MB) GB
                _BaseDesPath = txtTargetFolderInfo.Text + "\\" + sourceDirectoryInfo.Name + " - ";
                var index = 1;
                long currentSize = 0;

                CopyDirectory(sourceDirectoryInfo, ref index, ref currentSize);

                await _ActionExecutor.WaitForFinish();
                MessageBox.Show("Done");
            }
        }

        private void CopyDirectory(DirectoryInfo sourceDirectoryInfo, ref int index, ref long currentSize, string subPath = null)
        {
            // Check and copy folders
            foreach (var dirInfo in sourceDirectoryInfo.EnumerateDirectories())
            {
                CopyDirectory(dirInfo, ref index, ref currentSize, subPath + "\\" + dirInfo.Name);
            }

            // Create the folder in target folder
            var desFodlerInfo = Directory.CreateDirectory(_BaseDesPath + index + subPath);

            // Check and copy files
            foreach (var fileInfo in sourceDirectoryInfo.EnumerateFiles())
            {
                currentSize += fileInfo.Length;
                if (currentSize > _MaxSize)
                {
                    // The size is going to out of the max
                    // Reset veriables and create a new folder
                    currentSize = fileInfo.Length;

                    //// Delete the folder if it doesn't have sub file
                    //if (desFodlerInfo.GetFileSystemInfos().Length == 0)
                    //{
                    //    desFodlerInfo.Delete();
                    //}

                    index++;
                    desFodlerInfo = Directory.CreateDirectory(_BaseDesPath + index + subPath);
                }

                // Copy file
                string destFileName = desFodlerInfo.FullName + "\\" + fileInfo.Name;
                _ActionExecutor.AddActionToQueue(() =>
                {
                    fileInfo.CopyTo(destFileName);
                });
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
