using System;
using System.Windows.Input;

namespace Radix.Utils.Wpf
{
    /// <summary>
    /// Basic implementation of the ICommand interface.
    /// Allows to attach delegates for the Execute and CanExecute action.
    /// </summary>
    public class Command : ICommand
    {
        #region Properties (Execute and CanExecute delegates)
        private Func<object, bool> CanExecuteDelegate { get; set; }
        private Action<object> ExecuteDelegate { get; set; }
        #endregion

        #region Ctor/dtor
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="executeDelegate">The execute delegate.</param>
        /// <param name="canExecuteDelegate">The can execute delegate.</param>
        public Command(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate = null)
        {
            this.ExecuteDelegate = executeDelegate;
            this.CanExecuteDelegate = canExecuteDelegate;
        }
        #endregion

        #region ICommand implementation
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter) => this.CanExecuteDelegate?.Invoke(parameter) ?? true;

        /// <summary>
        /// Occurs when the condition, that determine whether execution is available, has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter) => this.ExecuteDelegate?.Invoke(parameter);
        #endregion
    }
}
