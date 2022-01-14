using System;
using System.Collections.Generic;
using System.Windows.Data;
using CustomerLib;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CustomerApp.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly ICustomerRepository _customerRepository;
        private Customer _selectedCustomer;

        public MainViewModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? 
                                  throw new ArgumentNullException("customerRepository");
            AddCommand = new RelayCommand(DoAdd);
            RemoveCommand = new RelayCommand(DoRemove, () => SelectedCustomer != null);
            SaveCommand = new RelayCommand(DoSave);
            SearchCommand = new RelayCommand<string>(DoSearch);
        }

        public IEnumerable<Customer> Customers => _customerRepository.Customers;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                RemoveCommand.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public IRelayCommand SaveCommand { get; }
        public IRelayCommand<string> SearchCommand { get; }

        private void DoAdd()
        {
            var customer = new Customer();
            _customerRepository.Add(customer);
            SelectedCustomer = customer;
            OnPropertyChanged("Customers");
        }

        private void DoRemove()
        {
            if (SelectedCustomer != null)
            {
                _customerRepository.Remove(SelectedCustomer);
                SelectedCustomer = null;
                OnPropertyChanged("Customers");
            }
        }

        private void DoSave()
        {
            _customerRepository.Commit();
        }

        private void DoSearch(string textToSearch)
        {
            var coll = CollectionViewSource.GetDefaultView(Customers);
            if (!string.IsNullOrWhiteSpace(textToSearch))
                coll.Filter = c => ((Customer)c).Country.ToLower().Contains(textToSearch.ToLower());
            else
                coll.Filter = null;
        }
    }
}