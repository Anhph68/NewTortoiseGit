using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;

namespace TDKT.Report
{
    public partial class DanhSach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TDKT td = new TDKT();
            //string cmd = null;
            //DataTable dt = new DataTable();
            //string year = Request.QueryString["year"].ToString(),
            //        cat = Request.QueryString["cat"].ToString(),
            //        ngaylapbc = Request.QueryString["ngaylapbc"].ToString();
            //switch (cat)
            //{
            //    case "0":
            //        cmd = "getRpt0";
            //        break;
            //    case "1":
            //        cmd = "getRpt11";
            //        break;
            //    case "2":
            //        cmd = "getRpt21";
            //        break;
            //    case "3":
            //        cmd = "getRpt31";
            //        break;
            //    default:
            //        break;
            //}
            //dt = td.getReportData(year, ngaylapbc, cmd);
            //StiReport report = new StiReport();
            //report.Load(Server.MapPath("Report" + cat + ".mrt"));
            //report.Compile();
            //report["namkt"] = year;
            //string[] d = ngaylapbc.Split('/');
            //string temp = d[1] + "/" + d[0] + "/" + d[2];
            //report["ngaylapbc"] = temp;
            //report.RegData(dt);
            //report.Dictionary.Synchronize();
            //StiWebViewerFx1.Report = report;

        }
    }
}