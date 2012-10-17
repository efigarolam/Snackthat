using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewcustomer : Settings
{
    /// <summary>
    /// Property to stores the Name of the Customer
    /// </summary>
    public string name;
    /// <summary>
    /// Property to stores the ID of the Customer
    /// </summary>
    public string customerid;

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
                int idCustomer = Convert.ToInt16(Request.QueryString.Get("id"));

                Customers customer = new Customers();
                DataTable dt;

                dt = customer.getCustomerByID(idCustomer);

                if (dt != null && dt.Columns[0].Caption == "Customer_Dont_Exists")
                {
                    Response.Redirect(webURL + "views/customers/viewallcustomers.aspx");
                }
                else if (dt.Rows.Count == 1)
                {
                    this.customerid = dt.Rows[0].ItemArray[0].ToString();
                    this.name = dt.Rows[0].ItemArray[1].ToString() + " " + dt.Rows[0].ItemArray[2].ToString();

                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();

                    dt = customer.getCustomerAddresses(idCustomer);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    Response.Redirect(webURL + "views/customers/viewallcustomers.aspx");
                }

            }
            else
            {
                Response.Redirect(webURL + "views/customers/viewallcustomers.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/customers/viewallcustomers.aspx");
        }
    }
}