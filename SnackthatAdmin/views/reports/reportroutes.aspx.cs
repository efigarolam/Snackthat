using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class reportroutes : Settings
{
    /// <summary>
    /// Obtains the information for the Report for Route and creates it.
    /// </summary>
    /// <param name="idRoute">Int ID of the Route</param>
    /// <returns>Returns true if the Report is generated correctly, otherwise returns false</returns>
    private Boolean loadReport(int idRoute)
    {
        DataSet ds = Reports.getReportByRoute(idRoute);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (idRoute == 0)
            {
                rptRoutesAll rpt = new rptRoutesAll();
                rpt.SetDataSource(ds);
                crvRoute.ReportSource = rpt;
            }
            else if (idRoute > 0)
            {
                rptRoutes rpt = new rptRoutes();
                rpt.SetDataSource(ds);
                crvRoute.ReportSource = rpt;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Loads the Page, generates the Report and redirect you if there's not enough data.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("id") != null && !Page.IsPostBack)
            {
                int idRoute = Convert.ToInt16(Request.QueryString.Get("id"));
                if (!this.loadReport(idRoute))
                {
                    Response.Redirect(webURL + "views/reports/reports.aspx?action=notify&id=1", false);
                }
            }
            else
            {
                Response.Redirect(webURL + "views/reports/reports.aspx?action=notify&id=2", false);
            }
        }
        catch (Exception ex)
        {
            this.setNotification("nothing");
            Response.Redirect(webURL + "views/reports/reports.aspx?action=notify&id=3", false);
        }
    }
}