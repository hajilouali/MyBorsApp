using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Models.Api.Response;
using Models.Site.Models.Chart;
using Models.Site.Models.contracts;
using Models.Site.ViewModels;
using MoreLinq;
using MyBorsApp.Areas.MainPanel.Models;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using Tools;

namespace MyBorsApp.Areas.MainPanel.Controllers
{
    public class CheartController : Controller
    {
        // GET: MainPanel/Cheart
        MyContext db = new MyContext();

        PhoenixFutureApiSdk.FutureSdk sdk = new PhoenixFutureApiSdk.FutureSdk("bmi");
        public ActionResult Index()
        {
            ViewBag.symbols = db.ContractCellslogs.Select(a => a.symbol).Distinct().OrderBy(p => p).ToList();
            return View();
        }
        public ActionResult chart(List<string> symbol, string starTime, string endDateTime)
        {
            ViewBag.symbol = JsonConvert.SerializeObject(symbol);
            ViewBag.start = starTime;
            ViewBag.end = endDateTime;
            List<ContractCellslog> r = new List<ContractCellslog>();
            List<dataPointss> dataPointss = new List<dataPointss>();
            List<ChartInformation> listchartinformation = new List<ChartInformation>();
            int d = 0;

            if (symbol.Count == 2)
            {
                d = 1;
            }

            foreach (var VARIABLE in symbol)
            {
                r = db.ContractCellslogs.Where(p => p.symbol == VARIABLE).ToList();

                if (starTime != null & endDateTime != null)
                {
                    var s = Convert.ToDateTime(starTime + " 08:00:00");
                    var e = Convert.ToDateTime(endDateTime + " 22:00:00");
                    r = r.Where(p => p.DateTimeCreatLog > s && p.DateTimeCreatLog < e).ToList();
                }
                else
                {
                    var s = Convert.ToDateTime(DateTime.Now.toshamsi() + " 08:00:00");
                    r = r.Where(p => p.DateTimeCreatLog >s).ToList();
                }

                if (r.Count > 0)
                {

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var ccc in r)
                    {
                        dataPoints.Add(new DataPoint(ccc.DateTimeCreatLog.ToString("g"), ccc.lastTradedPrice));

                    }
                    dataPointss i = new dataPointss();
                    i.name = r.FirstOrDefault().symbol;
                    i.type = "line";
                    i.showInLegend = true;
                    i.dataPoints = dataPoints;
                    dataPointss.Add(i);
                    ChartInformation calinformation = new ChartInformation()
                    {
                        Title = i.name,
                        HightPrice = i.dataPoints.Max(p => p.Y),
                        LowPrice = i.dataPoints.Min(p => p.Y),
                        Avarage = i.dataPoints.Average(p => p.Y)
                    };
                    listchartinformation.Add(calinformation);
                }

            }

            if (d == 1)
            {
                List<DataPoint> dataPoints = new List<DataPoint>();
                List<dataPointss> listcalculat = new List<dataPointss>();
                dataPointss calculate = new dataPointss();
                calculate.name = "حد فاصله";
                calculate.type = "line";
                calculate.showInLegend = true;
                int c = 0;
                var items = dataPointss.Select(p => p.dataPoints).ToArray();
                List<double> firstvalue = new List<double>();
                List<double> secendValoue = new List<double>();
                List<string> times = new List<string>();
                if (dataPointss.Count > 0)
                {
                    foreach (var VARIABLE in dataPointss.FirstOrDefault().dataPoints)
                    {
                        firstvalue.Add(VARIABLE.Y);
                        times.Add(VARIABLE.label);
                    }

                    foreach (var VARIABLE in dataPointss.Skip(1).First().dataPoints)
                    {
                        secendValoue.Add(VARIABLE.Y);
                    }
                    List<double> numbercheker = new List<double>();
                    numbercheker.Add(0);
                    numbercheker.Add(1);
                    numbercheker.Add(-1);
                    Boolean ischek = false;
                    foreach (var v in numbercheker)
                    {
                        if (firstvalue.Count - secendValoue.Count == v || secendValoue.Count - firstvalue.Count == v)
                        {
                            ischek = true;

                        }
                    }

                    if (firstvalue.FirstOrDefault() > secendValoue.FirstOrDefault())
                    {

                        for (int i = 0; i < firstvalue.Count - 1; i++)
                        {
                            try
                            {
                                DataPoint item = new DataPoint();
                                item.Y = firstvalue[i] - secendValoue[i];
                                item.label = times[i];
                                dataPoints.Add(item);
                            }
                            catch
                            {

                            }

                        }
                    }

                    if (firstvalue.FirstOrDefault() < secendValoue.FirstOrDefault())
                    {
                        for (int i = 0; i < firstvalue.Count - 1; i++)
                        {
                            try
                            {
                                DataPoint item = new DataPoint();
                                item.Y = secendValoue[i] - firstvalue[i];
                                item.label = times[i];
                                dataPoints.Add(item);
                            }
                            catch
                            {

                            }

                        }
                    }



                    calculate.dataPoints = dataPoints;
                    List<ChartInformation> calinformation = new List<ChartInformation>();
                    calinformation.Add(new ChartInformation()
                    {
                        Title = "حد فاصله",
                        HightPrice = calculate.dataPoints.Max(p => p.Y),
                        Avarage = calculate.dataPoints.Average(p => p.Y),
                        LowPrice = calculate.dataPoints.Min(p => p.Y)
                    });
                    listcalculat.Add(calculate);
                    ViewBag.calculateinformation = calinformation;
                    ViewBag.calulate = JsonConvert.SerializeObject(listcalculat);
                    ViewBag.valculateishave = d;
                }

            }

            ViewBag.chartinformation = listchartinformation;
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPointss);
            int cont = r.Count;
            return View();
        }
        
        public void ExportToExel(List<string> symbol, string starTime, string endDateTime)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                var s = Convert.ToDateTime(starTime + " 08:00:00");
                var e = Convert.ToDateTime(endDateTime + " 22:00:00");
                //Set the default application version as Excel 2016
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2010;

                //Create a workbook with a worksheet
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(symbol.Count);

                //Access first worksheet from the workbook instance
                int sheetnumber = 0;
                string filename = "";

                foreach (var item in symbol)
                {
                    filename += item + "-";
                    IWorksheet worksheet = workbook.Worksheets[sheetnumber];
                    worksheet.Name = item;
                    worksheet.Range["A1"].Text = "تاریخ و زمان";
                    worksheet.Range["B1"].Text = "مبلغ";
                    int index = 2;

                    var i = db.ContractCellslogs.Where(p =>
                        p.symbol == item && p.DateTimeCreatLog >= s &&
                        p.DateTimeCreatLog <= e).ToList();
                    foreach (var VARIABLE in i)
                    {
                        worksheet.Range["A" + index].Text = VARIABLE.DateTimeCreatLog.ToString("yyyy/MM/dd HH:mm");
                        worksheet.Range["B" + index].Number = VARIABLE.lastTradedPrice;
                        index++;
                    }
                    sheetnumber++;
                }
                //Insert sample text into cell “A1”
                filename += ".xlsx";


                //Save the workbook to disk in xlsx format
                workbook.SaveAs(filename, ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
            }


        }

    }
}