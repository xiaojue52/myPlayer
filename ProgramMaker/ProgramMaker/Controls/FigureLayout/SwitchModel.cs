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
namespace ProgramMaker.Controls.FigureLayout
{
    public class SwitchModel
    {
        public static void Select_model(object sender, RoutedEventArgs e, MainWindow window)
        {
            MenuItem mi = sender as MenuItem;
            switch (mi.Tag as string)
            {
                case "scene":
                    window.Maker.Visibility = Visibility.Visible;
                    window.Maker.Header = "_场景制作";
                    (window.Maker.Items[0] as MenuItem).Header = "_新建场景";
                    (window.Maker.Items[1] as MenuItem).Header = "_读取场景";
                    (window.Maker.Items[2] as MenuItem).Header = "_保存场景";
                    (window.Maker.Items[3] as MenuItem).Header = "_场景另存为";

                    (window.Maker.Items[0] as MenuItem).Tag = "_新建场景";
                    (window.Maker.Items[1] as MenuItem).Tag = "_读取场景";
                    (window.Maker.Items[2] as MenuItem).Tag = "_保存场景";
                    (window.Maker.Items[3] as MenuItem).Tag = "_场景另存为";

                    window.ProgramPanel.Visibility = Visibility.Collapsed;
                    window.ScenePanel.Visibility = Visibility.Visible;
                    //this.ScenePanel.ActualWidth = this.ActualWidth;
                    //MessageBox.Show("scene item clicked");
                    break;
                case "program":
                    window.Maker.Visibility = Visibility.Collapsed;
                    window.Maker.Header = "_节目制作";
                    (window.Maker.Items[0] as MenuItem).Header = "_新建节目";
                    (window.Maker.Items[1] as MenuItem).Header = "_读取节目";
                    (window.Maker.Items[2] as MenuItem).Header = "_保存节目";
                    (window.Maker.Items[3] as MenuItem).Header = "_节目另存为";

                    (window.Maker.Items[0] as MenuItem).Tag = "_新建节目";
                    (window.Maker.Items[1] as MenuItem).Tag = "_读取节目";
                    (window.Maker.Items[2] as MenuItem).Tag = "_保存节目";
                    (window.Maker.Items[3] as MenuItem).Tag = "_节目另存为";
                    window.ProgramPanel.Visibility = Visibility.Visible;
                    window.ScenePanel.Visibility = Visibility.Collapsed;
                    //MessageBox.Show("program item clicked");

                    HandleProgram.updateProgramPageSceneItem(window);
                        
                    break;
            }

        }
        public static void HandleEvent(object sender,MainWindow window)
        {
            MenuItem item = sender as MenuItem;
            string tag = item.Tag as string;
            switch (tag)
            {
                case "_新建场景":
                    HandleProgram.newScene(window);
                    break;
                case "_读取场景":
                    HandleProgram.readScene(window);
                    break;
                case "_保存场景":
                    HandleProgram.saveFile(window, tag);
                    break;
                case "_场景另存为":
                    HandleProgram.saveFile(window,tag);
                    break;
                case "_新建节目":
                    HandleProgram.newPrograme(window);
                    break;
                case "_读取节目":
                    HandleProgram.readPrograme(window);
                    break;
                case "_保存节目":
                    HandleProgram.saveFile(window, tag);
                    break;
                case "_节目另存为":
                    HandleProgram.saveFile(window, tag);
                    break;

            }
            
        }
       
    }
}
