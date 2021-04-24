using LabelMaker.Common;
using LabelMaker.Model;

namespace LabelMaker.ViewModel
{
    public class PointViewModel : PropertyChangedBase
    {
        private Point point;

        public int Number
        {
            get => point.Number;
            set
            {
                point.Number = value;
                OnPropertyChanged("Number");
            }
        }

        public int Horizons
        {
            get => point.Horizons;
            set
            {
                point.Horizons = value;
                OnPropertyChanged("Horizons");
            }
        }

        public PointViewModel()
        {
            point = new Point();
        }
    }
}
