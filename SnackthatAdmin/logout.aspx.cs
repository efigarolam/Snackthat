using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class logout : Settings
{
    /// <summary>
    /// Finalizes the Session of the User and Sign Out from the Cookie, finally redirects to the User to the login form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Page.Response.Redirect(webURL + "login.aspx");
    }
}