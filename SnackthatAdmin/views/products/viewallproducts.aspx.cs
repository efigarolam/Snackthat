using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class viewallproducts : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Products.
    /// </summary>
    protected void loadGridView()
    {
        Products product = new Products();

        GridView1.DataSource = product.getAllProducts();
        GridView1.DataBind();
    }

    /// <summary>
    /// Deletes a Product by ID
    /// </summary>
    /// <param name="id">Int ID of the Product</param>
    /// <returns>Returns true if the operation has been successfull, othewrise returns false</returns>
    protected Boolean deleteProduct(int id)
    {
        Products product = new Products();
        return product.deleteProductByID(id);
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
                    int idProduct = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (this.deleteProduct(idProduct))
                    {
                        Response.Redirect(webURL + "views/products/viewallproducts.aspx?action=notify&id=1", false);
                    }
                    else
                    {
                        Response.Redirect(webURL + "views/products/viewallproducts.aspx?action=notify&id=2", false);
                    }
                }
                else if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 1)
                    {
                        this.setNotification("success", "¡Éxito!", "El producto ha sido eliminado correctamente...");
                    }
                    else if (idNotif == 2)
                    {
                        this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    }
                    else if (idNotif == 3)
                    {
                        this.setNotification("error", "¡Ooooops!", "El producto no existe...");
                    }

                    loadGridView();
                }
                else
                {
                    this.setNotification("nothing");
                    Response.Redirect(webURL + "views/products/viewallproducts.aspx", false);
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
            Response.Redirect(webURL + "views/products/viewallproducts.aspx");
        }
    }
}