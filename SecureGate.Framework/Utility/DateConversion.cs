using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Framework.Utility
{
    public class DateConversion
    {
        public static DateTime ConvertToDateTime(string GetDateTime)
        {
            DateTime dtFinaldate; string sDateTime;

            if (GetDateTime != "")
            {
                try { dtFinaldate = Convert.ToDateTime(GetDateTime); }
                catch (Exception)
                {

                    string[] separators = new[] { ".", "-", "/" };
                    string[] sDate = GetDateTime.Split(separators, StringSplitOptions.None);
                    string[] sapratelast = sDate[2].Split(' ');
                    if (Convert.ToInt32(sDate[0]) > 12 && Convert.ToInt32(sapratelast[0]) < 12)
                    {
                        if (sapratelast.Length == 3)
                        {
                            sDateTime = sDate[1] + '/' + sapratelast[0] + '/' + sDate[0] + " " + sapratelast[1] + " " +
                                        sapratelast[2];
                        }
                        else if (sapratelast.Length == 2)
                        {
                            sDateTime = sDate[1] + '/' + sapratelast[0] + '/' + sDate[0] + " " + sapratelast[1];
                        }
                        else
                        {
                            sDateTime = sDate[1] + '/' + sapratelast[0] + '/' + sDate[0];
                        }
                    }
                    else
                    {
                        sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                    }

                    dtFinaldate = Convert.ToDateTime(sDateTime);
                }
            }
            else
            {
                dtFinaldate = Convert.ToDateTime("01/01/0001");
            }
            return dtFinaldate;
        }
    }
}
