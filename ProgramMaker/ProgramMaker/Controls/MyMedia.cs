using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using ProgramMaker.Controls.FigureLayout;

namespace ProgramMaker.Controls
{
    public class MyMedia : MyBaseControl

    {
        public MediaElement media;
        //public Border border;
        //private RotateTransform rotateTransform;
        private List<Stretch> StretchModel = new List<Stretch>();
        public void initLayout(LayoutData layoutData)
        {
            this.layoutData = layoutData;
            this.Width = layoutData.width;
            this.Height = layoutData.height;
            Canvas.SetTop(this, layoutData.canvasTop);
            Canvas.SetLeft(this, layoutData.canvasLeft);
            this.setContentSource(layoutData.sourcePath);
            this.rotateTransform.Angle = layoutData.angle;
            this.RenderTransform = rotateTransform;
            updateTextLayout();
            setMediaModel(layoutData.stretchModel);
        }
        public void updateLayoutDate()
        {
            updateTextLayout();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            layoutData.canvasLeft = Canvas.GetLeft(this);
            layoutData.canvasTop = Canvas.GetTop(this);
            rotateTransform = (this.RenderTransform as RotateTransform);
            if (rotateTransform == null)
            {
                layoutData.angle = 0;
            }
            else
               layoutData.angle = rotateTransform.Angle;
        }
        public MyMedia()
        {
            StretchModel.Add(Stretch.Fill);
            StretchModel.Add(Stretch.Uniform);
            StretchModel.Add(Stretch.None);

            rotateTransform = new RotateTransform();
            layoutData = new LayoutData();
            this.Width = this.MinWidth;
            this.Height = this.MinHeight;
            media = new MediaElement();
            border = new Border();
            border.Background = Brushes.Black;
            media.Height = this.Height;
            media.Width = this.Width;
            media.Stretch = Stretch.Fill;
            this.border.Child = media;
            Tag = "Media";
            layoutData.tag = "Media";
            layoutData.text = "";
            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = 0;
            layoutData.backgroundColor = 0;
            layoutData.textSize = 0;
            layoutData.textColor = 0;
            layoutData.textBackground = 0;
            this.AddChild(this.border);
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(Control_MouseDown), true);
        }
        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);

            this.Focus();
            MainWindow mWindow = App.Current.MainWindow as MainWindow;
            AttributeLayout.SetAttrTree(this, mWindow);
        }
        public void setContentSource(String mediaPath)
        {
            bool sourceFolderIsExists = System.IO.File.Exists(mediaPath);
            if (!sourceFolderIsExists)
            {
                media.Source = null;
                return; 
            }

            media.Source = new Uri(mediaPath, UriKind.RelativeOrAbsolute);
            layoutData.sourcePath = mediaPath;
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
       protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
       {
           base.OnKeyDown(e);
           if (IsShowResizeDecorator && (e.Key == Key.Delete))
           {
               Canvas test = this.Parent as Canvas;
               test.Children.Remove(this);
               this.border = null;
               this.media = null;
           }

       }
		public bool IsShowResizeDecorator {
			get { return (bool)GetValue(IsShowResizeDecoratorProperty); }
			set { SetValue(IsShowResizeDecoratorProperty, value); }
		}

		public static readonly DependencyProperty IsShowResizeDecoratorProperty =
            DependencyProperty.Register("IsShowResizeDecorator", typeof(bool), typeof(MyMedia));


        //my method
        private void updateTextLayout()
        {
            media.Width = this.Width;
            media.Height = this.Height;
        }

        public void setMediaModel(int index)
        {
            if (StretchModel.Count > 0)
            {
                media.Stretch = StretchModel[index];
                this.layoutData.stretchModel = index;
            }

        }
    }
}
