using System;
using System.Diagnostics;
using System.Windows;

namespace Radix.Utils.Wpf
{
    /// <summary>
    /// A Window that displays exception information; a replacement for <see cref="MessageBox"/>.
    /// </summary>
    public partial class ExceptionWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionWindow"/> class.
        /// </summary>
        public ExceptionWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays a modal window with details of the given <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The exception to be displayed.</param>
        /// <param name="title">The title, or null. If null the window extrats the AssemblyTitle
        /// from the currently executing assembly.</param>
        public static void Show(Exception exception, string title = null)
        {
            Debug.Assert(exception != null);
            ExceptionWindow exceptionWindow = new ExceptionWindow { DataContext = exception, Title = title ?? ProductInfo.AssemblyTitle };
            exceptionWindow.Owner = Application.Current.MainWindow;
            exceptionWindow.ShowDialog();
        }
    }
}
