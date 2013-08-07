using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.IO;
using System.Timers;
namespace ProgramPlayer.Controls.FigureLayout
{
    public class PlayerWindow : Window
    {
        private List<string> scenesList;
        private string rootDir;
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape)
            {
                if (this != null)
                {
                    //deleteTempFolder();
                    this.Close();
                }
            }

        }
        public PlayerWindow(string path)
        {
            //MyContainer myContainer = new MyContainer();
            scenesList = getScenesList(path);
            if (scenesList.Count>0)
            {
                MyContainer myContainer = new MyContainer();
                CreateLayout.showScene(myContainer, scenesList[0]);
                this.Content = myContainer;
                int time = myContainer.layoutData.sceneTime;
                setTimer(time,0);
            }       
            
        }
        private Timer aTimer;
        private void setTimer(int time, int i)
        {
            if (time == 0)
            {
                return;
            }
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler((sender,e)=>OnTimedEvent(sender,e,i));

            aTimer.Interval = time * 1000;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e, int i)
        {
            //MessageBox.Show("test");
            this.Dispatcher.BeginInvoke(new Action(() => this.playNextScene(i)));
            aTimer.Enabled = false;
        }
        private void playNextScene(int i)
        {
            if (scenesList.Count > i + 1)
            {
                MyContainer myContainer = new MyContainer();
                CreateLayout.showScene(myContainer, scenesList[i + 1]);
                this.Content = myContainer;
                int time = myContainer.layoutData.sceneTime;
                setTimer(time, i + 1);
            }
            else
            {
                //deleteTempFolder();
            }
        }
        private void deleteTempFolder()
        {
            //DirectoryInfo dir = new DirectoryInfo(rootDir);

            bool sourceFileIsExists = System.IO.Directory.Exists(rootDir);
            if (!sourceFileIsExists)
                return;
            System.IO.Directory.Delete(rootDir, true);
        }
        private List<string> getScenesList(string path)
        {
            List<string> scenesList = new List<string>();
            string dir = path.Substring(0, path.Length - "\\scenes.xml".Length);
            rootDir = dir;
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
                return scenesList;
            }
            doc.LoadXml(sceneData);
            XmlNode programeElement = doc.GetElementsByTagName("programeElement")[0];
            //string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < programeElement.ChildNodes.Count;i++ )
            {
                XmlNode element = programeElement.ChildNodes[i];
                scenesList.Add(dir + "\\" + element.InnerText);
            }
            return scenesList;
        }
    }
}
