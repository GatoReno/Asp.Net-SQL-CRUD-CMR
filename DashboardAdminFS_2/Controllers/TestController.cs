using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.XlsIO;
using System.IO;
using Syncfusion.Drawing;
using Google.Apis.Sheets.v4.Data;
using GemBox.Spreadsheet;
using ExcelDataReader;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace DashboardAdminFS_2.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        

        public ActionResult T2() {

            ViewBag.IDXFC = 10;
            return View();
        }

        [HttpPost]
        public void EXCELREAD(HttpPostedFileBase fx)
        {
            /*
            Application excel = new Application();
            Workbook sheet = excel.Workbooks.Open(filePatht);
            Worksheet x = excel.ActiveSheet as Worksheet;
            _Worksheet xlWorksheet = sheet.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;*/

            var x = fx;

            Stream stream = fx.InputStream;
            IExcelDataReader reader = null;

            if (fx.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (fx.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                ModelState.AddModelError("File", "This file format is not supported");
                    
            }

            var x1 = reader; //Hasta aquí obtengo todo el archivo


            DataSet hojas = reader.AsDataSet();

            foreach (System.Data.DataTable tabla in hojas.Tables)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        // leer item
                    }
                }
            }
            }
    }
}