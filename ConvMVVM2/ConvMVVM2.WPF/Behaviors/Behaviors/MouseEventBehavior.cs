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
        private bool IsCaptured = false;
        private readonly HashSet<MouseButton> DragButton = new HashSet<MouseButton>();
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
        }


        protected override void OnDetaching()
        {

            AssociatedObject.MouseDown -= MouseDown;
            AssociatedObject.MouseUp -= MouseUp;
            AssociatedObject.MouseMove -= MouseMove;

            AssociatedObject.MouseEnter -= MouseEnter;
            AssociatedObject.MouseLeave -= MouseLeave;
            AssociatedObject.MouseWheel -= MouseWheel;
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

        #region Event Handler
        private void MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseViewModel?.RaiseWheel(e.GetPosition(AssociatedObject), e.Delta > 0);
            e.Handled = true;

        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            MouseViewModel?.RaiseLeave(e.GetPosition(AssociatedObject));
            e.Handled = true;

        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            MouseViewModel?.RaiseEnter(e.GetPosition(AssociatedObject));
            e.Handled = true;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCaptured)
            {
                foreach (var btn in DragButton)
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

                    e.Handled = true;
                }
            }
            else
            {
                MouseViewModel.RaiseMove(e.GetPosition(AssociatedObject));
                e.Handled = true;
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsCaptured == false)
                return;

            if (DragButton.Contains(e.ChangedButton) && DragButton.Count == 1)
            {
                IsCaptured = false;
                Mouse.Capture(null);
            }
            DragButton.Remove(e.ChangedButton);


            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    MouseViewModel?.RaiseLeftClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Right:
                    MouseViewModel?.RaiseRightClick(e.GetPosition(AssociatedObject));
                    break;

                case MouseButton.Middle:
                    MouseViewModel?.RaiseMiddleClick(e.GetPosition(AssociatedObject));
                    break;
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsCaptured == false)
            {
                Mouse.Capture((IInputElement)sender);
                IsCaptured = true;
            }
            DragButton.Add(e.ChangedButton);

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
            e.Handled = true;

        }

        #endregion
    }
}
