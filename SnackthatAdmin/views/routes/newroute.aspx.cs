using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class newroute : Settings
{
    /// <summary>
    /// Flag to decides if hide the grid or not
    /// </summary>
    public Boolean hideGrid = false;

    /// <summary>
    /// Loads all the Customers and its Addresses on the gridview
    /// </summary>
    protected void loadCustomers()
    {
        Customers customers = new Customers();
        DataTable dt = customers.getCustomersAndAddressesToSave();

        if (dt != null && dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            this.hideGrid = false;
        }
        else
        {
            this.hideGrid = true;
        }
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
            if (Request.QueryString.Get("action") != null && Request.QueryString.Get("id") != null && Request.QueryString.Get("action") == "notify")
            {
                this.loadCustomers();
                int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                switch (idNotif)
                {
                    case 1:
                        this.setNotification("success", "¡Éxito!", "La ruta ha sido guardado correctamente");
                        break;
                    default:
                        this.setNotification("nothing");
                        break;
                }
            }
            else
            {
                this.loadCustomers();
                this.setNotification("nothing");
            }
        }
        catch (Exception ex)
        {
            this.setNotification("nothing");
            Response.Redirect(webURL + "views/routes/newroute.aspx");
        }   
    }

    /// <summary>
    /// Saves the new Route and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Name = Security.cleanSQL(TextBox1.Text);
        string Addresses = null;
        if (Page.Request["Direcciones"] != null)
        {
            Addresses = Security.cleanSQL(Page.Request["Direcciones"]);
        }        

        if (Security.isEmpty(Name))
        {
            this.setNotification("warning", "¡No hay nombre de ruta!", "Ingrese un nombre de ruta por favor...");
        }
        else
        {
            Routes route = new Routes(0, Name, Addresses);

            int result = route.saveRoute();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/routes/newroute.aspx?action=notify&id=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe una ruta con ese nombre...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}