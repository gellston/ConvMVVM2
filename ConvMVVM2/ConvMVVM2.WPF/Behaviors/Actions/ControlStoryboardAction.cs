using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using ConvMVVM2.WPF.Behaviors.Base;
using TriggerAction = ConvMVVM2.WPF.Behaviors.Base.TriggerAction;

namespace ConvMVVM2.WPF.Behaviors.Actions
{
    public enum StoryboardControlOption
    {
        Begin,
        Stop,
        Pause,
        Resume,
        TogglePause
    }


    public class ControlStoryboardAction : TriggerAction
    {
        #region Dependency Property
        public static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register(nameof(Storyboard), typeof(Storyboard), typeof(ControlStoryboardAction));

        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(nameof(TargetName), typeof(string), typeof(ControlStoryboardAction));

        public static readonly DependencyProperty ControlStoryboardOptionProperty = DependencyProperty.Register(nameof(ControlStoryboardOption), typeof(StoryboardControlOption), typeof(ControlStoryboardAction), new PropertyMetadata(StoryboardControlOption.Begin));

        public Storyboard Storyboard
        {
            get => (Storyboard)GetValue(StoryboardProperty);
            set => SetValue(StoryboardProperty, value);
        }

        public string TargetName
        {
            get => (string)GetValue(TargetNameProperty);
            set => SetValue(TargetNameProperty, value);
        }

        public StoryboardControlOption ControlStoryboardOption
        {
            get => (StoryboardControlOption)GetValue(ControlStoryboardOptionProperty);
            set => SetValue(ControlStoryboardOptionProperty, value);
        }
        #endregion

        #region Public Function

        public override void Invoke(object parameter)
        {
            if (Storyboard == null || AssociatedObject == null)
                return;

            FrameworkElement targetElement = AssociatedObject as FrameworkElement;

            if (!string.IsNullOrEmpty(TargetName))
            {
                targetElement = targetElement.FindName(TargetName) as FrameworkElement;
            }

            if (targetElement == null) return;

            switch (ControlStoryboardOption)
            {
                case StoryboardControlOption.Begin:
                    Storyboard.Begin(targetElement, true);
                    break;
                case StoryboardControlOption.Stop:
                    Storyboard.Stop(targetElement);
                    break;
                case StoryboardControlOption.Pause:
                    Storyboard.Pause(targetElement);
                    break;
                case StoryboardControlOption.Resume:
                    Storyboard.Resume(targetElement);
                    break;
                case StoryboardControlOption.TogglePause:
                    if (Storyboard.GetIsPaused(targetElement))
                        Storyboard.Resume(targetElement);
                    else
                        Storyboard.Pause(targetElement);
                    break;
            }
        }
        #endregion

        #region Protected Function

        protected override Freezable CreateInstanceCore()
        {
            return new ControlStoryboardAction();
        }
        #endregion
    }
}
