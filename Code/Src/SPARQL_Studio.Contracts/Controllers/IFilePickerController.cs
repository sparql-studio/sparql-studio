using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dk.ModularSystems.Sparql.Contracts.Controllers
{
    public interface IFilePickerController : IController
    {
        void OnFilesSelected(IEnumerable<String> filePaths);
    }
}
