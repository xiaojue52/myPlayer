using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ProgramMaker.Controls.FigureLayout;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ProgramMaker.Controls
{
    
    public class MyBaseControl : ContentControl
    {
        public LayoutData layoutData;
        public Border border;
        public RotateTransform rotateTransform;
        public MyBaseControl()
        {
            Style style = Application.Current.FindResource("MyBaseControl") as Style;
            this.Style = style;
        }
    }
}
