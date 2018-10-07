using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FolderSeparator
{
    public partial class MainForm : Form
    {
        private bool _SourcePathIsFromDialog { get; set; } = false;

        private Func<string> _GetSourcePathFunc { get; set; }

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

        private StringBuilder _bigFilePaths = null;

        private StringBuilder _BigFilePaths
        {
            get
            {
                if (_bigFilePaths == null)
                {
                    _bigFilePaths = new StringBuilder();
                }
                return _bigFilePaths;
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
            var sourcePath = _GetSourcePathFunc();

            if (!string.IsNullOrEmpty(sourcePath) && !string.IsNullOrEmpty(txtTargetFolderInfo.Text))
            {
                if (txtTargetFolderInfo.Text == sourcePath)
                {
                    MessageBox.Show("Plese choose different folder!");
                    return;
                }

                // Source folder and target folder are both selected, they are different
                var sourceDirectoryInfo = new DirectoryInfo(sourcePath);

                // Init check veriables
                _MaxSize = numUpDownSize.Value * 1024 * 1024 * 1024; // (B KB MB) GB
                _BaseDesPath = txtTargetFolderInfo.Text + "\\" + sourceDirectoryInfo.Name + " - ";
                var index = 1;
                long currentSize = 0;

                CopyDirectory(sourceDirectoryInfo, ref index, ref currentSize);

                await _ActionExecutor.WaitForFinish();

                var msg = new StringBuilder("Done.");
                if (_bigFilePaths != null)
                {
                    msg.AppendLine();
                    msg.AppendLine("But below files are bigger than your limit:");
                    msg.AppendLine(_BigFilePaths.ToString());
                }

                MessageBox.Show(msg.ToString());
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

                    if (fileInfo.Length > _MaxSize)
                    {
                        // This file is bigger than _MaxSize
                        _BigFilePaths.AppendLine(desFodlerInfo.FullName + "\\" + fileInfo.Name);
                    }
                }

                // Copy file
                string destFileName = desFodlerInfo.FullName + "\\" + fileInfo.Name;
                _ActionExecutor.AddActionToQueue(() =>
                {
                    fileInfo.CopyTo(destFileName, true);
                });
            }
        }

        private void txtSourceFolderInfo_DoubleClick(object sender, EventArgs e)
        {
            txtSourceFolderInfo.Text = SelectFolder();
            _SourcePathIsFromDialog = true;
        }

        private void txtTargetFolderInfo_DoubleClick(object sender, EventArgs e)
        {
            txtTargetFolderInfo.Text = SelectFolder();
        }

        private void txtSourceFolderInfo_TextChanged(object sender, EventArgs e)
        {
            if (_SourcePathIsFromDialog)
            {
                _GetSourcePathFunc = () => txtSourceFolderInfo.Text;
            }
            else
            {
                // The sourcePath is input by user
                _GetSourcePathFunc = () =>
                {
                    var sourcePath = txtSourceFolderInfo.Text.Trim();
                    // Check whether the path exist on disk
                    if (Directory.Exists(sourcePath))
                    {
                        return sourcePath;
                    }
                    else
                    {
                        MessageBox.Show("Please input an correct path.");
                        return null;
                    }
                };
            }

            _SourcePathIsFromDialog = false;
        }
    }
}
