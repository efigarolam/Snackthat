using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class editcustomer : Settings
{
    /// <summary>
    /// ArrayList to store the Addresses of the current Customer
    /// </summary>
    public ArrayList Addresses = new ArrayList();
    /// <summary>
    /// ArrayList to store the Phones of the current Customer
    /// </summary>
    public ArrayList Phones = new ArrayList();

    /// <summary>
    /// Method to loads the form with the data of the selected Customer
    /// </summary>
    /// <param name="idCustomer">Int ID of the Customer</param>
    protected void loadForm(int idCustomer)
    {
        Customers customer = new Customers();
        customer.idCustomer = idCustomer;

        customer.getCustomerByID();

        if (customer.idCustomer != 0)
        {
            HiddenField1.Value = customer.idCustomer.ToString();
            TextBox1.Text = customer.Name;
            TextBox2.Text = customer.LastName;
            TextBox3.Text = customer.Email;
            TextBox4.Text = customer.RFC;

            this.loadAddresses(customer.idCustomer);
            
        }
        else
        {
            Response.Redirect(webURL + "views/customers/viewallcustomers.aspx?action=notify&id=3", false);
        }
    }

    /// <summary>
    /// Method to loads the Addresses of the selected Customer
    /// </summary>
    /// <param name="idCustomer">Int ID of the Customer</param>
    protected void loadAddresses(int idCustomer)
    {
        Customers customer = new Customers();
        DataTable dt = customer.getCustomerAddresses(idCustomer);

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.Addresses.Add(dt.Rows[i].ItemArray[0].ToString());
                this.Phones.Add(dt.Rows[i].ItemArray[1].ToString());
            }
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
                            this.setNotification("success", "¡Éxito!", "El cliente ha sido actualizado correctamente...");
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

                int idCustomer = Convert.ToInt16(Request.QueryString.Get("id"));

                this.loadForm(idCustomer);
            }
            else if (Request.QueryString.Get("id") != null && Page.IsPostBack)
            {
                int idCustomer = Convert.ToInt16(Request.QueryString.Get("id"));
                this.loadAddresses(idCustomer);
            }
            else
            {
                Response.Redirect(webURL + "views/customers/viewallcustomers.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/customers/viewallcustomers.aspx?action=notify&id=3");
        }
    }

    /// <summary>
    /// Edits the current Customer and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idCustomer = Convert.ToInt16(HiddenField1.Value);
        string Name = Security.cleanSQL(TextBox1.Text);
        string LastName = Security.cleanSQL(TextBox2.Text);
        string Email = Security.cleanSQL(TextBox3.Text);
        string RFC = Security.cleanSQL(TextBox4.Text);
        int nAddresses = Convert.ToInt16(Page.Request["nAddresses"]);
        ArrayList listOfValidAddresses = new ArrayList();
        Boolean flag = false;

        try
        {
            int j = 1;
            for (int i = 0; i < nAddresses; i++)
            {
                string[] data = new string[2];

                if (Page.Request["Address" + j.ToString()] != "")
                {
                    data[0] = Security.cleanSQL(Page.Request["Address" + j.ToString()].ToString());
                    if (Page.Request["Phone" + j.ToString()] != "")
                    {
                        data[1] = Security.cleanSQL(Page.Request["Phone" + j.ToString()].ToString());
                        if (Security.isPhone(data[1]))
                        {
                            listOfValidAddresses.Add(data);
                            flag = true;
                        }
                        else
                        {
                            data[1] = "";
                            listOfValidAddresses.Add(data);
                            throw new Exception("1");
                        }
                    }
                    else
                    {
                        data[1] = "";
                        listOfValidAddresses.Add(data);
                        flag = true;
                    }
                }
                j++;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "1")
            {
                this.setNotification("warning", "¡Teléfono Inválido!", "El teléfono ingresado no es válido... El formato correcto es LADA+TELEFONO, ejemplo: 31400000");
                flag = false;
            }
        }

        if (Security.isEmpty(Name))
        {
            this.setNotification("warning", "¡No hay nombre!", "Ingrese un nombre por favor...");
        }
        else if (Security.isEmpty(LastName))
        {
            this.setNotification("warning", "¡No hay apellido!", "Ingrese un apellido por favor...");
        }
        else if (Security.isEmpty(Email))
        {
            this.setNotification("warning", "¡No hay email!", "Ingrese un email por favor...");
        }
        else if (!Security.isEmail(Email))
        {
            this.setNotification("warning", "¡Email inválido!", "El email ingresado no es válido...");
        }        
        else if (Security.isEmpty(RFC))
        {
            this.setNotification("warning", "¡No hay rfc!", "Ingrese un rfc por favor...");
        }
        else if (!Security.isRFC(RFC))
        {
            this.setNotification("warning", "¡RFC Inválido!", "El RFC ingresado no es válido...");
        }
        else if (listOfValidAddresses.Count == 0)
        {
            this.setNotification("warning", "¡No hay dirección!", "Ingresa una dirección como mínimo por favor...");
        }
        else if (flag)
        {
            Customers customer = new Customers(idCustomer, Name, LastName, Email, RFC, listOfValidAddresses);

            int result = customer.saveCustomer();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/customers/editcustomer.aspx?id=" + idCustomer + "&action=notify&nid=" + 1, false);
                    break;
                case -3:
                    this.setNotification("warning", "¡Campo(s) repetido(s)!", "Ya existe un cliente registrado con ese usuario, email o RFC...");
                    break;
                case 0:
                    this.setNotification("error", "¡Ooooops!", "No existe el cliente...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salió mal...");
                    break;
            }
        }
    }
}