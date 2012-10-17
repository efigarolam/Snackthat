using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewroute : Settings
{
    /// <summary>
    /// Property to stores the Name of the Route
    /// </summary>
    public string name;
    /// <summary>
    /// Property to stores the ID of the Route
    /// </summary>
    public string routeid;
    /// <summary>
    /// Flag to decides if shows the Customers or not.
    /// </summary>
    public Boolean showCustomers = false;

    /// <summary>
    /// Loads the page and analizes the QueryString to catch de GET vars.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("id") != null)
            {
                int idRoute = Convert.ToInt16(Request.QueryString.Get("id"));

                Routes route = new Routes();
                DataTable dt;

                dt = route.getRouteByID(idRoute);

                if (dt != null && dt.Columns[0].Caption == "Router_Dont_Exists")
                {
                    Response.Redirect(webURL + "views/routes/viewallroutes.aspx");
                }
                else if (dt.Rows.Count == 1)
                {
                    this.routeid = dt.Rows[0].ItemArray[0].ToString();
                    this.name = dt.Rows[0].ItemArray[1].ToString();

                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();

                    if (DetailsView1.Rows[2].Cells[1].Text == "&nbsp;")
                    {
                        DetailsView1.Rows[2].Cells[1].Text = "<a href=\"" + webURL + "views/routes/assignroutes.aspx\">Asignar</a>";
                    }

                    Customers customers = new Customers();

                    dt = customers.getCustomersAndAddressesByRoute(Convert.ToInt16(this.routeid));
                    if (dt != null && dt.Rows.Count > 0 )
                    {
                        showCustomers = true;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        showCustomers = false;
                    }
                }
                else
                {
                    Response.Redirect(webURL + "views/routes/viewallroutes.aspx");
                }

            }
            else
            {
                Response.Redirect(webURL + "views/routes/viewallroutes.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/routes/viewallroutes.aspx");
        }

    }
}