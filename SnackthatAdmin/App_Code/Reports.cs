using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// This class deals with the reports creation
/// </summary>
public class Reports
{    
    /// <summary>
    /// Method that obtain all the necessary information to create a Report by a Product.
    /// </summary>
    /// <param name="product">Int ID of the Product</param>
    /// <returns>Returns a DataSet with all the information of the Product</returns>
	public static DataSet getReportByProduct(int product)
    {
        dsProducts dsP = new dsProducts();
        dsProductsTableAdapters.reportsTableAdapter table = new dsProductsTableAdapters.reportsTableAdapter();

        try
        {
            table.FillByProduct(dsP.reports, product);
            return dsP;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Method that obtain all the necessary information to create a Report by a Route
    /// </summary>
    /// <param name="route">Int ID of the Route</param>
    /// <returns>Returns a DataSet with all the information of the Route</returns>
    public static DataSet getReportByRoute(int route)
    {
        dsProducts dsP = new dsProducts();
        dsProductsTableAdapters.reportsTableAdapter table = new dsProductsTableAdapters.reportsTableAdapter();

        try
        {
            table.FillByRoute(dsP.reports, route);            
            return dsP;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Method that obtain all the necessary information to create a Report by a Seller
    /// </summary>
    /// <param name="seller">Int ID of the Seller</param>
    /// <returns>Returns a DataSet with all the information of the Seller</returns>
    public static DataSet getReportBySeller(int seller)
    {
        dsProducts dsP = new dsProducts();
        dsProductsTableAdapters.reportsTableAdapter table = new dsProductsTableAdapters.reportsTableAdapter();

        try
        {
            table.FillBySeller(dsP.reports, seller);            
            return dsP;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Method that obtain all the necessary information to create a Report by a Month
    /// </summary>
    /// <param name="month">Int ID of the number of the Month</param>
    /// <returns>Returns a DataSet with all the information of the Month</returns>
    public static DataSet getReportByMonth(int month)
    {
        dsProducts dsP = new dsProducts();
        dsProductsTableAdapters.reportsTableAdapter table = new dsProductsTableAdapters.reportsTableAdapter();

        try
        {
            table.FillByMonth(dsP.reports, month);            
            return dsP;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Method that obtain all the necessary information to create a Report by Expired Products
    /// </summary>
    /// <returns>Returns a DataSet with all the information of Expired Products</returns>
    public static DataSet getReportByExpired()
    {
        dsProducts dsP = new dsProducts();
        dsProductsTableAdapters.reportsByExpiredTableAdapter table = new dsProductsTableAdapters.reportsByExpiredTableAdapter();

        try
        {
            table.Fill(dsP.reportsByExpired);
            return dsP;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}