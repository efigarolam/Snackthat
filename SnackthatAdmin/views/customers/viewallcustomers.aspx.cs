using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewallcustomers : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Customers.
    /// </summary>
    protected void loadGridView()
    {
        Customers customer = new Customers();

        GridView1.DataSource = customer.getAllCustomers();
        GridView1.DataBind();
    }

    /// <summary>
    /// Deletes a Customer by ID
    /// </summary>
    /// <param name="id">Int ID of the Customer</param>
    /// <returns>Returns true if the operation has been successfull, othewrise returns false</returns>
    protected Boolean deleteCustomer(int id)
    {
        Customers customer = new Customers();
        return customer.deleteCustomerByID(id);
    }

    /// <summary>
    /// Loads the page and analizes the QueryString to catch de GET vars.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("action") != null && Request.QueryString.Get("id") != null)
            {
                if (Request.QueryString.Get("action") == "delete")
                {
                    int idCustomer = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (this.deleteCustomer(idCustomer))
                    {
                        Response.Redirect(webURL + "views/customers/viewallcustomers.aspx?action=notify&id=1", false);
                    }
                    else
                    {
                        Response.Redirect(webURL + "views/customers/viewallcustomers.aspx?action=notify&id=2", false);
                    }
                }
                else if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 1)
                    {
                        this.setNotification("success", "¡Éxito!", "El cliente ha sido eliminado correctamente...");
                    }
                    else if (idNotif == 2)
                    {
                        this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    }
                    else if (idNotif == 3)
                    {
                        this.setNotification("error", "¡Ooooops!", "El cliente no existe...");
                    }

                    loadGridView();
                }
                else
                {
                    this.setNotification("nothing");
                    Response.Redirect(webURL + "views/customers/viewallcustomers.aspx", false);
                }
            }
            else
            {
                this.setNotification("nothing");
                this.loadGridView();
            }

        }
        catch (Exception ex)
        {
            this.setNotification("nothing");
            Response.Redirect(webURL + "views/customers/viewallcustomers.aspx");
        }
    }
}