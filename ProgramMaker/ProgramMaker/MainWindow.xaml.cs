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

namespace ProgramMaker {

	public partial class MainWindow : Window {
        //public List<String> scenesArray = new List<String>();
        public MyBaseControl currentControl;
        private Window preViewWindow;
		public MainWindow() {
			InitializeComponent();
            prepareEnv();
            AttributeLayout.InitSceneAttributeLayout(this, myContainer);
		}
		private void Canvas_Drop(object sender, DragEventArgs e) {
			string tag = e.Data.GetData("Tag") as string;
            CreateLayout.CreateControlByTag(tag, e,this.myContainer,this);
		}
       
        private void previewButton_Click(object sender, RoutedEventArgs e)
        {
            MyContainer previewCanvas = CreateLayout.previewCanvasContent(this.myContainer);

            preViewWindow = new Window();
            preViewWindow.Background = Brushes.Black;
            preViewWindow.Content = previewCanvas;
            
            //window.WindowStyle = WindowStyle.None;
            preViewWindow.WindowState = WindowState.Maximized;
            preViewWindow.Show();
            //window.ShowDialog();
            setTimer(this.myContainer.Timer);
        }
        private void SwitchModel_Event(object sender, RoutedEventArgs e)
        {
            SwitchModel.HandleEvent(sender,this);          
        }
        
        private void CanvasTextBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateLayoutData.setCanvasAttrbute(sender, this.myContainer);
        }
        private void LayoutTextBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateLayoutData.setLayoutAttrbute(sender, currentControl);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
        /*
        private void ScenePanel_MouseLeftButtonDownEvent(object sender, System.EventArgs e)
        {
            if (this.SceneHeight.Text == "" || this.SceneWidth.Text=="" || this.SceneTime.Text=="")
            {
                //MessageBox.Show("格式错误！");
                return;
            }
            if (!this.SceneHeight.IsFocused && !this.SceneWidth.IsFocused&&!this.SceneTime.IsFocused)
            {
                //MessageBox.Show("格式错误！");
                return;
            }
            this.myContainer.Height = Convert.ToDouble(this.SceneHeight.Text);
            this.myContainer.Width = Convert.ToDouble(this.SceneWidth.Text);
            this.myContainer.Timer = Convert.ToInt32(this.SceneTime.Text);
            UpdateLayoutData.UpdateCanvasLayoutDate(this.myContainer);
        }*/
        private void SourceButton_Event(object sender, RoutedEventArgs e)
        {
            HandleSources.readSource(sender, this);
        }
        private void TextContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tag = (sender as TextBox).Tag as string;
            switch (tag) {
                case "TextContent":
                    UpdateLayoutData.setControlSource(currentControl, this.TextContent.Text);
                    break;
                case "TextSize":
                    if (currentControl == null) return;
                    MyText myText = currentControl as MyText;
                    if (this.TextSize.Text == "" || !this.TextSize.IsFocused) return;
                    myText.setTextChildAttr(Convert.ToInt32(this.TextSize.Text), this.TextColor.SelectedIndex, this.TextBackground.SelectedIndex);
                    break;
            } 
            
        }
        
        public void prepareEnv()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            bool sourceFolderIsExists = System.IO.Directory.Exists(appPath+"\\source");
            bool sceneFolderIsExists = System.IO.Directory.Exists(appPath + "\\scene");
            if (!sourceFolderIsExists)
                System.IO.Directory.CreateDirectory(appPath + "\\source");
            if (!sceneFolderIsExists)
                System.IO.Directory.CreateDirectory(appPath + "\\scene");
        }

        private void Scenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            int index;
            if (listBox.Items.Count<1)
            {
                return;
            }
            if (listBox.SelectedItem==null)
            {
                index = 0;
            }
            else
                index = listBox.SelectedIndex;
            string scenePath = (listBox.Items[index] as ListBoxItem).DataContext as string;
            CreateLayout.showScene(this.myContainer, scenePath);
            AttributeLayout.InitSceneAttributeLayout(this,myContainer);
        }

        private void Select_model(object sender, RoutedEventArgs e)
        {
            SwitchModel.Select_model(sender,e,this);          
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ComboBox comboBox = sender as ComboBox;
            if (!IsLoaded || !comboBox.IsDropDownOpen)
            {
                return;
            }
            //if (!comboBox.IsFocused) return;
            string tag = comboBox.Tag as string;
            int index = comboBox.SelectedIndex;

            switch (tag)
            {
                case "ImageSwitch":
                    this.myContainer.setSceneSwitch("ImageSwitch",index);
                    break;
                case "SceneColor":
                    this.myContainer.setSceneSwitch("SceneColor",index);
                    break;
                case "ImageModel":
                    if (currentControl!=null)
                    {
                        MyImage myImage = currentControl as MyImage;
                        myImage.setImageModel(index);
                    }
                    break;
                case "MediaModel":
                    if (currentControl != null)
                    {
                        MyMedia myMedia = currentControl as MyMedia;
                        myMedia.setMediaModel(index);
                    }
                    break;
                case "TextBackground":
                    if (currentControl != null)
                    {
                        MyText myText = currentControl as MyText;
                        myText.setTextChildAttr(Convert.ToInt32(this.TextSize.Text), this.TextColor.SelectedIndex, this.TextBackground.SelectedIndex);
                    }
                    break;
                case "TextColor":
                    if (currentControl != null)
                    {
                        MyText myText = currentControl as MyText;
                        myText.setTextChildAttr(Convert.ToInt32(this.TextSize.Text), this.TextColor.SelectedIndex, this.TextBackground.SelectedIndex);
                    }
                    break;
            }
        }
        private Timer aTimer;
        public void setTimer(int time)
        {
            if (time==0)
            {
                return;
            }
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Interval = time*1000;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //MessageBox.Show("test");
            preViewWindow.Dispatcher.BeginInvoke(new Action(()=> preViewWindow.Close()));
            aTimer.Enabled = false;
        }
        private void DeleteScene_Event(object sender, RoutedEventArgs e)
        {
            HandleProgram.deleteScene(this.scenes);
        }
        private void ProgramePage_ButtonEvent(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("buttonClick");
            Button btn = sender as Button;
            string tag = btn.Tag as string;
            switch (tag)
            {
                case "AddScene":
                    HandleProgram.addScenesToPrograme(this);
                    break;
                case "RemoveScene":
                    HandleProgram.removeScenesFromprograme(this);
                    break;
                case "GeneratePrograme":
                    HandleProgram.generatePrograme(this);
                    break;
                case "ClearProgrameSceneList":
                    HandleProgram.clearProgrameSceneList(this);
                    break;
                    
            }
        }
	}
}
