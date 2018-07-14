using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Radix.Utils.Wpf;

namespace WpfTestApp
{
    class MainViewModel : BaseViewModel
    {
        public void DoIt()
        {

        }

        public void Test()
        {
            var previousCursor = Mouse.OverrideCursor;
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                // perform some action upon user-input.
                this.DoIt();
            }
            finally
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                    new Action(() => Mouse.OverrideCursor = previousCursor));
            }

            using (AppCursor.Wait())
            {
                // perform some action upon user-input.
                this.DoIt();
            }
        }
    }
}
