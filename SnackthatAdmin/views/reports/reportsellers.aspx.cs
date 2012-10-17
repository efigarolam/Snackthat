using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class views_reports_reportsellers : Settings
{
    /// <summary>
    /// Obtains the information for the Report for Seller and creates it.
    /// </summary>
    /// <param name="idSeller">Int ID of the Seller</param>
    /// <returns>Returns true if the Report is generated correctly, otherwise returns false</returns>
    private Boolean loadReport(int idSeller)
    {
        DataSet ds = Reports.getReportBySeller(idSeller);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (idSeller == 0)
            {
                rptSellersAll rpt = new rptSellersAll();
                rpt.SetDataSource(ds);
                crvSeller.ReportSource = rpt;
            }
            else if(idSeller > 0)
            {
                rptSellers rpt = new rptSellers();
                rpt.SetDataSource(ds);
                crvSeller.ReportSource = rpt;
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
                int idSeller = Convert.ToInt16(Request.QueryString.Get("id"));
                if (!this.loadReport(idSeller))
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