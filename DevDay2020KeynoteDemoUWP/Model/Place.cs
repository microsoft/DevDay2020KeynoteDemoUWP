using System;

namespace DevDay2020KeynoteDemoUWP.Model
{
    public class Place : Base
    {
        public Place(string countryName, string imageUriString)
        {
            CountryName = countryName;
            ImageUri = imageUriString;
        }

        public string PlaceName { get; set; }
        public string PlaceDescription { get; set; }
        public string CountryName { get; set; }
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