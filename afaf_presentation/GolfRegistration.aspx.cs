using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _GolfRegistration : System.Web.UI.Page
{

    #region pageLoad methods

    /// <summary>
    /// This method executes on load and populates the Golf Details (using helper methods) and checks for errors
    /// 11/18/12 Michael Larsen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>  
    /// Michael Larsen - 11/18/12
    /// Tested by Michael Larsen 11/27/12

    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {             
            if (Request["eventID"] == null)
            {
                Response.Redirect("Events.aspx",false);
            }
            else
            {
                String eventID = Request["eventID"].ToString();
                BusinessTier bt = new BusinessTier();
                DataSet ds = bt.getGolfDetails(eventID);
                DataSet ds2 = bt.getMaxGolfParticipants(eventID);
                DataSet ds3 = bt.getGolfParticipants(eventID);

                int numErrors = ds.Tables[0].Rows.Count + ds2.Tables[0].Rows.Count + ds3.Tables[0].Rows.Count;

                if (numErrors > 0)
                {
                    Response.Redirect("Oops.aspx",false);
                }
                else
                {
                    populateGolfDetails(ds);
                    populateParticipantInfo(ds2, ds3);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx",false);
        }
    }


    /// <summary>
    /// This method calculates and displays the spots Available
    /// </summary>
    /// <param name="dsMaxParticipants"></param>
    /// <param name="dsGolfParticipants"></param>
    /// Michael Larsen - 11/22/12
    /// Tested by Michael Larsen 11/27/12

    private void populateParticipantInfo(DataSet dsMaxParticipants, DataSet dsGolfParticipants)
    {
        int maxParticipants = Convert.ToInt32(dsMaxParticipants.Tables[1].Rows[0][0]);
        int numParticipants = 0;

        //  Iterate through list of parties and add up the numbers in the 'numInParty' to get total # of participants
        for (int i = 0; i < dsGolfParticipants.Tables[1].Rows.Count; i++)
            numParticipants += Convert.ToInt32(dsGolfParticipants.Tables[1].Rows[i][5]);

        int spotsLeft = maxParticipants - numParticipants;
        lblMaxParticipants.Text = "Participant Limit: " + maxParticipants;
        lblSpotsLeft.Text = "Spots Left: " + spotsLeft;
    }


    /// <summary>
    /// This method populates the Golf Details (minus the participants list)
    /// </summary>
    /// <param name="ds"></param>
    /// Michael Larsen - 11/22/12
    /// Tested by Michael Larsen 11/27/12

    private void populateGolfDetails(DataSet ds)
    {
        //  admissionPrice is initialized as a session variable since it will be accessed again in the checkout methods
        //  and we wont' have to call the business method again to grab it.
        Session["admissionPrice"] = ds.Tables[1].Rows[0][12].ToString();

        lblEventTime.Text = ds.Tables[1].Rows[0][4].ToString() + " " + ds.Tables[1].Rows[0][3].ToString();
        lblEventTitle.Text = ds.Tables[1].Rows[0][2].ToString();
        lblRegistrationClosingDate.Text = "Online registration closes " + ds.Tables[1].Rows[0][14].ToString();
        lblDescription.Text = ds.Tables[1].Rows[0][9].ToString();
        lblEventDetails.Text = ds.Tables[1].Rows[0][10].ToString();
        lblAdmission.Text = "Admission price per player: $" + ds.Tables[1].Rows[0][12].ToString();
        lblLocation.Text = ds.Tables[1].Rows[0][5].ToString();
        lblRepNameDisplay.Text = ds.Tables[1].Rows[0][6].ToString();
        lblRepEmailDisplay.Text = ds.Tables[1].Rows[0][7].ToString();
        lblRepPhoneDisplay.Text = ds.Tables[1].Rows[0][8].ToString();
        recipientImage.Attributes.Add("src", ds.Tables[1].Rows[0][13].ToString());

        GoogleMap.LoadAddress(ds.Tables[1].Rows[0][15].ToString() + " " + ds.Tables[1].Rows[0][16].ToString() + " " + ds.Tables[1].Rows[0][17].ToString() + " " + ds.Tables[1].Rows[0][18].ToString());

    }


    #endregion pageLoad methods
    

    #region btnCheckout methods

    /// <summary>
    /// This method verifies the user input by calling verifyGolfRegistration() (Business tier handles verification).
    /// Upon success, it adds two session variables and redirects to checkout.aspx   
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 11/18/12 Michael Larsen
    /// Tested by Michael Larsen 11/27/12

    protected void btnCheckout_Click(object sender, EventArgs e)
    { 
        try
        {
            BusinessTier bt = new BusinessTier();
            int numInParty;
            Int32.TryParse(txtNumInParty.Text, out numInParty);
            if (numInParty <= 0)
            {
                lblError.Text = "Please enter a valid number for 'Number in Party'";
                PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
            }
            else
            {
                Dictionary<String, String> golfRegInfo = grabGolfRegInfo();
                DataSet ds = bt.validateGolfReg(golfRegInfo);

                if (ds.Tables[0].Rows.Count > 0)
                    displayUserInputErrors(ds);
                else
                {
                    double cost = Convert.ToDouble(Session["admissionPrice"]) * Convert.ToDouble(txtNumInParty.Text);
                    golfRegInfo["cost"] = cost.ToString();
                    Session["regInfo"] = golfRegInfo;
                    Session["eventID"] = Request["eventID"].ToString();
                    Response.Redirect("Checkout.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx",false);
        }
    

    }


    /// <summary>
    /// This method displays the User Input errors
    /// </summary>
    /// <param name="ds"></param>
    /// Michael Larsen - 11/22/12
    /// Tested by Michael Larsen 11/27/12

    private void displayUserInputErrors(DataSet ds)
    {
        String errorString = "Please fix the following items:</br>";

        //  Iterate through errors
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            errorString += ds.Tables[0].Rows[i][1].ToString() + "<br/>";
        
        lblError.Text = errorString;
        PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
    }


    /// <summary>
    /// This helper method inserts the user input into a dictionary and returns it
    /// </summary>
    /// <returns></returns>
    /// Michael Larsen - 11/22/12
    /// Tested by Michael Larsen 11/27/12

    private Dictionary<String, String> grabGolfRegInfo()
    {       
        Dictionary<String, String> golfRegInfo = new Dictionary<String, String>();        

        golfRegInfo["eventID"] = Request["eventID"].ToString();
        golfRegInfo["firstName"] = txtFirstName.Text;
        golfRegInfo["lastName"] = txtLastName.Text;
        golfRegInfo["suffix"] = txtSuffix.Text;
        golfRegInfo["email"] = txtEmail.Text;
        golfRegInfo["emailConfirm"] = txtConfirmEmail.Text;
        golfRegInfo["phone"] = txtAreaCode.Text + txtPrefix.Text + txtLastFour.Text;
        golfRegInfo["numInParty"] = txtNumInParty.Text;    //  This was already validated to be good in the calling method
        Session["email"] = txtEmail.Text;

        return golfRegInfo;
    }


    #endregion btnCheckout methods
}