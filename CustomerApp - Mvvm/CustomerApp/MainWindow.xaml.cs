using System.Windows;
using System.Windows.Data;
using CustomerApp.ViewModel;
using CustomerLib;

namespace CustomerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current.MainVM;
        }

    }
}
