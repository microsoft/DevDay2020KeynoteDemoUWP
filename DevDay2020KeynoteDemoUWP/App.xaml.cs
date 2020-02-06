using DevDay2020KeynoteDemoUWP.Model;
using DevDay2020KeynoteDemoUWP.Pages;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DevDay2020KeynoteDemoUWP
{
    sealed partial class App : Application
    {
        public static ObservableCollection<GroupInfoList> GetGroupedPlaces()
        {
            var group1 = new GroupInfoList { Key = "Architecture" };

            group1.Add(new Place
            {
                PlaceDescription = "Scelerisque nunc laoreet viverra dictum sodales integer tincidunt.",
                CountryName = "Japan",
                CityName = "Hiroshima",
                ImageUri = "/Assets/Images/bantersnaps-wPMvPMD9KBI-unsplash.jpg",
                ImageAuthor = "bantersnaps",
                ImageAuthorUrl = "https://unsplash.com/photos/wPMvPMD9KBI"
            });
            group1.Add(new Place
            {
                PlaceDescription = "Velit amet fermentum lectus in scelerisque nullam urna, sit.",
                CountryName = "United Kingdom",
                CityName = "London",
                ImageUri = "/Assets/Images/eva-dang-EXdXLrZXS9Q-unsplash.jpg",
                ImageAuthor = "Eva Dang",
                ImageAuthorUrl = "https://unsplash.com/photos/EXdXLrZXS9Q"
            });
            group1.Add(new Place
            {
                PlaceDescription = "Velit amet fermentum lectus in scelerisque nullam urna, sit.",
                CountryName = "Spain",
                CityName = "Barcelona",
                ImageUri = "/Assets/Images/tomas-nozina-UP22zkjJGZo-unsplash.jpg",
                ImageAuthor = "Tomáš Nožina",
                ImageAuthorUrl = "https://unsplash.com/photos/UP22zkjJGZo"
            });

            var group2 = new GroupInfoList { Key = "Outdoor" };

            group2.Add(new Place
            {
                PlaceDescription = "Non placerat vestibulum viverra in tellus sem.",
                CountryName = "United States",
                CityName = "Page",
                ImageUri = "/Assets/Images/ashim-d-silva-WeYamle9fDM-unsplash.jpg",
                ImageAuthor = "Ashim D’Silva",
                ImageAuthorUrl = "https://unsplash.com/photos/WeYamle9fDM"
            });
            group2.Add(new Place
            {
                PlaceDescription = "Mi ipsum vitae phasellus egestas mi varius mauris.",
                CountryName = "Australia",
                CityName = "Melbourne",
                ImageUri = "/Assets/Images/annie-spratt-tB4Gf7ddcJY-unsplash.jpg",
                ImageAuthor = "Annie Spratt",
                ImageAuthorUrl = "https://unsplash.com/photos/tB4Gf7ddcJY"
            });
            group2.Add(new Place
            {
                PlaceDescription = "Quis neque sed scelerisque risus magnis quam ut.",
                CountryName = "South Africa",
                CityName = "Wild",
                ImageUri = "/Assets/Images/damian-patkowski-QeC4oPdKu7c-unsplash.jpg",
                ImageAuthor = "Damian Patkowski",
                ImageAuthorUrl = "https://unsplash.com/photos/QeC4oPdKu7c"
            });
            group2.Add(new Place
            {
                PlaceDescription = "Porttitor nunc, sed tincidunt bibendum rutrum.",
                CountryName = "Italy",
                CityName = "Rome",
                ImageUri = "/Assets/Images/willian-west-YpKiwlvhOpI-unsplash.jpg",
                ImageAuthor = "Willian West",
                ImageAuthorUrl = "https://unsplash.com/photos/YpKiwlvhOpI"
            });
            group2.Add(new Place
            {
                PlaceDescription = "Volutpat suspendisse tortor, nisi ullamcorper ut.",
                CountryName = "Germany",
                CityName = "Schwangau",
                ImageUri = "/Assets/Images/felix-NAytNmKtyiU-unsplash.jpg",
                ImageAuthor = "Felix",
                ImageAuthorUrl = "https://unsplash.com/photos/NAytNmKtyiU"
            });
            group2.Add(new Place
            {
                PlaceDescription = "Maecenas cursus eu aenean in eget.",
                CountryName = "France",
                CityName = "Paris",
                ImageUri = "/Assets/Images/willian-west-TVyjcTEKHLU-unsplash.jpg",
                ImageAuthor = "Willian West",
                ImageAuthorUrl = "https://unsplash.com/photos/TVyjcTEKHLU"
            });

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
            CustomizeTitleBar();

            void CustomizeTitleBar()
            {
                // Draw into the title bar.
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

                // Remove the solid-colored backgrounds behind the caption controls and system back button.
                var viewTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                viewTitleBar.ButtonBackgroundColor = Colors.Transparent;
                viewTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                viewTitleBar.ButtonForegroundColor = Color.FromArgb(255, 4, 119, 191);
            }

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) { }

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
            deferral.Complete();
        }
    }
}
