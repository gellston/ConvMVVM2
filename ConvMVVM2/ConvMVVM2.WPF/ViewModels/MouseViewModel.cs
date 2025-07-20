using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConvMVVM2.WPF.ViewModels
{
    public class MouseViewModel
    {

        #region Bubbling Event 
        public event Action<Point> LeftDownEvent = null;
        public event Action<Point> LeftClickEvent = null;
        public event Action<Point> LeftDragEvent = null;

        public event Action<Point> RightDownEvent = null;
        public event Action<Point> RightClickEvent = null;
        public event Action<Point> RightDragEvent = null;

        public event Action<Point> MiddleDownEvent = null;
        public event Action<Point> MiddleClickEvent = null;
        public event Action<Point> MiddleDragEvent = null;

        public event Action<Point> MoveEvent = null;
        public event Action<Point> EnterEvent = null;
        public event Action<Point> LeaveEvent = null;

        public event Action<Point, bool> WheelEvent = null;
        #endregion

        #region Tunneling Event
        public event Action<Point> PreviewLeftDownEvent = null;
        public event Action<Point> PreviewLeftClickEvent = null;
        public event Action<Point> PreviewLeftDragEvent = null;

        public event Action<Point> PreviewRightDownEvent = null;
        public event Action<Point> PreviewRightClickEvent = null;
        public event Action<Point> PreviewRightDragEvent = null;

        public event Action<Point> PreviewMiddleDownEvent = null;
        public event Action<Point> PreviewMiddleClickEvent = null;
        public event Action<Point> PreviewMiddleDragEvent = null;

        public event Action<Point> PreviewMoveEvent = null;

        public event Action<Point, bool> PreviewWheelEvent = null;
        #endregion

        #region Public Functions (Tunneling Event)
        public void RaisePreviewLeftDown(Point pt) => this.PreviewLeftDownEvent?.Invoke(pt);
        public void RaisePreviewLeftClick(Point pt) => this.PreviewLeftClickEvent?.Invoke(pt);
        public void RaisePreviewLeftDrag(Point pt) => this.PreviewLeftDragEvent?.Invoke(pt);

        public void RaisePreviewRightDown(Point pt) => this.PreviewRightDownEvent?.Invoke(pt);
        public void RaisePreviewRightClick(Point pt) => this.PreviewRightClickEvent?.Invoke(pt);
        public void RaisePreviewRightDrag(Point pt) => this.PreviewRightDragEvent?.Invoke(pt);

        public void RaisePreviewMiddleDown(Point pt) => this.PreviewMiddleDownEvent?.Invoke(pt);
        public void RaisePreviewMiddleClick(Point pt) => this.PreviewMiddleClickEvent?.Invoke(pt);
        public void RaisePreviewMiddleDrag(Point pt) => this.PreviewMiddleDragEvent?.Invoke(pt);

        public void RaisePreviewMove(Point pt) => this.PreviewMoveEvent?.Invoke(pt);

        public void RaisePreviewWheel(Point pt, bool isUp) => this.PreviewWheelEvent?.Invoke(pt, isUp);
        #endregion

        #region Public Functions (Bubbling Event)
        public void RaiseLeftDown(Point pt) => this.LeftDownEvent?.Invoke(pt);
        public void RaiseLeftClick(Point pt) => this.LeftClickEvent?.Invoke(pt);
        public void RaiseLeftDrag(Point pt) => this.LeftDragEvent?.Invoke(pt);

        public void RaiseRightDown(Point pt) => this.RightDownEvent?.Invoke(pt);
        public void RaiseRightClick(Point pt) => this.RightClickEvent?.Invoke(pt);
        public void RaiseRightDrag(Point pt) => this.RightDragEvent?.Invoke(pt);

        public void RaiseMiddleDown(Point pt) => this.MiddleDownEvent?.Invoke(pt);
        public void RaiseMiddleClick(Point pt) => this.MiddleClickEvent?.Invoke(pt);
        public void RaiseMiddleDrag(Point pt) => this.MiddleDragEvent?.Invoke(pt);

        public void RaiseMove(Point pt) => this.MoveEvent?.Invoke(pt);
        public void RaiseEnter(Point pt) => this.EnterEvent?.Invoke(pt);
        public void RaiseLeave(Point pt) => this.LeaveEvent?.Invoke(pt);

        public void RaiseWheel(Point pt, bool isUp) => this.WheelEvent?.Invoke(pt, isUp);
        #endregion


    }
}
