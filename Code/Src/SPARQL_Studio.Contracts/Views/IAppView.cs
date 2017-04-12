// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;
using dk.ModularSystems.Sparql.Contracts.Controllers;
using dk.ModularSystems.Sparql.Contracts.Views;

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
