using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class setexpiredproducts : Settings
{
    /// <summary>
    /// Load the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.setNotification("nothing");
        }
    }

    /// <summary>
    /// Set as Expired all the Products which its Expiration Date is lower than the Current Sytem Date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void  Button1_Click(object sender, EventArgs e)
    {
        Products expired = new Products();
        int result;

        result = expired.setExpiredProducts();

        if (result > 0)
        {
            this.setNotification("success", "Productos caducados dados de baja", "Se han dado de baja " + result + " productos...");
        }
        else
        {
            this.setNotification("warning", "No hay productos caducados", "No se encontraron productos caducados, no se dio de baja ninguno...");
        }
    }
}