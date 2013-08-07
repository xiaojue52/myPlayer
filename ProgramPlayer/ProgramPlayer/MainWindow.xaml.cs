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
using System.IO.Compression;
using System.IO;

namespace ProgramPlayer {

	public partial class MainWindow : Window {
        public MyBaseControl currentControl;
		public MainWindow() {
			InitializeComponent();
		}

        private void PlayerButton_Event(object sender, RoutedEventArgs e)
        {
            string path = this.getScensXml();
            if (path == "nofile") return;
            PlayerWindow playerWindow = new PlayerWindow(path);
            playerWindow.Background = Brushes.Black;
            playerWindow.WindowStyle = WindowStyle.None;
            playerWindow.WindowState = WindowState.Maximized;
            playerWindow.Show();
        }
        private string getScensXml()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".pro";
            dlg.Filter = "节目包(*.pro)|*.pro";
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result==true)
            {
                string dir = appPath + "\\temp\\" + dlg.SafeFileName.Substring(0, dlg.SafeFileName.Length - 4);
                bool directoryeExists = System.IO.Directory.Exists(dir);
                if (directoryeExists)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                    directoryInfo.Delete(true);
                }
                    
                ZipFile.ExtractToDirectory(dlg.FileName,dir);
                bool fileExists = System.IO.File.Exists(dir + "\\scenes.xml");
                if (fileExists)
                    return dir + "\\scenes.xml";
                else
                    return "nofile";
            }
            return "nofile";
        }
	}
}
