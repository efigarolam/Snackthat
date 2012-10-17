using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// This class deals with the Routes of the application, allows you to do the CRUD operations.
/// </summary>
public class Routes
{
    private int _idRoute;
    private string _Name;
    private string[] _Addresses;
    private ActiveRecord aR = new ActiveRecord();

    /// <summary>
    /// Allows you to set and get the ID of the Route
    /// </summary>
	public int idRoute
	{
        get
        {
            return this._idRoute;
        }
        set
        {
            this._idRoute = value;
        }        
	}

    /// <summary>
    /// Allows you to set and get the Name of the Route
    /// </summary>
    public string Name
    {
        get
        {
            return this._Name;
        }
        set
        {
            this._Name = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Addresses of the Route
    /// </summary>
    public string[] Addresses
    {
        get
        {
            return _Addresses;
        }
        set
        {
            this._Addresses = value;
        }

    }

    /// <summary>
    /// An empty constructor of the class Routes
    /// </summary>
    public Routes()
    {

    }

    /// <summary>
    /// Constructor of the class Routes
    /// </summary>
    /// <param name="idRoute">Int ID of the Route</param>
    /// <param name="Name">String with the Name of the Route</param>
    /// <param name="CustomersAddresses">String with the Addresses of the Customers assigned to the Route</param>
    public Routes(int idRoute, string Name, string CustomersAddresses)
    {
        this.idRoute = idRoute;
        this.Name = Name;
        if (CustomersAddresses != null)
        {
            this.Addresses = CustomersAddresses.Split(new char[] { ',' });
        }
        else
        {
            this.Addresses = null;
        }
    }

    /// <summary>
    /// Method to identify the result of the operation that has been done.
    /// </summary>
    /// <param name="dt">DataTable to analize and return the proper code</param>
    /// <returns>Returns the result code. 1 for success, -1 for existing Route, 0 for missing Route</returns>
    public int result(DataTable dt)
    {
        switch (dt.Columns[0].Caption)
        {
            case "idRoute":
                return 1;
            case "Route_Exists":
                return -1;
            case "Route_Not_Exists":
                return 0;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Method to save or update the current instance of a Route
    /// </summary>
    /// <returns>Returns the result code of the operation</returns>
    public int saveRoute()
    {
        ArrayList route = new ArrayList();
        ArrayList customer = new ArrayList();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        
        if (this.idRoute == 0)
        {
            route.Add(this.Name);

            dt = this.aR.callProcedure("setRoute", route);
            
            if (dt != null)
            {
                int result = this.result(dt);

                if (result == 1)
                {
                    if (this.Addresses != null)
                    {
                        for (int i = 0; i <= this.Addresses.Count() - 1; i++)
                        {
                            customer.Add(dt.Rows[0].ItemArray[0].ToString());
                            customer.Add(this.Addresses[i]);
                            this.aR.callProcedure("setCustomersRoutes", customer);
                            customer.Clear();
                        }
                    }

                    return result;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return 0;
            }
        }
        else if (this.idRoute > 0)
        {
            route.Add(this.idRoute);
            route.Add(this.Name);

            dt = this.aR.callProcedure("updateRoute", route);

            if (dt != null)
            {
                int result = this.result(dt);

                if (result == 1)
                {
                    if (this.Addresses != null)
                    {
                        for (int i = 0; i <= this.Addresses.Count() - 1; i++)
                        {
                            customer.Add(dt.Rows[0].ItemArray[0].ToString());
                            customer.Add(this.Addresses[i]);
                            this.aR.callProcedure("setCustomersRoutes", customer);
                            customer.Clear();
                        }
                    }

                    return result;
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Method to obtain all the Routes of the database.
    /// </summary>
    /// <returns>Returns a DataTable with all the rows of Routes</returns>
    public DataTable getAllRoutes()
    {
        return this.aR.callProcedure("getAllRoutes");
    }

    /// <summary>
    /// Method to delete an existing Route.
    /// </summary>
    /// <param name="id">Int ID of the Route to be deleted</param>
    /// <returns>Returns true if the operation has been successfull, false in the other way</returns>
    public Boolean deleteRouteByID(int id)
    {
        ArrayList parameters = new ArrayList();
        DataTable result;

        parameters.Add(id);
        result = this.aR.callProcedure("deleteRouteByID", parameters);

        if (result != null && result.Columns[0].Caption == "Route_Deleted")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to obtain an existing Route.
    /// </summary>
    /// <param name="id">Int ID of the Route to be obtained</param>
    /// <returns>Returns a DataTable with the row of the Route</returns>
    public DataTable getRouteByID(int id)
    {
        ArrayList parameters = new ArrayList();

        parameters.Add(id);

        return this.aR.callProcedure("getRouteByID", parameters);
    }

    /// <summary>
    /// Method which initialize the actual instance of the Route object with all the information from the database
    /// </summary>
    public void getRouteByID()
    {
        ArrayList parameters = new ArrayList();
        DataTable dt;
        parameters.Add(this.idRoute);

        dt = this.aR.callProcedure("getRouteToEdit", parameters);

        if (dt != null)
        {
            object[] customerdata = dt.Rows[0].ItemArray;

            if (customerdata.Length == 1)
            {
                this.idRoute = 0;
            }
            else
            {
                this.idRoute = Convert.ToInt16(customerdata[0].ToString());
                this.Name = customerdata[1].ToString();
            }
        }
        else
        {
            this.idRoute = 0;
        }
    }

    /// <summary>
    /// Method to obtain all the Routes that has been assigned
    /// </summary>
    /// <returns></returns>
    public DataTable getAssignedRoutes()
    {
        return this.aR.callProcedure("getAssignedRoutes");
    }

    /// <summary>
    /// Method to assign a Route to a Seller
    /// </summary>
    /// <param name="idRoute">Int ID of the Route</param>
    /// <param name="idSeller">Int ID of the Seller</param>
    public void setAssignedRoute(int idRoute, int idSeller)
    {
        ArrayList parameters = new ArrayList();

        parameters.Add(idRoute);
        parameters.Add(idSeller);

        this.aR.callProcedure("setAssignedRoute", parameters);
    }
}