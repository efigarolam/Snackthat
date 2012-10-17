using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewsell : Settings
{
    public string sellid;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("id") != null)
            {
                int idSell = Convert.ToInt16(Request.QueryString.Get("id"));

                Products sell = new Products();
                DataTable dt;

                dt = sell.getSellByID(idSell);

                if (dt != null && dt.Columns[0].Caption == "Sell_Not_Exists")
                {
                    Response.Redirect(webURL + "views/sells/viewallsells.aspx?action=notify&id=3", false);
                }
                else if (dt.Rows.Count == 1)
                {
                    this.sellid = dt.Rows[0].ItemArray[0].ToString();
                    
                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();
                    
                    DateTime date = Convert.ToDateTime(DetailsView1.Rows[4].Cells[1].Text);
                    DetailsView1.Rows[4].Cells[1].Text = date.ToString("dd/MM/yyyy");

                    dt = sell.getProductsBySell(idSell);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    Response.Redirect(webURL + "views/sells/viewallsells.aspx?action=notify&id=3", false);
                }

            }
            else
            {
                Response.Redirect(webURL + "views/sells/viewallsells.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/sells/viewallsells.aspx");
        }
    }
}