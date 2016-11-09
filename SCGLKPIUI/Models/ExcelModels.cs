using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
//using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace SCGLKPIUI.Models
{
    public class ExcelModels
    {
        /// <summary>
        /// Open excel file with specified path
        /// </summary>
        /// <param name="OpenFileName"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static DataTable openExcel(string OpenFileName, int sheet)
        {
            FileInfo newFile = new FileInfo(OpenFileName);
            try
            {
                using (var package = new ExcelPackage(newFile))
                {
                    // get the first worksheet in the workbook
                    ExcelWorksheet ws1 = package.Workbook.Worksheets[sheet];
                    DataTable tbl = new DataTable();
                    tbl.TableName = "org";
                    bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
                    foreach (var firstRowCell in ws1.Cells[1, 1, 1, 20])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (var rowNum = startRow; rowNum <= ws1.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws1.Cells[rowNum, 1, rowNum, 20];
                        DataRow row = tbl.NewRow();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                        tbl.Rows.Add(row);
                    }
                    return tbl;
                } // the using 
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public void DumpExcel(DataTable tbl, String FileName)
        {
            FileName = FileName + ".xlsx";
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Templates");

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true);

                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells["A1:C1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                //Somehow it dumps to excel auto download ... I think
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + FileName);
                System.Web.HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                System.Web.HttpContext.Current.Response.End();
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}