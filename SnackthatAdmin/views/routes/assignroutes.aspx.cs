using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class assignroutes : Settings
{
    /// <summary>
    /// Array Flag to decide if show a notification or not.
    /// </summary>
    public Boolean[] showNotification = new Boolean[2];
    /// <summary>
    /// Flag to decide if hide the form or not.
    /// </summary>
    public Boolean hideForm = false;
    /// <summary>
    /// Flag to decide if hide the grid or not.
    /// </summary>
    public Boolean hideGrid = false;

    /// <summary>
    /// Load all the Assigned Routes to the grid
    /// </summary>
    public void loadAssignedRoutes()
    {
        Routes routes = new Routes();
        DataTable data;

        data = routes.getAssignedRoutes();

        if (data != null && data.Rows.Count > 0)
        {
            this.hideGrid = false;
            GridView1.DataSource = data;
            GridView1.DataBind();
        }
        else
        {
            this.hideGrid = true;
        }
    }

    /// <summary>
    /// Load the page and make some validations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Routes routes = new Routes();
            Users users = new Users();
            DataTable data, data2, data3;

            data = routes.getAllRoutes();

            if (data != null && data.Rows.Count > 0)
            {
                showNotification[0] = false;
            }
            else
            {
                showNotification[0] = true;
            }

            data2 = users.getAllSellers();

            if (data2 != null && data2.Rows.Count > 0)
            {
                showNotification[1] = false;
            }
            else
            {
                showNotification[1] = true;
            }

            if (showNotification[0] && showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay ni rutas ni clientes");
            }
            else if (showNotification[0] && !showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay rutas creadas");
            }
            else if (!showNotification[0] && showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay vendedores");
            }
            else if (!showNotification[0] && !showNotification[1])
            {
                this.hideForm = false;
                this.setNotification("nothing");


                DropDownList1.DataSource = data;
                DropDownList1.DataTextField = "Nombre";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Selecciona una ruta");

                DropDownList2.DataSource = data2;
                DropDownList2.DataTextField = "Nombre";
                DropDownList2.DataValueField = "ID";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "Selecciona un vendedor");

                this.loadAssignedRoutes();
            }
        }
    }

    /// <summary>
    /// Assigns the selected Route to the selected Seller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Routes route = new Routes();

        try
        {
            int idRoute = Convert.ToInt16(DropDownList1.SelectedValue.ToString());
            int idSeller = Convert.ToInt16(DropDownList2.SelectedValue.ToString());

            route.setAssignedRoute(idRoute, idSeller);

            this.setNotification("success", "¡Éxito!", "La ruta ha sido asignada correctamente...");
            this.loadAssignedRoutes();
        }
        catch (Exception ex)
        {
            this.setNotification("error", "¡Ooooops!", "Selecciona una ruta o vendedor válido");
        }
    }
}