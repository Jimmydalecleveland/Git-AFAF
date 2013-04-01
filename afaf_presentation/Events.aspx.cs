using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Events : System.Web.UI.Page
{
    /// <summary>
    /// Grabs all the events from the database and loads them onto the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Andrew Heim - 11/26/2012
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BusinessTier bt = new BusinessTier();
            DataSet data = bt.getAllEvents();

            foreach (DataRow row in data.Tables[1].Rows)
            {
                if (row[11].ToString() == "1")
                {
                    Controls_EventHolder eventHolder = (Controls_EventHolder)Page.LoadControl("~/Controls/EventHolder.ascx"); ;
                    eventHolder.LoadEvent(row);
                    events.Controls.Add(eventHolder);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }


}