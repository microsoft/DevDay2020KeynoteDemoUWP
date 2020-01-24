using System;

namespace DevDay2020KeynoteDemoUWP.Model
{
    public class Place
    {
        public Place(string placeName, string imageUriString)
        {
            PlaceName = placeName;
            ImageUri = imageUriString;
        }

        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string CityName { get; set; }
        public PlaceType PlaceType { get; set; }
        public string ImageUri { get; set; }
        public string ImageAuthor { get; set; }
        public string ImageAuthorUrl { get; set; }
    }
}
