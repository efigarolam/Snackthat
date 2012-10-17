using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newcustomer : Settings
{
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
                        this.setNotification("success", "¡Éxito!", "El cliente ha sido guardado correctamente");
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
            Response.Redirect(webURL + "views/customers/newcustomer.aspx");
        }   
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
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
        else if(flag)
        {
            Customers customer = new Customers(0, Name, LastName, Email, RFC, listOfValidAddresses);

            int result = customer.saveCustomer();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/customers/newcustomer.aspx?action=notify&id=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe un cliente registrado con ese email...");
                    break;
                case -2:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe un cliente registrado con ese RFC...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}