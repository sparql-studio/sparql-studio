using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ModularSystems.Sparql.Contracts
{
    // Base interface for all view objects 
    //
    public interface IView
    {
        IController Controller { get; set; }

        Boolean IsDirty { get; }
        Boolean IsUntitled { get; }
        String Title { get; }

        event Action<Object> ContentChanged;
        event Action<Object> ContentSaved;
        event Action<Object> ContentLoaded;

        Boolean CanClose();

    }
}
