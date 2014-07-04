using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDKT.Models;

namespace TDKT.Controllers
{
    public class ChartsController : Controller
    {
        private TDKTEntities td = new TDKTEntities();
        // GET: Charts
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            ChartsModel model = new ChartsModel();
            model.Charts = new List<Highcharts>();

            var tmp = td.getTrienKhai(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString())).SingleOrDefault();

            string[] categories = { "Tổng số", "Chưa triển khai", "Đã triển khai" };
            const string NAME = "Cuộc kiểm toán";
            Data data = new Data(new[]
            {
                new Point
                {
                    Y = tmp.TongSo,
                    Color = Color.FromName("colors[0]")
                },
                new Point
                {
                    Y = tmp.ChuaTrienKhai,
                    Color = Color.FromName("colors[2]")
                },
                new Point
                {
                    Y = tmp.DaTrienKhai,
                    Color = Color.FromName("colors[1]"),
                    Drilldown = new Drilldown
                    {
                        Name = "Chrome versions",
                        Categories = new[] { "Chưa kết thúc", "Đã kết thúc" },
                        Data = new Data(new object[] { tmp.ChuaKetThuc, tmp.DaKetThuc}),
                        Color = Color.FromName("colors[1]")
                    }
                }
            });
            model.Charts.Add(ColDrillDown("chart", categories, NAME, data));
            var s = new Series
            {
                Type = ChartTypes.Pie,
                Name = "Cuộc kiểm toán",
                Data = new Data(new object[]
                    {
                        new object[] { "Chưa triển khai", tmp.ChuaTrienKhai },
                        new Point
                        {
                            Name = "Đã triển khai",
                            Y = tmp.DaTrienKhai,
                            Sliced = true,
                            Selected = true,
                        }
                    })
            };
            model.Charts.Add(PieChart("chart1", s));
            categories = new[] { "Đã kết thúc", "Đã trình duyệt BCKT", "Đã xét duyệt BCKT", "Đơn vị đã trình PHBCKT", "Vụ TH đã trình PHBCKT", "Đã phát hành BCKT" };
            var tmp2 = td.getPhatHanh(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString())).SingleOrDefault();
            Series[] colData = new[] {
                    new Series { Name = "", Data = new Data(new object[] { tmp2.dakt, tmp2.datrinhbc, tmp2.daduyetbc, tmp2.dvtrinhph, tmp2.thtrinhph, tmp2.ktnnph }) }
                };
            model.Charts.Add(ColChart1("chart2", categories, colData));
            return View(model);
        }

        public Highcharts ColDrillDown(string id, string[] categories, string NAME, Data data)
        {
            return new Highcharts(id)
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "" })
                .SetXAxis(new XAxis { Categories = categories })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Số cuộc kiểm toán" }, AllowDecimals = false })
                .SetLegend(new Legend { Enabled = false })
                .SetTooltip(new Tooltip { Formatter = "TooltipFormatter" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        Cursor = Cursors.Pointer,
                        Point = new PlotOptionsColumnPoint { Events = new PlotOptionsColumnPointEvents { Click = "ColumnPointClick" } },
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Enabled = true,
                            Color = Color.FromName("colors[0]"),
                            Formatter = "function() { return Highcharts.numberFormat(this.y, 0); }",
                            Style = "fontWeight: 'bold'"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Name = "Browser brands",
                    Data = data,
                    Color = Color.White
                })
                .SetExporting(new Exporting { Enabled = false })
                .AddJavascripFunction(
                    "TooltipFormatter",
                    @"var point = this.point, s = this.x +':<b> '+ Highcharts.numberFormat(this.y, 0) +' cuộc</b><br/>';
                      if (point.drilldown) {
                        s += 'Click vào đây để xem chi tiết'; //'+ point.category +' versions';
                      } // else { s += 'Click to return to browser brands'; }
                      return s;"
                )
                .AddJavascripFunction(
                    "ColumnPointClick",
                    @"var drilldown = this.drilldown;
                      if (drilldown) { // drill down
                        setChart(drilldown.name, drilldown.categories, drilldown.data.data, drilldown.color);
                      } else { // restore
                        setChart(name, categories, data.data);
                      }"
                )
                .AddJavascripFunction(
                    "setChart",
                    id + @".xAxis[0].setCategories(categories);" +
                    id + @".series[0].remove();" +
                    id + @".addSeries({
                         name: name,
                         data: data,
                         color: color || 'white'
                      });",
                    "name", "categories", "data", "color"
                )
                .AddJavascripVariable("colors", "Highcharts.getOptions().colors")
                .AddJavascripVariable("name", "'{0}'".FormatWith(NAME))
                .AddJavascripVariable("categories", JsonSerializer.Serialize(categories))
                .AddJavascripVariable("data", JsonSerializer.Serialize(data));
        }

        public Highcharts PieChart(string id, Series s)
        {
            return new Highcharts(id)
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ Highcharts.numberFormat(this.percentage, 0) +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Enabled = true,
                            Color = ColorTranslator.FromHtml("#fff"),
                            Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif',textShadow: '0 0 3px black'",
                            Formatter = "function() { return Highcharts.numberFormat(this.percentage, 0) +' %'; }",
                            Distance = -30
                        },
                        ShowInLegend = true
                    }
                }).SetSeries(s).SetExporting(new Exporting { Enabled = false });
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
                .SetYAxis(new YAxis { Min = 0, Title = new YAxisTitle { Text = "Số cuộc kiểm toán" } })
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
                .SetSeries(data
                //    new[] {
                //    new Series { Name = "", Data = new Data(new object[] { 
                //        49.9, 71.5, 
                //        new Point {
                //            Y = 106.4, Color = Color.FromArgb(169, 255, 150)
                //        }, 
                //        new Point {
                //            Y = 129.2, Color = Color.FromArgb(255, 188, 117)
                //        }
                //    }) }
                //}
                )
                .SetExporting(new Exporting { Enabled = false });
        }

    }
}