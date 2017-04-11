using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dk.ModularSystems.Sparql.Contracts.Controllers;
using dk.ModularSystems.Sparql.Contracts.Views;
using VDS.Common.Tries;

namespace dk.ModularSystems.Sparql
{
    public partial class FilePickerView : UserControl, IFilePickerView
    {
        public FilePickerView()
        {
            InitializeComponent();
        }

        public FilePickerView(IFilePickerController controller) :
            this()
        {
            _controller = controller;
        }

        protected void OnOpenSelectedFiles(IEnumerable<String> filePaths)
        {
            _controller.OnFilesSelected(filePaths);
        }

        protected class FileItem
        {
            public String FilePath { get; set; }
            public Boolean HidePath { get; set; }
            public Boolean HideExtension { get; set; }
            public override String ToString()
            {
                if (HidePath == true)
                {
                    return Path.GetFileNameWithoutExtension(FilePath);
                }
                return FilePath;
            }

            public FileItem(String filePath)
            {
                HidePath = true;
                HideExtension = true;
                FilePath = filePath;
            }
        }

        #region IFilePickerView

        public void SetFileList(IEnumerable<string> filePaths)
        {
            foreach (String filePath in filePaths)
            {
                Int32 itemIndex = lbFiles.Items.Add(new FileItem(filePath));
            }
        }

        public void SetFileList(string folderPath, string wildCard)
        {
            SetFileList(Directory.EnumerateFiles(folderPath, wildCard));
        }

        #endregion IFilePickerView

        private IFilePickerController _controller;
        public Contracts.IController Controller
        {
            get { return _controller; }
            set { _controller = (IFilePickerController)value; }
        }


        public Boolean CanClose()
        {
            return true;
        }

        public bool IsDirty
        {
            get { return false; }
        }

        public bool IsUntitled
        {
            get
            {
                return false; 
            }
        }

        private String _title = "File picker";
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public event Action<object> ContentChanged { add { } remove { } }
        public event Action<object> ContentSaved { add { } remove { } }
        public event Action<object> ContentLoaded { add { } remove { } }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedItems.Count > 0)
            {
                List<String> selectedFiles = new List<string>();
                foreach (FileItem item in lbFiles.SelectedItems)
                {
                    selectedFiles.Add(item.FilePath);
                }
                OnOpenSelectedFiles(selectedFiles);
            }
        }

        private void lbFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOpen_Click(sender, e);
        }
    }
}
