using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dk.ModularSystems.Sparql.Contracts.Views
{
    public interface IFilePickerView : IView
    {
        void SetFileList(IEnumerable<String> filePaths);
        void SetFileList(String folderPath, String wildCard);
    }
}
