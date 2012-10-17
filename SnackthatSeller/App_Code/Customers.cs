using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// This class deals with the Customers of the application, allows you to do the CRUD operations.
/// </summary>
public class Customers
{
    private int _idCustomer;
    private string _Name, _LastName, _Email, _RFC;
    private ArrayList _Addresses;
    private ActiveRecord aR = new ActiveRecord();

    /// <summary>
    /// Allows you to set and get the ID of the Customer
    /// </summary>
    public int idCustomer
    {
        get
        {
            return this._idCustomer;
        }
        set
        {
            this._idCustomer = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Name of the Customer
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
    /// Allows you to set and get the LastName of the Customer
    /// </summary>
    public string LastName
    {
        get
        {
            return this._LastName;
        }
        set
        {
            this._LastName = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Email of the Customer
    /// </summary>
    public string Email
    {
        get
        {
            return this._Email;
        }
        set
        {
            this._Email = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the RFC of the Customer
    /// </summary>
    public string RFC
    {
        get
        {
            return this._RFC;
        }
        set
        {
            this._RFC = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Addresses of the Customer
    /// </summary>
    public ArrayList Addresses
    {
        get
        { 
            return this._Addresses; 
        }
        set 
        {
            this._Addresses = value;
        }
    }

    /// <summary>
    /// An empty constructor, do nothing.
    /// </summary>
    public Customers()
	{

	}

    /// <summary>
    /// Constructor to initialize an instance of a Customer
    /// </summary>
    /// <param name="idCustomer">Int with the ID</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="LastName">String with the Lastname</param>
    /// <param name="Email">String with the Email</param>
    /// <param name="RFC">String with the RFC</param>
    /// <param name="Addresses">ArrayList with the Address</param>
    public Customers(int idCustomer, string Name, string LastName, string Email, string RFC, ArrayList Addresses)
    {
        this.idCustomer = idCustomer;
        this.Name = Name;
        this.LastName = LastName;
        this.Email = Email;
        this.RFC = RFC;
        this.Addresses = Addresses;
    }

    /// <summary>
    /// Constructor to initialize an instance of a Customer
    /// </summary>
    /// <param name="idCustomer">Int the ID</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="LastName">String with the Lastname</param>
    /// <param name="Email">String with the Email</param>
    /// <param name="RFC">String with the RFC</param>
    public Customers(int idCustomer, string Name, string LastName, string Email, string RFC)
    {
        this.idCustomer = idCustomer;
        this.Name = Name;
        this.LastName = LastName;
        this.Email = Email;
        this.RFC = RFC;
    }

    /// <summary>
    /// Method to identify the result of the operation that has been done.
    /// </summary>
    /// <param name="dt">DataTable to analize and return the proper code</param>
    /// <returns>Returns the result code. 1 for success, -1 for an Email warning, -2 for an RFC warning, -3 for existing Customer, 0 for missing Customer</returns>
    public int result(DataTable dt)
    {
        switch (dt.Columns[0].Caption)
        {
            case "idCustomer":
                return 1;
            case "Email_Exists":
                return -1;
            case "RFC_Exists":
                return -2;
            case "Customer_Exists":
                return -3;
            case "Customer_Not_Exists":
                return 0;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Method to save or update the current instance of a Customer
    /// </summary>
    /// <returns>Returns the result code of the operation</returns>
    public int saveCustomer()
    {
        ArrayList customer = new ArrayList();
        DataTable dt = new DataTable();

        if (this.idCustomer == 0)
        {
            customer.Add(this.Name);
            customer.Add(this.LastName);
            customer.Add(this.Email);
            customer.Add(this.RFC);

            dt = this.aR.callProcedure("setCustomer", customer);
            
            if (dt != null)
            {
                int result = this.result(dt);

                if (result == 1)
                {
                    if (this.Addresses.Count > 0)
                    {
                        ArrayList addresses = new ArrayList();
                        string[][] data = (string[][])this.Addresses.ToArray(typeof(string[]));

                        for (int i = 0; i < this.Addresses.Count; i++)
                        {
                            addresses.Add(dt.Rows[0].ItemArray[0].ToString());
                            addresses.Add(data[i][0]);
                            addresses.Add(data[i][1]);
                            this.aR.callProcedure("setAddressesCustomer", addresses);
                            addresses.Clear();
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
        else if (this.idCustomer > 0)
        {
            customer.Add(this.idCustomer);
            customer.Add(this.Name);
            customer.Add(this.LastName);
            customer.Add(this.Email);
            customer.Add(this.RFC);

            dt = this.aR.callProcedure("updateCustomer", customer);

            if (dt != null)
            {
                int result = this.result(dt);

                if (result == 1)
                {
                    if (this.Addresses.Count > 0)
                    {
                        ArrayList addresses = new ArrayList();
                        string[][] data = (string[][])this.Addresses.ToArray(typeof(string[]));

                        for (int i = 0; i < this.Addresses.Count; i++)
                        {
                            addresses.Add(dt.Rows[0].ItemArray[0].ToString());
                            addresses.Add(data[i][0]);
                            addresses.Add(data[i][1]);
                            this.aR.callProcedure("setAddressesCustomer", addresses);
                            addresses.Clear();
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
    /// Method to obtain all the Customers of the database.
    /// </summary>
    /// <param name="i">If i is 0 then you get all the customers and their data. Else If i is 1 then you get all the customers and their data and more information</param>
    /// <returns>Returns a DataTable with all the rows of Customers</returns>
    public DataTable getAllCustomers(int i)
    {
        ArrayList parametro = new ArrayList();
        parametro.Add(i);
        return this.aR.callProcedure("getAllCustomers", parametro);
    }

    /// <summary>
    /// Method to delete an existing Customer.
    /// </summary>
    /// <param name="id">Int ID of the Customer to be deleted</param>
    /// <returns>Returns true if the operation has been successfull, false in the other way</returns>
    public Boolean deleteCustomerByID(int id)
    {
        ArrayList parameter = new ArrayList();
        DataTable result;

        parameter.Add(id);
        result = this.aR.callProcedure("deleteCustomerByID", parameter);

        if (result != null && result.Columns[0].Caption == "Customer_Deleted")
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    /// <summary>
    /// Method to obtain an existing Customer.
    /// </summary>
    /// <param name="id">Int ID of the Customer to be obtained</param>
    /// <returns>Returns a DataTable with the row of the Customer</returns>
    public DataTable getCustomerByID(int id)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(id);

        return this.aR.callProcedure("getCustomerByID", parameter);
    }

    /// <summary>
    /// Method which initialize the actual instance of the Customer object with all the information from the database
    /// </summary>
    public void getCustomerByID()
    {
        ArrayList parameter = new ArrayList();
        DataTable dt;
        parameter.Add(this.idCustomer);

        dt = this.aR.callProcedure("getCustomerToEdit", parameter);

        if (dt != null)
        {
            object[] customerdata = dt.Rows[0].ItemArray;

            if (customerdata.Length == 1)
            {
                this.idCustomer = 0;
            }
            else
            {
                this.idCustomer = Convert.ToInt16(customerdata[0].ToString());
                this.Name = customerdata[1].ToString();
                this.LastName = customerdata[2].ToString();
                this.Email = customerdata[3].ToString();
                this.RFC = customerdata[4].ToString();
            } 
        }  
        else 
        {
            this.idCustomer = 0;
        }
    }

    /// <summary>
    /// Method to obtain all the Customers and its Addresses to save a Route
    /// </summary>
    /// <returns>Returns a DataTable with the rows.</returns>
    public DataTable getCustomersAndAddressesToSave()
    {
        return this.aR.callProcedure("getCustomersAndAddressesToSave");
    }

    /// <summary>
    /// Method to obtain all the Customers and its Addresses to edit a specific Route
    /// </summary>
    /// <param name="idRoute">Int ID of the Route to edit</param>
    /// <returns>Returns a DataTable with the rows.</returns>
    public DataTable getCustomersAndAddressesToEdit(int idRoute)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(idRoute);

        return this.aR.callProcedure("getCustomersAndAddressesToEdit", parameter);
    }

    /// <summary>
    /// Method to obtain all the Customers and its Addresses from a specific Route
    /// </summary>
    /// <param name="idRoute">Int ID of the Route</param>
    /// <returns>Returns a DataTable with the rows.</returns>
    public DataTable getCustomersAndAddressesByRoute(int idRoute)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(idRoute);

        return this.aR.callProcedure("getCustomersAndAddressesByRoute", parameter);
    }

    /// <summary>
    /// Method to obtain all the Addresses from a specific Customer
    /// </summary>
    /// <param name="idCustomer">Int ID of the Customer</param>
    /// <returns>Returns a DataTable with the rows.</returns>
    public DataTable getCustomerAddresses(int idCustomer)
    {
        ArrayList parameter = new ArrayList();
        
        parameter.Add(idCustomer);
        
        return this.aR.callProcedure("getCustomerAddresses", parameter);
    }

    /// <summary>
    /// Method to obtain the last Customers of the table
    /// </summary>
    /// <returns>A DataTable with 5 rows of the last Customers</returns>
    public DataTable getLastCustomers()
    {
        return this.aR.callProcedure("getLastCustomers");
    }
}
