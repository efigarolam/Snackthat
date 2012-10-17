using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editproduct : Settings
{
    /// <summary>
    /// Method to loads the form with the data of the selected Product
    /// </summary>
    /// <param name="idCustomer">Int ID of the Product</param>
    protected void loadForm(int idProduct)
    {
        Products product = new Products();
        product.idProduct = idProduct;

        product.getProductByID();

        if (product.idProduct != 0)
        {
            HiddenField1.Value = product.idProduct.ToString();
            TextBox1.Text = product.Name;
            TextBox2.Text = product.Amount.ToString();
            TextBox3.Text = product.Price.ToString() ;
            TextBox4.Text = product.Description;
            DropDownList1.SelectedValue = product.idPresentation.ToString();
            TextBox5.Text = product.ExpirationDate.ToString("dd/MM/yyyy");
        }
        else
        {
            Response.Redirect(webURL + "views/products/viewallproducts.aspx?action=notify&id=3", false);
        }
    }

    /// <summary>
    /// Loads the page and analizes the QueryString to catch de GET vars.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("id") != null && Page.IsPostBack != true)
            {
                if (Request.QueryString.Get("action") != null && Request.QueryString.Get("nid") != null && Request.QueryString.Get("action") == "notify")
                {
                    int idNotif = Convert.ToInt16(Request.QueryString.Get("nid"));

                    switch (idNotif)
                    {
                        case 1:
                            this.setNotification("success", "¡Éxito!", "El producto ha sido actualizado correctamente...");
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

                int idProduct = Convert.ToInt16(Request.QueryString.Get("id"));

                this.loadForm(idProduct);
            }
            else if (Request.QueryString.Get("id") != null && Page.IsPostBack)
            {
                //
            }
            else
            {
                Response.Redirect(webURL + "views/products/viewallproducts.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/products/viewallproducts.aspx?action=notify&id=3");
        }
    }

    /// <summary>
    /// Edits the current Product and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Boolean flag1 = false, flag2 = false, flag3 = false, flag4 = false;
        int idProduct = Convert.ToInt16(HiddenField1.Value);
        int idPresentation = Convert.ToUInt16(DropDownList1.SelectedValue);
        string Name = Security.cleanSQL(TextBox1.Text);
        string Description = Security.cleanSQL(TextBox4.Text);
        DateTime FirstDate = DateTime.Now;
        int Amount = 0;
        double Price = 0;
        DateTime ExpirationDate = Convert.ToDateTime("01/01/1985 00:00:00");

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
                        flag3 = true;
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
            Products product = new Products(idProduct, idPresentation, Name, Amount, Price, Description, FirstDate, ExpirationDate);

            int result = product.saveProduct();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/products/editproduct.aspx?id=" + idProduct + "&action=notify&nid=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo(s) repetido(s)!", "Ya existe un producto registrado con ese nombre y presentación...");
                    break;
                case 0:
                    this.setNotification("error", "¡Ooooops!", "No existe el producto...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}