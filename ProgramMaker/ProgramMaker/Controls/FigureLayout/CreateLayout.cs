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
using ProgramMaker.Controls;
using ProgramMaker.Controls.FigureLayout;
using System.Timers;
using System.Xml;
using System.IO;

namespace ProgramMaker.Controls.FigureLayout
{
    public class CreateLayout
    {
        public static MyContainer previewCanvasContent(MyContainer myContainer)
        {
            MyContainer previewCanvas = new MyContainer();
            previewCanvas.initLayout(myContainer.layoutData);
            int count = myContainer.Children.Count;
            for (int i = 0; i < count; i++)
            {
                ContentControl temp = myContainer.Children[i] as ContentControl;
                switch (temp.Tag as string)
                {
                    case "Text":
                        MyText text = new MyText();
                        MyText tempText = (MyText)temp;
                        text.Tag = "preview";
                        text.initLayout(tempText.layoutData);
                        previewCanvas.Children.Add(text);
                        //AttributeLayout.SetAttrTree(tag, this);
                        break;
                    case "Image":
                        MyImage image = new MyImage();
                        MyImage tempImage = (MyImage)temp;
                        image.Tag = "preview";
                        image.initLayout(tempImage.layoutData);
                        previewCanvas.Children.Add(image);
                        //AttributeLayout.SetAttrTree(tag, this);
                        break;
                    case "Media":
                        MyMedia media = new MyMedia();
                        MyMedia tempMedia = (MyMedia)temp;
                        media.Tag = "preview";
                        media.initLayout(tempMedia.layoutData);
                        previewCanvas.Children.Add(media);
                        //AttributeLayout.SetAttrTree(tag, this);
                        break;
                    case "Office":
                        MyOffice myOffice = new MyOffice();
                        MyOffice tempOffice = (MyOffice)temp;
                        myOffice.Tag = "preview";
                        myOffice.initLayout(tempOffice.layoutData);
                        previewCanvas.Children.Add(myOffice);
                        //AttributeLayout.SetAttrTree(tag, this);
                        break;
                }
            }
            return previewCanvas;
        }
        public static void CreateControlByTag(string tag, DragEventArgs e, MyContainer canvas, MainWindow mWindow)
        {
            Point position = e.GetPosition(canvas);
            //MyContainer canvas = new MyContainer();
            switch (tag)
            {
                case "Text":
                    MyText myText = new MyText();
                    Canvas.SetLeft(myText, Math.Max(0, position.X - myText.Width / 2));
                    Canvas.SetTop(myText, Math.Max(0, position.Y - myText.Height / 2));
                    canvas.Children.Add(myText);
                    myText.updateLayoutDate();
                    AttributeLayout.SetAttrTree(myText, mWindow);
                    break;
                case "Media":
                    MyMedia myMedia = new MyMedia();
                    Canvas.SetLeft(myMedia, Math.Max(0, position.X - myMedia.Width / 2));
                    Canvas.SetTop(myMedia, Math.Max(0, position.Y - myMedia.Height / 2));
                    canvas.Children.Add(myMedia);
                    myMedia.updateLayoutDate();
                    AttributeLayout.SetAttrTree(myMedia, mWindow);
                    break;
                case "Image":
                    MyImage myImage = new MyImage();
                    Canvas.SetLeft(myImage, Math.Max(0, position.X - myImage.Width / 2));
                    Canvas.SetTop(myImage, Math.Max(0, position.Y - myImage.Height / 2));
                    canvas.Children.Add(myImage);
                    myImage.updateLayoutDate();
                    AttributeLayout.SetAttrTree(myImage, mWindow);
                    break;
                case "Word":
                    MyOffice myOffice = new MyOffice();
                    Canvas.SetLeft(myOffice, Math.Max(0, position.X - myOffice.Width / 2));
                    Canvas.SetTop(myOffice, Math.Max(0, position.Y - myOffice.Height / 2));
                    canvas.Children.Add(myOffice);
                    myOffice.updateLayoutDate();
                    AttributeLayout.SetAttrTree(myOffice, mWindow);
                    break;
            }
            //return canvas;
        }
        public static void showScene(MyContainer myContainer, string path)
        {
            myContainer.Children.Clear();
            XmlDocument doc = HandleSources.readXMLDoc(path);
            if (doc==null)
            {
                return;
            }
            XmlNode scenePage = doc.GetElementsByTagName("scenePage")[0];
            XmlNode canvas = scenePage.ChildNodes[0];
            LayoutData layoutData = new LayoutData();

            layoutData.backgroundColor = Convert.ToInt32(canvas.Attributes[11].Value);
            layoutData.sceneTime = Convert.ToInt32(canvas.Attributes[10].Value);
            layoutData.stretchModel = Convert.ToInt32(canvas.Attributes[9].Value);
            layoutData.imageSwitch = Convert.ToInt32(canvas.Attributes[8].Value);
            layoutData.canvasTop = double.Parse(canvas.Attributes[7].Value);
            layoutData.canvasLeft = double.Parse(canvas.Attributes[6].Value);
            layoutData.text = canvas.Attributes[5].Value;
            layoutData.angle = double.Parse(canvas.Attributes[4].Value);
            layoutData.sourcePath = canvas.Attributes[3].Value;
            layoutData.width = double.Parse(canvas.Attributes[2].Value);
            layoutData.height = double.Parse(canvas.Attributes[1].Value);
            layoutData.tag = canvas.Attributes[0].Value;

            myContainer.initLayout(layoutData);
            createChild(canvas, myContainer);

        }
        private static void createChild(XmlNode canvas, MyContainer myContainer)
        {

            int count = canvas.ChildNodes.Count;
            for (int i = 0; i < count; i++)
            {
                LayoutData layoutData = new LayoutData();
                XmlNode element = canvas.ChildNodes.Item(i);
                layoutData.textBackground = Convert.ToInt32(element.Attributes[14].Value);
                layoutData.textColor = Convert.ToInt32(element.Attributes[13].Value);
                layoutData.textSize = Convert.ToInt32(element.Attributes[12].Value);
                layoutData.backgroundColor = Convert.ToInt32(element.Attributes[11].Value);
                layoutData.sceneTime = Convert.ToInt32(element.Attributes[10].Value);
                layoutData.stretchModel = Convert.ToInt32(element.Attributes[9].Value);
                layoutData.imageSwitch = Convert.ToInt32(element.Attributes[8].Value);
                layoutData.canvasTop = double.Parse(element.Attributes[7].Value);
                layoutData.canvasLeft = double.Parse(element.Attributes[6].Value);
                layoutData.text = element.Attributes[5].Value;
                layoutData.angle = double.Parse(element.Attributes[4].Value);
                layoutData.sourcePath = element.Attributes[3].Value;
                layoutData.width = double.Parse(element.Attributes[2].Value);
                layoutData.height = double.Parse(element.Attributes[1].Value);
                layoutData.tag = element.Attributes[0].Value;
                switch (layoutData.tag)
                {
                    case "Text":
                        MyText myText = new MyText();
                        myText.initLayout(layoutData);
                        myContainer.Children.Add(myText);
                        break;
                    case "Media":
                        MyMedia myMedia = new MyMedia();
                        myMedia.initLayout(layoutData);
                        myContainer.Children.Add(myMedia);
                        break;
                    case "Image":
                        MyImage myImage = new MyImage();
                        myImage.initLayout(layoutData);
                        myContainer.Children.Add(myImage);
                        break;
                    case "Office":
                        MyOffice myOffice = new MyOffice();
                        myOffice.initLayout(layoutData);
                        myContainer.Children.Add(myOffice);
                        break;
                }
                layoutData = null;
            }
        } 
    }
}
