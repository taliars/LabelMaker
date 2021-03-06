using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using LabelMaker.Common;
using LabelMaker.Core;
using LabelMaker.Helpers;
using LabelMaker.Services.Contract;

namespace LabelMaker.ViewModel
{
    internal class MainViewModel : PropertyChangedBase
    {
        private readonly AppSettings appSettings;
        private readonly IPdfService pdfService;

        public MainInfoViewModel MainInfo { get; set; }

        public ObservableCollection<PointViewModel> Points { get; set; }

        public ObservableCollection<HorizonViewModel> Horizons { get; set; }

        public RelayCommand Print { get; set; }

        public bool CanPrint => Horizons.Any()
            && !string.IsNullOrEmpty(MainInfo.OrderNumber)
            && !string.IsNullOrEmpty(MainInfo.Company);

        public string Path { get; set; }

        public MainViewModel(AppSettings appSettings, IPdfService pdfService)
        {
            Points = new ObservableCollection<PointViewModel>();
            Horizons = new ObservableCollection<HorizonViewModel>();

            Points.CollectionChanged += Points_CollectionChanged;

            MainInfo = new MainInfoViewModel(appSettings.Company, CanPrint, UpdateHorizons, Points);

            Print = new RelayCommand(async o => await CreatePdfDocument());
            this.appSettings = appSettings;
            this.pdfService = pdfService;
        }

        private async Task CreatePdfDocument()
        {
            var horizons = Horizons.Select(x => x.LabelContent).ToArray();
            var docStream = await pdfService.CreateDocument(appSettings, horizons);

            DocumentHelper.OpenWithDefaultApp(docStream, Path);
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
