using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewexpiredproducts : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Expired Products.
    /// </summary>
    protected void loadGridView()
    {
        Products product = new Products();
        DataTable result = product.getExpiredProducts();

        if (result != null && result.Rows.Count > 0)
        {
            GridView1.DataSource = result;
            GridView1.DataBind();
        }
        else
        {
            this.setNotification("error", "¡No hay productos caducados!", "Excelente, no hay productos caducados al día de hoy...");
        }
      
    }

    /// <summary>
    /// Load the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.loadGridView();
        this.setNotification("nothing");
    }

    /// <summary>
    /// Change the format of the dates on the Column Expiration Date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime date = Convert.ToDateTime(e.Row.Cells[4].Text);

            e.Row.Cells[4].Text = date.ToString("dd/MM/yyyy");
        }
    }
}