using DevDay2020KeynoteDemoUWP.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DevDay2020KeynoteDemoUWP
{
    public sealed partial class WonderbarPage
    {
        private readonly List<Place> _places = App.GetGroupedPlaces().SelectMany(p => p).ToList();

        public WonderbarPage()
        {
            InitializeComponent();
        }
    }
}
