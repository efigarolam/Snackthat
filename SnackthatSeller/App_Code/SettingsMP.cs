using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class works like an abstraction layer to share properties between all the web MasterPages.
/// </summary>
public class SettingsMP : System.Web.UI.MasterPage
{
    /// <summary>
    /// Property to set the general URL to link correctly the styles and scripts.
    /// </summary>
    public static string _webURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Snackthatseller/";

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
    /// An empty Constructor, do nothing
    /// </summary>
	public SettingsMP()
	{
	}
}