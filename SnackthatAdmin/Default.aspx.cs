using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : Settings
{
    /// <summary>
    /// Logic flags to control the display of the content in the page.
    /// </summary>
    public Boolean notUsers = false, notCustomers = false, notRoutes = false, notProducts = false;
    
    /// <summary>
    /// Load all the data from the database, the last records by Users, Customers, Products & Routes.
    /// </summary>
    public void loadAll()
    {
        DataTable users = new Users().getLastUsers();
        DataTable customers = new Customers().getLastCustomers();
        DataTable routes = new Routes().getLastRoutes();
        DataTable products = new Products().getLastProducts();

        if (users != null && users.Rows.Count > 0)
        {
            GridView1.DataSource = users;
            GridView1.DataBind();
            notUsers = false;
        }
        else
        {
            notUsers = true;
        }
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
        if (routes != null && routes.Rows.Count > 0)
        {
            GridView3.DataSource = routes;
            GridView3.DataBind();
            notRoutes = false;
        }
        else
        {
            notRoutes = true;
        }
        if (products != null && products.Rows.Count > 0)
        {
            GridView4.DataSource = products;
            GridView4.DataBind();
            notProducts = false;
        }
        else
        {
            notProducts = true;
        }
    }

    /// <summary>
    /// Load the web page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.loadAll();
    }
}