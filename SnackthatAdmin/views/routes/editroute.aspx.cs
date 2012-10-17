using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class editroute : Settings
{
    /// <summary>
    /// ArrayList with all the ID of the Addresses of the Customers By Route
    /// </summary>
    public ArrayList idAddressesByRoute = new ArrayList();
    /// <summary>
    /// Flag to decides if show the grid or not.
    /// </summary>
    public Boolean showCustomers = false;

    /// <summary>
    /// Method to loads the form with the data of the selected Customer
    /// </summary>
    /// <param name="idCustomer">Int ID of the Customer</param>
    protected void loadForm(int idRoute)
    {
        Routes route = new Routes();
        
        route.idRoute = idRoute;
        route.getRouteByID();

        if (route.idRoute != 0)
        {
            HiddenField1.Value = route.idRoute.ToString();
            TextBox1.Text = route.Name;
            this.loadCustomers(route.idRoute);
        }
        else
        {
            Response.Redirect(webURL + "views/routes/viewallroutes.aspx?action=notify&id=3", false);
        }
    }

    /// <summary>
    /// Loads all the Customers and its Addresses on the gridview
    /// </summary>
    protected void loadCustomers(int idRoute)
    {

        Customers customers = new Customers();
        DataTable dt;
        dt = customers.getCustomersAndAddressesToEdit(idRoute);
        if (dt != null && dt.Rows.Count > 0)
        {
            showCustomers = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            showCustomers = false;
        }

        DataTable addressesByRoute;
        addressesByRoute = customers.getCustomersAndAddressesByRoute(idRoute);

        for (int i = 0; i < addressesByRoute.Rows.Count; i++)
        {
            this.idAddressesByRoute.Add(addressesByRoute.Rows[i].ItemArray[0].ToString());
        }

        this.setDataToScript(this.idAddressesByRoute);
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
            if (Request.QueryString.Get("id") != null && Page.IsPostBack != true)
            {
                if (Request.QueryString.Get("action") != null && Request.QueryString.Get("nid") != null && Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("nid"));

                    switch (idNotif)
                    {
                        case 1:
                            this.setNotification("success", "¡Éxito!", "La ruta ha sido actualizado correctamente...");
                            break;
                        default:
                            this.setNotification("nothing");
                            break;
                    }
                }
                else
                {
                    this.setNotification("nothing");
                }

                int idRoute = Convert.ToInt16(Request.QueryString.Get("id"));

                this.loadForm(idRoute);
            }
            else if (Request.QueryString.Get("id") != null && Page.IsPostBack)
            {
                int idRoute = Convert.ToInt16(Request.QueryString.Get("id"));
                this.loadCustomers(idRoute);
            }
            else
            {
                Response.Redirect(webURL + "views/routes/viewallroutes.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/routes/viewallroutes.aspx?action=notify&id=3");
        }
    }

    /// <summary>
    /// Edits the current Route and make the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idRoute = Convert.ToInt16(HiddenField1.Value);
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
            Routes route = new Routes(idRoute, Name, Addresses);

            int result = route.saveRoute();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/routes/editroute.aspx?id=" + idRoute + "&action=notify&nid=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo(s) repetido(s)!", "Ya existe una ruta registrado con ese nombre...");
                    break;
                case 0:
                    this.setNotification("error", "¡Ooooops!", "No existe la ruta...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}