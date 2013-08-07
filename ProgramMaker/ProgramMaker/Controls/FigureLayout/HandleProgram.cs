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
using System.IO.Compression;
using System.IO;

namespace ProgramMaker.Controls.FigureLayout
{
    public class HandleProgram
    {
        public static void saveFile(MainWindow window,string tag)
        {
            window.prepareEnv();
            switch(tag)
            {
                case "_保存场景":
                    saveScene(window);
                    break;
                case "_场景另存为":
                    saveSceneTo(window);
                    break;
                case "_保存节目":
                    savePrograme(window);
                    break;
                case "_节目另存为":
                    saveProgrameTo(window);
                    break;
            }
            
            
        }
        private static void saveSceneTo(MainWindow window)
        {
            
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dlg.InitialDirectory = appPath + "\\scene";
            dlg.DefaultExt = ".Sce";
            dlg.Filter = "场景文件(*.Sce)|*.Sce";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                HandleSources.saveScene(window.myContainer, filename);
                MyListBoxItem lbi = new MyListBoxItem();
                lbi.DataContext = dlg.FileName;
                
                lbi.resolution = window.myContainer.Width + "," + window.myContainer.Height;
                lbi.Content = dlg.SafeFileName.Substring(0, dlg.SafeFileName.Length - 4) + "{" + lbi.resolution + "}";
                window.scenes.Items.Add(lbi);
                //window.scenesArray.Add(dlg.FileName);
                window.scenes.SelectedIndex = window.scenes.Items.Count - 1;
            }
        }
        private static void saveScene(MainWindow window)
        {
            if (window.scenes.SelectedIndex == -1)
            {
                saveSceneTo(window);
            }
            else
            {
                string filename = (window.scenes.SelectedItem as MyListBoxItem).DataContext as string;
                (window.scenes.SelectedItem as MyListBoxItem).resolution = window.myContainer.Width + "," + window.myContainer.Height;
                HandleSources.saveScene(window.myContainer, filename);
            }
        }
        public static void newScene(MainWindow window)
        {
            window.scenes.SelectedIndex = -1;
            window.myContainer.resetContainer();
        }
        public static void deleteScene(ListBox scenes)
        {
            if (scenes.SelectedItem!=null)
            {
                //MessageBox.Show("_删除");
                int index = scenes.SelectedIndex;
                //scenesArray.RemoveAt(index);
                scenes.Items.RemoveAt(index);  
            } 
            
        }
        
        public static void readScene(MainWindow window)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = appPath + "\\scene";
            dlg.DefaultExt = ".Sce";
            dlg.Filter = "场景文件(*.Sce)|*.Sce";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                //HandleSources.saveScene(window.myContainer, filename);
                int count = window.scenes.Items.Count;
                MyListBoxItem lbi = new MyListBoxItem();
                XmlDocument doc = HandleSources.readXMLDoc(dlg.FileName);
                if (doc == null) return;
                XmlNode resolution = doc.GetElementsByTagName("resolution")[0];
                lbi.resolution = resolution.InnerText;
                lbi.Content = dlg.SafeFileName.Substring(0, dlg.SafeFileName.Length - 4)+"{"+lbi.resolution+"}";
                lbi.DataContext = dlg.FileName;
                
                window.scenes.Items.Add(lbi);
                //window.scenesArray.Add(dlg.FileName);
                window.scenes.SelectedIndex = window.scenes.Items.Count - 1;
            }
        }
        public static void updateProgramPageSceneItem(MainWindow window)
        {
            window.SceneItems.Items.Clear();
            for (int i = 0; i < window.scenes.Items.Count; i++)
            {
                MyListBoxItem lbi = new MyListBoxItem();
                lbi.DataContext = (window.scenes.Items[i] as MyListBoxItem).DataContext;
                lbi.Content = (window.scenes.Items[i] as MyListBoxItem).Content;
                lbi.resolution = (window.scenes.Items[i] as MyListBoxItem).resolution;
                window.SceneItems.Items.Add(lbi);
            }
        }
        public static void newPrograme(MainWindow window)
        {
            MessageBox.Show("newPrograme");
        }
        public static void readPrograme(MainWindow window)
        {
            MessageBox.Show("readPrograme");
        }
        public static void savePrograme(MainWindow window)
        {
            MessageBox.Show("savePrograme");
        }
        public static void saveProgrameTo(MainWindow window)
        {
            MessageBox.Show("saveProgrameTo");
        }
        public static void addScenesToPrograme(MainWindow window)
        {
            if (window.SceneItems.SelectedIndex != -1)
            {
                MyListBoxItem lbi = new MyListBoxItem();
                lbi.Content = (window.SceneItems.SelectedItem as MyListBoxItem).Content;
                lbi.DataContext = (window.SceneItems.SelectedItem as MyListBoxItem).DataContext;
                lbi.resolution = (window.SceneItems.SelectedItem as MyListBoxItem).resolution;
                if (window.ProgrameSceneList.Items.Count < 1)
                {
                    window.ProgrameSceneList.Items.Add(lbi);
                }
                else
                {
                    string resolution = (window.ProgrameSceneList.Items[0] as MyListBoxItem).resolution;
                    if(lbi.resolution!=resolution){
                        MessageBox.Show("分辨率不同！");
                        return;
                    }
                    window.ProgrameSceneList.Items.Add(lbi);
                }
                
            }
        }
        public static void removeScenesFromprograme(MainWindow window)
        {
            if (window.ProgrameSceneList.SelectedIndex != -1)
            {
                window.ProgrameSceneList.Items.RemoveAt(window.ProgrameSceneList.SelectedIndex);
            }
        }
        public static void generatePrograme(MainWindow window)
        {
            if (window.ProgrameSceneList.Items.Count < 1)
            {
                MessageBox.Show("_请添加场景！");
                return;
            }
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pro";
            dlg.Filter = "节目包(*.pro)|*.pro";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            List<string> scenesArray = new List<string>();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                for (int i = 0; i < window.ProgrameSceneList.Items.Count;i++ )
                {
                    string path = (window.ProgrameSceneList.Items[i] as ListBoxItem).DataContext as string;
                    scenesArray.Add(path);
                }
                string filename = dlg.FileName;
                savePrograme(scenesArray,filename);
                MyListBoxItem lbi = new MyListBoxItem();
                lbi.Content = dlg.SafeFileName.Substring(0, dlg.SafeFileName.Length-4);
                window.ProgrameList.Items.Add(lbi);
            }
        }
        private static void savePrograme(List<string> scenesArray, string filename)
        {
            string zipFolder = filename.Substring(0, filename.Length - 4) + "~";
            string sourceFolder = zipFolder + "\\source";

            bool zipFolderExists = System.IO.Directory.Exists(zipFolder);
            if (!zipFolderExists)
                System.IO.Directory.CreateDirectory(zipFolder);
            bool sourceFolderIsExists = System.IO.Directory.Exists(sourceFolder);
            if (!sourceFolderIsExists)
                System.IO.Directory.CreateDirectory(sourceFolder);

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode programeElement = doc.CreateElement("programeElement");
            for (int i = 0; i < scenesArray.Count; i++)
            {
                XmlNode element = doc.CreateElement("scene" + i);

                if (!HandleSources.copyFileToZipFolder(scenesArray[i], zipFolder)) continue;
                string[] filenames = scenesArray[i].Split(new Char[] { '\\' });
                string name = filenames[filenames.Length - 1];
                element.InnerText = name;
                programeElement.AppendChild(element);
            }

            doc.AppendChild(programeElement);
            doc.Save(zipFolder + "\\scenes.xml");
            FileInfo file = new FileInfo(filename);
            file.Delete();
            ZipFile.CreateFromDirectory(zipFolder, filename);
            DirectoryInfo dir = new DirectoryInfo(zipFolder);
            dir.Delete(true);
            //System.IO.Directory.Delete(zipFolder);
        }
        public static void clearProgrameSceneList(MainWindow window)
        {
            window.ProgrameSceneList.Items.Clear();
        }
    }
}
