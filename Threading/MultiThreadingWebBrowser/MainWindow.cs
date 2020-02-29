// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MultiThreadingWebBrowser
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            placeHolder.Source = new Uri("https://whichbrowser.net/"); // http://www.msn.com"
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            placeHolder.Source = new Uri(newLocation.Text);
            placeHolder.DataContextChanged += PlaceHolder_DataContextChanged;
            placeHolder.ContentRendered += PlaceHolder_ContentRendered;
            placeHolder.LayoutUpdated += PlaceHolder_LayoutUpdated;
            placeHolder.LoadCompleted += PlaceHolder_LoadCompleted;
            placeHolder.Loaded += PlaceHolder_Loaded;
            placeHolder.Navigated += PlaceHolder_Navigated;
            placeHolder.NavigationFailed += PlaceHolder_NavigationFailed;
            placeHolder.SourceUpdated += PlaceHolder_SourceUpdated;
            placeHolder.TargetUpdated += PlaceHolder_TargetUpdated;
            placeHolder.Unloaded += PlaceHolder_Unloaded;
        }

        // https://stackoverflow.com/questions/2652460/how-to-get-the-name-of-the-current-method-from-code
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        private void PlaceHolder_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_LayoutUpdated(object sender, EventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_ContentRendered(object sender, EventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void PlaceHolder_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine(GetCurrentMethod());
        }

        private void NewWindowHandler(object sender, RoutedEventArgs e)
        {
            var newWindowThread = new Thread(ThreadStartingPoint);
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }

        private void ThreadStartingPoint()
        {
            var tempWindow = new MainWindow();
            tempWindow.Show();
            Dispatcher.Run();
        }
    }
}