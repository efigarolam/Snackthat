using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de Users
/// </summary>
public class Users
{
    private int _idUser, _idPrivilege;
    private string _Name, _LastName, _Username, _Pwd, _Email, _Phone, _Address, _RFC;
    private ActiveRecord aR = new ActiveRecord();

    /// <summary>
    /// Allows you to set and get the ID of the User
    /// </summary>
    public int idUser
    {
        get
        {
            return this._idUser;
        }
        set
        {
            this._idUser = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the ID of the Privilege the User
    /// </summary>
    public int idPrivilege
    {
        get
        {
            return this._idPrivilege;
        }
        set
        {
            this._idPrivilege = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Name of the User
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
    /// Allows you to set and get the LastName of the User
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
    /// Allows you to set and get the Username of the User
    /// </summary>
    public string Username
    {
        get
        {
            return this._Username;
        }
        set
        {
            this._Username = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Password of the User
    /// </summary>
    public string Password
    {
        get
        {
            return this._Pwd;
        }
        set
        {
            this._Pwd = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Email of the User
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
    /// Allows you to set and get the Phone of the User
    /// </summary>
    public string Phone
    {
        get
        {
            return this._Phone;
        }
        set
        {
            this._Phone = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the Address of the User
    /// </summary>
    public string Address
    {
        get
        {
            return this._Address;
        }
        set
        {
            this._Address = value;
        }
    }

    /// <summary>
    /// Allows you to set and get the RFC of the User
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
    /// An empty Constructor, do nothing.
    /// </summary>
	public Users()
	{

	}

    /// <summary>
    /// Constructor to initialize an instance of a User
    /// </summary>
    /// <param name="idUser">Int ID with the User</param>
    /// <param name="idPrivilege">Int ID with the User Privilege</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="LastName">String with the LastName</param>
    /// <param name="Username">String with the Username</param>
    /// <param name="Password">String with the Password</param>
    /// <param name="Email">String with the Email</param>
    /// <param name="Phone">String with the Phone</param>
    /// <param name="Address">String with the Address</param>
    /// <param name="RFC">String with the RFC</param>
    public Users(int idUser, int idPrivilege, string Name, string LastName, string Username, string Password, string Email, string Phone, string Address, string RFC)
    {
        this.idUser = idUser;
        this.idPrivilege = idPrivilege;
        this.Name = Name;
        this.LastName = LastName;
        this.Username = Username;
        this.Password = Password;
        this.Email = Email;
        this.Phone = Phone;
        this.Address = Address;
        this.RFC = RFC;
    }

    /// <summary>
    /// Constructor to initialize an instance of a User
    /// </summary>
    /// <param name="idUser">Int with the ID of the User</param>
    /// <param name="Privilege">String with the Privilege</param>
    /// <param name="Name">String with the Name</param>
    /// <param name="LastName">String with the LastName</param>
    /// <param name="Username">String with the Username</param>
    /// <param name="Password">String with the Password</param>
    /// <param name="Email">String with the Email</param>
    /// <param name="Phone">String with the Phone</param>
    /// <param name="Address">String with the Address</param>
    /// <param name="RFC">String with the RFC</param>
    public Users(int idUser, string Privilege, string Name, string LastName, string Username, string Password, string Email, string Phone, string Address, string RFC)
    {
        this.idUser = idUser;
        switch (Privilege)
        {
            case "Gerente":
                this.idPrivilege = 1;
                break;
            case "Jefe":
                this.idPrivilege = 2;
                break;
            case "Inventario":
                this.idPrivilege = 3;
                break;
            case "Vendedor":
                this.idPrivilege = 4;
                break;
            default:
                break;
        }
        this.Name = Name;
        this.LastName = LastName;
        this.Username = Username;
        this.Password = Password;
        this.Email = Email;
        this.Phone = Phone;
        this.Address = Address;
        this.RFC = RFC;
    }

    /// <summary>
    /// Method to iniatilize the properties of the current User Objetc with the information from the database
    /// </summary>
    /// <param name="username">String Username of the User to find the information</param>
    public void getInstanceOf(string username)
    {
        ArrayList parameter = new ArrayList();
        parameter.Add(username);

        DataTable userinfo = aR.callProcedure("getUserByUsername", parameter);

        if (userinfo != null && userinfo.Rows.Count == 1)
        {
            this.idUser = Convert.ToInt16(userinfo.Rows[0].ItemArray[0].ToString());
            this.idPrivilege = Convert.ToInt16(userinfo.Rows[0].ItemArray[1].ToString());
            this.Name = userinfo.Rows[0].ItemArray[2].ToString();
            this.LastName = userinfo.Rows[0].ItemArray[3].ToString();
            this.Username = userinfo.Rows[0].ItemArray[4].ToString();
            this.Email = userinfo.Rows[0].ItemArray[6].ToString();
            this.Phone = userinfo.Rows[0].ItemArray[7].ToString();
            this.Address = userinfo.Rows[0].ItemArray[8].ToString();
            this.RFC = userinfo.Rows[0].ItemArray[9].ToString();
        }
    }

    /// <summary>
    /// Method to identify the result of the operation that has been done.
    /// </summary>
    /// <param name="dt">DataTable to analize and return the proper code</param>
    /// <returns>Returns the result code. 1 for success, -1 for an Email warning, -2 for an RFC warning, -3 for existing User, 0 for missing User</returns>
    public int result(DataTable dt)
    {
        switch (dt.Columns[0].Caption)
        {
            case "idUser":
                return 1;
            case "Email_Exists":
                return -1;
            case "RFC_Exists":
                return -2;
            case "User_Exists":
                return -3;
            case "User_Not_Exists":
                return 0;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Method to save or update the current instance of a User
    /// </summary>
    /// <returns>Returns the result code of the operation</returns>
    public int saveUser()
    {
        ArrayList user = new ArrayList();
        DataTable dt = new DataTable();

        if (this.idUser == 0)
        {
            user.Add(this.idPrivilege);
            user.Add(this.Name);
            user.Add(this.LastName);
            user.Add(this.Username);
            user.Add(this.Password);
            user.Add(this.Email);
            user.Add(this.Phone);
            user.Add(this.Address);
            user.Add(this.RFC);

            dt = this.aR.callProcedure("setUser", user);

            if (dt != null)
            {
                return this.result(dt);
            } else {
                return 0;
            }
        }
        else if (this.idUser > 0)
        {
            user.Add(this.idUser);
            user.Add(this.idPrivilege);
            user.Add(this.Name);
            user.Add(this.LastName);
            user.Add(this.Username);
            user.Add(this.Password);
            user.Add(this.Email);
            user.Add(this.Phone);
            user.Add(this.Address);
            user.Add(this.RFC);

            dt = this.aR.callProcedure("updateUser", user);

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
    /// Method to obtain all the Users of the database.
    /// </summary>
    /// <returns>Returns a DataTable with all the rows of Users</returns>
    public DataTable getAllUsers()
    {
        return this.aR.callProcedure("getAllUsers");
    }

    /// <summary>
    /// Method to obtain an existing User.
    /// </summary>
    /// <param name="id">Int ID of the Users to be obtained</param>
    /// <returns>Returns a DataTable with the row of the User</returns>
    public DataTable getUserByID(int id)
    {
        ArrayList parameters = new ArrayList();

        parameters.Add(id);

        return this.aR.callProcedure("getUserByID", parameters);
    }

    /// <summary>
    /// Method which initialize the actual instance of the User object with all the information from the database
    /// </summary>
    public void getUserByID()
    {
        ArrayList parameters = new ArrayList();
        DataTable dt;
        parameters.Add(this.idUser);

        dt = this.aR.callProcedure("getUserToEdit", parameters);

        if (dt != null)
        {
            object[] userdata = dt.Rows[0].ItemArray;

            if (userdata.Length == 1)
            {
                this.idUser = 0;
            }
            else
            {
                this.idUser = Convert.ToInt16(userdata[0].ToString());
                this.idPrivilege = Convert.ToInt16(userdata[1].ToString());
                this.Name = userdata[2].ToString();
                this.LastName = userdata[3].ToString();
                this.Username = userdata[4].ToString();
                this.Password = userdata[5].ToString();
                this.Email = userdata[6].ToString();
                this.Phone = userdata[7].ToString();
                this.Address = userdata[8].ToString();
                this.RFC = userdata[9].ToString();
            }
        }
        else
        {
            this.idUser = 0;
        }
    }

    /// <summary>
    /// Method to delete an existing User.
    /// </summary>
    /// <param name="id">Int ID of the User to be deleted</param>
    /// <returns>Returns true if the operation has been successfull, false in the other way</returns>
    public Boolean deleteUserByID(int id)
    {
        ArrayList parameters = new ArrayList();
        DataTable result;

        parameters.Add(id);
        result = this.aR.callProcedure("deleteUserByID", parameters);

        if (result != null && result.Columns[0].Caption == "User_Deleted")
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    /// <summary>
    /// Method to obtain all the Users of the database with the Privilege of a Seller.
    /// </summary>
    /// <returns>Returns a DataTable with all the rows of Users</returns>
    public DataTable getAllSellers()
    {
        return this.aR.callProcedure("getAllSellers");
    }

    /// <summary>
    /// Method to obtain the ID of a user
    /// </summary>
    /// <param name="username">Is the username of a user</param>
    /// <returns>The ID of the username</returns>
    public int getIDByUsername(string username)
    {
        ArrayList parameter = new ArrayList();

        parameter.Add(username);

        return Convert.ToInt16(this.aR.callProcedure("getIDByUsername", parameter).Rows[0].ItemArray[0]);
    }

    /// <summary>
    /// Method to verify if the User exists in the database
    /// </summary>
    /// <returns>Returns true if the Username and the Passwords corresponds with a row on the table, othewise returns false</returns>
    public int login()
    {
        ArrayList parameters = new ArrayList();
        DataTable result;

        parameters.Add(this.Username);
        parameters.Add(this.Password);

        result = aR.callProcedure("login", parameters);


        if (result != null && result.Rows.Count == 1)
        {
            return Convert.ToInt16(result.Rows[0].ItemArray[0].ToString());
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Method to obtain the privilege of the current instance of the User object
    /// </summary>
    /// <returns>Returns a String with the privilege</returns>
    public String getPrivilegeByUser()
    {
        ArrayList parameter = new ArrayList();
        DataTable result;

        parameter.Add(this.Username);

        result = aR.callProcedure("getPrivilegeByUser", parameter);

        if (result != null && result.Rows.Count == 1)
        {
            return result.Rows[0].ItemArray[0].ToString();
        }
        else
        {
            return "";
        }
    }
}