using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newproduct : Settings
{
    /// <summary>
    /// Loads the page and analizes the QueryString to catch de GET vars.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("action") != null && Request.QueryString.Get("id") != null && Request.QueryString.Get("action") == "notify")
            {
                int idNotif = Convert.ToInt16(Request.QueryString.Get("id"));
                
                switch (idNotif)
                {
                    case 1:
                        this.setNotification("success", "¡Éxito!", "El producto ha sido guardado correctamente");
                        break;
                    default:
                        this.setNotification("nothing");
                        break;
                }
            }
            else
            {
                this.setNotification("nothing");
            }
        }
        catch (Exception ex)
        {
            this.setNotification("nothing");
            Response.Redirect(webURL + "views/products/newproduct.aspx");
        } 
    }

    /// <summary>
    /// Saves the new Product and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Boolean flag1 = false, flag2 = false, flag3 = false, flag4 = false;
        int idPresentation = Convert.ToUInt16(DropDownList1.SelectedValue);
        string Name = Security.cleanSQL(TextBox1.Text);
        string Description = Security.cleanSQL(TextBox4.Text);
        DateTime FirstDate = DateTime.Now;
        int Amount = 0;
        double Price = 0;
        DateTime ExpirationDate = Convert.ToDateTime("01/01/1985");

        try
        {
            if (Security.isEmpty(Name))
            {
                throw new Exception("1");
            }
            else if (Security.isEmpty(Description))
            {
                throw new Exception("2");
                
            }

            flag4 = true;

            try
            {
                Amount = Convert.ToInt16(TextBox2.Text);
                flag1 = true;
                try
                {
                    Price = Convert.ToDouble(TextBox3.Text);
                    flag2 = true;
                    try
                    {
                        ExpirationDate = Convert.ToDateTime(TextBox5.Text);
                        if (ExpirationDate > DateTime.Now)
                        {
                            flag3 = true;
                        }
                        else
                        {
                            this.setNotification("warning", "¡Producto caducado!", "No se pueden dar de alta productos caducados...");
                            flag3 = false;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        this.setNotification("warning", "¡Fecha inválida!", "La fecha de caducidad ingresada no es válida...");
                        flag3 = false;
                    }
                }
                catch (Exception ex)
                {
                    this.setNotification("warning", "¡Precio inválido!", "El precio ingresado no es válido...");
                    flag2 = false;
                }
            }
            catch (Exception ex)
            {
                this.setNotification("warning", "¡Cantidad inválida!", "La cantidad ingresada no es válida...");
                flag1 = false;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "1")
            {
                this.setNotification("warning", "¡No hay nombre!", "Ingrese un nombre de producto por favor...");
            }
            else if (ex.Message == "2")
            {
                this.setNotification("warning", "¡No hay descripción!", "Ingrese una descripción por favor...");
            }
            flag4 = false;
        }
        
        if (flag1 && flag2 && flag3 && flag4)
        {
            Products product = new Products(0, idPresentation, Name, Amount, Price, Description, FirstDate, ExpirationDate);

            int result = product.saveProduct();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/products/newproduct.aspx?action=notify&id=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe un producto con ese nombre y esa presentación...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}