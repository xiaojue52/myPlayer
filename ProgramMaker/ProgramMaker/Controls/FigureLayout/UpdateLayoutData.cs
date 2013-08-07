using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramMaker.Controls;
using System.Windows.Controls;

namespace ProgramMaker.Controls.FigureLayout
{
    public class UpdateLayoutData
    {
        public static void UpdateCanvasLayoutDate(MyContainer myContainer)
        {
            myContainer.updateLayoutDate();
        }
        public static void UpdateChildLayoutData(ContentControl item)
        {
            switch (item.Tag as string)
            {
                case "Text":
                    MyText myText = item as MyText;
                    myText.updateLayoutDate();
                    break;
                case "Image":
                    MyImage myImage = item as MyImage;
                    myImage.updateLayoutDate();
                    break;
                case "Media":
                    MyMedia myMedia = item as MyMedia;
                    myMedia.updateLayoutDate();
                    break;
                case "Office":
                    MyOffice myOffice = item as MyOffice;
                    myOffice.updateLayoutDate();
                    break;
            }
        }

        public static void setControlSource(MyBaseControl currentControl, String content)
        {
            string tag = currentControl.Tag as string;
            switch (tag)
            {
                case "Text":
                    MyText myText = currentControl as MyText;
                    myText.setContentSource(content);
            	    break;
                case "Image":
                    MyImage myImage = currentControl as MyImage;
                    myImage.setContentSource(content);
            	    break;
                case "Media":
                    MyMedia myMedia = currentControl as MyMedia;
                    myMedia.setContentSource(content);
                    break;
                case "Office":
                    MyOffice myOffice = currentControl as MyOffice;
                    myOffice.setContentSource(content);
                    break;
            }
        }
        public static void setCanvasAttrbute(object sender,MyContainer  myContainer)
        {
            TextBox txb = sender as TextBox;
            if (!txb.IsFocused) return;
            string tag = txb.Tag as string;
            switch (tag)
            {
                case "SceneHeight":
                    String height = txb.Text;
                    if (height == "") break;
                    myContainer.Height = Convert.ToDouble(height);
                    break;
                case "SceneWidth":
                    String width = txb.Text;
                    if (width == "") break; ;
                    myContainer.Width = Convert.ToDouble(width);
                    break;
                case "SceneTime":
                    String time = txb.Text;
                    if (time == "") break; ;
                    myContainer.Timer = Convert.ToInt32(time);
                    break;

            }
            UpdateCanvasLayoutDate(myContainer);
        }
        public static void setLayoutAttrbute(object sender, MyBaseControl currentControl)
        {
            if (currentControl == null) return;
            TextBox txb = sender as TextBox;
            if (!txb.IsFocused) return;
            string tag = txb.Tag as string;
            switch (tag)
            {
                case "ControlHeight":
                    String height = txb.Text;
                    if (height == "") break;
                    currentControl.Height = Convert.ToDouble(height);
                    break;
                case "ControlWidth":
                    String width = txb.Text;
                    if (width == "") break; ;
                    currentControl.Width = Convert.ToDouble(width);
                    break;
            }
            UpdateChildLayoutData(currentControl);
        }
    }
}
