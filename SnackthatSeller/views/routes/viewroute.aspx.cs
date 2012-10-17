using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewroute : Settings
{
    public string name;
    public string routeid;
    public Boolean showData = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Routes route = new Routes();
            DataTable dt;

            dt = route.getAllRoutes();

            if (dt != null && dt.Rows.Count == 1)
            {
                showData = true;
                this.routeid = dt.Rows[0].ItemArray[0].ToString();
                this.name = dt.Rows[0].ItemArray[1].ToString();

                DetailsView1.DataSource = dt;
                DetailsView1.DataBind();

                Customers customers = new Customers();

                dt = customers.getCustomersAndAddressesByRoute(Convert.ToInt16(this.routeid));
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    this.setNotification("nothing");
                }
            }
            else
            {
                showData = false;
                this.setNotification("error", "No hay ruta asignada", "Todavía no te es asignada una ruta...");
            }
        }
        catch (Exception ex)
        {
            this.setNotification("error", "No hay ruta asignada", "Todavía no te es asignada una ruta");
        }

    }
}