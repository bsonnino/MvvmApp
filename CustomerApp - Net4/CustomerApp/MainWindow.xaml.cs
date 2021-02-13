using System.Windows;
using System.Windows.Data;
using CustomerLib;

namespace CustomerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ICustomerRepository customerRepository = new CustomerRepository();

        public MainWindow()
        {
            InitializeComponent();
            master.ItemsSource = customerRepository.Customers;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var customer = new Customer();
            customerRepository.Add(customer);
            master.SelectedItem = customer;
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            if (detail.DataContext != null)
                customerRepository.Remove((Customer) detail.DataContext);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            customerRepository.Commit();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var coll = CollectionViewSource.GetDefaultView(master.ItemsSource);
            if (!string.IsNullOrWhiteSpace(searchText.Text))
                coll.Filter = c => ((Customer) c).Country.ToLower().Contains(searchText.Text.ToLower());
            else
                coll.Filter = null;
        }
    }
}
