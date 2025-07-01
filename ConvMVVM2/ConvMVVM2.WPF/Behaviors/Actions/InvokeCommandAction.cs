using ConvMVVM2.WPF.Behaviors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TriggerAction = ConvMVVM2.WPF.Behaviors.Base.TriggerAction;


namespace ConvMVVM2.WPF.Behaviors.Actions
{
    public class InvokeCommandAction : TriggerAction<DependencyObject>
    {
        #region Dependency Property
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command),typeof(ICommand),typeof(InvokeCommandAction),new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =  DependencyProperty.Register(nameof(CommandParameter),typeof(object),typeof(InvokeCommandAction),new PropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        #endregion

        #region Public Functions
        protected override void Invoke(object parameter)
        {
            var command = Command;
            var param = CommandParameter ?? parameter;

            if (command?.CanExecute(param) == true)
            {
                command.Execute(param);
            }
        }
        #endregion

    }
}
