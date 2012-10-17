using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewallsells : Settings
{
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
            this.setNotification("error", "No hay ventas", "No haz realizado ninguna venta al parecer...");
        }
    }

    protected Boolean deleteSell(int id)
    {
        Products sell = new Products();
        return sell.deleteSellByID(id);
    }

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DateTime date = Convert.ToDateTime(e.Row.Cells[6].Text);
            e.Row.Cells[6].Text = date.ToString("dd/MM/yyyy");
        }
    }
}