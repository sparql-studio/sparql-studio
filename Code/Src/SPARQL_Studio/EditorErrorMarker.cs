// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Markers;
using VDS.RDF.Parsing;

namespace dk.ModularSystems.Sparql
{
    internal class EditorErrorMarker : Marker
    {
        public EditorErrorMarker(IDocument doc, RdfParseException ex, EditorErrorMarkingStyle markingStyle) :

            base(doc.PositionToOffset(new TextLocation( 0 , ex.StartLine-1)), 
                 doc.GetLineSegment(ex.StartLine-1).Length, 
                 markingStyle.MarkerType,
                 markingStyle.MarkerColor )
        {
            switch (markingStyle.MarkerExtension)
            {
                case EditorErrorMarkingStyle.Extension.Char:
                    Offset = doc.PositionToOffset(new TextLocation(ex.StartPosition - 2, ex.StartLine - 1));
                    Length = 1;
                    break;
                case EditorErrorMarkingStyle.Extension.Line:
                    Offset = doc.PositionToOffset(new TextLocation(0, ex.StartLine-1));
                    Length = doc.GetLineSegment(ex.StartLine - 1).Length;
                    break;
                case EditorErrorMarkingStyle.Extension.Word:
                default:
                    Offset = doc.PositionToOffset(new TextLocation(0, ex.StartLine-1));
                    Length = doc.GetLineSegment(ex.StartLine - 1).Length;

                    // TODO: Mark only a single word
                    // 
                    //Offset = doc.PositionToOffset(new TextLocation(ex.StartPosition-2, ex.StartLine-1));
                    //Length = doc.GetLineSegment(ex.StartLine - 1).Length; 
                    break;
            }


            this.ToolTip = ex.Message;
        }
    }
}
