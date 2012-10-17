using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class views_reports_reportexpired : Settings
{
    /// <summary>
    /// Obtains the information for the Report of Expired Products and creates it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = Reports.getReportByExpired();
        if (ds != null && ds.Tables[1].Rows.Count > 0)
        {
            rptExpired rpt = new rptExpired();
            rpt.SetDataSource(ds);
            crvExpired.ReportSource = rpt;
        }
        else
        {
            Response.Redirect(webURL + "views/reports/reports.aspx?action=notify&id=1", false);
        }
    }
}