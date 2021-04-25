using System;
using System.Collections.Generic;

using LabelMaker.Common;
using LabelMaker.Helpers;
using LabelMaker.Model;

namespace LabelMaker.ViewModel
{
    public class MainInfoViewModel : PropertyChangedBase
    {
        private readonly MainInfo mainInfo;
        private readonly bool CanPrint;
        private readonly Action UpdateHorizons;
        readonly ICollection<PointViewModel> Points;

        public string Company
        {
            get => mainInfo.Company;
            set
            {
                if (mainInfo.Company != value)
                {
                    mainInfo.Company = value;
                    OnPropertyChanged(nameof(Company), nameof(CanPrint));
                }
            }
        }

        public string OrderNumber
        {
            get => mainInfo.OrderNumber;
            set
            {
                if (mainInfo.OrderNumber != value)
                {
                    mainInfo.OrderNumber = value;
                    OnPropertyChanged(nameof(OrderNumber), nameof(CanPrint));
                    UpdateHorizons();
                }
            }
        }

        public int Year
        {
            get => mainInfo.Year;
            set
            {
                if (mainInfo.Year != value)
                {
                    mainInfo.Year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        public int PointsCount
        {
            get => mainInfo.PointsCount;
            set
            {
                if (mainInfo.PointsCount != value)
                {
                    mainInfo.PointsCount = value == -1 ? 0 : value;
                    Points.UpdateWithNewPointCount(mainInfo);
                    OnPropertyChanged("PointsCount");
                    UpdateHorizons.Invoke();
                }
            }
        }

        public MainInfoViewModel(string company, bool canPrint, Action updateHorizons, ICollection<PointViewModel> points)
        {
            mainInfo = new MainInfo
            {
                Company = company,
                Year = DateTime.Now.Year
            };

            this.CanPrint = canPrint;
            this.Points = points;
            this.UpdateHorizons = updateHorizons;
        }
    }
}
