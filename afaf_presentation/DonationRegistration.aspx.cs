using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DonationRegistration : System.Web.UI.Page
{
    #region pageLoad method

    /// <summary>
    /// This method runs as the page is loaded. The donation information that is displayed is based on the 
    /// query string "eventID".
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Aaron Copeland - 11/27/2012
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

                // get the details for the Donation Event
                DataSet ds = bt.getDonationDetails(eventID);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    String errorString = "Please fix the following items:</br>";

                    //  Iterate through errors
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        errorString += ds.Tables[0].Rows[i][1] + "<br/>";

                    lblError.Text = errorString;
                    PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
                }

                // populate the page with the recipients info
                recipientImage.Attributes.Add("src", ds.Tables[1].Rows[0][6].ToString());
                lblEventTitle.Text = ds.Tables[1].Rows[0][2].ToString();                
                lblEventDescription.Text = ds.Tables[1].Rows[0][3].ToString();
                lblEventDetails.Text = ds.Tables[1].Rows[0][4].ToString();
                hdnReciepantsID.Value = ds.Tables[1].Rows[0][1].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    #endregion



    #region Checkout method

    /// <summary>
    /// This method validates the donation information entered by the user.
    /// If problems exist the user is prompted to fix them.
    /// If the information does not produce an error the information is saved as a session variable
    /// and user is directed to the checkout page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Aaron Copeland - 11/27/2012
    /// Tested by Aaron Copeland - 11/28/2012
    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        BusinessTier bt = new BusinessTier();
        Dictionary<String, String> donationInfo = new Dictionary<string, string>();

        try
        {
            // check if they are anonymous
            string isAnonymousPerson = "no";
            if(chkAnonymous.Checked)
                isAnonymousPerson = "yes";

            // try to parse the dontaion amount
            double cashValue = 0;           
            Double.TryParse(txtCashValue.Text, out cashValue);

            if (cashValue <= 0)
                lblError.Text = "Please enter a valid donation amount.";
            else
            {
                donationInfo.Add("anonymousPerson", isAnonymousPerson);
                donationInfo.Add("firstName", txtFirstName.Text);
                donationInfo.Add("lastName", txtLastName.Text);
                donationInfo.Add("suffix", txtSuffix.Text);
                donationInfo.Add("phone", txtAreaCode.Text + txtPrefix.Text + txtLineNumber.Text);  
                donationInfo.Add("email", txtEmail.Text);
                donationInfo.Add("emailConfirm", txtConfirmEmail.Text);                              
                donationInfo.Add("cashValue", cashValue.ToString());
                donationInfo.Add("address", "123 Temp");
                donationInfo.Add("city", "Ogden");
                donationInfo.Add("state", "UT");
                donationInfo.Add("zip", "84414");
                donationInfo.Add("recipientID", hdnReciepantsID.Value.ToString());
                // sends the dictionary to the business tier to check the fields
                DataSet ds = bt.validateDonationInfo(donationInfo);
                Session["email"] = txtEmail.Text;
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
                    Session["regInfo"] = donationInfo;     // dictionary used as a session variable 
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