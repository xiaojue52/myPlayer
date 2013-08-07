using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramMaker.Controls.FigureLayout
{
    public class AttributeLayout
    {
        public static void InitSceneAttributeLayout(MainWindow window, MyContainer myContainer)
        {
            LayoutData layoutData = myContainer.layoutData;
            window.SceneTime.Text = layoutData.sceneTime.ToString();
            window.SceneWidth.Text = layoutData.width.ToString();
            window.SceneHeight.Text = layoutData.height.ToString();
            window.ImageSwitch.SelectedIndex = layoutData.imageSwitch;
            window.SceneColor.SelectedIndex = layoutData.backgroundColor;
            window.SceneTime.Text = layoutData.sceneTime.ToString();
        }   
        private static void showTextAttrGird(MyText myText,MainWindow window)
        {
            window.SourceGrid.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrBg.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrFS.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrFC.Visibility = System.Windows.Visibility.Visible;
            window.ControlWidth.Text = myText.layoutData.width.ToString();
            window.ControlHeight.Text = myText.layoutData.height.ToString();
            window.TextContent.Text = myText.layoutData.text;
            window.TextSize.Text = myText.layoutData.textSize.ToString();

            window.TextColor.SelectedIndex = myText.layoutData.textColor;
            window.TextBackground.SelectedIndex = myText.layoutData.textBackground;
        }
        private static void showImageAttrGrid(MyImage myImage, MainWindow window)
        {
            window.SourceGrid.Visibility = System.Windows.Visibility.Visible;
            window.ImageAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.SourceAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrGrid.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrBg.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFS.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFC.Visibility = System.Windows.Visibility.Collapsed;
            window.ControlWidth.Text = myImage.layoutData.width.ToString();
            window.ControlHeight.Text = myImage.layoutData.height.ToString();
            window.LayoutModel.Tag = "ImageModel";
            window.LayoutModel.SelectedIndex = myImage.layoutData.stretchModel;
            
            window.SourceButton.Tag = "Image";
            window.SourcePath.Text = myImage.layoutData.sourcePath;
        }
        private static void showMediaAttrGrid(MyMedia myMedia, MainWindow window)
        {
            window.SourceGrid.Visibility = System.Windows.Visibility.Visible;
            window.ImageAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.SourceAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrGrid.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrBg.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFS.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFC.Visibility = System.Windows.Visibility.Collapsed;
            window.ControlWidth.Text = myMedia.layoutData.width.ToString();
            window.ControlHeight.Text = myMedia.layoutData.height.ToString();
            window.LayoutModel.Tag = "MediaModel";
            window.LayoutModel.SelectedIndex = myMedia.layoutData.stretchModel;

            window.SourceButton.Tag = "Media";
            window.SourcePath.Text = myMedia.layoutData.sourcePath;
        }
        private static void showOfficeAttrGrid(MyOffice myOffice, MainWindow window)
        {
            window.SourceGrid.Visibility = System.Windows.Visibility.Visible;
            window.ImageAttrGrid.Visibility = System.Windows.Visibility.Collapsed;
            window.SourceAttrGrid.Visibility = System.Windows.Visibility.Visible;
            window.TextAttrGrid.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrBg.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFS.Visibility = System.Windows.Visibility.Collapsed;
            window.TextAttrFC.Visibility = System.Windows.Visibility.Collapsed;
            window.ControlWidth.Text = myOffice.layoutData.width.ToString();
            window.ControlHeight.Text = myOffice.layoutData.height.ToString();

            window.SourceButton.Tag = "Office";
            window.SourcePath.Text = myOffice.layoutData.sourcePath;
        }
        public static void SetAttrTree(MyBaseControl myBaseControl,MainWindow window)
        {
            window.currentControl = myBaseControl;
            string tag = myBaseControl.Tag as string;
            window.ControlAttrTree.Visibility = System.Windows.Visibility.Visible;
            switch (tag)
            {
                case "Text":
                    MyText myText = myBaseControl as MyText;
                    showTextAttrGird(myText,window);
                    break;
                case "Image":
                    MyImage myImage = myBaseControl as MyImage;
                    showImageAttrGrid(myImage,window);
                    break;
                case "Media":
                    MyMedia myMedia = myBaseControl as MyMedia;
                    showMediaAttrGrid(myMedia, window);
                    break;
                case "Office":
                    MyOffice myOffice = myBaseControl as MyOffice;
                    showOfficeAttrGrid(myOffice, window);
                    break;
            }
        }
    }
}
