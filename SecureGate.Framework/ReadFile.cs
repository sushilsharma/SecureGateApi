using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.IO;
using System.Text.RegularExpressions;
using Excel;
using System.Data.OleDb;

namespace SecureGate.Framework
{
    public class ReadFile
    {
        DataSet dsFileData;
        string error = string.Empty;
        /// <summary>
        /// read the provided text file and set the data to upload
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet ReadTxtFile(string filePath, string fieldSeperator)
        {
            dsFileData = new DataSet();
            DataTable dt = new DataTable();
            String strLine = String.Empty;
            StreamReader srdr = null;
            //reading the file stream and adding the values to the table
            try
            {
                srdr = File.OpenText(filePath);
                strLine = srdr.ReadLine();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (strLine == null)
            {
                error = "File is empty";
            }

            //Create Table Columns using first line
            string[] strVals = strLine.Split(fieldSeperator.ToCharArray());
            int totalNumberOfValues = strVals.Length;
            for (int colCount = 0; colCount < totalNumberOfValues; colCount++)
            {
                dt.Columns.Add(strVals[colCount]);
            }

            // Add the file data to the dataset
            do
            {
                strLine = srdr.ReadLine();
                if (strLine == null)
                {
                    break;
                }
                AddDataRowToTable(strLine, dt);
            } while (true);

            dsFileData = new DataSet();
            // Adding the newly created table to the dataset
            dsFileData.Tables.Add(dt);
            srdr.Close();
            File.Delete(filePath);

            return dsFileData;
        }

        /// <summary>
        /// Add Data Row in dataset based on value from .csv file
        /// </summary>
        /// <param name="strCSVLine"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataRow AddDataRowToTable(string csvLine, DataTable dt)
        {
            string[] vals = csvLine.Split(new char[] { ',' });
            DataRow drow = dt.NewRow();
            for (int colCount = 0; colCount < dt.Columns.Count; colCount++)
            {
                drow[colCount] = vals[colCount];
            }
            dt.Rows.Add(drow);
            return drow;
        }

        /// <summary>
        /// Read the provided XML file and set the data to upload
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet ReadXmlFile(string filePath)
        {
            dsFileData = new DataSet();
            if (filePath.Trim() != "")
            {
                dsFileData = new DataSet();
                // Propulating the dataset with the XMl contents
                try
                {
                    dsFileData.ReadXml(filePath.Trim());
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

            }
            return dsFileData;
        }

        /// <summary>
        /// Read the provided XML file and set the data to upload
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet ReadExcel(string filePath)
        {
            DataSet res = new DataSet();
            if (filePath.Trim() != "")
            {
                try
                {
                    IExcelDataReader iExcelDataReader = null;
                    FileInfo fileInfo = new FileInfo(filePath);
                    string file = fileInfo.Name;
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    if (file.Split('.')[1].Equals("xls") || file.Split('.')[1].Equals("XLS"))
                    {
                        iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(fs);
                    }
                    else if (file.Split('.')[1].Equals("csv") || file.Split('.')[1].Equals("CSV"))
                    {
                        iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                        //DataTable csvDatatable = ConvertCSVtoDataTable(filePath);
                    }

                    else if (file.Split('.')[1].Equals("xlsx") || file.Split('.')[1].Equals("XLSX"))
                    {
                        iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    }
                    else if (file.Split('.')[1].Equals("xlsm") || file.Split('.')[1].Equals("XLSM"))
                    {
                        iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    }
                    fs.Dispose();
                    iExcelDataReader.IsFirstRowAsColumnNames = true;

                    DataSet dsUnUpdated = new DataSet();
                    dsUnUpdated = iExcelDataReader.AsDataSet();
                    iExcelDataReader.Close();
                    if (dsUnUpdated != null && dsUnUpdated.Tables.Count > 0)
                    {
                        res = dsUnUpdated;
                    }
                    else
                    {
                        // "No data";
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            return res;
        }




        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        //public DataSet ReadExcelFile(string filePath)
        //{

        //    dsFileData = new DataSet();
        //    if (filePath.Trim() != "")
        //    {
        //        try
        //        {
        //            OleDbConnection oconn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Replace("\\\\", "\\") + ";Extended Properties=Excel 8.0");
        //            OleDbDataAdapter oda = new OleDbDataAdapter("select * from [Sheet1$]", oconn);
        //            oda.Fill(dsFileData);
        //            File.Delete(filePath);

        //        }
        //        catch (Exception ex)
        //        {
        //            error = ex.Message;
        //        }
        //    }
        //    return dsFileData;
        //}


        public DataSet ConvertExcelToDataTable(string FileName)
        {
            DataSet res = null;
            try
            {
                IExcelDataReader iExcelDataReader = null;
                FileInfo fileInfo = new FileInfo(FileName);
                string file = fileInfo.Name;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                if (file.Split('.')[1].Equals("xls"))
                {
                    iExcelDataReader = ExcelReaderFactory.CreateBinaryReader(fs);
                }
                else if (file.Split('.')[1].Equals("xlsx"))
                {
                    iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                }
                else if (file.Split('.')[1].Equals("xlsm"))
                {
                    iExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                }
                fs.Dispose();
                iExcelDataReader.IsFirstRowAsColumnNames = true;

                DataSet dsUnUpdated = new DataSet();
                dsUnUpdated = iExcelDataReader.AsDataSet();
                iExcelDataReader.Close();
                if (dsUnUpdated != null && dsUnUpdated.Tables.Count > 0)
                {
                    DataRow[] drRow = dsUnUpdated.Tables[0].Select("[Load Date] is not null");
                    DataTable dttable = drRow.CopyToDataTable();
                    DataSet dsLoadFilter = new DataSet();
                    dsLoadFilter.Tables.Add(dttable);

                    //for (int i = dsUnUpdated.Tables[0].Rows.Count - 1; i >= 0; i--)
                    //{
                    //    if (dsUnUpdated.Tables[0].Rows[i][1] == null)
                    //        dsUnUpdated.Tables[0].Rows[i].Delete();
                    //}


                    dsLoadFilter.Tables[0].Columns.Add("RowID");
                    foreach (DataRow dr in dsLoadFilter.Tables[0].Rows)
                    {
                        dr["RowID"] = Guid.NewGuid().ToString();
                    }

                    res = dsLoadFilter;
                }
                else
                {
                    // "No data";
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            return res;
        }
    }
}
