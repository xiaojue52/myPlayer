using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ProgramMaker.Controls;
using ProgramMaker.Controls.FigureLayout;

namespace DiagramDesigner {
	public class MoveThumb : Thumb {
		private RotateTransform rotateTransform;
		private ContentControl designerItem;

		public MoveThumb() {
            //this.BorderBrush = Brushes.Red;
            //this.Background = new SolidColorBrush(Colors.MediumBlue);
			DragStarted += new DragStartedEventHandler(this.MoveThumb_DragStarted);
			DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
		}

		private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e) {
			this.designerItem = DataContext as ContentControl;
            if ((this.designerItem.Tag as string) == "preview") return;
            Canvas canvas = designerItem.Parent as Canvas;
            int count = canvas.Children.Count;
            canvas.Children.Remove(designerItem);

            canvas.Children.Insert(count - 1, this.designerItem);
			if (this.designerItem != null) {
				this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
			}
		}

		private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e) {
            if ((this.designerItem.Tag as string) == "preview") return;
			if (this.designerItem != null) {
				Point dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

				if (this.rotateTransform != null) {
					dragDelta = this.rotateTransform.Transform(dragDelta);
				}

				Canvas.SetLeft(this.designerItem, Canvas.GetLeft(this.designerItem) + dragDelta.X);
				Canvas.SetTop(this.designerItem, Canvas.GetTop(this.designerItem) + dragDelta.Y);
                UpdateLayoutData.UpdateChildLayoutData(this.designerItem);
				System.Console.WriteLine(string.Format("{0},{1}", this.designerItem.GetValue(Canvas.LeftProperty), this.designerItem.GetValue(Canvas.TopProperty)));
			}
            
		}
	}
}
