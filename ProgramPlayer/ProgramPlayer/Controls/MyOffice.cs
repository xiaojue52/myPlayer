using System.Collections.Generic;
using System;
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
using ProgramPlayer.Controls.FigureLayout;
using System.Xml;
using System.IO;
using System.Windows.Xps.Packaging;
using System.IO.Packaging;

namespace ProgramPlayer.Controls
{
    public class MyOffice : MyBaseControl
    {
        private DocumentViewer view;
        public void initLayout(LayoutData layoutData)
        {
            this.layoutData = layoutData;
            this.Width = layoutData.width;
            this.Height = layoutData.height;
            Canvas.SetTop(this, layoutData.canvasTop);
            Canvas.SetLeft(this, layoutData.canvasLeft);
            this.setContentSource(layoutData.sourcePath);
            this.rotateTransform.Angle = layoutData.angle;
            this.RenderTransform = rotateTransform;
            updateTextLayout();
        }
        public void updateLayoutDate()
        {
            updateTextLayout();
            layoutData.width = this.Width;
            layoutData.height = this.Height;
            //layoutData.text = "";
            layoutData.canvasLeft = Canvas.GetLeft(this);
            layoutData.canvasTop = Canvas.GetTop(this);
            rotateTransform = (this.RenderTransform as RotateTransform);
            if (rotateTransform == null)
            {
                layoutData.angle = 0;
            }
            else
                layoutData.angle = rotateTransform.Angle;
        }
        public MyOffice()
        {
            //this.contentPresenter
            rotateTransform = new RotateTransform();
            layoutData = new LayoutData();
            layoutData.tag = "Office";
            layoutData.text = "";
            layoutData.imageSwitch = 0;
            layoutData.stretchModel = 0;
            layoutData.sceneTime = 0;
            layoutData.backgroundColor = 0;
            border = new Border();

            this.Width = this.MinWidth;
            this.Height = this.MinHeight;

            view = new DocumentViewer();
            view.Width = this.Width;
            view.Height = this.Height;

            this.border.Child = view;
            this.Tag = "Office";
            this.AddChild(this.border);
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(Control_MouseDown), true);
        }

        private void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //base.OnMouseDown(e);
            MainWindow mWindow = App.Current.MainWindow as MainWindow;
        }
        public void setContentSource(string path)
        {
            bool sourceFolderIsExists = System.IO.File.Exists(path);
            if (!sourceFolderIsExists)
            {
                return;
            }
            XpsDocument xpsPackage = new XpsDocument(path, System.IO.FileAccess.Read);
            FixedDocumentSequence fixedDocumentSequence = xpsPackage.GetFixedDocumentSequence();

            view.Document = fixedDocumentSequence;
            xpsPackage.Close();
            layoutData.sourcePath = path;
        }
        protected override void OnMouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            //MessageBox.Show("office");
            if ((Tag as string) == "preview") return;
            IsShowResizeDecorator = !IsShowResizeDecorator;
            if (IsShowResizeDecorator)
            {
                this.Focus();
            }
        }
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (IsShowResizeDecorator && (e.Key == Key.Delete))
            {
                Canvas test = this.Parent as Canvas;
                test.Children.Remove(this);
            }

        }
        public bool IsShowResizeDecorator
        {
            get { return (bool)GetValue(IsShowResizeDecoratorProperty); }
            set { SetValue(IsShowResizeDecoratorProperty, value); }
        }

        public static readonly DependencyProperty IsShowResizeDecoratorProperty =
            DependencyProperty.Register("IsShowResizeDecorator", typeof(bool), typeof(MyOffice));

        //my method
        private void updateTextLayout()
        {
            view.Width = this.Width;
            view.Height = this.Height;
        }
        /*
        private XpsDocument ConvertWordDocToXPSDoc(string wordDocName)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            bool sourceFileIsExists = System.IO.File.Exists(wordDocName);
            if (!sourceFileIsExists)
            {
                wordDocName = appPath + "\\" + wordDocName;
                if (!System.IO.File.Exists(wordDocName)) return null;
            }
            string xpsDocName = (new DirectoryInfo(wordDocName)).FullName;
            xpsDocName = xpsDocName.Replace(new FileInfo(xpsDocName).Extension, "") + ".xps";
            Microsoft.Office.Interop.Word.Application
                wordApplication = new Microsoft.Office.Interop.Word.Application();
            wordApplication.DisplayDocumentInformationPanel = false;
            wordApplication.Documents.Add(wordDocName);
            Microsoft.Office.Interop.Word.Document doc = wordApplication.ActiveDocument;
            try
            {
                doc.SaveAs(xpsDocName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXPS);
                wordApplication.Quit();
                XpsDocument xpsDoc = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);

                return xpsDoc;
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
            return null;
        }*/
    }
}
