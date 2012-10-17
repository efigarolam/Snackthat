using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class edituser : Settings
{
    /// <summary>
    /// Method to loads the form with the data of the selected User
    /// </summary>
    /// <param name="idCustomer">Int ID of the Customer</param>
    protected void loadForm(int idUser) 
    {
        Users user = new Users();
        user.idUser = idUser;

        user.getUserByID();

        if (user.idUser != 0)
        {
            HiddenField1.Value = user.idUser.ToString();
            TextBox1.Text = user.Name;
            TextBox2.Text = user.LastName;
            TextBox3.Text = user.Username;
            DropDownList1.SelectedValue = user.idPrivilege.ToString();
            TextBox5.Text = user.Email;
            TextBox6.Text = user.RFC;
            TextBox7.Text = user.Address;
            TextBox8.Text = user.Phone;
        }
        else
        {
            Response.Redirect(webURL + "views/users/viewallusers.aspx?action=notify&id=3", false);
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
                            this.setNotification("success", "¡Éxito!", "El usuario ha sido guardado correctamente");
                            break;
                        default:
                            break;
                    }
                }
                else 
                {
                    this.setNotification("nothing");
                }

                int idUser = Convert.ToInt16(Request.QueryString.Get("id"));

                this.loadForm(idUser);
            }
            else if (Request.QueryString.Get("id") != null && Page.IsPostBack)
            {
                //
            }
            else
            {
                Response.Redirect(webURL + "views/users/viewallusers.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/users/viewallusers.aspx?action=notify&id=3");
        }   
    }

    /// <summary>
    /// Edits the current User and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idUser = Convert.ToInt16(HiddenField1.Value);
        int idPrivilege = Convert.ToUInt16(DropDownList1.SelectedValue);
        string Name = Security.cleanSQL(TextBox1.Text);
        string LastName = Security.cleanSQL(TextBox2.Text);
        string Username = Security.cleanSQL(TextBox3.Text);
        string Email = Security.cleanSQL(TextBox5.Text);
        string Phone = Security.cleanSQL(TextBox8.Text);
        string Address = Security.cleanSQL(TextBox7.Text);
        string RFC = Security.cleanSQL(TextBox6.Text);

        if (Security.isEmpty(Name))
        {
            this.setNotification("warning", "¡No hay nombre!", "Ingrese un nombre por favor...");
        }
        else if (Security.isEmpty(LastName))
        {
            this.setNotification("warning", "¡No hay apellido!", "Ingrese un apellido por favor...");
        }
        else if (Security.isEmpty(Username))
        {
            this.setNotification("warning", "¡No hay usuario!", "Ingrese un usuario por favor...");
        }
        else if (Security.isEmpty(Email))
        {
            this.setNotification("warning", "¡No hay email!", "Ingrese un email por favor...");
        }
        else if (!Security.isEmail(Email))
        {
            this.setNotification("warning", "¡Email inválido!", "El email ingresado no es válido...");
        }
        else if (Security.isEmpty(Address))
        {
            this.setNotification("warning", "¡No hay dirección!", "Ingrese una dirección por favor...");
        }
        else if (Security.isEmpty(RFC))
        {
            this.setNotification("warning", "¡No hay rfc!", "Ingrese un rfc por favor...");
        }
        else if (!Security.isRFC(RFC))
        {
            this.setNotification("warning", "¡RFC Inválido!", "El RFC ingresado no es válido...");
        }
        else if (!Security.isEmpty(Phone) && !Security.isPhone(Phone))
        {
            this.setNotification("warning", "¡Teléfono Inválido!", "El teléfono ingresado no es válido... El formato correcto es LADA+TELEFONO, ejemplo: 31400000");
        }
        else
        {
            Users user = new Users(idUser, idPrivilege, Name, LastName, Username, "", Email, Phone, Address, RFC);

            int result = user.saveUser();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/users/edituser.aspx?id=" + idUser + "&action=notify&nid=" + 1, false);
                    break;
                case -3:
                    this.setNotification("warning", "¡Campo(s) repetido(s)!", "Ya existe alguien registrado con ese usuario, email o RFC...");
                    break;
                case 0:
                    this.setNotification("error", "¡Ooooops!", "No existe el usuario...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}