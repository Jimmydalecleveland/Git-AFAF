using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Provides the connection Strings for all databases that are used
/// </summary>
public class Connections
{
	public Connections() { }

    /// <summary>
    /// This is the Main database to store all activity on this site
    /// </summary>
    /// <returns>The Connection String to connect to the Main Database</returns>
    public static String connStr()
    {
        String connstr = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=|DataDirectory|\AFAF.accdb";  //Update when database is built
        return connstr;
    }

    /// <summary>
    /// This is used to log any errors throughout the project
    /// </summary>
    /// <returns>The Connection string to connect to the Error Log Database</returns>
    public static String connErrorLogStr()
    {
        String connstr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|\errorlog.mdb"; // Stored at App_Data\errorlog.mdb
        return connstr;
    }
}