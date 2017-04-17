// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using dk.ModularSystems.Sparql.Ontology;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface ITextEditorView : IView
    {
        void Save(String pathName);
        void Open(String pathName);
        void SetText(String text);

        Boolean AutocompleteEnabled { get; set; }
        String PathName { get; }
        Boolean ReadOnly { set; get; }
    }
}
