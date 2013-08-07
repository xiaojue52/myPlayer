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
using ProgramPlayer.Controls;
using ProgramPlayer.Controls.FigureLayout;
using System.Timers;
using System.Xml;
using System.IO;

namespace ProgramPlayer.Controls.FigureLayout
{
    public class CreateLayout
    {
        public static void showScene(MyContainer myContainer, string path)
        {
            myContainer.Children.Clear();
            string parentDir = "temp\\"+System.IO.Directory.GetParent(path).Name;
            
            XmlDocument doc = new XmlDocument();
            string sceneData = "";
            try
            {
                StreamReader sr = new StreamReader(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sceneData += line;
                }
                sr.Close();
            }
            catch (System.Exception ex)
            {
                return;
            }
            doc.LoadXml(sceneData);
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
            layoutData.sourcePath = parentDir+"\\"+canvas.Attributes[3].Value;
            layoutData.width = double.Parse(canvas.Attributes[2].Value);
            layoutData.height = double.Parse(canvas.Attributes[1].Value);
            layoutData.tag = canvas.Attributes[0].Value;

            myContainer.initLayout(layoutData);
            createChild(canvas, myContainer,parentDir);

        }
        private static void createChild(XmlNode canvas, MyContainer myContainer,string parentDir)
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
                layoutData.sourcePath = parentDir +"\\"+ element.Attributes[3].Value;
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
