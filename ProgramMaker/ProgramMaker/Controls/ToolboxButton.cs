using System.Windows.Controls;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

namespace ThemeDesigner.Controls {
	public class ToolboxButton : Button {
		public string Icon {
			get { return (string)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
       /* public string testqqq
        {
            get { return (string)getvalue(iconproperty1111); }
            set { setvalue(iconproperty1111, value); }
        }*/

		protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {
			base.OnPreviewMouseDown(e);
			this.dragStartPoint = new Point?(e.GetPosition(this));
			this.CaptureMouse();
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);

			if (this.IsMouseCaptured) {
				if (e.LeftButton != MouseButtonState.Pressed) {
					this.dragStartPoint = null;
				}
				if (this.dragStartPoint.HasValue) {
					Point position = e.GetPosition(this);
					if ((SystemParameters.MinimumHorizontalDragDistance <= Math.Abs((double)(position.X - this.dragStartPoint.Value.X))) ||
							(SystemParameters.MinimumVerticalDragDistance <= Math.Abs((double)(position.Y - this.dragStartPoint.Value.Y)))) {
						DataObject dataObject = new DataObject("Tag", this.Tag as string);
						DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
					}
					e.Handled = true;
				}
			}
		}

		protected override void OnMouseUp(MouseButtonEventArgs e) {
			base.OnMouseUp(e);
			if (this.IsMouseCaptured) {
				this.ReleaseMouseCapture();
			}
		}

		public static readonly DependencyProperty IconProperty =
			DependencyProperty.Register("Icon", typeof(string), typeof(ToolboxButton));

		private Point? dragStartPoint = null;
	}
}
