using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ConvMVVM2.WPF.ViewModels;

namespace ConvMVVM2.WPF.Behaviors
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
            base.OnAttached();

            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseUp += MouseUp;
            AssociatedObject.MouseMove += MouseMove;

            AssociatedObject.MouseEnter += MouseEnter;
            AssociatedObject.MouseLeave += MouseLeave;
            AssociatedObject.MouseWheel += MouseWheel;
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();

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
            this.MouseViewModel?.RaiseWheel(e.GetPosition(this.AssociatedObject), e.Delta > 0);
            e.Handled = true;

        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            this.MouseViewModel?.RaiseLeave(e.GetPosition(this.AssociatedObject));
            e.Handled = true;

        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            this.MouseViewModel?.RaiseEnter(e.GetPosition(this.AssociatedObject));
            e.Handled = true;
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCaptured)
            {
                foreach (var btn in this.DragButton)
                {

                    switch (btn)
                    {
                        case MouseButton.Left:
                            this.MouseViewModel?.RaiseLeftDrag(e.GetPosition(this.AssociatedObject));
                            break;

                        case MouseButton.Right:
                            this.MouseViewModel?.RaiseRightDrag(e.GetPosition(this.AssociatedObject));
                            break;

                        case MouseButton.Middle:
                            this.MouseViewModel?.RaiseMiddleDrag(e.GetPosition(this.AssociatedObject));
                            break;
                    }

                    e.Handled = true;
                }
            }
            else
            {
                this.MouseViewModel.RaiseMove(e.GetPosition(this.AssociatedObject));
                e.Handled = true;
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsCaptured == false)
                return;

            if (this.DragButton.Contains(e.ChangedButton) && this.DragButton.Count == 1)
            {
                this.IsCaptured = false;
                Mouse.Capture(null);
            }
            this.DragButton.Remove(e.ChangedButton);


            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    this.MouseViewModel?.RaiseLeftClick(e.GetPosition(this.AssociatedObject));
                    break;

                case MouseButton.Right:
                    this.MouseViewModel?.RaiseRightClick(e.GetPosition(this.AssociatedObject));
                    break;

                case MouseButton.Middle:
                    this.MouseViewModel?.RaiseMiddleClick(e.GetPosition(this.AssociatedObject));
                    break;
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsCaptured == false)
            {
                Mouse.Capture((IInputElement)sender);
                this.IsCaptured = true;
            }
            DragButton.Add(e.ChangedButton);

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    this.MouseViewModel?.RaiseLeftDown(e.GetPosition(this.AssociatedObject));
                    break;

                case MouseButton.Right:
                    this.MouseViewModel?.RaiseRightDown(e.GetPosition(this.AssociatedObject));
                    break;

                case MouseButton.Middle:
                    this.MouseViewModel?.RaiseMiddleDown(e.GetPosition(this.AssociatedObject));
                    break;

            }
            e.Handled = true;

        }

        #endregion
    }
}
