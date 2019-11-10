
using System.Windows.Media;

namespace VisualTree
{
    class PainterColor
    {
        public PainterColor( SolidColorBrush background, SolidColorBrush frame, Color text )
        {
            Background = background;
            FrameSelected = frame;
            Text = text;
        }
        
        public SolidColorBrush Background { get; }
        public SolidColorBrush FrameSelected { get; }
        public Color Text { get; }
    }
}

