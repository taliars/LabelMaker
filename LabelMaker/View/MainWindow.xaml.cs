using System.Windows;

using LabelMaker.ViewModel;

using MahApps.Metro.Controls;

using Microsoft.Win32;

namespace LabelMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }

        private void SaveToPDF_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = $"{viewModel.MainInfo.OrderNumber}.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                viewModel.Path = dialog.FileName;
            }
}
    }
}
