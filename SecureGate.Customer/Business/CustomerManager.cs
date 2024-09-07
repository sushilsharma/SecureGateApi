
using APIController.Framework;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.Framework;
using SecureGate.Framework.DataAccess;
using SecureGate.Framework.PasswordUtility;
using SecureGate.Framework.Serializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using SecureGate.DataAccess.Common;
using SecureGate.Customer.DataAccess;

namespace SecureGate.Customer.Business
{
    public class CustomerManager : ICustomer
    {
        BaseAppLogger _loggerInstance;
        public CustomerManager(BaseAppLogger loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }
        public string GetCustomerByMnemonic(string input)
        {
            string customerDetails = "";
            try
            {
                customerDetails = CustomerDataAccessManager.GetCustomerByMnemonic(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("GetCustomerByMnemonic Error - " + ex.ToString(), 502);
            }


            return customerDetails;
        }

        public string AddCustomer(string input)
        {
            string customerDetails = "";
            try
            {
                customerDetails = CustomerDataAccessManager.AddCustomer(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("AddCustomer Error - " + ex.ToString(), 502);
            }


            return customerDetails;
        }


        public string UpdateCustomer(string input)
        {
            string customerDetails = "";
            try
            {
                customerDetails = CustomerDataAccessManager.UpdateCustomer(input);
            }
            catch (Exception ex)
            {
                _loggerInstance.Information("AddCustomer Error - " + ex.ToString(), 502);
            }


            return customerDetails;
        }
    }
}
