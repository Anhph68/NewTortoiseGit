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

namespace TDKT.Report
{
    public partial class DanhSach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cmd = null;
            DataTable dt = new DataTable();
            //string year = Request.QueryString["year"].ToString(),
            //        cat = Request.QueryString["cat"].ToString(),
            //        ngaylapbc = Request.QueryString["ngaylapbc"].ToString();
            string year = "2012", cat = "1", ngaylapbc = "06/25/2014";

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
            report["namkt"] = year;
            string[] d = ngaylapbc.Split('/');
            string temp = d[1] + "/" + d[0] + "/" + d[2];
            report["ngaylapbc"] = temp;
            report.RegData(dt);
            report.Dictionary.Synchronize();
            StiWebViewerFx1.Report = report;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable getReportData(string year, string ngaylapbc, string cmd)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@namkt", year);
            da.SelectCommand.Parameters.AddWithValue("@ngaylapbc", ngaylapbc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}