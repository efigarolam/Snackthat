using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class reportproducts : Settings
{
    /// <summary>
    /// Obtains the information for the Report for Products and creates it.
    /// </summary>
    /// <param name="idProduct">Int ID of the Product</param>
    /// <returns>Returns true if the Report is generated correctly, otherwise returns false</returns>
    private Boolean loadReport(int idProduct)
    {
        DataSet ds = Reports.getReportByProduct(idProduct);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (idProduct == 0)
            {
                rptProductsAll rpt = new rptProductsAll();
                rpt.SetDataSource(ds);
                crvProduct.ReportSource = rpt;
            }
            else if(idProduct > 0)
            {
                rptProducts rpt = new rptProducts();
                rpt.SetDataSource(ds);
                crvProduct.ReportSource = rpt;
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
                int idProduct = Convert.ToInt16(Request.QueryString.Get("id"));
                if (!this.loadReport(idProduct))
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