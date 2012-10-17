using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// This class deals with the Products of the application, allows you to do the CRUD operations.
/// </summary>
public class Products
{
    private int _idProduct, _idPresentation, _Amount;
    private double _Price;
    private string _Name, _Description;
    private DateTime _FirstDate, _ExpirationDate;
    private ActiveRecord aR = new ActiveRecord();

    /// <summary>
    /// Allows you to set and get the ID of the Product
    /// </summary>
    public int idProduct
    {
        get
        {
            return this._idProduct;
        }
        set
        {
            this._idProduct = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the ID of Presentation of the Product
    /// </summary>
    public int idPresentation
    {
        get
        {
            return this._idPresentation;
        }
        set
        {
            this._idPresentation = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Amount of the Product
    /// </summary>
    public int Amount
    {
        get
        {
            return this._Amount;
        }
        set
        {
            this._Amount = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Price of the Product
    /// </summary>
    public double Price
    {
        get
        {
            return this._Price;
        }
        set
        {
            this._Price = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Name of the Product
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
    /// Allows you to set and get the Description of the Product
    /// </summary>
    public string Description
    {
        get
        {
            return this._Description;
        }
        set
        {
            this._Description = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Generation Date of the Product
    /// </summary>
    public DateTime FirstDate
    {
        get
        {
            return this._FirstDate;
        }
        set
        {
            this._FirstDate = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Expiration Date of the Product
    /// </summary>
    public DateTime ExpirationDate
    {
        get
        {
            return this._ExpirationDate;
        }
        set
        {
            this._ExpirationDate = value;
        }
    }

    /// <summary>
    /// An empty constructor, do nothing
    /// </summary>
	public Products()
	{

	}

    /// <summary>
    /// Constructor to initialize an instance of a Product
    /// </summary>
    /// <param name="idProduct">Int with the ID</param>
    /// <param name="idPresentation">Int with the ID of Presentation</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="Amount">Int with the Amount</param>
    /// <param name="Price">Double with the Price</param>
    /// <param name="Description">String with the Description</param>
    /// <param name="FirstDate">DateTime with the First Date</param>
    /// <param name="ExpirationDate">DateTime with the Expiration Date</param>
    public Products(int idProduct, int idPresentation, string Name, int Amount, double Price, string Description, DateTime FirstDate, DateTime ExpirationDate)
    {
        this.idProduct = idProduct;
        this.idPresentation = idPresentation;
        this.Name = Name;
        this.Amount = Amount;
        this.Price = Price;
        this.Description = Description;
        this.FirstDate = FirstDate;
        this.ExpirationDate = ExpirationDate;
    }

    /// <summary>
    /// Constructor to initialize an instance of a Product
    /// </summary>
    /// <param name="idProduct">Int with the ID</param>
    /// <param name="idPresentation">String with the Presentation</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="Amount">Int with the Amount</param>
    /// <param name="Price">Double with the Price</param>
    /// <param name="Description">String with the Description</param>
    /// <param name="FirstDate">DateTime with the First Date</param>
    /// <param name="ExpirationDate">DateTime with the Expiration Date</param>
    public Products(int idProduct, string Presentation, string Name, int Amount, double Price, string Description, DateTime FirstDate, DateTime ExpirationDate)
    {
        this.idProduct = idProduct;
        switch (Presentation)
        {
            case "80 gr":
                this.idPresentation = 1;
                break;
            case "150 gr":
                this.idPresentation = 2;
                break;
            case "250 gr":
                this.idPresentation = 3;
                break;
            default:
                break;
        }
        this.Name = Name;
        this.Amount = Amount;
        this.Price = Price;
        this.Description = Description;
        this.FirstDate = FirstDate;
        this.ExpirationDate = ExpirationDate;
    }

    /// <summary>
    /// Method to identify the result of the operation that has been done.
    /// </summary>
    /// <param name="dt">DataTable to analize and return the proper code</param>
    /// <returns>Returns the result code. 1 for success, -1 for existing Product, 0 for missing Product</returns>
    public int result(DataTable dt)
    {
        switch (dt.Columns[0].Caption)
        {
            case "idProduct":
                return 1;
            case "Product_Exists":
                return -1;
            case "Product_Not_Exists":
                return 0;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Method to save or update the current instance of a Product
    /// </summary>
    /// <returns>Returns the result code of the operation</returns>
    public int saveProduct()
    {
        ArrayList product = new ArrayList();
        
        DataTable dt = new DataTable();

        if (this.idProduct == 0)
        {
            product.Add(this.idPresentation);
            product.Add(this.Name);
            product.Add(this.Amount);
            product.Add(this.Price);
            product.Add(this.Description);
            product.Add(this.FirstDate.ToString("yyyy:MM:dd hh:mm:ss"));
            product.Add(this.ExpirationDate.ToString("yyyy:MM:dd"));

            dt = this.aR.callProcedure("setProduct", product);

            if (dt != null)
            {
                return this.result(dt);
            }
            else
            {
                return 0;
            }
        }
        else if (this.idProduct > 0)
        {
            product.Add(this.idProduct);
            product.Add(this.idPresentation);
            product.Add(this.Name);
            product.Add(this.Amount);
            product.Add(this.Price);
            product.Add(this.Description);
            product.Add(this.ExpirationDate.ToString("yyyy:MM:dd"));

            dt = this.aR.callProcedure("updateProduct", product);

            if (dt != null)
            {
                return this.result(dt);
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
    /// Method to obtain all the Products of the database.
    /// </summary>
    /// <returns>Returns a DataTable with all the rows of Customers</returns>
    public DataTable getAllProducts()
    {
        return this.aR.callProcedure("getAllProducts");
    }

    /// <summary>
    /// Method to obtain an existing Product.
    /// </summary>
    /// <param name="id">Int ID of the Product to be obtained</param>
    /// <returns>Returns a DataTable with the row of the Product</returns>
    public DataTable getProductByID(int id)
    {
        ArrayList parameters = new ArrayList();

        parameters.Add(id);

        return this.aR.callProcedure("getProductByID", parameters);
    }

    /// <summary>
    /// Method which initialize the actual instance of the Product object with all the information from the database
    /// </summary>
    public void getProductByID()
    {
        ArrayList parameters = new ArrayList();
        DataTable dt;

        parameters.Add(this.idProduct);

        dt = this.aR.callProcedure("getProductToEdit", parameters);

        if (dt != null)
        {
            object[] productdata = dt.Rows[0].ItemArray;

            if (productdata.Length == 1)
            {
                this.idProduct = 0;
            }
            else
            {
                this.idProduct = Convert.ToInt16(productdata[0].ToString());
                this.idPresentation = Convert.ToInt16(productdata[1].ToString());
                this.Name = productdata[2].ToString();
                this.Amount = Convert.ToInt16(productdata[3].ToString());
                this.Price = Convert.ToDouble(productdata[4].ToString());
                this.Description = productdata[5].ToString();
                this.FirstDate = Convert.ToDateTime(productdata[6].ToString());
                this.ExpirationDate = Convert.ToDateTime(productdata[7].ToString());
            }
        }
        else
        {
            this.idProduct = 0;
        }
    }

    /// <summary>
    /// Method to delete an existing Product.
    /// </summary>
    /// <param name="id">Int ID of the Product to be deleted</param>
    /// <returns>Returns true if the operation has been successfull, false in the other way</returns>
    public Boolean deleteProductByID(int id)
    {
        ArrayList parameters = new ArrayList();
        DataTable result;

        parameters.Add(id);
        result = this.aR.callProcedure("deleteProductByID", parameters);

        if (result != null && result.Columns[0].Caption == "Product_Deleted")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to assign Products to a Seller
    /// </summary>
    /// <param name="idSeller">Int ID of the Seller</param>
    /// <param name="idProducts">Array of String with the IDs of the Products to be assigned</param>
    /// <param name="Amounts">Array of String with the Amounts of the Products to be assgined</param>
    public void assignProducts(int idSeller, string[] idProducts, string[] Amounts)
    {
        ArrayList parameters = new ArrayList();
        ArrayList parameter = new ArrayList();

        parameter.Add(idSeller);
        this.aR.callProcedure("resetAssignedProducts", parameter);

        for (int i = 0; i < idProducts.Length; i++)
        {
            if (Convert.ToInt16(Amounts[i]) > 0)
            {
                parameters.Add(idSeller);
                parameters.Add(idProducts[i]);
                parameters.Add(Amounts[i]);
                this.aR.callProcedure("assignProductToSeller", parameters);
                parameters.Clear();
            }            
        }
    }

    /// <summary>
    /// Method to delete all the expired Products of the application according to the current data
    /// </summary>
    /// <returns>Returns an int with the number of Products deleted by expiration</returns>
    public int setExpiredProducts()
    {
        DataTable result = this.aR.callProcedure("setExpiredProducts");

        if (result != null)
        {
            return Convert.ToInt16(result.Rows[0].ItemArray[0].ToString());
        } else
        {
            return 0;
        }
    }

    /// <summary>
    /// Method to obtain all the expired Products
    /// </summary>
    /// <returns>Returns a DataTable with the rows</returns>
    public DataTable getExpiredProducts()
    {
        return aR.callProcedure("getExpiredProducts");
    }

    /// <summary>
    /// Method to obtain all the done Sells
    /// </summary>
    /// <returns>Returns a DataTable with the rows</returns>
    public DataTable getAllSells()
    {
        return this.aR.callProcedure("getSellsToView");
    }

    /// <summary>
    /// Method to delete an specific Sell
    /// </summary>
    /// <param name="id">Int ID of the Sell</param>
    /// <returns>Returns true if the operation has been successfull, false in the other way</returns>
    public Boolean deleteSellByID(int id)
    {
        ArrayList parameter = new ArrayList();
        DataTable result;

        parameter.Add(id);
        result = this.aR.callProcedure("deleteSellByID", parameter);

        if (result != null && result.Columns[0].Caption == "Sell_Deleted")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to obtain a specific Sell
    /// </summary>
    /// <param name="id">Int ID of the Sell</param>
    /// <returns>Returns a DataTable with the row</returns>
    public DataTable getSellByID(int id)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(id);

        return this.aR.callProcedure("getSellByID", parameter);
    }

    /// <summary>
    /// Method to obtain the Products of a specific Sell
    /// </summary>
    /// <param name="id">Int ID of the Sell</param>
    /// <returns>Returns a DataTable with the rows</returns>
    public DataTable getProductsBySell(int id)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(id);

        return this.aR.callProcedure("getProductsSell", parameter);
    }

    /// <summary>
    /// Method to obtain the last Products of the table
    /// </summary>
    /// <returns>Returns a DataTable with the last 5 rows of Products</returns>
    public DataTable getLastProducts()
    {
        return aR.callProcedure("getLastProducts");
    }
}