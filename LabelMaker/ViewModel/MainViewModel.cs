using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

using LabelMaker.Common;
using LabelMaker.Configuration;
using LabelMaker.Helpers;

namespace LabelMaker.ViewModel
{
    public class MainViewModel : PropertyChangedBase
    {
        public MainInfoViewModel MainInfo { get; set; }

        public ObservableCollection<PointViewModel> Points { get; set; }

        public ObservableCollection<HorizonViewModel> Horizons { get; set; }

        public RelayCommand Print { get; set; }

        public bool CanPrint => Horizons.Any()
            && !string.IsNullOrEmpty(MainInfo.OrderNumber)
            && !string.IsNullOrEmpty(MainInfo.Company);

        public string Path { get; set; }

        public MainViewModel(AppSettings appSettings)
        {
            Points = new ObservableCollection<PointViewModel>();
            Horizons = new ObservableCollection<HorizonViewModel>();

            Points.CollectionChanged += Points_CollectionChanged;

            MainInfo = new MainInfoViewModel(appSettings.Company, CanPrint, UpdateHorizons, Points);

            Print = new RelayCommand(o => PdfHelper.CreateDocument(Path, appSettings, Horizons.Select(x => x.LabelContent).ToArray()));
        }

        public void Points_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (PointViewModel item in e.OldItems)
                {
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PointViewModel item in e.NewItems)
                {
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        private void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateHorizons();
        }

        private void UpdateHorizons()
        {
            Horizons.Clear();

            foreach (var point in Points)
            {
                var horizons = Enumerable.Range(1, point.Horizons);

                foreach (var horizon in horizons)
                {
                    Horizons.Add(new HorizonViewModel
                    {
                        PointNumber = point.Number,
                        LabelContent = $"{point.Number}-{horizon}-{MainInfo.OrderNumber}"
                    });
                }
            }

            OnPropertyChanged(nameof(Horizons), nameof(CanPrint));
        }
    }
}
