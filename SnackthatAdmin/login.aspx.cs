using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class login : Settings
{
    /// <summary>
    /// Load the web page and shows a notification if the user is atempting to access into a forbidden area.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.Request.QueryString.Get("ReturnUrl") != null)
        {
            this.setNotification("error", "¡Error de Acceso!", "No tienes los privilegios suficientes para utilizar el módulo que estás solicitando... Puedes regresar a donde estabas dando click en atrás o puedes proporcionar los datos de una cuenta con los privilegios adecuados...");
        }
        else
        {
            this.setNotification("nothing");
        }        
    }

    /// <summary>
    /// Bring access if the User information is correct, else revoke access.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (ValidateUser(Security.cleanSQL(Login1.UserName), Security.encrypt(Login1.Password)))
        {
            FormsAuthentication.Initialize();
            String strRole = AssignRoles(Security.cleanSQL(Login1.UserName));

            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, Login1.UserName, DateTime.Now, DateTime.Now.AddMinutes(3600), false, strRole, FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
            Response.Redirect(FormsAuthentication.GetRedirectUrl(Security.cleanSQL(Login1.UserName), false));
        }
    }

    /// <summary>
    /// Allows to validate an existing User.
    /// </summary>
    /// <param name="Username">String with the Username</param>
    /// <param name="Password">String with the Password</param>
    /// <returns>Returns true if the User exists, otherwise returns false</returns>
    private Boolean ValidateUser(String Username, String Password)
    {
        Users user = new Users();

        user.Username = Username;
        user.Password = Password;

        return user.login();
    }

    /// <summary>
    /// Determines the role of the User.
    /// </summary>
    /// <param name="Username">String with the Username to find</param>
    /// <returns>Returns the actual Privilege of the User</returns>
    private String AssignRoles(String Username)
    {
        Users user = new Users();

        user.Username = Username;

        return user.getPrivilegeByUser();
    }
}