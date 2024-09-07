
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureGate.Customer.Business
{
    public interface ICustomer
    {
        string GetCustomerByMnemonic(string input);
        string AddCustomer(string input);
        string UpdateCustomer(string input);
    }
}
