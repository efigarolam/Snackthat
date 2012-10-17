using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class sell : Settings
{
    public Boolean[] showNotification = new Boolean[2];
    public Boolean hideForm = false;
    public Boolean hideGrid = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Products products = new Products();
            Customers customers = new Customers();
            DataTable data, data2;

            data = customers.getAllCustomers(0);

            if (data != null && data.Rows.Count > 0)
            {
                showNotification[0] = false;
            }
            else
            {
                showNotification[0] = true;
            }

            data2 = products.getAllProducts();

            if (data2 != null && data2.Rows.Count > 0)
            {
                showNotification[1] = false;
            }
            else
            {
                showNotification[1] = true;
            }

            if (showNotification[0] && showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay ni clientes ni productos");
            }
            else if (showNotification[0] && !showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay clientes");
            }
            else if (!showNotification[0] && showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay productos");
            }
            else if (!showNotification[0] && !showNotification[1])
            {
                this.hideForm = false;
                this.setNotification("nothing");


                DropDownList1.DataSource = data;
                DropDownList1.DataTextField = "Nombre";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Selecciona un cliente");

                GridView1.DataSource = data2;
                GridView1.DataBind();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int idCustomer = 0;
        try
        {
            idCustomer = Convert.ToInt16(DropDownList1.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            idCustomer = 0;
        }
        
        string idProducts = Page.Request["Productos"];
        string Amounts = Page.Request["Cantidad"];
        Double Total = Convert.ToDouble(Page.Request["Total"]);

        string[] aidProducts;
        string[] aAmounts;

        try
        {
            aidProducts = idProducts.Split(new char[] { ',' });
        }
        catch(Exception ex)
        {
            aidProducts = new string[0];
        }

        try
        {
            aAmounts = Amounts.Split(new char[] { ',' });
        }
        catch(Exception ex)
        {
            aAmounts = new string[0];
        }

        if (idCustomer == 0)
        {
            this.setNotification("error", "¡Ooooops!", "Selecciona un cliente...");
        }
        else if (aidProducts.Length == 0)
        {
            this.setNotification("warning", "¡No hay productos seleccionados!", "Selecciona un producto como mínimo...");
        }
        else if (aidProducts.Length != aAmounts.Length)
        {
            this.setNotification("error", "¡Ooooops!", "No hay el mismo número de productos seleccionados que cantidades...");
        }
        else if (Total == 0)
        {
            this.setNotification("error", "¡Ooooops!", "Algo salió mal...");
        }
        else
        {
            Products products = new Products();

            products.saveSell(idCustomer, this.user.idUser, Total, aidProducts, aAmounts);
            this.setNotification("success", "¡Éxito!", "Los productos han sido asignados");
        } 
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cssclass = "id" + Convert.ToInt16(e.Row.Cells[2].Text);
            HtmlString select = new HtmlString("<select class='span2 quantity "+ cssclass +"' name='Cantidad'>");
            HtmlString selectend = new HtmlString("</select>");
            
  
            GridViewRow gvr = (GridViewRow)e.Row.Cells[1].NamingContainer;
            
            

            string options = "";
            for (int i = 0; i <= Convert.ToInt16(e.Row.Cells[5].Text); i++)
            {
                options += "<option value='"+i.ToString()+"'>"+i.ToString()+"</option>";
            }

            gvr.Cells[1].Text = select.ToString() + options + selectend.ToString();
        } 
    }
}