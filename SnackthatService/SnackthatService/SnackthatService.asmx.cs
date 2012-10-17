using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace SnackthatService
{
    /// <summary>
    /// This Web Service sets and gets information to be syncronized.
    /// </summary>
    [WebService(Namespace = "http://localhost/SnackthatService", Name = "SnackthatService", Description = "Snackthat Web Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SnackthatWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// Obtains all the Products assigned to a Seller
        /// </summary>
        /// <param name="idSeller">Int ID of the Seller</param>
        /// <returns>Returns a DataTable with all the rows of assigned Products</returns>
        [WebMethod]
        public DataTable getProductsBySeller(int idSeller)
        {
            return new Data().getProductsBySeller(idSeller);
        }

        /// <summary>
        /// Obtains the Route assigned to a Seller
        /// </summary>
        /// <param name="idSeller">Int ID of the Seller</param>
        /// <returns>Returns a DataTable with the row of the assigned Route</returns>
        [WebMethod]
        public DataTable getRouteBySeller(int idSeller)
        {
            return new Data().getRouteBySeller(idSeller);
        }

        /// <summary>
        /// Obtains all the Sellers of the application
        /// </summary>
        /// <returns>Returns a DataTable with all the rows of Sellers</returns>
        [WebMethod]
        public DataTable getSellers()
        {
            return new Data().getSellers();
        }

        /// <summary>
        /// Obtains all the Customers assigned to a Route
        /// </summary>
        /// <param name="idRoute">Int ID of the Route</param>
        /// <returns>Returns a DataTable with all the rows of assigned Customers</returns>
        [WebMethod]
        public DataTable getCustomersByRoute(int idRoute)
        {
            return new Data().getCustomersByRoute(idRoute);
        }

        /// <summary>
        /// Store all the new Customers created by the Seller
        /// </summary>
        /// <param name="dt">DataTable with the new Customers</param>
        /// <param name="idRoute">Int ID of the current Route of the Seller</param>
        [WebMethod]
        public void setCustomers(DataTable dt, int idRoute)
        {
            new Data().setCustomers(dt, idRoute);
        }

        /// <summary>
        /// Store all the information about to the Sells.
        /// </summary>
        /// <param name="dtProducts">DataTable with the left Products</param>
        /// <param name="dtSells">DataTable with all the done Sells</param>
        /// <param name="dtProductsSells">DataTable with all the sold Products</param>
        /// <param name="idRoute">Int ID of the current Route of the Seller</param>
        [WebMethod]
        public void setSells(DataTable dtProducts, DataTable dtSells, DataTable dtProductsSells, int idRoute)
        {
            new Data().setSells(dtProducts, dtSells, dtProductsSells, idRoute);
        }
    }
}