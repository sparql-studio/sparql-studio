// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System.Drawing;
using DigitalRune.Windows.TextEditor.Markers;

namespace dk.ModularSystems.Sparql
{
    public class EditorErrorMarkingStyle
    {
        public static EditorErrorMarkingStyle ParseErrorMarkingStyle = new EditorErrorMarkingStyle(Extension.Line , MarkerType.SolidBlock, Color.Salmon );
        public static EditorErrorMarkingStyle ValidationErrorMarkingStyle = new EditorErrorMarkingStyle(Extension.Word, MarkerType.WaveLine, Color.Red);

        public enum Extension
        {
            Char,
            Word,
            Line,
        }

        public Extension    MarkerExtension { get; protected set; }
        public MarkerType   MarkerType { get; protected set; }
        public Color        MarkerColor { get; protected set; }

        public EditorErrorMarkingStyle(Extension extension, MarkerType markerType, Color color)
        {
            MarkerExtension = extension;
            MarkerType = markerType;
            MarkerColor = color;
        }

    }
}
