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
    [Authorize]
    public class ChartsController : Controller
    {
        private TDKTEntities td = new TDKTEntities();
        // GET: Charts
        public ActionResult Index()
        {
            Session["Url"] = Request.RawUrl;
            ChartsModel model = new ChartsModel();
            model.Charts = new List<Highcharts>();

            var tmp = td.getTrienKhai(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).SingleOrDefault();

            string[] categories = { "Tổng số", "Chưa triển khai", "Đã triển khai" };
            const string NAME = "Cuộc kiểm toán";
            Data data = new Data(new[]
            {
                new Point
                {
                    Y = tmp.TongSo,
                    Color = Color.FromArgb(66, 139, 202)
                },
                new Point
                {
                    Y = tmp.ChuaTrienKhai,
                    Color = Color.FromArgb(217, 83, 79)
                },
                new Point
                {
                    Y = tmp.DaTrienKhai,
                    Color = Color.FromArgb(240, 173, 78),
                    Drilldown = new Drilldown
                    {
                        Name = "Chrome versions",
                        Categories = new[] { "Chưa kết thúc", "Đã kết thúc" },
                        Data = new Data(new object[] { tmp.ChuaKetThuc, tmp.DaKetThuc}),
                        Color = Color.FromArgb(240, 173, 78)
                    }
                }
            });
            model.Charts.Add(ColDrillDown("chart", categories, NAME, data));
            var s = new Series
            {
                Type = ChartTypes.Pie,
                Name = "Cuộc kiểm toán",
                Data = new Data(new object[] { 
                    new Point { Name="Chưa triển khai", Y = tmp.ChuaTrienKhai, Color = Color.FromArgb(217, 83, 79) },
                    new Point { Name = "Đã triển khai", Y = tmp.DaTrienKhai, Color = Color.FromArgb(240, 173, 78) } 
                })
            };

            model.Charts.Add(PieChart("chart1", s));
            categories = new[] { "Đã kết thúc", "Đã trình duyệt BCKT", "Đã xét duyệt BCKT", "Đơn vị đã trình PHBCKT", "Vụ TH đã trình PHBCKT", "Đã phát hành BCKT" };
            var tmp2 = td.getPhatHanh(Session["year"].ToString(), DateTime.Parse(Session["date"].ToString()), Session["donvi"] == null ? "" : Session["donvi"].ToString()).SingleOrDefault();
            Series[] colData = new[] {
                    new Series { Name = "", Data = new Data(new object[] { tmp2.dakt, tmp2.datrinhbc, tmp2.daduyetbc, tmp2.dvtrinhph, tmp2.thtrinhph, tmp2.ktnnph }) }
                };

            model.Charts.Add(ColChart1("chart2", categories, colData).SetOptions(new GlobalOptions
            {
                Colors = new System.Drawing.Color[] { Color.FromArgb(92, 184, 92), Color.FromArgb(240, 173, 78), Color.FromArgb(217, 83, 79) }
            }));
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
                            Color = Color.FromArgb(240, 173, 78),
                            Formatter = "function() { return Highcharts.numberFormat(this.y, 0); }",
                            Style = "fontWeight: 'bold'"
                        }
                    }
                }).SetCredits(new Credits { Enabled = false })
                .SetSeries(new Series
                {
                    Name = "Browser brands",
                    Data = data,
                    Color = Color.White
                })
                .SetExporting(new Exporting { Enabled = true })
                .AddJavascripFunction(
                    "TooltipFormatter",
                    @"var point = this.point, s = this.x +':<b> '+ Highcharts.numberFormat(this.y, 0) +' cuộc</b><br/>';
                      if (point.drilldown) {
                        s += 'Click vào đây để xem chi tiết'; //'+ point.category +' versions';
                      } else { s += 'Click để trở lại màn hình trước'; }
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
            Highcharts tmp = new Highcharts(id)
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
                }).SetCredits(new Credits { Enabled = false }).SetSeries(s).SetExporting(new Exporting { Enabled = false });

            return tmp;
        }

        public Highcharts ColChart1(string id, string[] cat, Series[] data)
        {
            return new Highcharts(id)
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "" })
                .SetXAxis(new XAxis { Categories = cat })
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
                            Color = Color.FromArgb(92, 184, 92),
                            Align = HorizontalAligns.Center,
                            X = 0,
                            Y = 0,
                            Formatter = "function() { return this.y; }",
                            Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif'"
                        }
                    }
                }).SetCredits(new Credits { Enabled = false })
                .SetSeries(data)
                .SetExporting(new Exporting { Enabled = false });
        }

    }
}