using LabelMaker.Common;
using LabelMaker.Model;

namespace LabelMaker.ViewModel
{
    public class PointViewModel : PropertyChangedBase
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
