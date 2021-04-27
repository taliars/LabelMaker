using LabelMaker.Common;
using LabelMaker.Core;

namespace LabelMaker.ViewModel
{
    internal class PointViewModel : PropertyChangedBase
    {
        private readonly Point point;

        public int Number
        {
            get => point.Number;
            set
            {
                point.Number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public int Horizons
        {
            get => point.Horizons;
            set
            {
                point.Horizons = value;
                OnPropertyChanged(nameof(Horizons));
            }
        }

        public PointViewModel()
        {
            point = new Point();
        }
    }
}
