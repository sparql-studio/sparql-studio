namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IViewContainer
    { }

    public interface IViewContainer<out TView> : IViewContainer
        where TView : IView 
    {
        TView GetView();
    }
}
