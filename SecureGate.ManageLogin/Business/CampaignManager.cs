using SecureGate.APIController.Framework.AppLogger;
using SecureGate.Framework;
using SecureGate.Framework.DataAccess;
using SecureGate.Framework.PasswordUtility;
using SecureGate.Framework.Serializer;
using SecureGate.Framework.Utility;
//using glassRUNProduct.DataAccess.Common;
using SecureGate.ManageLogin.DataAccess;
using SecureGate.ManageLogin.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace SecureGate.ManageLogin.Business
{
    public class CampaignManager : ICampaignManager
    {
        BaseAppLogger _loggerInstance;
        public CampaignManager(BaseAppLogger loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }



        public string GetWhereClauseForOrderList(dynamic SearchDTO)
        {
            string whereClause = "";

            string jsonInput = Convert.ToString(SearchDTO);
            JObject jsonObj = JObject.Parse(jsonInput);
            var SearchParameterList = jsonObj["SearchParameterList"].ToList();


            foreach (var esp in SearchParameterList)
            {

                if (esp["fieldType"].ToString() == "string")
                {

                    if (esp["operatorName"].ToString() == "contains")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " LIKE '%" + esp["fieldValue"].ToString() + "%'";
                    }
                    else if (esp["operatorName"].ToString() == "notcontains")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " NOT LIKE '%" + esp["fieldValue"].ToString() + "%'";
                    }
                    else if (esp["operatorName"].ToString() == "startswith")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " LIKE '" + esp["fieldValue"].ToString() + "%'";
                    }
                    else if (esp["operatorName"].ToString() == "endswith")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " LIKE '%" + esp["fieldValue"].ToString() + "'";
                    }
                    else if (esp["operatorName"].ToString() == "=")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " = '" + esp["fieldValue"].ToString() + "'";
                    }
                    else if (esp["operatorName"].ToString() == "<>")
                    {
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " <> '" + esp["fieldValue"].ToString() + "'";
                    }
                    else if (esp["operatorName"].ToString() == "Include")
                    {
                        esp["fieldName"] = esp["fieldName"].ToString().TrimEnd(',');
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " IN (" + esp["fieldValue"].ToString() + ")";
                    }
                    else if (esp["operatorName"].ToString() == "Exclude")
                    {
                        esp["fieldName"] = esp["fieldName"].ToString().TrimEnd(',');
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " NOT In (" + esp["fieldValue"].ToString() + ")";
                    }
                    else if (esp["operatorName"].ToString() == "in")
                    {
                        esp["fieldName"] = esp["fieldName"].ToString().TrimEnd(',');
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " IN (" + esp["fieldValue"].ToString() + ")";
                    }
                    else if (esp["operatorName"].ToString() == "notin")
                    {
                        esp["fieldName"] = esp["fieldName"].ToString().TrimEnd(',');
                        whereClause = whereClause + " and " + esp["fieldName"].ToString() + " NOT In (" + esp["fieldValue"].ToString() + ")";
                    }

                }

                if (esp["fieldType"].ToString() == "date")
                {

                    if (esp["operatorName"].ToString() == "=")
                    {
                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) = CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }
                    else if (esp["operatorName"].ToString() == "<>")
                    {
                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) <> CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }
                    else if (esp["operatorName"].ToString() == "<")
                    {
                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) < CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }
                    else if (esp["operatorName"].ToString() == ">")
                    {
                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) > CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }
                    else if (esp["operatorName"].ToString() == "<=")
                    {

                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) <= CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }
                    else if (esp["operatorName"].ToString() == ">=")
                    {
                        whereClause = whereClause + " and CONVERT(date," + esp["fieldName"].ToString() + ",103) >= CONVERT(date,'" + esp["fieldValue"].ToString() + "',103)";
                    }


                }




            }

            return whereClause;
        }



        public JObject SearchUserDetails(dynamic Input)
        {
            dynamic returnObject = new JObject();
            string whereClause = "";

            _loggerInstance.Information("Portal SearchUserDetails: building where clause Start ", 502);
            whereClause = GetWhereClauseForOrderList(Input);

            string jsonInput = Convert.ToString(Input);
            JObject jsonObj = JObject.Parse(jsonInput);
            jsonObj["whereClause"] = whereClause;



            _loggerInstance.Information("Portal SearchUserDetails: building where clause End ", 502);

            DataSet dsorderList = new DataSet();

            _loggerInstance.Information("Portal SearchUserDetails: Get data from database Start ", 502);
            dsorderList = CampaignDataAccessManager.SearchUserDetails<DataSet>(jsonObj);
            _loggerInstance.Information("Portal SearchUserDetails: Get data from database End ", 502);

            if (dsorderList.Tables.Count == 2)
            {
                DataTable dt1 = dsorderList.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    DataRow rowDt1 = dt1.Rows[0];
                    if (!string.IsNullOrEmpty(rowDt1[0].ToString()))
                    {
                        string xmlorderList = rowDt1[0].ToString();
                        if (xmlorderList != null)
                        {
                            _loggerInstance.Information("Portal SearchUserDetails: Serializtion of order list to DTO Start ", 502);


                            XDocument doc = XDocument.Parse(xmlorderList); //or XDocument.Load(path)
                            string jsonText = JsonConvert.SerializeXNode(doc);
                            returnObject = JsonConvert.DeserializeObject<JObject>(jsonText);




                            _loggerInstance.Information("Portal SearchUserDetails: Serializtion of order list to DTO End ", 502);

                        }
                    }

                }

                DataTable dt2 = dsorderList.Tables[1];
                if (dt2.Rows.Count > 0)
                {
                    DataRow rowDt2 = dt2.Rows[0];
                    if (!string.IsNullOrEmpty(rowDt2["TotalCount"].ToString()))
                    {
                        if (Convert.ToInt64(rowDt2["TotalCount"].ToString()) != 0)
                        {
                            _loggerInstance.Information("Portal SearchUserDetails: Set Total Count, Status, Class Start ", 502);

                            string jsonInputq = Convert.ToString(returnObject);
                            JObject jsonObjqw = JObject.Parse(jsonInputq);

                            var CampaignList = jsonObjqw["Profile"]["ProfileList"].ToList();

                            for (int i = 0; i < CampaignList.Count(); i++)
                            {
                                jsonObjqw["Profile"]["ProfileList"][i]["TotalCount"] = Convert.ToInt64(rowDt2["TotalCount"].ToString());
                            }
                            returnObject = jsonObjqw;
                            _loggerInstance.Information("Portal SearchUserDetails: Set Total Count, Status, Class End ", 502);
                        }
                    }
                }

            }

            _loggerInstance.Information("Portal SearchUserDetails: SearchUserDetails API End ", 502);
            return returnObject;
        }





        public JObject GetCampaignData(dynamic Input)
        {
            dynamic returnObject = new JObject();
            string whereClause = "";

            _loggerInstance.Information("Portal GetCampaignData: building where clause Start ", 502);
            whereClause = GetWhereClauseForOrderList(Input);

            string jsonInput = Convert.ToString(Input);
            JObject jsonObj = JObject.Parse(jsonInput);
            jsonObj["whereClause"] = whereClause;



            _loggerInstance.Information("Portal GetCampaignData: building where clause End ", 502);

            DataSet dsorderList = new DataSet();

            _loggerInstance.Information("Portal GetCampaignData: Get data from database Start ", 502);
            dsorderList = CampaignDataAccessManager.GetCampaignData<DataSet>(jsonObj);
            _loggerInstance.Information("Portal GetCampaignData: Get data from database End ", 502);

            if (dsorderList.Tables.Count == 2)
            {
                DataTable dt1 = dsorderList.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    DataRow rowDt1 = dt1.Rows[0];
                    if (!string.IsNullOrEmpty(rowDt1[0].ToString()))
                    {
                        string xmlorderList = rowDt1[0].ToString();
                        if (xmlorderList != null)
                        {
                            _loggerInstance.Information("Portal GetCampaignData: Serializtion of order list to DTO Start ", 502);


                            XDocument doc = XDocument.Parse(xmlorderList); //or XDocument.Load(path)
                            string jsonText = JsonConvert.SerializeXNode(doc);
                            returnObject = JsonConvert.DeserializeObject<JObject>(jsonText);




                            _loggerInstance.Information("Portal GetCampaignData: Serializtion of order list to DTO End ", 502);

                        }
                    }

                }

                DataTable dt2 = dsorderList.Tables[1];
                if (dt2.Rows.Count > 0)
                {
                    DataRow rowDt2 = dt2.Rows[0];
                    if (!string.IsNullOrEmpty(rowDt2["TotalCount"].ToString()))
                    {
                        if (Convert.ToInt64(rowDt2["TotalCount"].ToString()) != 0)
                        {
                            _loggerInstance.Information("Portal Order: Set Total Count, Status, Class Start ", 502);

                            string jsonInputq = Convert.ToString(returnObject);
                            JObject jsonObjqw = JObject.Parse(jsonInputq);

                            var CampaignList = jsonObjqw["Campaign"]["CampaignList"].ToList();

                            for (int i = 0; i < CampaignList.Count(); i++)
                            {
                                jsonObjqw["Campaign"]["CampaignList"][i]["TotalCount"] = Convert.ToInt64(rowDt2["TotalCount"].ToString());
                            }
                            returnObject = jsonObjqw;
                            _loggerInstance.Information("Portal Order: Set Total Count, Status, Class End ", 502);
                        }
                    }
                }

            }

            _loggerInstance.Information("Portal GetCampaignData: GetCampaignData API End ", 502);
            return returnObject;
        }




        public JObject CompanyAndUserDetails(string Input)
        {
            string enquiryDetails = CampaignDataAccessManager.CompanyAndUserDetails(Input);
            dynamic returnObject = new JObject();

            if (!string.IsNullOrEmpty(enquiryDetails))
            {
                dynamic Json = JsonConvert.DeserializeObject(enquiryDetails);

                _loggerInstance.Information("CompanyAndUserDetails Manager function call started", 502);

                JArray enquiryDetailResponse = new JArray();

                try
                {
                    returnObject = Json;
                    _loggerInstance.Information("CompanyAndUserDetails Manager function call ended", 502);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("CompanyAndUserDetails Exception:" + ex.Message);
                }

            }
            return returnObject;
        }

        public JObject CreateCampaign(string Input)
        {
            string enquiryDetails = CampaignDataAccessManager.CreateCampaign(Input);
            dynamic returnObject = new JObject();

            if (!string.IsNullOrEmpty(enquiryDetails))
            {
                dynamic Json = JsonConvert.DeserializeObject(enquiryDetails);

                _loggerInstance.Information("CreateCampaign Manager function call started", 502);

                JArray enquiryDetailResponse = new JArray();

                try
                {
                    returnObject = Json;
                    _loggerInstance.Information("CreateCampaign Manager function call ended", 502);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("CreateCampaign Exception:" + ex.Message);
                }

            }
            return returnObject;
        }

        public JObject MappedCampaignToUser(string Input)
        {
            string enquiryDetails = CampaignDataAccessManager.MappedCampaignToUser(Input);
            dynamic returnObject = new JObject();

            if (!string.IsNullOrEmpty(enquiryDetails))
            {
                dynamic Json = JsonConvert.DeserializeObject(enquiryDetails);

                _loggerInstance.Information("MappedCampaignToUser Manager function call started", 502);

                JArray enquiryDetailResponse = new JArray();

                try
                {
                    returnObject = Json;
                    _loggerInstance.Information("MappedCampaignToUser Manager function call ended", 502);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("MappedCampaignToUser Exception:" + ex.Message);
                }

            }
            return returnObject;
        }


    }
}
