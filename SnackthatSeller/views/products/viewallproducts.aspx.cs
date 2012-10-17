using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewallproducts : Settings
{
    protected void loadGridView()
    {
        Products product = new Products();
        DataTable dt = product.getAllProducts();

        if (dt != null && dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            this.setNotification("error", "No hay productos asignados", "No se te han asignado productos...");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("action") != null && Request.QueryString.Get("id") != null)
            {
                if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 2)
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