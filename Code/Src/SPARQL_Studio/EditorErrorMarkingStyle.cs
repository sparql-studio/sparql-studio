using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Markers;
using VDS.RDF.Parsing;

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
