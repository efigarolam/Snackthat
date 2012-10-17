using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class viewproduct : Settings
{
    /// <summary>
    /// Property to stores the Name of the Product
    /// </summary>
    public string name;
    /// <summary>
    /// Property to stores the ID of the Product
    /// </summary>
    public string productid;

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
                int idProduct = Convert.ToInt16(Request.QueryString.Get("id"));

                Products product = new Products();
                DataTable dt;

                dt = product.getProductByID(idProduct);

                if (dt != null && dt.Columns[0].Caption == "Product_Dont_Exists")
                {
                    Response.Redirect(webURL + "views/products/viewallproducts.aspx");
                }
                else if (dt.Rows.Count == 1)
                {
                    this.productid = dt.Rows[0].ItemArray[0].ToString();
                    this.name = dt.Rows[0].ItemArray[1].ToString();

                    DetailsView1.DataSource = dt;
                    DetailsView1.DataBind();
                }
                else
                {
                    Response.Redirect(webURL + "views/products/viewallproducts.aspx");
                }

            }
            else
            {
                Response.Redirect(webURL + "views/products/viewallproducts.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(webURL + "views/products/viewallproducts.aspx");
        }
    }
}