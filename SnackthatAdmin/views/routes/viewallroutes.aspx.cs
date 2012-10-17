using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewallroutes : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Routes.
    /// </summary>
    protected void loadGridView()
    {
        Routes route = new Routes();

        GridView1.DataSource = route.getAllRoutes();
        GridView1.DataBind();
    }

    /// <summary>
    /// Deletes a Route by ID
    /// </summary>
    /// <param name="id">Int ID of the Route</param>
    /// <returns>Returns true if the operation has been successfull, othewrise returns false</returns>
    protected Boolean deleteRoute(int id)
    {
        Routes route = new Routes();
        return route.deleteRouteByID(id);
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

                    if (this.deleteRoute(idCustomer))
                    {
                        Response.Redirect(webURL + "views/routes/viewallroutes.aspx?action=notify&id=1", false);
                    }
                    else
                    {
                        Response.Redirect(webURL + "views/routes/viewallroutes.aspx?action=notify&id=2", false);
                    }
                }
                else if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 1)
                    {
                        this.setNotification("success", "¡Éxito!", "La ruta ha sido eliminado correctamente...");
                    }
                    else if (idNotif == 2)
                    {
                        this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    }
                    else if (idNotif == 3)
                    {
                        this.setNotification("error", "¡Ooooops!", "La ruta no existe...");
                    }

                    loadGridView();
                }
                else
                {
                    this.setNotification("nothing");
                    Response.Redirect(webURL + "views/routes/viewallroutes.aspx", false);
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
            Response.Redirect(webURL + "views/routes/viewallroutes.aspx");
        }
    }
}