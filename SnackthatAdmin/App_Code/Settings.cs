using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// This class works like an abstraction layer to share properties between all the web Pages.
/// </summary>
public class Settings : System.Web.UI.Page
{
    /// <summary>
    /// Property to set the general URL to link correctly the styles, scripts and redirects.
    /// </summary>
    public static string _webURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Snackthat/";
    
    /// <summary>
    /// Array to store the notifications.
    /// </summary>
    public string[] notification = new string[3];

    /// <summary>
    /// ArrayList to store some data used to generate scripts on the client side.
    /// </summary>
    public ArrayList dataToScript;

    /// <summary>
    /// Users object to store the logued user information.
    /// </summary>
    public Users user;

    /// <summary>
    /// Method to set the dataToScriptProperty
    /// </summary>
    /// <param name="data">ArrayList with the data</param>
    public void setDataToScript(ArrayList data)
    {
        this.dataToScript = data;
    }

    /// <summary>
    /// Method to set the notification
    /// </summary>
    /// <param name="type">String with the Type of the notification</param>
    /// <param name="title">String with the Title of the notification</param>
    /// <param name="message">String with the Message of the notification</param>
    protected void setNotification(string type, string title = "", string message = "")
    {
        this.notification[0] = type;
        this.notification[1] = title;
        this.notification[2] = message;
    }

    /// <summary>
    /// Allows you to set and get the webURl property
    /// </summary>
    public string webURL
    {
        get
        {
            return _webURL;
        }
    }

    /// <summary>
    /// Constructor of the class, initialize the user property with the logued User.
    /// </summary>
	public Settings()
	{
        if (Page.User.Identity.IsAuthenticated)
        {
            this.user = new Users();
            this.user.getInstanceOf(Page.User.Identity.Name);
        }
	}
}