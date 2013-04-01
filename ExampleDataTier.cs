using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ExampleDataTier
/// </summary>
public class ExampleDataTier
{
	public ExampleDataTier()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    ///Example DataTier Method
    /// <summary>
    /// This is the summary of what this method does.
    /// </summary>
    /// <param name="eventID">This is a description of this parameter: what it contains 
    /// and what it is used for
    /// </param>
    /// <returns>This is a description of what this method returns</returns>
    /// Author’s Name - Date
    public DataSet getDinnerDetails(String event1)
    {
        //DataSet declaration here (NO Assignment statements here)
        DataSet ds = null;
        //Other 
        try
        {
            //Assign values to member variables, add Error Table to the DataSet
            ds = new DataSet();
            //...
            //other code here
        }
        catch (Exception ex)
        {
            //log the error to the ErrorLog database
            ErrorLog.logError(ex);
            //Add an entry in the error table of the DataSet
        }
        //return the DataSet here
        return ds;
    }
	
	public void JordanOndrusek()
	{
		// Edited by Jason Vance
		return;
	}

    public void jasonVance()
    {
		// Edited by Jordan Ondrusek
        String jsonVance = "Jason Vance";//Edited by Olivia Johnson
		//MyComment
    }

    public void OliviaJonshon()//SVN Test
    {
    }
	public void aaronCopeland()
	{
		// Here is some nifty code added by Michael Larsen
	}

	public void michaelLarsen()
	{
		//code
	}

    public void jimmyCleveland(string test)
    public void jimmyCleveland(string fluff)
    {
        String = "moo";
    }
}