using System;

namespace DevDay2020KeynoteDemoUWP.Model
{
    public class Place : Base
    {
        public Place()
        {
            Id = Guid.NewGuid();
        }

        public Place(string countryName, string imageUriString, string placeName, string placeDescription, string author, string url)
        {
            Id = Guid.NewGuid();
            CountryName = countryName;
            ImageUri = imageUriString;
            PlaceName = placeName;
            PlaceDescription = placeDescription;
            ImageAuthor = author;
            ImageAuthorUrl = url;
        }

        public Guid Id { get; set; }
        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public PlaceType PlaceType { get; set; }
        public string ImageUri { get; set; }
        public string ImageAuthor { get; set; }
        public string ImageAuthorUrl { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }
    }
}