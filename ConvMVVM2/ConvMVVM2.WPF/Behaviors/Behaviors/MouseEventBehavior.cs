using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ConvMVVM2.WPF.ViewModels;
using ConvMVVM2.WPF.Behaviors.Base;

/* 'ConvMVVM2.WPF (net9.0-windows)' 프로젝트에서 병합되지 않은 변경 내용
추가됨:
using ConvMVVM2;
using ConvMVVM2.WPF;
using ConvMVVM2.WPF.Behaviors;
using ConvMVVM2.WPF.Behaviors.Behaviors;
*/

namespace ConvMVVM2.WPF.Behaviors.Behaviors
{
    public class MouseEventBehavior : Behavior<UIElement>
    {

        #region Private Property
        private bool IsBubbleCaptured = false;
        private bool IsTunnelCaptured = false;
        private readonly HashSet<MouseButton> BubblingDragButton = new HashSet<MouseButton>();
        private readonly HashSet<MouseButton> TunnlingDragButton = new HashSet<MouseButton>();

        private Window _ownerWindow;
        #endregion

        #region Protected Functions
        protected override void OnAttached()
        {

            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseUp += MouseUp;
            AssociatedObject.MouseMove += MouseMove;

            AssociatedObject.MouseEnter += MouseEnter;
            AssociatedObject.MouseLeave += MouseLeave;
            AssociatedObject.MouseWheel += MouseWheel;



            AssociatedObject.PreviewMouseDown += PreviewMouseDown;
            AssociatedObject.PreviewMouseUp += PreviewMouseUp;
            AssociatedObject.PreviewMouseMove += PreviewMouseMove;
            AssociatedObject.PreviewMouseWheel += PreviewMouseWheel;

            AssociatedObject.LostMouseCapture += AssociatedObject_LostMouseCapture;

            _ownerWindow = Window.GetWindow(this.AssociatedObject);
            if (_ownerWindow != null)
            {
                _ownerWindow.Deactivated += _ownerWindow_Deactivated;
                _ownerWindow.Closed += _ownerWindow_Closed;
            }
   

        }



        protected override void OnDetaching()
        {

            AssociatedObject.MouseDown -= MouseDown;
            AssociatedObject.MouseUp -= MouseUp;
            AssociatedObject.MouseMove -= MouseMove;

            AssociatedObject.MouseEnter -= MouseEnter;
            AssociatedObject.MouseLeave -= MouseLeave;
            AssociatedObject.MouseWheel -= MouseWheel;


            AssociatedObject.PreviewMouseDown -= PreviewMouseDown;
            AssociatedObject.PreviewMouseUp -= PreviewMouseUp;
            AssociatedObject.PreviewMouseMove -= PreviewMouseMove;
            AssociatedObject.PreviewMouseWheel -= PreviewMouseWheel;

            AssociatedObject.LostMouseCapture -= AssociatedObject_LostMouseCapture;

            if (_ownerWindow != null)
            {
                _ownerWindow.Deactivated -= _ownerWindow_Deactivated;
                _ownerWindow.Closed -= _ownerWindow_Closed;
                _ownerWindow = null;
            }

            StopCapture();
        }
        #endregion

        #region Private Functions
        private void StartCapture()
        {
            if (ReferenceEquals(Mouse.Captured, AssociatedObject)) return;
            AssociatedObject.CaptureMouse();
        }
        

        private void StopCapture()
        {
            try
            {
                if (ReferenceEquals(Mouse.Captured, AssociatedObject))
                    AssociatedObject.ReleaseMouseCapture();

            }
            finally
            {
                IsBubbleCaptured = false;
                IsTunnelCaptured = false;
                BubblingDragButton.Clear();
                TunnlingDragButton.Clear();
            }
        }
        #endregion

        #region Dependency Property
        public static DependencyProperty MouseViewModelProperty = DependencyProperty.Register("MouseViewModel", typeof(MouseViewModel), typeof(MouseEventBehavior));
        public MouseViewModel MouseViewModel
        {
            get => (MouseViewModel)GetValue(MouseViewModelProperty);
            set => SetValue(MouseViewModelProperty, value);
        }
        #endregion

        #region Event Handler (Safety Process)
        private void AssociatedObject_LostMouseCapture(object sender, MouseEventArgs e)
        {
            this.StopCapture();

        }

        private void _ownerWindow_Closed(object sender, EventArgs e)
        {
            this.StopCapture();
        }

        private void _ownerWindow_Deactivated(object sender, EventArgs e)
        {
            this.StopCapture();
        }
        #endregion

        #region Event Handler (Tunneling Event)
        private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseViewModel?.RaisePreviewWheel(e.GetPosition(AssociatedObject), e.Delta > 0);

        }

        private void PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (IsTunnelCaptured)
            {
                foreach (var btn in TunnlingDragButton)
                {

                    switch (btn)
                    {
                        case MouseButton.Left:
                            MouseViewModel?.RaisePreviewLeftDrag(e.GetPosition(AssociatedObject));
                            break;

                        case MouseButton.Right:
                            MouseViewModel?.RaisePreviewRightDrag(e.GetPosition(AssociatedObject));
                            break;

                        case MouseButton.Middle:
                            MouseViewModel?.RaisePreviewMiddleDrag(e.GetPosition(AssociatedObject));
                            break;
                    }
                }
            }
            else
            {
                MouseViewModel?.RaisePreviewMove(e.GetPosition(AssociatedObject));
            }
        }

        private void PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsTunnelCaptured == false)
                return;

            if (TunnlingDragButton.Contains(e.ChangedButton) && TunnlingDragButton.Count == 1)
            {
                StopCapture();
            }
            TunnlingDragButton.Remove(e.ChangedButton);


            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseViewModel?.RaisePreviewLeftUp(e.GetPosition(AssociatedObject));    
                    MouseViewModel?.RaisePreviewLeftClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Right:
                    MouseViewModel?.RaisePreviewRightUp(e.GetPosition (AssociatedObject));
                    MouseViewModel?.RaisePreviewRightClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Middle:
                    MouseViewModel?.RaisePreviewMiddleUp(e.GetPosition(AssociatedObject));
                    MouseViewModel?.RaisePreviewMiddleClick(e.GetPosition(AssociatedObject));
                    break;
            }
        }

        private void PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsTunnelCaptured == false && this.AssociatedObject == e.OriginalSource)
            {
                this.StartCapture();
                IsTunnelCaptured = true;
            }
            TunnlingDragButton.Add(e.ChangedButton);

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseViewModel?.RaisePreviewLeftDown(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Right:
                    MouseViewModel?.RaisePreviewRightDown(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Middle:
                    MouseViewModel?.RaisePreviewMiddleDown(e.GetPosition(AssociatedObject));
                    break;

            }
        }

        #endregion

        #region Event Handler (Bublling Event)


        private void MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseViewModel?.RaiseWheel(e.GetPosition(AssociatedObject), e.Delta > 0);
            ///e.Handled = true;

        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            MouseViewModel?.RaiseLeave(e.GetPosition(AssociatedObject));
            ///e.Handled = true;

        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            MouseViewModel?.RaiseEnter(e.GetPosition(AssociatedObject));
            ///e.Handled = true;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (IsBubbleCaptured)
            {
                foreach (var btn in BubblingDragButton)
                {

                    switch (btn)
                    {
                        case MouseButton.Left:
                            MouseViewModel?.RaiseLeftDrag(e.GetPosition(AssociatedObject));
                            break;

                        case MouseButton.Right:
                            MouseViewModel?.RaiseRightDrag(e.GetPosition(AssociatedObject));
                            break;

                        case MouseButton.Middle:
                            MouseViewModel?.RaiseMiddleDrag(e.GetPosition(AssociatedObject));
                            break;
                    }

                    ///e.Handled = true;
                }
            }
            else
            {
                MouseViewModel?.RaiseMove(e.GetPosition(AssociatedObject));
                ///e.Handled = true;
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsBubbleCaptured == false)
                return;

            if (BubblingDragButton.Contains(e.ChangedButton) && BubblingDragButton.Count == 1)
            {
                StopCapture();
            }
            BubblingDragButton.Remove(e.ChangedButton);


            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseViewModel?.RaiseLeftUp(e.GetPosition(AssociatedObject));
                    MouseViewModel?.RaiseLeftClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Right:
                    MouseViewModel?.RaiseRightUp(e.GetPosition(AssociatedObject));
                    MouseViewModel?.RaiseRightClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Middle:
                    MouseViewModel?.RaiseMiddleUp(e.GetPosition(AssociatedObject));
                    MouseViewModel?.RaiseMiddleClick(e.GetPosition(AssociatedObject));
                    break;
            }
            ///e.Handled = true;
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsBubbleCaptured == false)
            {
                this.StartCapture();
                IsBubbleCaptured = true;
            }
            BubblingDragButton.Add(e.ChangedButton);

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseViewModel?.RaiseLeftDown(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Right:
                    MouseViewModel?.RaiseRightDown(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Middle:
                    MouseViewModel?.RaiseMiddleDown(e.GetPosition(AssociatedObject));
                    break;

            }
            ///e.Handled = true;

        }

        #endregion
    }
}
