using LabelMaker.Common;
using LabelMaker.Core;

namespace LabelMaker.ViewModel
{
    internal class HorizonViewModel : PropertyChangedBase
    {
        private readonly Horizon horizon;

        public string LabelContent
        {
            get => horizon.LabelContent;
            set
            {
                horizon.LabelContent = value;
                OnPropertyChanged(nameof(LabelContent));
            }
        }

        public int PointNumber
        {
            get => horizon.PointNumber;
            set
            {
                horizon.PointNumber = value;
                OnPropertyChanged(nameof(PointNumber));
            }
        }

        public HorizonViewModel()
        {
            horizon = new Horizon();
        }
    }
}
