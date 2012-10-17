using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class views_reports_reports : Settings
{
    /// <summary>
    /// Load all the Routes in a DropDownList
    /// </summary>
    private void loadRoutes()
    {
        DataTable routes = new Routes().getAllRoutes();

        if (routes != null && routes.Rows.Count > 0)
        {
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataTextField = "Nombre";
            DropDownList1.DataSource = routes;
            DropDownList1.DataBind();
            ListItem allRoutes = new ListItem("Todas las rutas", "0");
            DropDownList1.Items.Insert(0, allRoutes);
        }
    }

    /// <summary>
    /// Load all the Products in a DropDownList
    /// </summary>
    private void loadProducts()
    {
        DataTable products = new Products().getAllProducts();

        if (products != null && products.Rows.Count > 0)
        {
            DropDownList2.DataValueField = "ID";
            DropDownList2.DataTextField = "Nombre";
            DropDownList2.DataSource = products;
            DropDownList2.DataBind();
            ListItem allProducts = new ListItem("Todos los productos", "0");
            DropDownList2.Items.Insert(0, allProducts);
        }
    }

    /// <summary>
    /// Load all the Sellers in a DropDownList
    /// </summary>
    private void loadSellers()
    {
        DataTable sellers = new Users().getAllSellers();

        if (sellers != null && sellers.Rows.Count > 0)
        {
            DropDownList3.DataValueField = "ID";
            DropDownList3.DataTextField = "Nombre";
            DropDownList3.DataSource = sellers;
            DropDownList3.DataBind();
            ListItem allSellers = new ListItem("Todos los vendedores", "0");
            DropDownList3   .Items.Insert(0, allSellers);
        }
    }

    /// <summary>
    /// Load all the Months in a DropDownList
    /// </summary>
    private void loadMonths()
    {
        ListItem allMonths = new ListItem("Todos los meses", "0");
        ListItem january = new ListItem("Enero", "1");
        ListItem february = new ListItem("Febrero", "2");
        ListItem march = new ListItem("Marzo", "3");
        ListItem april = new ListItem("Abril", "4");
        ListItem may = new ListItem("Mayo", "5");
        ListItem june = new ListItem("Junio", "6");
        ListItem july = new ListItem("Julio", "7");
        ListItem august = new ListItem("Agosto", "8");
        ListItem september = new ListItem("Septiembre", "9");
        ListItem october = new ListItem("Octubre", "10");
        ListItem november = new ListItem("Noviembre", "11");
        ListItem december = new ListItem("Diciembre", "12");

        DropDownList4.Items.Add(allMonths);
        DropDownList4.Items.Add(january);
        DropDownList4.Items.Add(february);
        DropDownList4.Items.Add(march);
        DropDownList4.Items.Add(april);
        DropDownList4.Items.Add(may);
        DropDownList4.Items.Add(june);
        DropDownList4.Items.Add(july);
        DropDownList4.Items.Add(august);
        DropDownList4.Items.Add(september);
        DropDownList4.Items.Add(october);
        DropDownList4.Items.Add(november);
        DropDownList4.Items.Add(december);
    }

    /// <summary>
    /// Loads the Page and analize the QueryString to catch the GET vars and make some validations.
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
                        this.setNotification("warning", "¡No hay datos!", "No hay información suficiente para generar el reporte...");
                        break;
                    case 2:
                        this.setNotification("error", "¡Registro incorrecto!", "El registro seleccionado, al parecer no existe... ");
                        break;
                    case 3:
                        this.setNotification("error", "¡Oooops!", "Algo salió mal...");
                        break;
                    default:
                        this.setNotification("nothing");
                        break;
                }
                if (!Page.IsPostBack)
                {
                    this.loadRoutes();
                    this.loadProducts();
                    this.loadSellers();
                    this.loadMonths();
                }
            }
            else if (!Page.IsPostBack)
            {
                this.loadRoutes();
                this.loadProducts();
                this.loadSellers();
                this.loadMonths();
                this.setNotification("nothing");
            }
        }
        catch (Exception ex)
        {
            this.setNotification("nothing");
            Response.Redirect(webURL + "views/reports/reports.aspx");
        }   
    }

    /// <summary>
    /// Redirects to the form that creates the report with the GET var of the ID from the Route.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int idRoute;
        try
        {
            idRoute = Convert.ToInt16(DropDownList1.SelectedValue);
        }
        catch (Exception ex)
        {
            idRoute = 0;
        }

        Page.Response.Redirect(webURL + "views/reports/reportroutes.aspx?id=" + idRoute.ToString());
    }

    /// <summary>
    /// Redirects to the form that creates the report with the GET var of the ID from the Product.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        int idProduct;
        try
        {
            idProduct = Convert.ToInt16(DropDownList2.SelectedValue);
        }
        catch (Exception ex)
        {
            idProduct = 0;
        }

        Page.Response.Redirect(webURL + "views/reports/reportproducts.aspx?id=" + idProduct.ToString());
    }

    /// <summary>
    /// Redirects to the form that creates the report with the GET var of the ID from the Seller.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        int idSeller;
        try
        {
            idSeller = Convert.ToInt16(DropDownList3.SelectedValue);
        }
        catch (Exception ex)
        {
            idSeller = 0;
        }

        Page.Response.Redirect(webURL + "views/reports/reportsellers.aspx?id=" + idSeller.ToString());
    }

    /// <summary>
    /// Redirects to the form that creates the report with the GET var of the ID from the Month.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button4_Click(object sender, EventArgs e)
    {
        int idMonth;
        try
        {
            idMonth = Convert.ToInt16(DropDownList4.SelectedValue);
        }
        catch (Exception ex)
        {
            idMonth = 0;
        }

        Page.Response.Redirect(webURL + "views/reports/reportmonth.aspx?id=" + idMonth.ToString());
    }

    /// <summary>
    /// Redirects to the form that creates the report of Expired Products.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button5_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect(webURL + "views/reports/reportexpired.aspx");
    }
}