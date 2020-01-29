using DevDay2020KeynoteDemoUWP.Model;
using DevDay2020KeynoteDemoUWP.Pages;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DevDay2020KeynoteDemoUWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static ObservableCollection<GroupInfoList> GetGroupedPlaces()
        {
            var group1 = new GroupInfoList { Key = "Architecture" };
            group1.Add(new Place("Japan", "/Assets/Images/bantersnaps-wPMvPMD9KBI-unsplash.jpg", "Eget", "Scelerisque nunc laoreet viverra dictum sodales integer tincidunt."));
            group1.Add(new Place("United Kingdom", "/Assets/Images/eva-dang-EXdXLrZXS9Q-unsplash.jpg", "Imperdiet", "Velit amet fermentum lectus in scelerisque nullam urna, sit."));
            group1.Add(new Place("Spain", "/Assets/Images/tomas-nozina-UP22zkjJGZo-unsplash.jpg", "Donec", "Pharetra, ultrices ut magna risus pellentesque at aliquet."));

            var group2 = new GroupInfoList { Key = "Outdoor" };
            group2.Add(new Place("United States", "/Assets/Images/ashim-d-silva-WeYamle9fDM-unsplash.jpg", "Elementum", "Non placerat vestibulum viverra in tellus sem."));
            group2.Add(new Place("Australia", "/Assets/Images/annie-spratt-tB4Gf7ddcJY-unsplash.jpg", "Morbi", "Mi ipsum vitae phasellus egestas mi varius mauris."));
            group2.Add(new Place("South Africa", "/Assets/Images/damian-patkowski-QeC4oPdKu7c-unsplash.jpg", "Iaculis", "Quis neque sed scelerisque risus magnis quam ut."));
            group2.Add(new Place("Italy", "/Assets/Images/willian-west-YpKiwlvhOpI-unsplash.jpg", "Nunc", "Porttitor nunc, sed tincidunt bibendum rutrum."));
            group2.Add(new Place("Germany", "/Assets/Images/felix-NAytNmKtyiU-unsplash.jpg", "Vitae", "Volutpat suspendisse tortor, nisi ullamcorper ut."));
            group2.Add(new Place("France", "/Assets/Images/willian-west-TVyjcTEKHLU-unsplash.jpg", "Diam", "Maecenas cursus eu aenean in eget."));

            var groups = new ObservableCollection<GroupInfoList>();
            groups.Add(group1);
            groups.Add(group2);

            return groups;
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
