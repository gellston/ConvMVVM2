using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConvMVVM2.Core.MVVM
{
    public sealed class PropertyUndoAction<T> : IUndoAction
    {
        #region Private Property
        private readonly ViewModelBase _target;
        private readonly string _propertyName;
        private readonly T _oldValue;
        private readonly T _newValue;
        #endregion



        #region Constructor

        public PropertyUndoAction(ViewModelBase target,string propertyName,T oldValue,T newValue)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _oldValue = oldValue;
            _newValue = newValue;
        }

        #endregion


        #region Private Functions
        private void SetValue(T value)
        {
            var prop = _target.GetType().GetProperty(_propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop == null || !prop.CanWrite)
            {
                throw new InvalidOperationException(
                    $"Property '{_propertyName}' not found or not writable on {_target.GetType().Name}.");
            }

            prop.SetValue(_target, value);
        }
        #endregion

        #region Static Functions

        public static PropertyUndoAction<T> Create(ViewModelBase target,T oldValue, T newValue,[CallerMemberName] string propertyName = null)
        {
            return new PropertyUndoAction<T>(target, propertyName, oldValue, newValue);
        }
        #endregion


        #region Public Functions

        public void Undo()
        {
            SetValue(_oldValue);
        }

        public void Redo()
        {
            SetValue(_newValue);
        }
        #endregion
    }
}
