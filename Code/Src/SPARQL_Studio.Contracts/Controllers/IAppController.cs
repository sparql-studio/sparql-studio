using System;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IAppController : IController
    {
        void OnExit();
        void OnNewQueryView(IAppView sender);
        void OnOpenQuery(IAppView sender);
        void OnOpenQuery(IAppView sender, String queryPath);
        void OnSaveQuery(IAppView sender, IQueryView queryView);
        void OnSaveQueryAs(IAppView sender, IQueryView queryView);
        void OnAbout(IAppView sender);
        void OnTutorial(IAppView sender);
        void OnTutorialQueries(IAppView sender);
        void OnOfficialSpecs(IAppView sender);

        void Initialize(String[] args);
        void SetView(IAppView view);
        void OnException(Exception exception);

        void OnSparqlPublicEndpoints(IAppView sender);
    }
}
