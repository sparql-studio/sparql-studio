using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dk.ModularSystems.Sparql.Contracts.Controllers;
using dk.ModularSystems.Sparql.Contracts.Views;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IAppView : IView
    {
        void ExitApplication();
        void ShowAppInfo(IAppController controller);
        IQueryView CreateNewQueryView(IQueryController controller);
        IQueryView CreateNewQueryView(IQueryController controller, String pathName);

        IQueryResultsView CreateNewQueryResultsView(SparqlQueryResultsInfo results, IQueryResultsController controller);
        IResourceBrowserView CreateNewResourceBrowserView(String resourceUri, IResourceBrowserController controller);

        void OpenQuery(IQueryController controller);
        void OpenQuery(IQueryController controller, String pathName);
        void SaveQuery(IQueryView queryView, String pathName);
        void SaveQueryWithNewName(IQueryView queryView);
        
        IEnumerable<IView> GetViews();

        IEnumerable<String> GetEndpoints();
        void SetEndpoints(IEnumerable<String> endpointUriStrings);
        
        String GetCurrentEndpoint();
        void SetCurrentEndpoint(String endpointUriString);

        void ReportError(Exception exception);

        void SetStatusReady();
        void SetStatusBusy(String statusText);
        void SetStatusBusy(String statusText, Int32 progressPct);

        IFilePickerView CreateNewFilePickerView(IFilePickerController controller, String folder, String filter);
    }
}
