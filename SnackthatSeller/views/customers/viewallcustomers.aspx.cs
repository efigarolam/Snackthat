using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewallcustomers : Settings
{
    public bool hasCustomers = false;

    protected void loadGridView()
    {
        Customers customer = new Customers();
        DataTable dt = customer.getAllCustomers(0);

        if (dt != null && dt.Rows.Count > 0)
        {
            this.hasCustomers = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            this.hasCustomers = false;
            this.setNotification("error", "No hay ruta asignada", "No se te ha asignado una ruta y por consecuencia no hay clientes...");
        }
    }

    protected Boolean deleteCustomer(int id)
    {
        Customers customer = new Customers();
        return customer.deleteCustomerByID(id);
    }

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