using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This class deals with the Security and Integrity of the data of the application and its store.
/// </summary>
public class Security
{
	public Security()
	{
	}

    /// <summary>
    /// Method that encrypt a String, used for encrypting passwords with the SHA1 hash algorithm
    /// </summary>
    /// <param name="str">Returns the encrypted String</param>
    /// <returns></returns>
    public static string encrypt(string str)
    {
        SHA1 sha1 = SHA1Managed.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        StringBuilder sb = new StringBuilder();
        
        stream = sha1.ComputeHash(encoding.GetBytes(str));
        for (int i = 0; i < stream.Length; i++)
        {
            sb.AppendFormat("{0:x2}", stream[i]);
        }
        
        return sb.ToString();
    }

    /// <summary>
    /// Method that clean the special SQL caracters to avoid a SQL Injection Attack.
    /// </summary>
    /// <param name="str">Returns the cleaned string</param>
    /// <returns></returns>
    public static string cleanSQL(string str)
    {
        str = str.Replace("'", "");
        str = str.Replace(";", "");
        str = str.Replace("-", "_");
        str = str.Trim();

        return str;
    }

    /// <summary>
    /// Method to validate if the passed String is a valid Password
    /// </summary>
    /// <param name="str">Returns true if the String is a valid Password, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isPassword(string str)
    {
        String expression = "(?=^.{6,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[a-z]).*$";

        if (Regex.IsMatch(str, expression))
        {
            if (Regex.Replace(str, expression, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to validate if the passed String is an SQL Injection
    /// </summary>
    /// <param name="str">Returns true if the String is an Injection, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isInjection(string str)
    {
        String expression = "(script)|(&lt;)|(&gt;)|(%3c)|(%3e)|(SELECT) |(UPDATE) |(INSERT) |(DELETE)|(GRANT) |(REVOKE)|(UNION)|(&amp;lt;)|(&amp;gt;)";
        if (Regex.IsMatch(str, expression))
        {
            if (Regex.Replace(str, expression, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to validate if the passed String is a  valid RFC
    /// </summary>
    /// <param name="str">Returns true if the String is a valid RFC, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isRFC(string str)
    {
        String expression = "^[a-zA-Z]{3,4}(\\d{6})((\\D|\\d){3})?$";

        if (Regex.IsMatch(str, expression))
        {
            if (Regex.Replace(str, expression, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to validate if the passed String is a valid Phone
    /// </summary>
    /// <param name="str">Returns true if the String is a valid Phone, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isPhone(string str)
    {
        String expression = "^\\+?\\d{1,3}?[- .]?\\(?(?:\\d{2,3})\\)?[- .]?\\d\\d\\d[- .]?\\d\\d\\d\\d$";

        if (Regex.IsMatch(str, expression))
        {
            if (Regex.Replace(str, expression, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to validate if the passed String is a valid Email
    /// </summary>
    /// <param name="str">Returns true if the String is a valid Email, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isEmail(string str)
    {
        String expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(str, expression))
        {
            if (Regex.Replace(str, expression, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to validate if the String is an empty string, to prevent null cells in some cases.
    /// </summary>
    /// <param name="str">Returns true if the String is empty, otherwise returns false</param>
    /// <returns></returns>
    public static Boolean isEmpty(string str)
    {
        if (str == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}