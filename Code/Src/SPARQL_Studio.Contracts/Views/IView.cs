// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;

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
