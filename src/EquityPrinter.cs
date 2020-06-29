using System;
using TimHanewich.Csv;
using Yahoo.Finance;
using System.Reflection;
using System.Collections.Generic;

namespace Yahoo.Finance.Printing
{
    public static class EquityPrinter
    {
        public static string PrintToCsvContent(this Equity[] equities)
        {
            if (equities.Length == 0)
            {
                throw new Exception("This equity array is empty.");
            }

            //Get the properties of each
            PropertyInfo[] props = equities[0].GetType().GetProperties();
            List<PropertyInfo> CoreProperties = new List<PropertyInfo>();
            foreach (PropertyInfo pi in props)
            {
                object thiso = pi.GetValue(equities[0]);
                if (thiso.GetType().IsClass == false || thiso.GetType() == typeof(string))
                {
                    CoreProperties.Add(pi);
                }
            }

            //Get the properties of summary
            List<PropertyInfo> SummaryProperties = new List<PropertyInfo>();
            if (equities[0].Summary != null)
            {
                PropertyInfo[] props_summary = equities[0].Summary.GetType().GetProperties();
                
                //Summary
                foreach (PropertyInfo pi in props_summary)
                {
                    object thiso = pi.GetValue(equities[0].Summary);
                    if (thiso.GetType().IsClass == false || thiso.GetType() == typeof(string))
                    {
                        SummaryProperties.Add(pi);
                    }
                }
            }
            

            //Get the properties of statistics
            List<PropertyInfo> StatisticsProperties = new List<PropertyInfo>();
            if (equities[0].Statistics != null)
            {
                PropertyInfo[] props_statistics = equities[0].Statistics.GetType().GetProperties();
                
                foreach (PropertyInfo pi in props_statistics)
                {
                    object thiso = pi.GetValue(equities[0].Statistics);
                    if (thiso.GetType().IsClass == false || thiso.GetType() == typeof(string))
                    {
                        StatisticsProperties.Add(pi);
                    }
                }
            }
            
            
            



            CsvFile csv = new CsvFile();

            //Set up header
            DataRow dr_header = csv.AddNewRow();

            //Write headers
            foreach (PropertyInfo pi in CoreProperties)
            {
                dr_header.Values.Add(pi.Name);
            }
            foreach (PropertyInfo pi in SummaryProperties)
            {
                dr_header.Values.Add(pi.Name);
            }
            foreach (PropertyInfo pi in StatisticsProperties)
            {
                dr_header.Values.Add(pi.Name);
            }


            //Write data
            foreach (Equity e in equities)
            {
                DataRow dr = csv.AddNewRow();

                //Core
                foreach (PropertyInfo pi in CoreProperties)
                {
                    try
                    {
                        dr.Values.Add(pi.GetValue(e).ToString());
                    }
                    catch
                    {
                        dr.Values.Add("-");
                    }
                    
                }

                //Summary
                foreach (PropertyInfo pi in SummaryProperties)
                {
                    try
                    {
                        dr.Values.Add(pi.GetValue(e.Summary).ToString());
                    }
                    catch
                    {
                        dr.Values.Add("-");
                    }
                    
                }

                //Statistics
                foreach (PropertyInfo pi in StatisticsProperties)
                {
                    try
                    {
                        dr.Values.Add(pi.GetValue(e.Statistics).ToString());
                    }
                    catch
                    {
                        dr.Values.Add("-");
                    }
                    
                }

                

                



            }

            return csv.GenerateAsCsvFileContent();

        }
    }
}