using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _RunRegistration : System.Web.UI.Page
{

    #region Page Load method(s)

    /// <summary>
    /// Populates the run page details if a valid eventID is part of the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Jason Vance - 11/19/2012
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Clear out the result labels if they need to be
            lblError.Text = "";
            lblFormSuccess.Text = "";

            // Get the event id
            String eventId = Request.QueryString["eventID"];
            BusinessTier bt = new BusinessTier();
            DataSet ds = null;

            // If the event id is good, then fill in the page information
            if (eventId != null)
            {
                ds = bt.getRunDetails(eventId);
                populateRunDetails(ds);

                ds = bt.getRunParticipants(eventId);
                populatePartcipantsBox(ds);

                // Populate our on-going list of runners
                populateRunnersSoFar();
            }
            else
            {
                Response.Redirect("Oops.aspx");
            }

            // Keep the postbask url of the add runner button consistent, needed because buttons to edit runners change the url
            btnAddAnotherRunner.PostBackUrl = "RunRegistration.aspx?eventID=" + Request.QueryString["eventID"];
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Puts a runners information back onto the form for editing
    /// </summary>
    /// <param name="runnerInfo"></param>
    /// Jason Vance - 11/23/2012
    private void populateRunnerForm(Dictionary<String, String> runnerInfo)
    {
        try
        {
            txtFirstName.Text = runnerInfo["firstName"];
            txtLastName.Text = runnerInfo["lastName"];
            txtNameSuffix.Text = runnerInfo["suffix"];
            chkAnonymous.Checked = (runnerInfo["anonymous"] == "yes") ? true : false;
            ddlTshirtSizes.SelectedValue = runnerInfo["shirtSize"];
            txtEmail.Text = runnerInfo["email"];
            txtConfirmEmail.Text = runnerInfo["emailConfirm"];
            txtEmergContactName.Text = runnerInfo["emergContact"];
            ddlTicketTypes.SelectedValue = runnerInfo["cost"];

            // runner's phone number
            String phoneNum = runnerInfo["phone"];
            String areaCode = phoneNum.Substring(0, 3);
            String prefix = phoneNum.Substring(3, 3);
            String suffix = phoneNum.Substring(6, 4);
            txtPhoneAreaCode.Text = areaCode;
            txtPhonePrefix.Text = prefix;
            txtPhoneSuffix.Text = suffix;

            // Emergency contact phone number
            phoneNum = runnerInfo["emergPh"];
            areaCode = phoneNum.Substring(0, 3);
            prefix = phoneNum.Substring(3, 3);
            suffix = phoneNum.Substring(6, 4);
            txtEmergContactPhoneAreaCode.Text = areaCode;
            txtEmergContactPhonePrefix.Text = prefix;
            txtEmergContactPhoneSuffix.Text = suffix;
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Puts the information from the DataSet into the various page labels
    /// </summary>
    /// <param name="ds">DataSet with all the run event information</param>
    /// Jason Vance - 11/20/2012
    private void populateRunDetails(DataSet ds)
    {
        try
        {
            hdnEventId.Value = ds.Tables[1].Rows[0][0].ToString();
            hdnRecipientId.Value = ds.Tables[1].Rows[0][1].ToString();
            lblEventTitle.Text = ds.Tables[1].Rows[0][2].ToString();
            lblEventTime.Text = ds.Tables[1].Rows[0][3].ToString();
            lblEventDate.Text = ds.Tables[1].Rows[0][4].ToString();
            lblRegistrationClosingDate.Text = "Online registration closes " + ds.Tables[1].Rows[0][15].ToString();
            lblEventAddress.Text = ds.Tables[1].Rows[0][5].ToString();
            lblEventRep.Text = ds.Tables[1].Rows[0][6].ToString();
            lblEventRepEmail.Text = ds.Tables[1].Rows[0][7].ToString();
            lblEventRepPhone.Text = ds.Tables[1].Rows[0][8].ToString();
            lblEventDescription.Text = ds.Tables[1].Rows[0][9].ToString();
            lblEventDetails.Text = ds.Tables[1].Rows[0][10].ToString();
            lblAdultPrice.Text = ds.Tables[1].Rows[0][13].ToString();
            lblChildPrice.Text = ds.Tables[1].Rows[0][12].ToString();
            imgRecipientPhoto.ImageUrl = ds.Tables[1].Rows[0][14].ToString();

            populateTicketTypeDropDown(ds);
            GoogleMap.LoadAddress(ds.Tables[1].Rows[0][16].ToString() + " " + ds.Tables[1].Rows[0][17].ToString() + " " + ds.Tables[1].Rows[0][18].ToString() + " " + ds.Tables[1].Rows[0][19].ToString());
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Populates the ticket type drop down list with the prices for adult and child
    /// </summary>
    /// <param name="ds">DataSet with fields for adultPrice and childPrice</param>
    /// Jason Vance - 11/20/2012
    private void populateTicketTypeDropDown(DataSet ds)
    {
        try
        {
            ddlTicketTypes.Items[0].Value = ds.Tables[1].Rows[0][13].ToString();    // Adult price
            ddlTicketTypes.Items[1].Value = ds.Tables[1].Rows[0][12].ToString();    // Child price
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Populates the participants box using the DataSet passed in. Uses the 
    /// List<String> version of this method to complete its work</String>
    /// </summary>
    /// <param name="people">DataSet of people who are registered for this event</param>
    /// Jason Vance - 11/19/2012
    private void populatePartcipantsBox(DataSet people)
    {
        List<String> peopleList = new List<String>();

        try
        {
            // Create a List<String> out of the people in the DataSet
            foreach (DataRow row in people.Tables[1].Rows)
            {
                String firstName = row[0].ToString();
                String lastName = row[1].ToString();
                String fullName = firstName + " " + lastName;
                peopleList.Add(fullName);
            }

            // Use the List we just created to fill in the participants box
            populatePartcipantsBox(peopleList);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Populates the participants box using the List<String> passed in
    /// </summary>
    /// <param name="people">List of people who are registered for this event</param>
    /// Jason Vance - 11/19/2012
    private void populatePartcipantsBox(List<String> people)
    {
        int count = 0;

        try
        {
            participantsList.InnerHtml = "";    // First clear out the list

            foreach (String person in people)
            {
                participantsList.InnerHtml += person + "<br/>";
                count++;
            }
            lblNumOfRunners.Text = "" + count;
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Populates the list of runners so far based on the list of runners on the session
    /// </summary>
    /// Jason Vance - 11/23/2012
    private void populateRunnersSoFar()
    {
        Decimal totalCost = 0;
        List<Dictionary<String, String>> runners;

        try
        {
            // Clear the list
            divRunnersSoFar.Controls.Clear();

            // Get the runners off the session
            runners = (List<Dictionary<String, String>>)Session["runners"];
            // If there were runners on the session...
            if (runners != null)
            {
                int count = 0;
                // Add each runner's name to the list of runners for this registration
                foreach (Dictionary<String, String> runner in runners)
                {
                    // Make a button for each name
                    Button button = new Button();
                    button.Click += new EventHandler(btnEditName_Click);
                    button.Text = runner["firstName"];
                    button.PostBackUrl = "RunRegistration.aspx?eventID=" + Request.QueryString["eventID"] + "&nameID=" + count;
                    button.ID = "btnEditName" + count;
                    button.Attributes.Add("class", "button-runners");
                    divRunnersSoFar.Controls.Add(button);
                    count++;

                    // Get the total cost of all the tickets
                    Decimal cost = Convert.ToDecimal(runner["cost"]);
                    totalCost += cost;
                }

                lblTotalCost.Text = "" + totalCost;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    #endregion Page Load method(s)

    #region Button Events

    /// <summary>
    /// When a user clicks on the name of runner to edit it, put that runner's info into the form
    /// set some hidden fields to keep track of whether we are editing or adding a runner
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Jason Vance - 11/23/2012
    protected void btnEditName_Click(object sender, EventArgs e)
    {
        try
        {
            // Clear out any messages
            lblFormSuccess.Text = "";

            // index of the runner to edit
            String nameId = Request.QueryString["nameID"];
            if (nameId != null)
            {
                int index = Convert.ToInt32(nameId);

                if (index >= 0)
                {
                    List<Dictionary<String, String>> runners = (List<Dictionary<String, String>>)Session["runners"];
                    Dictionary<String, String> runnerInfo = runners[index];
                    populateRunnerForm(runnerInfo);

                    hdnNameId.Value = "" + index;
                    hdnEditingRunner.Value = "yes";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Button event for "Proceed to checkout" button
    /// Adds the current runner to the list of runners in the session, then registers them
    /// If all goes well, redirects to checkout. Otherwise, informs the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Jason Vance - 11/23/2012
    protected void btnProceedToCheckout_Click(object sender, EventArgs e)
    {
        Dictionary<String, String> runnerInfo;
        List<Dictionary<String, String>> runners;
        BusinessTier bt = new BusinessTier();
        DataSet result;

        try
        {
            // Get the runners on the session
            runners = (List<Dictionary<String, String>>)Session["runners"];
            // If there were no runners on the session, assign a new List to the variable
            if (runners == null)
            {
                runners = new List<Dictionary<String, String>>();
            }

            // Get the runner info from the form
            runnerInfo = extractRunnerInfo();

            Session["eventID"] = hdnEventId.Value;

            // Validate the entered information
            DataSet ds = bt.validateRunReg(runnerInfo);
            // If there were problems let the user know
            if (ds.Tables[0].Rows.Count > 0)
            {
                showErrorMessages(ds);
                PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
                return; // Get out so the user can fix what's wrong
            }
            else { lblError.Text = ""; }

            // If we're editing an existing runner
            if (hdnEditingRunner.Value == "yes")
            {
                if (!runnerIsDuplicate(runnerInfo))
                {
                    // Replace the old runner with the new runner
                    int index = Convert.ToInt32(hdnNameId.Value);
                    runners[index] = runnerInfo;

                    // reset the hidden fields
                    hdnNameId.Value = "-1";
                    hdnEditingRunner.Value = "no";

                    // Tell the user that editing worked
                    lblFormSuccess.Text = txtFirstName.Text + " was edited";
                }
                else
                {
                    lblError.Text = "This runner is already registered";
                    PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
                    return;
                }
            }
            else
            {
                if (!runnerIsDuplicate(runnerInfo))
                {
                    // Add the runner to the list of runners
                    runners.Add(runnerInfo);
                    // Tell the user that adding worked
                    lblFormSuccess.Text = txtFirstName.Text + " was added";
                }
                else
                {
                    lblError.Text = "This runner is already registered";
                    PresentationHelpers.FocusControlOnPageLoad(lblError.ClientID, this.Page);
                    return;
                }
            }

            // Register the runners
            result = bt.registerRunGroup(runners);

            // If there were problems let the user know
            if (result.Tables[0].Rows.Count > 0)
            {
                showErrorMessages(result);

                // Empty out all fields to avoid registering a runner twice
                emptyFormFields();
                // Let the user know that the runner was added to the list
                populateRunnersSoFar();
            }
            else
            {
                lblError.Text = "";
                // There weren't any problems registering, so send the user to checkout
                Session.Add("regInfo", runners);
                Response.Redirect("Checkout.aspx",false);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Empties all the fields on the form
    /// </summary>
    /// Jason Vance - 11/23/2012
    private void emptyFormFields()
    {
        try
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNameSuffix.Text = "";
            chkAnonymous.Checked = false;
            txtPhoneAreaCode.Text = "";
            txtPhonePrefix.Text = "";
            txtPhoneSuffix.Text = "";
            txtEmail.Text = "";
            txtConfirmEmail.Text = "";
            txtEmergContactName.Text = "";
            txtEmergContactPhoneAreaCode.Text = "";
            txtEmergContactPhonePrefix.Text = "";
            txtEmergContactPhoneSuffix.Text = "";
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Creates a Dictionary of runner registration info by extracting all the fields on the page.
    /// </summary>
    /// <returns></returns>
    ///  Jason Vance - 11/23/2012
    private Dictionary<String, String> extractRunnerInfo()
    {
        Dictionary<String, String> runnerInfo = new Dictionary<String, String>();

        try
        {
            runnerInfo.Add("eventID", hdnEventId.Value.ToString());
            runnerInfo.Add("firstName", txtFirstName.Text);
            runnerInfo.Add("lastName", txtLastName.Text);
            runnerInfo.Add("suffix", txtNameSuffix.Text);
            String anonymous = chkAnonymous.Checked ? "yes" : "no";
            runnerInfo.Add("anonymous", anonymous);
            runnerInfo.Add("shirtSize", ddlTshirtSizes.SelectedValue);
            String phone = txtPhoneAreaCode.Text + txtPhonePrefix.Text + txtPhoneSuffix.Text;
            runnerInfo.Add("phone", phone);
            runnerInfo.Add("email", txtEmail.Text);
            runnerInfo.Add("emailConfirm", txtConfirmEmail.Text);
            runnerInfo.Add("emergContact", txtEmergContactName.Text);
            String emergPhone = txtEmergContactPhoneAreaCode.Text + txtEmergContactPhonePrefix.Text + txtEmergContactPhoneSuffix.Text;
            runnerInfo.Add("emergPh", emergPhone);
            runnerInfo.Add("transactionID", "");
            runnerInfo.Add("personID", "");
            runnerInfo.Add("cost", ddlTicketTypes.SelectedValue);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

        return runnerInfo;
    }

    /// <summary>
    /// Button event for "Save and add another runner" button
    /// Adds a Dictionary of runner info to the List<Dictionary> of runners
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Jason Vance - 11/23/2012
    protected void btnAddAnotherRunner_Click(object sender, EventArgs e)
    {
        Dictionary<String, String> runnerInfo;
        List<Dictionary<String, String>> runners;
        BusinessTier bt = new BusinessTier();

        try
        {
            // Get the runners on the session
            runners = (List<Dictionary<String, String>>)Session["runners"];
            // If there were no runners on the session, assign a new List to the variable
            if (runners == null)
            {
                runners = new List<Dictionary<String, String>>();
            }

            // Get the runner off the form
            runnerInfo = extractRunnerInfo();

            // Validate the entered information
            DataSet ds = bt.validateRunReg(runnerInfo);
            // If there were problems let the user know
            if (ds.Tables[0].Rows.Count > 0)
            {
                showErrorMessages(ds);
                return; // Get out so the user can fix what's wrong
            }
            else { lblError.Text = ""; }

            // If we're editing an existing runner
            if (hdnEditingRunner.Value == "yes")
            {
                if (!runnerIsDuplicate(runnerInfo))
                {
                    // Replace the old runner with the new runner
                    int index = Convert.ToInt32(hdnNameId.Value);
                    runners[index] = runnerInfo;

                    // reset the hidden fields
                    hdnNameId.Value = "-1";
                    hdnEditingRunner.Value = "no";

                    // Tell the user that editing worked
                    lblFormSuccess.Text = txtFirstName.Text + " was edited";
                }
                else
                {
                    lblError.Text = "This runner is already registered";
                    return;
                }
            }
            else
            {
                if (!runnerIsDuplicate(runnerInfo))
                {
                    // Add the runner to the list of runners
                    runners.Add(runnerInfo);
                    // Tell the user that adding worked
                    lblFormSuccess.Text = txtFirstName.Text + " was added";
                }
                else
                {
                    lblError.Text = "This runner is already registered";
                    return;
                }
            }

            // Put the list of runners back on the session
            Session.Add("runners", runners);

            // Populate our on-going list of runners
            populateRunnersSoFar();

            // empty out appropriate fields for a new runner
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNameSuffix.Text = "";
            txtPhoneAreaCode.Text = "";
            txtPhonePrefix.Text = "";
            txtPhoneSuffix.Text = "";
            txtEmail.Text = "";
            txtConfirmEmail.Text = "";
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Checks to see if the runner already exists in the runner list on the session
    /// Will return true if every field is the same (other than emergency contact info), false otherwise
    /// </summary>
    /// <param name="newRunner"></param>
    /// <returns></returns>
    /// Jason Vance - 11/30/2012
    private Boolean runnerIsDuplicate(Dictionary<String, String> runnerToCheck)
    {
        Boolean result = false;
        List<Dictionary<String, String>> runners = (List<Dictionary<String, String>>)Session["runners"];
        // If there were no runners on the session, assign a new List to the variable
        if (runners == null)
        {
            return false;
        }

        // Go through each runner in the list and check the fields
        int index = 0;  // Used to see if we will be creating a duplicate by editing a current runner
        foreach (Dictionary<String, String> runner in runners)
        {
            if (runner["firstName"] == runnerToCheck["firstName"]
                && runner["lastName"] == runnerToCheck["lastName"]
                && runner["suffix"] == runnerToCheck["suffix"]
                && runner["anonymous"] == runnerToCheck["anonymous"]
                && runner["shirtSize"] == runnerToCheck["shirtSize"]
                && runner["phone"] == runnerToCheck["phone"]
                && runner["email"] == runnerToCheck["email"]
                && runner["emailConfirm"] == runnerToCheck["emailConfirm"]
                && runner["cost"] == runnerToCheck["cost"])
            {
                // Everything matches

                // If we are editing a current runner, then check the index
                if (hdnEditingRunner.Value == "yes")
                {
                    // If it's the same index, then we are not creating a duplicate,
                    // the runner just wasn't modified before saving
                    if (Convert.ToInt32(hdnNameId.Value) != index)
                    {
                        result = true;
                        break;
                    }
                }
                else
                {
                    result = true;
                    break;
                }
            }

            index++;
        }

        return result;
    }

    /// <summary>
    /// Takes the errors out of the DataSet's table[0] and puts them all into an error label
    /// </summary>
    /// <param name="ds"></param>
    /// Jason Vance - 11/30/2012
    private void showErrorMessages(DataSet ds)
    {
        String errMsg = "";
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            errMsg += row[1].ToString() + "<br/>";
        }
        lblError.Text = errMsg;
    }

    #endregion Button Events
}