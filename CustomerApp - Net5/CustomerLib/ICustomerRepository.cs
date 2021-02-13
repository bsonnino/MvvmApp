using System.Collections.Generic;

namespace CustomerLib
{
    public interface ICustomerRepository
    {
        bool Add(Customer customer);
        bool Remove(Customer customer);
        bool Commit();
        IEnumerable<Customer> Customers { get; }
    }
}
