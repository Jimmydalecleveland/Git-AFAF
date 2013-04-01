using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Controls_EventHolder : System.Web.UI.UserControl
{
    /// <summary>
    /// Specifies a Golf Event
    /// </summary>
    /// Andrew Heim - 11/26/2012
    const string EVENTGOLF = "Golf";

    /// <summary>
    /// Specifies a Donation Event
    /// </summary>
    /// Andrew Heim - 11/26/2012
    const string EVENTDONATION = "Donation";

    /// <summary>
    /// Specifies a Dinner Event
    /// </summary>
    /// Andrew Heim - 11/26/2012
    const string EVENTDINNER = "Dinner";

    /// <summary>
    /// Specifies a Run Event
    /// </summary>
    /// Andrew Heim - 11/26/2012
    const string EVENTRUN = "Run";

    /// <summary>
    /// Loads an event's details into the control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Andrew Heim - 11/26/2012
    public void LoadEvent(DataRow data)
    {
        try
        {
            string eventID = data[0].ToString();

            hdnEventId.Value = eventID;
            hdnEventType.Value = getEventType(eventID);
            lblEventTitle.Text = data[2].ToString();
            lblEventTime.Text = data[4].ToString() + " " + data[3].ToString();
            lblEventDetails.Text = data[10].ToString();
            imgRecipientPhoto.ImageUrl = data[12].ToString();      
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    private string getEventType(string eventID)
    {
        try
        {
            BusinessTier bt = new BusinessTier();
            DataSet ds = bt.getEventType(eventID);
            string eventType = ds.Tables[1].Rows[0][0].ToString();
            return eventType;
         }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
            return "";
        }
    }

    /// <summary>
    /// Determines which event type it is and directs them to the proper page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Andrew Heim - 11/26/2012
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            string eventID = hdnEventId.Value;
            string eventType = hdnEventType.Value;
            string page = "";

            if (eventType == EVENTDINNER)
                page = "DinnerRegistration.aspx";
            else if(eventType == EVENTDONATION)
                page = "DonationRegistration.aspx";
            else if (eventType == EVENTGOLF)
                page = "GolfRegistration.aspx";
            else if (eventType == EVENTRUN)
                page = "RunRegistration.aspx";

            if (!string.IsNullOrEmpty(page))
            {
                page += "?eventID=" + eventID;
                Response.Redirect(page, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }
}