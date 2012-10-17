using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class newuser : Settings
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
                        this.setNotification("success", "¡Éxito!", "El usuario ha sido guardado correctamente");
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
            Response.Redirect(webURL + "views/users/newuser.aspx");
        }   
    }

    /// <summary>
    /// Saves the new User and makes the validations.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idPrivilege = Convert.ToUInt16(DropDownList1.SelectedValue);
        string Name = Security.cleanSQL(TextBox1.Text);
        string LastName = Security.cleanSQL(TextBox2.Text);
        string Username = Security.cleanSQL(TextBox3.Text);
        string Password = TextBox4.Text;
        string Email = Security.cleanSQL(TextBox5.Text);
        string Phone = Security.cleanSQL(TextBox8.Text);
        string Address = Security.cleanSQL(TextBox7.Text);
        string RFC = Security.cleanSQL(TextBox6.Text);

        if (!Security.isPassword(Password))
        {
            this.setNotification("warning", "¡Contraseña inválida!", "La contraseña debe contener al menos 6 carácteres, entre ellos al menos un carácter especial o número.");
        }
        else if (Security.isEmpty(Name))
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
            Users user = new Users(0, idPrivilege, Name, LastName, Username, Security.encrypt(Password), Email, Phone, Address, RFC);

            int result = user.saveUser();

            switch (result)
            {
                case 1:
                    Response.Redirect(webURL + "views/users/newuser.aspx?action=notify&id=" + 1, false);
                    break;
                case -1:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe alguien registrado con ese email...");
                    break;
                case -2:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe alguien registrado con ese RFC...");
                    break;
                case -3:
                    this.setNotification("warning", "¡Campo repetido!", "Ya existe alguien registrado con ese usuario...");
                    break;
                default:
                    this.setNotification("error", "¡Ooooops!", "Algo salio mal...");
                    break;
            }
        }
    }
}