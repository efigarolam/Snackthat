using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class assignproducts : Settings
{
    /// <summary>
    /// Array Flag to decide if show a notification or not.
    /// </summary>
    public Boolean[] showNotification = new Boolean[2];
    /// <summary>
    /// Flag to decide if hide the form or not.
    /// </summary>
    public Boolean hideForm = false;
    /// <summary>
    /// Flag to decide if hide the grid or not.
    /// </summary>
    public Boolean hideGrid = false;

    /// <summary>
    /// Load the page and make some validations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Products products = new Products();
            Users users = new Users();
            DataTable data, data2;

            data = users.getAllSellers();

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
                this.setNotification("error", "¡Ooooops!", "No hay ni vendedores ni productos");
            }
            else if (showNotification[0] && !showNotification[1])
            {
                this.hideForm = true;
                this.setNotification("error", "¡Ooooops!", "No hay vendedores");
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
                DropDownList1.Items.Insert(0, "Selecciona una vendedor");

                GridView1.DataSource = data2;
                GridView1.DataBind();
            }
        }
    }

    /// <summary>
    /// Assigns the selected Products to the selected Seller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idSeller = 0;
        try
        {
            idSeller = Convert.ToInt16(DropDownList1.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            idSeller = 0;
        }
        
        string idProducts = Page.Request["Productos"];
        string Amounts = Page.Request["Cantidad"];
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

        if (idSeller == 0)
        {
            this.setNotification("error", "¡Ooooops!", "Selecciona un vendedor...");
        }
        else if (aidProducts.Length == 0)
        {
            this.setNotification("warning", "¡No hay productos seleccionados!", "Selecciona un producto como mínimo...");
        }
        else if (aidProducts.Length != aAmounts.Length)
        {
            this.setNotification("error", "¡Ooooops!", "No hay el mismo número de productos seleccionados que cantidades...");
        }
        else
        {
            Products products = new Products();
            products.assignProducts(idSeller, aidProducts, aAmounts);
            this.setNotification("success", "¡Éxito!", "Los productos han sido asignados");
        } 
    }

    /// <summary>
    /// Creates a HTML Select Tag with options from 0 to the amount of each Product of the row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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