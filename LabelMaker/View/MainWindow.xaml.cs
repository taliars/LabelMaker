using System.Windows;
using LabelMaker.Core;
using LabelMaker.Services.Contract;
using LabelMaker.ViewModel;

using MahApps.Metro.Controls;
using Microsoft.Extensions.Options;
using Microsoft.Win32;

namespace LabelMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainViewModel viewModel;

        public MainWindow(IOptions<AppSettings> settings, IPdfService pdfService)
        {
            InitializeComponent();
            viewModel = new MainViewModel(settings.Value, pdfService);
            this.DataContext = viewModel;
        }

        private void SaveToPDF_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = $"Этикетки {viewModel.MainInfo.OrderNumber}.pdf",
                AddExtension = true,
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                viewModel.Path = dialog.FileName;
            }
        }
    }
}
