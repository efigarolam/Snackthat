using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class sync : Settings
{
    public int idSeller;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setNotification("nothing");
        this.idSeller = new Users().getIDByUsername(Page.User.Identity.Name);
    }

    /*protected void Button1_Click(object sender, EventArgs e)
    {
        int idSeller = new Users().getIDByUsername(Page.User.Identity.Name);
        Sync syncronization = new Sync();

        syncronization.receiveData(idSeller);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Sync syncronization = new Sync();

        syncronization.sendData();
    }*/
}