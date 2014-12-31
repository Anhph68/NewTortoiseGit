using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft.Report;

namespace THKQKT.BaoCao
{
    public partial class PB01 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("pbieu01_THKNKT", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            StiReport report = new StiReport();
            report.Load(Server.MapPath("PBIEU01_a.mrt"));
            report.Compile();
            report.RegData(dt);
            StiWebViewer1.Report = report;
        }
    }
}