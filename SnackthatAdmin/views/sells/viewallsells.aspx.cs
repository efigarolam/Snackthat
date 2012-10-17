using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewallsells : Settings
{
    /// <summary>
    /// Method which loads the gridview with all the Sells.
    /// </summary>
    public void loadGridView()
    {
        Products sells = new Products();
        DataTable dt = sells.getAllSells();

        if (dt != null && dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            this.setNotification("error", "No hay ventas", "No se han registrado ventas todavía");
        }
    }

    /// <summary>
    /// Deletes a Sell by ID
    /// </summary>
    /// <param name="id">Int ID of the Sell</param>
    /// <returns>Returns true if the operation has been successfull, othewrise returns false</returns>
    protected Boolean deleteSell(int id)
    {
        Products sell = new Products();
        return sell.deleteSellByID(id);
    }

    /// <summary>
    /// Load the page and analizes the QueryString to catch de GET vars.
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
                    int idSell = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (this.deleteSell(idSell))
                    {
                        Response.Redirect(webURL + "views/sells/viewallsells.aspx?action=notify&id=1", false);
                    }
                    else
                    {
                        Response.Redirect(webURL + "views/sells/viewallsells.aspx?action=notify&id=2", false);
                    }
                }
                else if (Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));

                    if (idNotif == 1)
                    {
                        this.setNotification("success", "¡Éxito!", "La venta ha sido eliminada correctamente...");
                    }
                    else if (idNotif == 2)
                    {
                        this.setNotification("error", "¡Ooooops!", "Algo salió mal...");
                    }
                    else if (idNotif == 3)
                    {
                        this.setNotification("error", "¡Ooooops!", "La venta no existe...");
                    }

                    loadGridView();
                }
                else
                {
                    this.setNotification("nothing");
                    Response.Redirect(webURL + "views/sells/viewallsells.aspx", false);
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
            Response.Redirect(webURL + "views/sells/viewallsells.aspx");
        }
    }

    /// <summary>
    /// Changes the format of the Date of the Column Sell Date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime date = Convert.ToDateTime(e.Row.Cells[7].Text);
            e.Row.Cells[7].Text = date.ToString("dd/MM/yyyy");
        }
    }
}