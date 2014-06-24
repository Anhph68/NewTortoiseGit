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
        // GET: Charts
        public ActionResult Index()
        {
            ChartsModel model = new ChartsModel();
            model.Charts = new List<Highcharts>();

            Highcharts chart = new Highcharts("chart0")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                }).SetTitle(new Title
                {   // Title
                    Text = "Nhiệt độ trung bình hàng tháng",
                    X = -20
                }).SetSubtitle(new Subtitle
                {   // Subtitle
                    Text = "Source: WorldClimate.com",
                    X = -20
                }).SetXAxis(new XAxis
                {   // X Axis
                    Categories = ChartsData.Categories
                }).SetYAxis(new YAxis
                {   // Y Axis
                    Title = new YAxisTitle { Text = "Nhiệt độ (°C)" },
                    PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    }
                }).SetTooltip(new Tooltip
                {   // Tooltip
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                }).SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(new[]
                {
                    new Series { Name = "Tokyo", Data = new Data(ChartsData.TokioData) },
                    new Series { Name = "New York", Data = new Data(ChartsData.NewYorkData) },
                    new Series { Name = "Berlin", Data = new Data(ChartsData.BerlinData) },
                    new Series { Name = "London", Data = new Data(ChartsData.LondonData) }
                }).SetExporting(new Exporting { Enabled = false });

            model.Charts.Add(chart);

            chart = new Highcharts("chart1")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "Monthly Average Rainfall" })
                .SetSubtitle(new Subtitle { Text = "Source: WorldClimate.com" })
                .SetXAxis(new XAxis { Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" } })
                .SetYAxis(new YAxis { Min = 0, Title = new YAxisTitle { Text = "Rainfall(mm)" } })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Left,
                    VerticalAlign = VerticalAligns.Top,
                    X = 100,
                    Y = 70,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' mm'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.2,
                        BorderWidth = 0
                    }
                })
                .SetSeries(new[]
                {
                    new Series { Name = "Tokyo", Data = new Data(new object[] { 49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 }) },
                    new Series { Name = "London", Data = new Data(new object[] { 48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2 }) },
                    new Series { Name = "New York", Data = new Data(new object[] { 83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3 }) },
                    new Series { Name = "Berlin", Data = new Data(new object[] { 42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1 }) }
                })
                .SetExporting(new Exporting { Enabled = false });

            model.Charts.Add(chart);

            chart = new Highcharts("chart2")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "Browser market shares at a specific website, 2010" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Browser share",
                    Data = new Data(new object[]
                    {
                        new object[] { "Firefox", 45.0 },
                        new object[] { "IE", 26.8 },
                        new Point
                        {
                            Name = "Chrome",
                            Y = 12.8,
                            Sliced = true,
                            Selected = true
                        },
                        new object[] { "Safari", 8.5 },
                        new object[] { "Opera", 6.2 },
                        new object[] { "Others", 0.7 }
                    })
                }).SetExporting(new Exporting { Enabled = false });

            model.Charts.Add(chart);

            return View(model);
        }

        public ActionResult BasicLine()
        {
            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    MarginRight = 130,
                    MarginBottom = 25,
                    ClassName = "chart"
                }).SetTitle(new Title
                {   // Title
                    Text = "Nhiệt độ trung bình hàng tháng",
                    X = -20
                }).SetSubtitle(new Subtitle
                {   // Subtitle
                    Text = "Source: WorldClimate.com",
                    X = -20
                }).SetXAxis(new XAxis
                {   // X Axis
                    Categories = ChartsData.Categories
                }).SetYAxis(new YAxis
                {   // Y Axis
                    Title = new YAxisTitle { Text = "Nhiệt độ (°C)" },
                    PlotLines = new[]
                    {
                        new YAxisPlotLines
                        {
                            Value = 0,
                            Width = 1,
                            Color = ColorTranslator.FromHtml("#808080")
                        }
                    }
                }).SetTooltip(new Tooltip
                {   // Tooltip
                    Formatter = @"function() {
                                        return '<b>'+ this.series.name +'</b><br/>'+
                                    this.x +': '+ this.y +'°C';
                                }"
                }).SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -10,
                    Y = 100,
                    BorderWidth = 0
                })
                .SetSeries(new[]
                {
                    new Series { Name = "Tokyo", Data = new Data(ChartsData.TokioData) },
                    new Series { Name = "New York", Data = new Data(ChartsData.NewYorkData) },
                    new Series { Name = "Berlin", Data = new Data(ChartsData.BerlinData) },
                    new Series { Name = "London", Data = new Data(ChartsData.LondonData) }
                }).SetExporting(new Exporting { Enabled = false });

            chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "Browser market shares at a specific website, 2010" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        AllowPointSelect = true,
                        Cursor = Cursors.Pointer,
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Color = ColorTranslator.FromHtml("#000000"),
                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Browser share",
                    Data = new Data(new object[]
                    {
                        new object[] { "Firefox", 45.0 },
                        new object[] { "IE", 26.8 },
                        new Point
                        {
                            Name = "Chrome",
                            Y = 12.8,
                            Sliced = true,
                            Selected = true
                        },
                        new object[] { "Safari", 8.5 },
                        new object[] { "Opera", 6.2 },
                        new object[] { "Others", 0.7 }
                    })
                }).SetExporting(new Exporting { Enabled = false });

            return View(chart);
        }
    }
}