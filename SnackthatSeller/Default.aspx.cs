using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : Settings
{
    public Boolean notCustomers = false, notRoute = false, notSells = false;

    public void loadAll()
    {
        DataTable customers = new Customers().getLastCustomers();
        DataTable route = new Routes().getAllRoutes();
        DataTable sells = new Products().getLastSells();

        if (customers != null && customers.Rows.Count > 0)
        {
            GridView2.DataSource = customers;
            GridView2.DataBind();
            notCustomers = false;
        }
        else
        {
            notCustomers = true;
        }
        if (route != null && route.Rows.Count > 0)
        {
            DetailsView1.DataSource = route;
            DetailsView1.DataBind();
            notRoute = false;
        }
        else
        {
            notRoute = true;
        }
        if (sells != null && sells.Rows.Count > 0)
        {
            GridView1.DataSource = sells;
            GridView1.DataBind();
            notSells = false;
        }
        else
        {
            notSells = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.loadAll();
    }
}