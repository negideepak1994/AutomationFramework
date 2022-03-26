using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.FrameworkComponents
{
    public class CSVReader : GlobalVariable
    {
        public static List<Dictionary<string, string>> CSVInputCollection = new List<Dictionary<string, string>>();

        public class HeaderConstants
        {
            public const string RowNumber = "RowNumber";
        }

        public static DataTable ReadCSV(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                //adding additional columns to capture row number
                dt.Columns.Add(HeaderConstants.RowNumber);
                int currentRowCount = 0;
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 0)
                    {
                        currentRowCount++;
                    }
                    DataRow dr = dt.NewRow();
                    dr[HeaderConstants.RowNumber] = currentRowCount;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static List<Dictionary<string, string>> ReadCSV(string filePath, string testCaseID)
        {
            DataTable table = ReadCSV(filePath);
            CSVInputCollection = table.AsEnumerable().Where(row => row.Field<string>("TestCaseID") == testCaseID).Select(

                row => table.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString()
                    )).ToList();
            return CSVInputCollection;
        }
    }
}
