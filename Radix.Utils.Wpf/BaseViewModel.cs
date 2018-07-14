using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Radix.Utils.Wpf
{
    /// <summary>
    /// Serves as base class for ViewModels; provides default implementations
    /// of <see cref="INotifyPropertyChanging"/>, <see cref="INotifyPropertyChanged"/>
    /// and <see cref="IDisposable"/>.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanging" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.IDisposable" />
    /// <remarks>
    /// The methods <see cref="BeforePropertyChanging(string)"/> and <see cref="AfterPropertyChanged(string)"/>
    /// get called from within <see cref="OnPropertyChanging(string)"/> and <see cref="OnPropertyChanged(string)"/>
    /// and allow derived classes to get notified about any property changes without the need to
    /// subscribe to/unsubscribe from the respective events.
    /// Therefore derived class should call <see cref="OnPropertyChanging(string)"/> and <see cref="OnPropertyChanged(string)"/>
    /// immediately before/after changing any property value that deserves notification.
    /// The <see cref="IDisposable"/> pattern is extended by the event <see cref="Disposing"/>
    /// that is raised at the very beginning of the <see cref="Dispose"/> method.
    /// </remarks>
    public abstract class BaseViewModel : INotifyPropertyChanging, INotifyPropertyChanged, IDisposable
    {
        #region INotifyPropertyChanging
        /// <summary>
        /// Occurs before a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that is about to change.</param>
        protected void OnPropertyChanging(string propertyName)
        {
            this.BeforePropertyChanging(propertyName);
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// This method gets called before a <see cref="PropertyChanging"/> event is being raised.
        /// Override to get notified so you don't need to add an event handler for the event.
        /// </summary>
        /// <param name="propertyName">The name of the property that is going to change.</param>
        protected virtual void BeforePropertyChanging(string propertyName) { }
        #endregion

        #region INotifyPropertyChanged
        /// <summary>
        /// Occurs when a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.AfterPropertyChanged(propertyName);
        }

        /// <summary>
        /// This method gets called after a <see cref="PropertyChanged"/> event has been raised.
        /// Override to get notified so you don't need to add an event handler for the event.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected virtual void AfterPropertyChanged(string propertyName) { }
        #endregion

        #region IDisposable Support
        /// <summary>
        /// Occurs before the element is about to be disposed explicitly through a call to <see cref="Dispose"/>.
        /// </summary>
        /// <remarks>
        /// The event is not triggered when the element is disposed by the GC.
        /// </remarks>
        public event EventHandler Disposing;

        /// <summary>
        /// Checks if the element is disposed and throws a <see cref="ObjectDisposedException"/> if it is.
        /// </summary>
        /// <exception cref="ObjectDisposedException"></exception>
        protected void CheckDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(this.GetType().ToString());
        }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.IsDisposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        ~BaseViewModel()
        {
            if (!Environment.HasShutdownStarted)
            {
                Debug.WriteLine($"WARNING: Finalizer for {this.GetType()} called; did you forget to Dispose() me?");
            }
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Implementation is correct, however, it contains a additional code (raising the Disposing event) that the rule doesn't expect here:)")]
        public void Dispose()
        {
            this.Disposing?.Invoke(this, EventArgs.Empty);
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
