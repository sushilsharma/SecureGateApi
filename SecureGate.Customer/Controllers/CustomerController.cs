using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.APIController.Framework.Controllers;
using System.Net;
using Newtonsoft.Json;
using SecureGate.Framework.PasswordUtility;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using SecureGate.Framework;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using SecureGate.APIController.Framework;
using SecureGate.DataAccess.Common;
using SecureGate.Framework.DataAccess;
using SecureGate.APIController.Framework.Business;
using SecureGate.Customer.Business;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace SecureGate.Customer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseAPIController
    {
        protected override EnumLoggerType LoggerName
        {
            get
            {
                return EnumLoggerType.Customer;
            }
        }





        [HttpPost]
        [Route("/api/[controller]/GetCustomerByMnemonic")]
        public async Task<IActionResult> GetCustomerByMnemonic(dynamic Json)
        {
            JObject returnObject = new JObject();

            try
            {
                string Input = JsonConvert.SerializeObject(Json);

                if (string.IsNullOrEmpty(Input))
                {
                    LoggerInstance.Warning("GetCustomerByMnemonic --> Invalid input: CustomerMnemonic is missing", 400);
                    return BadRequest("CustomerMnemonic is required.");
                }

                LoggerInstance.Information($"GetCustomerByMnemonic --> Input Mnemonic : {Input}", 502);

                // Assuming your customer manager has an async method
                ICustomer CustomerInterface = new CustomerManager(LoggerInstance);
                string output = CustomerInterface.GetCustomerByMnemonic(Input);

                if (string.IsNullOrEmpty(output))
                {
                    LoggerInstance.Warning($"GetCustomerByMnemonic --> No data found for Mnemonic: {output}", 404);
                    return NotFound("Customer not found");
                }

                LoggerInstance.Information($"GetCustomerByMnemonic --> Output : {output}", 502);
         
                returnObject = (JObject)JsonConvert.DeserializeObject(output);
               
            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"GetCustomerByMnemonic Exception: {ex.Message}", 500);
                return StatusCode(500, "Internal server error");
            }

            return Ok(returnObject);
        }


        [HttpPost]
        [Route("/api/[controller]/AddCustomer")]
        public async Task<IActionResult> AddCustomer(dynamic Json)
        {
            JObject returnObject = new JObject();

            try
            {
                string Input = JsonConvert.SerializeObject(Json);

                if (string.IsNullOrEmpty(Input))
                {
                    LoggerInstance.Warning("AddCustomer --> Invalid input: CustomerMnemonic is missing", 400);
                    return BadRequest("AddCustomer is required.");
                }

                LoggerInstance.Information($"AddCustomer --> Input AddCustomer : {Input}", 502);

                // Assuming your customer manager has an async method
                ICustomer CustomerInterface = new CustomerManager(LoggerInstance);
                string output = CustomerInterface.AddCustomer(Input);

                if (string.IsNullOrEmpty(output))
                {
                    LoggerInstance.Warning($"AddCustomer --> No data found for AddCustomer: {output}", 404);
                    return NotFound("Customer not found");
                }

                LoggerInstance.Information($"AddCustomer --> Output : {output}", 502);

                returnObject = (JObject)JsonConvert.DeserializeObject(output);

            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"AddCustomer Exception: {ex.Message}", 500);
                return StatusCode(500, "Internal server error");
            }

            return Ok(returnObject);
        }


        [HttpPost]
        [Route("/api/[controller]/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(dynamic Json)
        {
            JObject returnObject = new JObject();

            try
            {
                string Input = JsonConvert.SerializeObject(Json);

                if (string.IsNullOrEmpty(Input))
                {
                    LoggerInstance.Warning("UpdateCustomer --> Invalid input: CustomerMnemonic is missing", 400);
                    return BadRequest("CustomerMnemonic is required.");
                }

                LoggerInstance.Information($"UpdateCustomer --> Input Mnemonic : {Input}", 502);

                // Assuming your customer manager has an async method
                ICustomer CustomerInterface = new CustomerManager(LoggerInstance);
                string output = CustomerInterface.UpdateCustomer(Input);

                if (string.IsNullOrEmpty(output))
                {
                    LoggerInstance.Warning($"UpdateCustomer --> No data found for Mnemonic: {output}", 404);
                    return NotFound("Customer not found");
                }

                LoggerInstance.Information($"UpdateCustomer --> Output : {output}", 502);

                returnObject = (JObject)JsonConvert.DeserializeObject(output);

            }
            catch (Exception ex)
            {
                LoggerInstance.Error($"UpdateCustomer Exception: {ex.Message}", 500);
                return StatusCode(500, "Internal server error");
            }

            return Ok(returnObject);
        }

    }
}
