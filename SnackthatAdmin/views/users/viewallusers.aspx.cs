using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewallusers : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Users.
    /// </summary>
    protected void loadGridView()
    {
        Users user = new Users();

        GridView1.DataSource = user.getAllUsers();
        GridView1.DataBind(); 
    }

    /// <summary>
    /// Deletes a User by ID
    /// </summary>
    /// <param name="id">Int ID of the User</param>
    /// <returns>Returns true if the operation has been successfull, othewrise returns false</returns>
    protected Boolean deleteUser(int id)
    {
        Users user = new Users();
        return user.deleteUserByID(id);
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
                    int idUser = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (this.deleteUser(idUser))
                    {
                        Response.Redirect(webURL + "views/users/viewallusers.aspx?action=notify&id=1", false);
                    }
                    else
                    {
                        Response.Redirect(webURL + "views/users/viewallusers.aspx?action=notify&id=2", false);
                    }
                }
                else if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 1)
                    {
                        this.setNotification("success", "¡Éxito!", "El usuario ha sido eliminado correctamente...");
                    }
                    else if(idNotif == 2)
                    {
                        this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    }
                    else if (idNotif == 3)
                    {
                        this.setNotification("error", "¡Ooooops!", "El usuario no existe...");
                    }

                    loadGridView();
                }
                else
                {
                    this.setNotification("nothing");
                    Response.Redirect(webURL + "views/users/viewallusers.aspx", false);
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
            Response.Redirect(webURL + "views/users/viewallusers.aspx");
        }              
    }
}