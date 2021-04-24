using LabelMaker.Common;
using LabelMaker.Model;

namespace LabelMaker.ViewModel
{
    public class HorizonViewModel : PropertyChangedBase
    {
        private Horizon horizon;

        public string LabelContent
        {
            get => horizon.LabelContent;
            set
            {
                horizon.LabelContent = value;
                OnPropertyChanged("LabelContent");
            }
        }

        public int PointNumber
        {
            get => horizon.HorizonNumber;
            set
            {
                horizon.HorizonNumber = value;
                OnPropertyChanged("HorizonNumber");
            }
        }


        public HorizonViewModel()
        {
            horizon = new Horizon();
        }
    }
}
