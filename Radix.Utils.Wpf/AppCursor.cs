using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Radix.Utils.Wpf
{
    /// <summary>
    /// Sets the application cursor via <see cref="Mouse.OverrideCursor"/> upon creation
    /// and resets the cursor to the previous one upon <see cref="Dispose"/>.
    /// This class is meant to be used in a <c>using</c> block when performing
    /// long running operations like so:
    /// <code>
    /// using (AppCursor.Wait())
    /// {
    ///   // perform operation here
    /// }
    /// </code>
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class AppCursor : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets the cursor that was set when this instance was created.
        /// Upon disposing the cursor will be set back to this value.
        /// </summary>
        public Cursor PreviousCursor { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to delay the reset of the cursor until the application runs idle.
        /// If set to <c>true</c>, the cursor is set to <c>null</c> when the application runs idle;
        /// otherwise the cursor is set to the the previous value, i.e. the value it was before the constructor
        /// of <see cref="AppCursor"/> was called, upon being disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the reset of the cursor is delayed until idle; otherwise, <c>false</c>.
        /// </value>
        public bool DelayResetUntilIdle { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AppCursor"/> class
        /// and sets the cursor to the given cursor, usually <see cref="Cursors.Wait"/>.
        /// </summary>
        /// <param name="delayResetUntilIdle">Determines whether to delay the reset of the cursor until the application runs idle.</param>
        /// <param name="cursor">The cursor to set.</param>
        /// <exception cref="ArgumentNullException">cursor</exception>
        public AppCursor(Cursor cursor, bool delayResetUntilIdle = false)
        {
            Debug.Assert(cursor != null);
            Debug.WriteLine($"AppCursor::ctor: setting cursor to {cursor}");

            this.PreviousCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = cursor;

            this.DelayResetUntilIdle = delayResetUntilIdle;
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="AppCursor"/>, setting
        /// the cursor to <see cref="Cursors.Wait"/>.
        /// </summary>
        /// <param name="delayResetUntilIdle">Determines whether to delay the reset of the cursor until the application runs idle.</param>
        /// <returns>A disposable instance of <see cref="AppCursor"/>.</returns>
        public static AppCursor Wait(bool delayResetUntilIdle = false) => new AppCursor(Cursors.Wait, delayResetUntilIdle);

        #region IDisposable Support
        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        void Dispose(bool disposing)
        {
            if (this.IsDisposed)
                return;
            else if (disposing)
            {
                if (this.DelayResetUntilIdle)
                {
                    Debug.WriteLine($"AppCursor::Dispose: resetting cursor to null on ApplicationIdle.");
                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle,
                        new Action(() => AppCursor.ResetCursor()));

                }
                else
                {
                    Debug.WriteLine($"AppCursor::Dispose: resetting cursor to previous value: {this.PreviousCursor}");
                    Mouse.OverrideCursor = this.PreviousCursor;
                }
            }
            this.IsDisposed = true;
        }

        /// <summary>
        /// Resets the cursor.
        /// </summary>
        public static void ResetCursor()
        {
            // may get called multiple times in a row, don't thash tracer
            if (Mouse.OverrideCursor != null)
            {
                Debug.WriteLine($"AppCursor::ResetCursor: resetting cursor to null.");
                Mouse.OverrideCursor = null;
            }
        }

        ~AppCursor()
        {
            Debug.WriteLine("WARNING: AppCursor::~AppCursor() has been called! Did you forget to dispose me???");
            Dispose(false);
        }

        /// <summary>
        /// Resets the cursor. The behavior of the reset depends on the value of <see cref="DelayResetUntilIdle"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
