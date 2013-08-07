using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProgramPlayer.Controls.FigureLayout;

namespace ProgramPlayer.Controls
{

    public class MyImage : MyBaseControl
    {
        private Border border;
        private Image image;
        private RotateTransform rotateTransform;
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
            setImageModel(layoutData.stretchModel);
        }
        public void updateLayoutDate()
        {
            updateTextLayout();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            //layoutData.text = "";
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
        public MyImage()
        {
            StretchModel.Add(Stretch.Fill);
            StretchModel.Add(Stretch.Uniform);
            StretchModel.Add(Stretch.None);
            //StretchModel.Add(Stretch.UniformToFill);


            rotateTransform = new RotateTransform();
            layoutData = new LayoutData();
            layoutData.tag = "Image";
            layoutData.text = "";
            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = 0;
            layoutData.backgroundColor = 0;
            border = new Border();
            image = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("pack://Application:,,,/Images/Image.png", UriKind.Absolute);
            bi3.EndInit();
            image.Source = bi3;
            image.Stretch = Stretch.Fill;
            this.Width = this.MinWidth;
            this.Height = this.MinHeight;
            this.image.Width = this.Width;
            this.image.Height = this.Height;
            this.border.Child = image;
            this.Tag = "Image";
            this.AddChild(this.border);
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(Control_MouseDown), true);
        }
        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);

            MainWindow mWindow = App.Current.MainWindow as MainWindow;
            //AttributeLayout.SetAttrTree(this, mWindow);
        }
        public void setContentSource(String path)
        {
            bool sourceFolderIsExists = System.IO.File.Exists(path);
            if (!sourceFolderIsExists)
            {
                image.Source = null;
                return; 
            }
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            if (path.Substring(0,6)=="source")
            {
                bi3.UriSource = new Uri(Environment.CurrentDirectory + "\\" + path, UriKind.RelativeOrAbsolute);
            }else
                bi3.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            
            bi3.EndInit();
            image.Source = bi3;
            bi3 = null;
            //image.SetValue(Image.SourceProperty, new Uri(path));
            layoutData.sourcePath = path;
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
            if (IsShowResizeDecorator&&(e.Key == Key.Delete))
            {
                Canvas test = this.Parent as Canvas;
                test.Children.Remove(this);
                this.image = null;
            }

        }
		public bool IsShowResizeDecorator {
			get { return (bool)GetValue(IsShowResizeDecoratorProperty); }
			set { SetValue(IsShowResizeDecoratorProperty, value); }
		}

		public static readonly DependencyProperty IsShowResizeDecoratorProperty =
            DependencyProperty.Register("IsShowResizeDecorator", typeof(bool), typeof(MyImage));

        //my method
        private void updateTextLayout()
        {
            image.Width = this.Width;
            image.Height = this.Height;
        }
        public void setImageModel(int index)
        {
            if (StretchModel.Count>0)
            {
                image.Stretch = StretchModel[index];
                this.layoutData.stretchModel = index;
            }
            
        }
    }
}
