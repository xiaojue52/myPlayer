using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using ProgramPlayer.Controls.FigureLayout;

namespace ProgramPlayer.Controls
{

    public class MyContainer : Canvas
    {
        public LayoutData layoutData;
        private RotateTransform rotateTransform;
        private List<SolidColorBrush> colors = new List<SolidColorBrush>();
        public int Timer;
        public MyContainer()
        {
            colors.Add(Brushes.Gray);
            colors.Add(Brushes.Blue);
            colors.Add(Brushes.White);
            colors.Add(Brushes.Yellow);
            colors.Add(Brushes.Black);

            Timer = 1;
            this.Background = Brushes.Gray;
            this.Width = 900;
            this.Height = 700;
            layoutData = new LayoutData();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            layoutData.tag = "Canvas";
            layoutData.text = "";

            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = Timer;
            layoutData.backgroundColor = 0;

            rotateTransform = (this.RenderTransform as RotateTransform);
            if (rotateTransform == null) return;
            layoutData.angle = rotateTransform.Angle;
        }
       
        public void setContentSource(String imagePath)
        {
            bool sourceFolderIsExists = System.IO.File.Exists(imagePath);
            if (!sourceFolderIsExists)
            {
                this.Background = colors[layoutData.backgroundColor] ;
                return;
            }
            layoutData.sourcePath = imagePath;
            if (layoutData.imageSwitch == 1)
            {
                return;
            }
            ImageBrush bi = new ImageBrush();
            bi.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            this.Background = bi;
            bi = null;
            
        }
        public void setBackgroundColor(int index)
        {
            if (layoutData.imageSwitch == 0)
            {
                return;
            }
            else
            {
                layoutData.backgroundColor = index;
                this.Background = colors[index];
            }
        }
        public void setSceneSwitch(string tag, int index)
        {
            if (tag == "ImageSwitch")
            {
                layoutData.imageSwitch = index;
            }
            else
                layoutData.backgroundColor = index;
            setContentSource(layoutData.sourcePath);
            setBackgroundColor(layoutData.backgroundColor);
            
        }
        public void initLayout(LayoutData layoutData){
            this.layoutData = layoutData;
            this.setContentSource(layoutData.sourcePath);
            this.Width = layoutData.width;
            this.Height = layoutData.height;
            this.setBackgroundColor(layoutData.backgroundColor);
        }


        //my method
        public void updateLayoutDate()
        {
            layoutData.sceneTime = Timer;
            layoutData.width = this.Width;
            layoutData.height = this.Height;
        }
        public void resetContainer()
        {
            this.Children.Clear();
            Timer = 1;
            this.Background = Brushes.Gray;
            this.Width = 900;
            this.Height = 700;
            layoutData = new LayoutData();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            layoutData.tag = "Canvas";
            layoutData.text = "";
            layoutData.sourcePath = null;

            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = Timer;
            layoutData.backgroundColor = 0;
            MainWindow mWindow = App.Current.MainWindow as MainWindow;
            //AttributeLayout.InitSceneAttributeLayout(mWindow, this);
            rotateTransform = (this.RenderTransform as RotateTransform);
            if (rotateTransform == null) return;
            layoutData.angle = rotateTransform.Angle;
        }
    }
}
