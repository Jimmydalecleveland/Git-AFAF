using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

/// <summary>
/// This class deals with the getting and setting of the Errors on this website
/// </summary>
public class ErrorLog
{
    public ErrorLog() { }

    /// <summary>
    /// Gets all Errors in the error database
    /// </summary>
    /// <returns>A dataset with any errors in this function in Tables[0] 
    ///         and the Returned values in Tables[1]</returns>
    /// Alex Marcum - 11/20/2012
    public static DataSet getAllErrors()
    {
        DataSet ds = new DataSet();
        OleDbConnection conn = new OleDbConnection();
        String sqlStatement = "SELECT * FROM ErrorLog";

        ds.Tables.Add("Error");
        ds.Tables[0].Columns.Add("ErrorID", typeof(Int32));
        ds.Tables[0].Columns[0].AutoIncrement = true;
        ds.Tables[0].Columns.Add("Error Message", typeof(String));
        ds.Tables.Add("Results");

        try
        {
            conn.ConnectionString = Connections.connErrorLogStr();
            OleDbCommand cmd = new OleDbCommand(sqlStatement, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds.Tables[1]);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            DataRow errorMessage = ds.Tables[0].NewRow();
            errorMessage["Error Message"] = ex.Message;
            ds.Tables[0].Rows.Add(errorMessage);
        }
        finally
        {
            conn.Close();
        }

        return ds;

    }

    /// <summary>
    /// Logs an exception into the ErrorLog table
    /// </summary>
    /// <param name="ex"></param>
    /// <returns>Returns the ErrorID of the logged exception, 
    ///         if any error occurs a 0 is returned</returns>
    /// Alex Marcum - 11/20/2012
    public static DataSet logError(Exception ex, String additionalInformation = "")
    {
        System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame(1, true);

        //Class Name is the Whole path to the Filename
        string fileName = stackFrame.GetFileName();
        System.IO.FileInfo temp = new System.IO.FileInfo(fileName);
        fileName = temp.Name;
        string functionName = stackFrame.GetMethod().Name.ToString();
        int line = stackFrame.GetFileLineNumber();

        String connStr = Connections.connErrorLogStr();
        OleDbConnection conn = new OleDbConnection(connStr);
        DataSet ds = new DataSet();

        try
        {
            int code = 0; //default error code for now until we establish a list of exceptions and related numbers
            DateTime errorTime = DateTime.Now;  //date time stored in order to be used later in getErrorID method

            String sql = @"INSERT INTO ErrorLog([timeStamp], [fileName], [functionName], [lineNumber],
                        [errorText], [errorCode], [extraData]) 
                        Values(@now, @file, @function, @line, @eText, @eCode, @extraData);";

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("@now", errorTime.ToString());
            cmd.Parameters.AddWithValue("@file", fileName);
            cmd.Parameters.AddWithValue("@function", functionName);
            cmd.Parameters.AddWithValue("@line", line);
            cmd.Parameters.AddWithValue("@eText", ex.Message);
            cmd.Parameters.AddWithValue("@eCode", code);
            cmd.Parameters.AddWithValue("@extraData", additionalInformation);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            ds = getErrorID(errorTime);  //helper method used to get ErrorID based on the timestamp of the error
            return ds;
        }
        catch (Exception /*ex*/)
        {
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    /// <summary>
    /// Helper method to get ErrorID of logged exception based on the timestamp
    /// </summary>
    /// <param name="errorTime"></param>
    /// <returns></returns>
    /// Alex Marcum - 11/20/2012
    private static DataSet getErrorID(DateTime errorTime)
    {
        String connStr = Connections.connErrorLogStr();
        OleDbConnection conn = new OleDbConnection(connStr);
        DataSet ds = new DataSet();

        try
        {
            String sql = "SELECT [ErrorID], [ErrorCode] FROM ErrorLog WHERE [TimeStamp] = @time;";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.Parameters.AddWithValue("@time", errorTime.ToString());

            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(ds, "Error");
            //int errorID = Int32.Parse(ds.Tables["Error"].Rows[0]["ErrorID"].ToString());
            return ds;
        }
        catch (Exception /*ex2*/)
        {
            return ds;
        }
        finally
        {
            conn.Close();
        }
    }
}