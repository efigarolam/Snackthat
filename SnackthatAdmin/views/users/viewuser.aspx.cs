using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewuser : Settings
{
    /// <summary>
    /// Property to stores the Username of the User
    /// </summary>
    public string username;
    /// <summary>
    /// Property to stores the ID of the User
    /// </summary>
    public string userid;
    
    /// <summary>
    /// Loads the page and analizes the QueryString to catch de GET vars.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("id") != null)
            {
                int idUser = Convert.ToInt16(Request.QueryString.Get("id"));

                Users user = new Users();
                DataTable dt;

                dt = user.getUserByID(idUser);

                if (dt != null && dt.Columns[0].Caption == "User_Dont_Exists")
                {
                    Response.Redirect(webURL + "views/users/viewallusers.aspx");
                }
                else if (dt.Rows.Count == 1)
                {
                    this.userid = dt.Rows[0].ItemArray[0].ToString();
                    this.username = dt.Rows[0].ItemArray[1].ToString() + " " + dt.Rows[0].ItemArray[2].ToString();
                    
                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();
                }
                else
                {
                    Response.Redirect(webURL + "views/users/viewallusers.aspx");
                }

            }
            else
            {
                Response.Redirect(webURL + "views/users/viewallusers.aspx");
            }
        } catch(Exception ex)
        {
            Response.Redirect(webURL + "views/users/viewallusers.aspx");
        }
    }
}