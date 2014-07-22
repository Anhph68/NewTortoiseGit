using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;

namespace TDKT.Report
{
    public partial class DanhSach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session.SessionID == Request.QueryString["key"])
            {
                string cmd = null;
                DataTable dt = new DataTable();
                string year = Request.QueryString["year"].ToString(),
                        cat = Request.QueryString["cat"].ToString(),
                        ngaylapbc = Request.QueryString["date"].ToString();

                switch (cat)
                {
                    case "0":
                        cmd = "getRpt0";
                        break;
                    case "1":
                        cmd = "getRpt1";
                        break;
                    case "2":
                        cmd = "getRpt2";
                        break;
                    case "3":
                        cmd = "getRpt3";
                        break;
                    default:
                        break;
                }
                dt = getReportData(year, ngaylapbc, cmd);
                StiReport report = new StiReport();
                report.Load(Server.MapPath("Report" + cat + ".mrt"));
                report.Compile();
                report.RegData(dt);
                report["namkt"] = year;
                report["ngaylapbc"] = ngaylapbc;
                
                //report.Dictionary.Synchronize();
                StiWebViewerFx1.View(report);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable getReportData(string year, string ngaylapbc, string cmd)
        {
            Debug.Write(DateTime.ParseExact(ngaylapbc, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@namkt", year);
            da.SelectCommand.Parameters.AddWithValue("@ngaylapbc", DateTime.ParseExact(ngaylapbc, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}