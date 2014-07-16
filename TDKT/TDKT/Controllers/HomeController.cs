using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;
using System.Drawing;
using DotNet.Highcharts.Enums;

namespace TDKT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        TDKTEntities td = new TDKTEntities();

        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            ViewBag.TrienKhai = td.getTrienKhai(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).SingleOrDefault();

            string[] categories = new[] { "Đã kết thúc", "Đã trình duyệt BCKT", "Đã xét duyệt BCKT", "Đơn vị đã trình PHBCKT", "Vụ TH đã trình PHBCKT", "Đã phát hành BCKT" };

            ChartsModel model = new ChartsModel();
            model.Charts = new List<Highcharts>();
            var tmp2 = td.getPhatHanh(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).SingleOrDefault();
            Series[] colData = new[] {
                    new Series { Name = "", Data = new Data(new object[] { tmp2.dakt, tmp2.datrinhbc, tmp2.daduyetbc, tmp2.dvtrinhph, tmp2.thtrinhph, tmp2.ktnnph }) }
                };
            model.Charts.Add(ColChart1("chart", categories, colData));

            return View(model);
        }

        public ActionResult Chart()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult chooseYear()
        {
            return PartialView(td.getYears());
        }

        [HttpGet]
        public ActionResult AjaxHandler(string key)
        {
            var allResult = td.getYears().ToList();

            IEnumerable<getYears_Result> filteredResult;
            //Check whether the companies should be filtered by keyword
            if (!string.IsNullOrEmpty(key))
            {
                filteredResult = allResult
                    .Where(c => c.namkt.ToLower().Contains(key.ToLower()));
            }
            else filteredResult = allResult;

            return Json(filteredResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Year")]
        [ValidateAntiForgeryToken]
        public ActionResult chooseYear(string year)
        {
            Session["year"] = year;

            return Redirect(Session["Url"].ToString());
        }

        [HttpPost]
        public ActionResult chooseDate(string key, string code)
        {

            if (code == HttpContext.Session.SessionID)
            {
                Session["date"] = key;
                return null;
            }

            return HttpNotFound();
        }

        public Highcharts ColChart1(string id, string[] cat, Series[] data)
        {
            return new Highcharts(id)
                .SetOptions(new GlobalOptions
                {
                    Colors = new System.Drawing.Color[] { Color.FromArgb(124, 181, 236), Color.FromArgb(255, 188, 117), Color.FromArgb(169, 255, 150), Color.FromArgb(128, 133, 233), Color.FromArgb(241, 92, 128) }
                })
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "" })
                .SetXAxis(new XAxis
                {
                    Categories = cat //new[] { "Số cuộc đã kết thúc", "Chưa triển khai", "Đã triển khai", "Đã kết thúc" } 
                })
                .SetYAxis(new YAxis { Min = 0, Title = new YAxisTitle { Text = "Số cuộc kiểm toán" }, AllowDecimals = false })
                .SetLegend(new Legend { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' cuộc'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Enabled = true,
                            Rotation = 0,
                            Color = ColorTranslator.FromHtml("#FFFFFF"),
                            Align = HorizontalAligns.Center,
                            X = 0,
                            Y = 0,
                            Formatter = "function() { return this.y; }",
                            Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif',textShadow: '0 0 3px black'"
                        }
                    }
                })
                .SetSeries(data)
                .SetExporting(new Exporting { Enabled = false });
        }
    }
}