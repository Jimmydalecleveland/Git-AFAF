using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DinnerRegistration : System.Web.UI.Page
{
    #region pageLoad methods

    /// <summary>
    /// This method runs as the page is loaded. The event information that is displayed is based on the 
    /// query string "eventID".
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Aaron Copeland - 11/19/2012
    /// Tested by Aaron Copeland - 11/28/2012
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request["eventID"] == null)  // Go to events main page
            {
                Response.Redirect("Events.aspx", false);
            }
            else
            {

                String eventID = Request.QueryString["eventID"];
                BusinessTier bt = new BusinessTier();

                // get the details for the Dinner Event
                DataSet ds = bt.getDinnerDetails(eventID);

                // Calculates The Participants
                DataSet ds2 = bt.getMaxDinnerParticipants(eventID);
                DataSet ds3 = bt.getDinnerParticipants(eventID);

                // Prints out any errors
                String errorString = "";

                checkDSErrorTable(ds, errorString);
                checkDSErrorTable(ds2, errorString);
                checkDSErrorTable(ds3, errorString);
                
                lblError.Text = errorString;

               
                // populate the information on the page
                populateDinnerDetails(ds);
                                
                // calculates the number of participants 
                calculateParticipants(ds2, ds3);

                GoogleMap.LoadAddress(ds.Tables[1].Rows[0][15].ToString() + " " + ds.Tables[1].Rows[0][16].ToString() + " " + ds.Tables[1].Rows[0][17].ToString() + " " + ds.Tables[1].Rows[0][18].ToString()); 
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// This method populates the pages fields with the information returned by the DataSet
    /// </summary>
    /// <param name="dsDinnerDetails">The DataSet that contains the dinner's event details</param>
    /// Aaron Copeland - 11/19/2012
    /// Tested by Aaron Copeland - 11/28/2012
    private void populateDinnerDetails(DataSet dsDinnerDetails)
    {
        // the table column numbers have been updated according to the contract 11/28/2012 10:30am --AC

        // Event Title, Date, and Time
        lblEventTitle.Text = dsDinnerDetails.Tables[1].Rows[0][2].ToString();
        recipientImage.Attributes.Add("src", dsDinnerDetails.Tables[1].Rows[0][14].ToString());
        lblEventDate.Text = dsDinnerDetails.Tables[1].Rows[0][4].ToString() + " " + dsDinnerDetails.Tables[1].Rows[0][3].ToString();
        lblRegistrationClosingDate.Text = "Online registration closes " + dsDinnerDetails.Tables[1].Rows[0][15].ToString();

        // Event Description, Details, and Ticket Price
        lblEventDescription.Text = dsDinnerDetails.Tables[1].Rows[0][9].ToString();
        lblEventDetails.Text = dsDinnerDetails.Tables[1].Rows[0][10].ToString();
        lblAdultPrice.Text = dsDinnerDetails.Tables[1].Rows[0][13].ToString();
        lblChildPrice.Text = dsDinnerDetails.Tables[1].Rows[0][12].ToString();

        // Event Location                
        lblEventLocation.Text = dsDinnerDetails.Tables[1].Rows[0][5].ToString();

        // Event Contact Rep
        lblRepNameValue.Text = dsDinnerDetails.Tables[1].Rows[0][6].ToString();
        lblRepEmailValue.Text = dsDinnerDetails.Tables[1].Rows[0][7].ToString();
        lblRepPhoneValue.Text = dsDinnerDetails.Tables[1].Rows[0][8].ToString();
    }

    /// <summary>
    /// This method gets the number of max participants and calculates the number of available spots.
    /// </summary>
    /// <param name="dsMaxPart">The DataSet that contains the maximum number of participants for the event</param>
    /// <param name="dsDinnerPart">The DataSet that contains the number of registered people and the number in their party.</param>
    /// Aaron Copeland - 11/19/2012
    /// Tested by Aaron Copeland - 11/28/2012
    private void calculateParticipants(DataSet dsMaxPart, DataSet dsDinnerPart)
    {
        int maxParticipants = Convert.ToInt32(dsMaxPart.Tables[1].Rows[0][0]);
        int numParticipants = 0;
        for (int i = 0; i < dsDinnerPart.Tables[1].Rows.Count; i++)
        {
            // adds the number in each party together to get the total number of guests
            numParticipants = numParticipants + Convert.ToInt32(dsDinnerPart.Tables[1].Rows[i][5].ToString());
        }

        int spotsLeft = maxParticipants - numParticipants;
        lblMaxParticipants.Text = "Participant Limit: " + maxParticipants;
        lblSpotsLeft.Text = "Spots Left: " + spotsLeft;
    }

    /// <summary>
    /// This method checks the returning DataSet's error table and saves it to the error string. 
    /// </summary>
    /// <param name="ds">The DataSet to be checked</param>
    /// <param name="errorString">The errorString that the errors will be appended to</param>
    /// Aaron Copeland - 11/19/2012
    /// Tested by Aaron Copeland - 11/28/2012
    private void checkDSErrorTable(DataSet ds, string errorString)
    {
        // check if there are any errors trying to retrieve the information
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (errorString.Equals(""))
                errorString = "Please fix the following items:</br>";

            //  Iterate through errors
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                errorString += ds.Tables[0].Rows[i][1] + "<br/>";            
        }
    }

    #endregion

    #region btnCheckout method

    /// <summary>
    /// This method validates the dinner registration information entered by the user.
    /// If problems exist the user is prompted to fix them.
    /// If the information does not produce an error the information is saved as a session variable
    /// and user is directed to the checkout page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Aaron Copeland - 11/19/2012
    /// Tested by Aaron Copeland - 11/28/2012
    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        // verifyDinnerRegistration
        BusinessTier bt = new BusinessTier();
        Dictionary<String, String> dinnerRegInfo = new Dictionary<string, string>();
        
        try
        {
            // variables for the party size calculation
            int numAdults;
            int numChildren;
            int numParty;
            
            // attempt to parse the number of adults and number of children
            Int32.TryParse(txtNumAdults.Text, out numAdults);
            Int32.TryParse(txtNumChildren.Text, out numChildren);

            // party size was properly calculated above
            numParty = numAdults + numChildren;
            
            // checks if the party size is zero or if the number of adults or children is a negative number
            if (numAdults <= 0 || numAdults < 0 || numChildren < 0)
            {
                lblError.Text = "Please enter a valid number for adults and children";
                PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
            }
            else
            {
                // creates a dictionary with the registrants information as specified by the contract 11/28/2012 10:30am --AC
                dinnerRegInfo.Add("firstName", txtFirstName.Text);
                dinnerRegInfo.Add("lastName", txtLastName.Text);
                dinnerRegInfo.Add("suffix", txtSuffix.Text);
                dinnerRegInfo.Add("email", txtEmail.Text);
                dinnerRegInfo.Add("emailConfirm", txtConfirmEmail.Text);
                dinnerRegInfo.Add("phone", txtAreaCode.Text + txtPrefix.Text + txtLineNumber.Text);
                dinnerRegInfo.Add("numInParty", numParty.ToString());
                dinnerRegInfo.Add("numChildren", numChildren.ToString());
                dinnerRegInfo.Add("numAdults", numAdults.ToString());
                dinnerRegInfo.Add("transactionID", Guid.NewGuid().ToString());
                dinnerRegInfo.Add("cost", ((numChildren * Convert.ToDouble(lblChildPrice.Text))
                    + (numAdults * Convert.ToDouble(lblAdultPrice.Text))).ToString());
                Session["email"] = txtEmail.Text;
                // sends the dictionary to the business tier to check the fields
                DataSet ds = bt.validateDinnerReg(dinnerRegInfo);

                // gets any errors returned
                if (ds.Tables[0].Rows.Count > 0)
                {
                    String errorString = "Please fix the following items:</br>";

                    //  Iterate through errors
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        errorString += ds.Tables[0].Rows[i][1] + "<br/>";

                    lblError.Text = errorString;
                    PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);

                }
                else
                {
                    // no errors were returned
                    Session["regInfo"] = dinnerRegInfo;     // dictionary used as a session variable 
                    Session["eventID"] = Request["eventID"].ToString();
                    Response.Redirect("Checkout.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    #endregion
}