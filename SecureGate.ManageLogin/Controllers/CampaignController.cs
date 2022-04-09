using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecureGate.APIController.Framework.AppLogger;
using SecureGate.APIController.Framework.Controllers;
using SecureGate.ManageLogin.Business;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;
using ClosedXML.Excel;

namespace SecureGate.ManageLogin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : BaseAPIController
    {
        protected override EnumLoggerType LoggerName
        {
            get { return EnumLoggerType.Login; }
        }



        [HttpPost]
        [Route("CreateCampaign")]
        public IActionResult CreateCampaign(dynamic jsonInfo)
        {
            try
            {
                var data = Convert.ToString(jsonInfo);
                JObject parsrobject = JObject.Parse(data);
                LoggerInstance.Information("CreateCampaign Call Started", 5002);
                string Input = JsonConvert.SerializeObject(parsrobject);
                dynamic returnObject = new JObject();
                LoggerInstance.Information("CreateCampaign Input : " + Input, 5002);
                ICampaignManager objCampaignManager = new CampaignManager(LoggerInstance);
                returnObject = objCampaignManager.CreateCampaign(Input);
                LoggerInstance.Information("CreateCampaign Call End", 5002);
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("CreateCampaign :" + ex.Message, 5004);
                return StatusCode(500);
            }

        }


        [HttpPost]
        [Route("MappedCampaignToUser")]
        public IActionResult MappedCampaignToUser(dynamic jsonInfo)
        {
            try
            {
                var data = Convert.ToString(jsonInfo);
                JObject parsrobject = JObject.Parse(data);
                LoggerInstance.Information("MappedCampaignToUser Call Started", 5002);
                string Input = JsonConvert.SerializeObject(parsrobject);
                dynamic returnObject = new JObject();
                LoggerInstance.Information("MappedCampaignToUser Input : " + Input, 5002);
                ICampaignManager objCampaignManager = new CampaignManager(LoggerInstance);
                returnObject = objCampaignManager.MappedCampaignToUser(Input);
                LoggerInstance.Information("MappedCampaignToUser Call End", 5002);
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("CreateCampaign :" + ex.Message, 5004);
                return StatusCode(500);
            }

        }



        [HttpPost]
        [Route("GetCampaignData")]
        public IActionResult LoadQueueDetails(dynamic jsonInfo)
        {
            try
            {
                var data = Convert.ToString(jsonInfo);
                JObject parsrobject = JObject.Parse(data);
                LoggerInstance.Information("GetCampaignData Call Started", 5002);
                string Input = JsonConvert.SerializeObject(parsrobject);
                dynamic returnObject = new JObject();
                LoggerInstance.Information("GetCampaignData Input : " + Input, 5002);
                ICampaignManager objCampaignManager = new CampaignManager(LoggerInstance);
                returnObject = objCampaignManager.GetCampaignData(Input);
                LoggerInstance.Information("GetCampaignData Call End", 5002);
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("GetCampaignData :" + ex.Message, 5004);
                return StatusCode(500);
            }

        }


        [HttpPost]
        [Route("SearchUserDetails")]
        public IActionResult SearchUserDetails(dynamic jsonInfo)
        {
            try
            {
                var data = Convert.ToString(jsonInfo);
                JObject parsrobject = JObject.Parse(data);
                LoggerInstance.Information("SearchUserDetails Call Started", 5002);
                string Input = JsonConvert.SerializeObject(parsrobject);
                dynamic returnObject = new JObject();
                LoggerInstance.Information("SearchUserDetails Input : " + Input, 5002);
                ICampaignManager objCampaignManager = new CampaignManager(LoggerInstance);
                returnObject = objCampaignManager.SearchUserDetails(Input);
                LoggerInstance.Information("SearchUserDetails Call End", 5002);
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                LoggerInstance.Error("SearchUserDetails :" + ex.Message, 5004);
                return StatusCode(500);
            }

        }

        public static DataTable GetDataFromExcel(string path, dynamic worksheet)
        {
            //Save the uploaded Excel file.


            DataTable dt = new DataTable();
            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            if (!string.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            else
                            {
                                break;
                            }
                        }
                        firstRow = false;
                    }
                    else
                    {
                        int i = 0;
                        DataRow toInsert = dt.NewRow();
                        foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                        {
                            try
                            {
                                toInsert[i] = cell.Value.ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            i++;
                        }
                        dt.Rows.Add(toInsert);
                    }
                }
                return dt;
            }
        }

        [HttpPost]
        [Route("UploadCampaignUserList")]
        public IActionResult UploadCampaignUserList(dynamic Json)
        {
            bool isValidateAllColumn = true;
            LoggerInstance.Information("UploadCampaignUserList Call Started", 5002);
            dynamic rspjsonobj = new ExpandoObject();
            dynamic MainObject = new ExpandoObject();
            dynamic userListObject = new ExpandoObject();
       
            userListObject.Type = "Customer";
            try
            {
                rspjsonobj.IsValidExcel = true;
                rspjsonobj.ErrorMessage = "";
                string output = "";

                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

                var root = configurationBuilder.Build();
                string sPath = root.GetSection("FilePathExport").Value;

                if (sPath != null && !Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }

                LoggerInstance.Information("UploadCampaignUserList sPath: " + sPath, 5002);
                string FileName = Guid.NewGuid().ToString() + Json.Json.FileName.Value;
                LoggerInstance.Information("UploadCampaignUserList FileName: " + FileName, 5002);
                DataSet ds = new DataSet();
                if (!System.IO.File.Exists(Path.Combine(sPath, FileName)))
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(Path.Combine(sPath, FileName), Convert.FromBase64String(Json.Json.File.Value));

                        var workbook = new XLWorkbook(Path.Combine(sPath, FileName));
                        var ws1 = workbook.Worksheet(1);
                        var totalRows = ws1.LastRowUsed().RowNumber();
                        var totalColumns = ws1.LastColumnUsed().ColumnNumber();
                        

                        string firstColumn = ws1.Row(1).Cell(1).Value.ToString();
                        if(!string.IsNullOrEmpty(firstColumn))
                        {
                            if (firstColumn.ToLower() != "customercode")
                            {
                                rspjsonobj.IsValidExcel = false;
                                rspjsonobj.ErrorMessage = "invalid format";
                            }
                            else
                            {
                                rspjsonobj.IsValidExcel = true;
                            }
                        }
                        else
                        {
                            rspjsonobj.IsValidExcel = false;
                            rspjsonobj.ErrorMessage = "invalid format";
                        }
                       

                        if (rspjsonobj.IsValidExcel)
                        {
                            for (int i = 2; i <= totalRows; i++)
                            {
                                int rowNumber = i;
                                string customercode = ws1.Row(rowNumber).Cell(1).Value.ToString();
                                string phonenumber = ws1.Row(rowNumber).Cell(2).Value.ToString();

                                if (string.IsNullOrEmpty(customercode))
                                {
                                    isValidateAllColumn = false;

                                    rspjsonobj.IsValidExcel = false;
                                    rspjsonobj.ErrorMessage = "Invalid Excel file.";

                                    ws1.Row(rowNumber).Cell(5).Comment.AddText("CustomerCode is missing.");
                                    ws1.Row(rowNumber).Cell(5).Style.Font.SetFontColor(XLColor.Red);

                                }

                                if (string.IsNullOrEmpty(phonenumber))
                                {
                                    isValidateAllColumn = false;

                                    rspjsonobj.IsValidExcel = false;
                                    rspjsonobj.ErrorMessage = "Invalid Excel file.";

                                    ws1.Row(rowNumber).Cell(6).Comment.AddText("PhoneNumber is missing.");
                                    ws1.Row(rowNumber).Cell(6).Style.Font.SetFontColor(XLColor.Red);

                                }
                            }

                    

                            if (isValidateAllColumn == false)
                            {
                                string fileName = DateTime.Now.ToString();
                                fileName = "CampaignUserList" + DateTime.Now.ToString("MMddyyyy-hhmm") + ".xlsx";
                                LoggerInstance.Information("UploadCampaignUserList isValidateAllColumn", 5002);
                                using (MemoryStream myMemoryStream = new MemoryStream())
                                {
                                    workbook.SaveAs(myMemoryStream);
                                    FileStream file = new FileStream(sPath + fileName, FileMode.Create, FileAccess.Write);
                                    myMemoryStream.WriteTo(file);
                                    file.Close();
                                    myMemoryStream.Close();
                                    //workbook.Save();
                                    workbook.Dispose();
                                }
                                List<dynamic> orderList = new List<dynamic>();
                                byte[] byt = null;
                                byt = System.IO.File.ReadAllBytes(sPath + fileName);

                                try
                                {

                                }
                                catch (Exception ex)
                                {
                                    var dd = ex.Message;
                                    LoggerInstance.Information("UploadCampaignUserList ReadAllBytes Exception: " + ex.StackTrace + ex.Message, 5002);
                                }

                                rspjsonobj.byt = byt;
                            }
                            else
                            {
                                DataTable userList = new DataTable();

                                try
                                {
                                    userList = GetDataFromExcel(Path.Combine(sPath, FileName), ws1);
                                    dynamic returnObject = new JObject();
                                    userListObject.UserList= DataTableToJsonObject(userList);
                                    MainObject.Json = userListObject;
                                    string Input = JsonConvert.SerializeObject(MainObject);
                                
                                    ICampaignManager objCampaignManager = new CampaignManager(LoggerInstance);
                                    dynamic outputList = objCampaignManager.CompanyAndUserDetails(Input);

                                    //string outputSerialize = JsonConvert.SerializeObject(outputList);
                                    //JObject outputobj = (JObject)JsonConvert.DeserializeObject(outputSerialize);
                                    //var enquiryObject = outputobj["Json"]["ProfileList"];

                                    List<dynamic> UserList = new List<dynamic>();
                                    if (!string.IsNullOrEmpty(Convert.ToString(outputList["Json"]["ProfileList"])))
                                    {
                                        //Convert To List with object dynamic
                                        UserList = ((JArray)outputList["Json"]["ProfileList"]).ToObject<List<dynamic>>();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    var ff = ex.Message;
                                }



                                

                                LoggerInstance.Information("UploadCampaignUserList DataTableToJsonObject start", 5002);
                                rspjsonobj.ItemBasePriceList = DataTableToJsonObject(userList);
                                LoggerInstance.Information("UploadCampaignUserList DataTableToJsonObject end", 5002);
                                rspjsonobj.IsValidExcel = true;
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        rspjsonobj.IsValidExcel = false;
                        rspjsonobj.ErrorMessage = "Invalid Excel file.";
                        rspjsonobj.InnerMessage = ex.StackTrace + ex.Message;

                        LoggerInstance.Information("UploadCampaignUserList Invalid Excel Exception: " + ex.StackTrace + ex.Message, 5002);

                    }
                    System.IO.File.Delete(Path.Combine(sPath, FileName));
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Information("UploadItemPriceList Exception: " + ex.StackTrace + ex.Message, 5002);
            }

            return Ok(rspjsonobj);
        }

        public object CreatingUserLoginList(dynamic outputList)
        {
            dynamic MainObject = new ExpandoObject();
     
            dynamic userListObject = new ExpandoObject();

            MainObject.Json = userListObject;

            return MainObject;
        }






            public object DataTableToJsonObject(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

    }
}
