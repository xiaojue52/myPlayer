using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ProgramMaker.Controls;
using System.Windows.Controls;
using System.Xml;
using System.IO;
using System.IO.Compression;

namespace ProgramMaker.Controls.FigureLayout
{
    public class HandleSources
    {
       public static void saveScene(MyContainer canvas ,string filename)
       {
           string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
           XmlDocument doc = new XmlDocument();
           XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
           doc.AppendChild(docNode);
           XmlNode canvasElement = doc.CreateElement("canvas");
           XmlAttribute canvasTag = doc.CreateAttribute("tag");
           canvasTag.Value = canvas.layoutData.tag;
           canvasElement.Attributes.Append(canvasTag);

           XmlAttribute canvasHeight = doc.CreateAttribute("height");
           canvasHeight.Value = canvas.layoutData.height.ToString();
           canvasElement.Attributes.Append(canvasHeight);

           XmlAttribute canvasWidth = doc.CreateAttribute("width");
           canvasWidth.Value = canvas.layoutData.width.ToString();
           canvasElement.Attributes.Append(canvasWidth);

           XmlAttribute canvasSourcePath = doc.CreateAttribute("sourcePath");
           canvasSourcePath.Value = copyFileToSourceFoler(canvas.layoutData.sourcePath, appPath);
           
           canvasElement.Attributes.Append(canvasSourcePath);

           XmlAttribute canvasRotateTransform = doc.CreateAttribute("rotateTransform");
           canvasRotateTransform.Value = "0";
           canvasElement.Attributes.Append(canvasRotateTransform);

           XmlAttribute canvasText = doc.CreateAttribute("text");
           canvasText.Value = canvas.layoutData.text;
           canvasElement.Attributes.Append(canvasText);

           XmlAttribute canvasLeft = doc.CreateAttribute("canvasLeft");
           canvasLeft.Value = canvas.layoutData.canvasLeft.ToString();
           canvasElement.Attributes.Append(canvasLeft);

           XmlAttribute canvasTop = doc.CreateAttribute("canvasTop");
           canvasTop.Value = canvas.layoutData.canvasTop.ToString();
           canvasElement.Attributes.Append(canvasTop);

           XmlAttribute imageSwitch = doc.CreateAttribute("imageSwitch");
           imageSwitch.Value = canvas.layoutData.imageSwitch.ToString();
           canvasElement.Attributes.Append(imageSwitch);

           XmlAttribute stretchModel = doc.CreateAttribute("stretchModel");
           stretchModel.Value = canvas.layoutData.stretchModel.ToString();
           canvasElement.Attributes.Append(stretchModel);

           XmlAttribute sceneTime = doc.CreateAttribute("sceneTime");
           sceneTime.Value = canvas.layoutData.sceneTime.ToString();
           canvasElement.Attributes.Append(sceneTime);

           XmlAttribute backgroundColor = doc.CreateAttribute("backgroundColor");
           backgroundColor.Value = canvas.layoutData.backgroundColor.ToString();
           canvasElement.Attributes.Append(backgroundColor);


           XmlNode scenePage = doc.CreateElement("scenePage");
           doc.AppendChild(scenePage);
           scenePage.AppendChild(canvasElement);

           /*the source path*/
           XmlNode sourceList = doc.CreateElement("sourceList");
           XmlNode sourceElement = doc.CreateElement("sourceElement");
           sourceElement.InnerText = canvasSourcePath.Value;
           sourceList.AppendChild(sourceElement);
           scenePage.AppendChild(sourceList);

           /*the resolution*/
           XmlNode resolution = doc.CreateElement("resolution");
           resolution.InnerText = canvasWidth.Value + "," + canvasHeight.Value;
           scenePage.AppendChild(resolution);

           appendChildToXml(doc, canvasElement, canvas, sourceList);
           doc.Save(filename);
       }
       private static void appendChildToXml(XmlDocument doc, XmlNode element, MyContainer canvas, XmlNode sourceList)
       {
           string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
           int count = canvas.Children.Count;
           MyBaseControl myBaseControl = null;
           LayoutData layoutData = null;
           for (int i = 0; i < count; i++)
           {
               myBaseControl = canvas.Children[i] as MyBaseControl;
               layoutData = myBaseControl.layoutData;
               
               XmlNode canvasElement = doc.CreateElement(layoutData.tag);
               XmlAttribute canvasTag = doc.CreateAttribute("tag");
               canvasTag.Value = layoutData.tag;
               canvasElement.Attributes.Append(canvasTag);

               XmlAttribute canvasHeight = doc.CreateAttribute("height");
               canvasHeight.Value = layoutData.height.ToString();
               canvasElement.Attributes.Append(canvasHeight);

               XmlAttribute canvasWidth = doc.CreateAttribute("width");
               canvasWidth.Value = layoutData.width.ToString();
               canvasElement.Attributes.Append(canvasWidth);

               XmlAttribute canvasSourcePath = doc.CreateAttribute("sourcePath");
               canvasSourcePath.Value = copyFileToSourceFoler(layoutData.sourcePath, appPath);
               
               canvasElement.Attributes.Append(canvasSourcePath);

               XmlAttribute canvasRotateTransform = doc.CreateAttribute("rotateTransform");
               canvasRotateTransform.Value = layoutData.angle.ToString();
               canvasElement.Attributes.Append(canvasRotateTransform);

               XmlAttribute canvasText = doc.CreateAttribute("text");
               canvasText.Value = layoutData.text;
               canvasElement.Attributes.Append(canvasText);

               XmlAttribute canvasLeft = doc.CreateAttribute("canvasLeft");
               canvasLeft.Value = layoutData.canvasLeft.ToString();
               canvasElement.Attributes.Append(canvasLeft);

               XmlAttribute canvasTop = doc.CreateAttribute("canvasTop");
               canvasTop.Value = layoutData.canvasTop.ToString();
               canvasElement.Attributes.Append(canvasTop);

               XmlAttribute imageSwitch = doc.CreateAttribute("imageSwitch");
               imageSwitch.Value = layoutData.imageSwitch.ToString();
               canvasElement.Attributes.Append(imageSwitch);

               XmlAttribute stretchModel = doc.CreateAttribute("stretchModel");
               stretchModel.Value = layoutData.stretchModel.ToString();
               canvasElement.Attributes.Append(stretchModel);

               XmlAttribute sceneTime = doc.CreateAttribute("sceneTime");
               sceneTime.Value = layoutData.sceneTime.ToString();
               canvasElement.Attributes.Append(sceneTime);

               XmlAttribute backgroundColor = doc.CreateAttribute("backgroundColor");
               backgroundColor.Value = layoutData.backgroundColor.ToString();
               canvasElement.Attributes.Append(backgroundColor);

               XmlAttribute textSize = doc.CreateAttribute("textSize");
               textSize.Value = layoutData.textSize.ToString();
               canvasElement.Attributes.Append(textSize);

               XmlAttribute textColor = doc.CreateAttribute("textColor");
               textColor.Value = layoutData.textColor.ToString();
               canvasElement.Attributes.Append(textColor);

               XmlAttribute textBackground = doc.CreateAttribute("textBackground");
               textBackground.Value = layoutData.textBackground.ToString();
               canvasElement.Attributes.Append(textBackground);

               XmlNode sourceElement = doc.CreateElement("sourceElement");
               sourceElement.InnerText = canvasSourcePath.Value;
               sourceList.AppendChild(sourceElement);
               element.AppendChild(canvasElement);
           }
       }
       private static string copyFileToSourceFoler(string path, string appPath)
       {
           if (path == null || path == "") return "";
           
           bool sourceFileIsExists = System.IO.File.Exists(path);
           string[] filenames = path.Split(new Char[]{'\\'});
           if (filenames.Length < 1) return "";
           string filename = filenames[filenames.Length - 1];
           if (!sourceFileIsExists) return "";
           if (!System.IO.Directory.Exists(appPath+"\\source"))
           {
               System.IO.Directory.CreateDirectory(appPath + "\\source");
           }

           if (path == (appPath + "\\source\\" + filename))
           {
               return path;
           }
           System.IO.File.Copy(path, appPath + "\\source\\"+filename, true);
           return "source\\" + filename;
       }

       public static void readSource(object sender,MainWindow window)
       {
           Button btn = sender as Button;
           string tag = btn.Tag as string;
           // Create OpenFileDialog 
           Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

           // Set filter for file extension and default file extension 
           switch (tag)
           {
               case "CanvasImage":
                   dlg.DefaultExt = ".bmp|.png|.gif|.jpg|.jpeg";
                   dlg.Filter = "图片文件(*.bmp,*.png,*.gif,*.jpg,*.jpeg)|*.bmp;*.png;*.gif;*.jpg;*.jpeg";
                   break;
               case "Image":
                   dlg.DefaultExt = ".bmp|.png|.gif|.jpg|.jpeg";
                   dlg.Filter = "图片文件(*.bmp,*.png,*.gif,*.jpg,*.jpeg)|*.bmp;*.png;*.gif;*.jpg;*.jpeg";
                   break;
               case "Media":
                   dlg.DefaultExt = ".wmv|.mp3|.mp4|.avi";
                   dlg.Filter = "媒体文件(*.wmv,*.mp3,*.mp4,*.avi)|*.wmv;*.mp3;*.mp4;*.avi";
                   break;
               case "Office":
                   dlg.DefaultExt = ".xps";
                   dlg.Filter = "媒体文件(*.xps)|*.xps";
                   break;
           }


           // Display OpenFileDialog by calling ShowDialog method 
           Nullable<bool> result = dlg.ShowDialog();

           // Get the selected file name and display in a TextBox 
           if (result == true)
           {
               // Open document 
               string filename = dlg.FileName;
               //string filePath = 
               switch (tag)
               {
                   case "CanvasImage":

                       window.myContainer.setContentSource(filename);
                       window.Canvas_image.Text = dlg.FileName;
                       break;
                   case "Image":
                       window.SourcePath.Text = dlg.FileName;
                       UpdateLayoutData.setControlSource(window.currentControl, filename);
                       break;
                   case "Media":
                       window.SourcePath.Text = dlg.FileName;
                       UpdateLayoutData.setControlSource(window.currentControl, filename);
                       break;
                   case "Office":
                       window.SourcePath.Text = dlg.FileName;
                       UpdateLayoutData.setControlSource(window.currentControl, filename);
                       break;
               }

           }
       }
      
       public static bool copyFileToZipFolder(string scenePath, string zipFolder)
       {
           bool sourceFileIsExists = System.IO.File.Exists(scenePath);
           if (!sourceFileIsExists)
           {
               return false;
           }
           string[] filenames = scenePath.Split(new Char[]{'\\'});
           if (filenames.Length < 1) return false;
           string filename = filenames[filenames.Length - 1];
           System.IO.File.Copy(scenePath, zipFolder + "\\" + filename, true);
           XmlDocument doc = readXMLDoc(scenePath);
           if (doc == null) return false;
           XmlNode scenePage = doc.GetElementsByTagName("scenePage")[0];
           XmlNode sourceList = scenePage.ChildNodes[1];
           for (int i = 0; i < sourceList.ChildNodes.Count;i++)
           {
               string path = sourceList.ChildNodes[i].InnerText;
               copyFileToSourceFoler(path, zipFolder);
           }
           return true;
       }
       public static XmlDocument readXMLDoc(string path)
       {
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
               return doc;
           }
           doc.LoadXml(sceneData);
           return doc;
       }
    }
}
