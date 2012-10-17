using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class login : Settings
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        int result = ValidateUser(Security.cleanSQL(Login1.UserName), Security.encrypt(Login1.Password));
        if (result > 0)
        {
            FormsAuthentication.Initialize();
            String strRole = AssignRoles(Security.cleanSQL(Login1.UserName));
            
            //this.idSeller = result;

            //The AddMinutes determines how long the user will be logged in after leaving
            //the site if he doesn't log off.
            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                Login1.UserName, DateTime.Now,
                DateTime.Now.AddMinutes(3600), false, strRole,
                FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(fat)));
            Response.Redirect(FormsAuthentication.GetRedirectUrl(Security.cleanSQL(Login1.UserName), false));
        }
    }

    private int ValidateUser(String strUsername, String strPassword)
    {
        Users user = new Users();

        user.Username = strUsername;
        user.Password = strPassword;

        return user.login();
    }

    private String AssignRoles(String strUsername)
    {
        Users user = new Users();

        user.Username = strUsername;

        return user.getPrivilegeByUser();
    }
}