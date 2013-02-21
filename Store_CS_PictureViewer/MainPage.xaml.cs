using System;
using Windows.Storage;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store_CS_PictureViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// The below method queries to the 
        /// Picture Library with all its sub folders and
        /// read all pictures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            //Define thr query to iterate thriugh all the subfolders 
            var pictureQueryOptions = new QueryOptions();
            //Read through all the subfolders.
            pictureQueryOptions.FolderDepth = FolderDepth.Deep;

            //Apply the query on the PicturesLibrary
            var pictureQuery = KnownFolders.PicturesLibrary.CreateFileQueryWithOptions(pictureQueryOptions);
            //
            var picturesInformation = new FileInformationFactory(pictureQuery, ThumbnailMode.PicturesView);
            picturesSource.Source = picturesInformation.GetVirtualizedFilesVector();
        }
    }

    /// <summary>
    /// The converter class used to display images
    /// </summary>
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                var img = (IRandomAccessStream)value;
                var picture = new BitmapImage();
                picture.SetSource(img);
                return picture;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
