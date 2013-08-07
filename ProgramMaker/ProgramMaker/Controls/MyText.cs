using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ProgramMaker.Controls.FigureLayout;
using System.Windows.Media;

namespace ProgramMaker.Controls {
    public class MyText : MyBaseControl
    {
        private TextBox label;
        //private RotateTransform rotateTransform;
        private List<SolidColorBrush> colors = new List<SolidColorBrush>();
        public void initLayout(LayoutData layoutData)
        {
            this.layoutData = layoutData;
            this.Width = layoutData.width;
            this.Height = layoutData.height;
            Canvas.SetTop(this, layoutData.canvasTop);
            Canvas.SetLeft(this, layoutData.canvasLeft);
            this.setContentSource(layoutData.text);
            this.rotateTransform.Angle = layoutData.angle;
            this.RenderTransform = rotateTransform;
            updateTextLayout();
            setTextChildAttr(layoutData.textSize,layoutData.textColor,layoutData.textBackground);
        }
        public void updateLayoutDate()
        {
            updateTextLayout();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            //layoutData.text = this.Content as string;
            layoutData.canvasLeft = Canvas.GetLeft(this);
            layoutData.canvasTop = Canvas.GetTop(this);
            layoutData.sourcePath = "";
            rotateTransform = (this.RenderTransform as RotateTransform);
            if (rotateTransform == null)
            {
                layoutData.angle = 0;
            }
            else
                layoutData.angle = rotateTransform.Angle;
        }
		protected override void OnMouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e) {
			base.OnMouseDoubleClick(e);
            if ((Tag as string) == "preview") return;
			IsShowResizeDecorator = !IsShowResizeDecorator;
            if (IsShowResizeDecorator)
            {
                this.Focus();
            }
		}
        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);

            MainWindow mWindow = App.Current.MainWindow as MainWindow;
            AttributeLayout.SetAttrTree(this, mWindow);
        }
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (IsShowResizeDecorator && (e.Key == Key.Delete))
            {
                Canvas canvas = this.Parent as Canvas;
                canvas.Children.Remove(this);
            }

        }
        public MyText()
        {
            colors.Add(Brushes.Gray);
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.White);
            colors.Add(Brushes.Yellow);
            colors.Add(Brushes.Black);
            label = new TextBox();
            rotateTransform = new RotateTransform();
            Tag = "Text";
            label.Text = "";
            //label.Background = Brushes.Gray;
            label.FontSize = 16;
            label.Background = Brushes.Gray;
            label.Foreground = Brushes.Black;
            Width = this.MinWidth;
            Height = this.MinHeight;
            label.Width = Width;
            label.MaxHeight = Height;
            label.TextWrapping = TextWrapping.Wrap;
            label.AcceptsTab = true;
            label.AcceptsReturn = true;
            layoutData = new LayoutData();
            layoutData.tag = "Text";
            layoutData.text = "";
            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = 0;
            layoutData.backgroundColor = 0;
            layoutData.textSize = 16;
            layoutData.textColor = 4;
            layoutData.textBackground = 0;
            this.AddChild(label);
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(Control_MouseDown), true);
        }

        public void setContentSource(String content)
        {
            this.label.Text = content;
            layoutData.text = content;
        }
        
		public bool IsShowResizeDecorator {
			get { return (bool)GetValue(IsShowResizeDecoratorProperty); }
			set { SetValue(IsShowResizeDecoratorProperty, value); }
		}

		public static readonly DependencyProperty IsShowResizeDecoratorProperty =
			DependencyProperty.Register("IsShowResizeDecorator", typeof(bool), typeof(MyText));


        //my method
        private void updateTextLayout()
        {
            label.Width = this.Width;
            label.MaxHeight = this.Height;
        }

        public void setTextChildAttr(int fontSize, int fontColor,int background)
        {
            label.Background = colors[background];
            label.FontSize = fontSize;
            label.Foreground = colors[fontColor];
            layoutData.textBackground = background;
            layoutData.textColor = fontColor;
            layoutData.textSize = fontSize;
        }
	}
}
