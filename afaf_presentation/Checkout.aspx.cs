using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Checkout : System.Web.UI.Page
{
    Dictionary<String, String> regInfo; //Holds the session dictionary that is passed containing the registrant's details
    List<Dictionary<String, String>> runnerList = new List<Dictionary<string, string>>(); // Holds the list of session dictionaries containing the runner's info
    String eventType;
    /// <summary>
    /// Used to count how many times the page has been hit, so the back function can go to the proper previous screen
    /// </summary>
    /// Created by: Andrew Heim 11-30-12
    public int PostBackCount;

    /// <summary>
    /// On page load, two functions are called - getEventType and Display???ReistrantDetails
    ///The Display function fills in the checkout table to display the total cost.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Created by: OJ 11-18-12
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Used to bring the user back to the previous screen with the info filled out -Andrew Heim 11/30/12
            if (!IsPostBack)
            {
                PostBackCount = 1;
                Session["PostBackCount"] = PostBackCount;
            }
            else
            {
                PostBackCount = ((int)Session["PostBackCount"]);
                Session["PostBackCount"] = ++PostBackCount;
            }

            BusinessTier bt = new BusinessTier();

            String eventID = Session["eventID"].ToString();

            DataSet ds = bt.getEventType(eventID);

            eventType = ds.Tables[1].Rows[0][0].ToString();

            btnPayPal.Enabled = chkLiabilityStatus();   //enables or disables the continue to paypal button based on the liabilty checkbox

            if (eventType != "Run")
                regInfo = (Dictionary<String, String>)Session["regInfo"];       //if not a 5k, session variable is a dictionary object
            else
                runnerList = (List<Dictionary<String, string>>)Session["regInfo"];  //if a 5k, session variable is a list of dictionary objects

            lblEventType.Text = eventType + " Event";

            switch (eventType)
            {
                case "Golf": displayGolfRegistrantDetails();
                    break;
                case "Dinner": displayDinnerRegistrantDetails();
                    break;
                case "Run": displayRunRegistrantDetails();
                    break;
                case "Donation": displayDonationDetails();
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Saves additional information needed for Paypal
    /// </summary>
    /// Created by: Nathan Schroader 12-3-12
    private void savePaypalInfo()
    {
        String eventID = Session["eventID"].ToString();
        Session["eventID"] = eventID;
        Session["eventType"] = eventType;
        Session["checkoutTable"] = tblCartItems;
    }

    /// <summary>
    /// Fills in the donation checkout table to display the donation amount.
    /// </summary>
    /// Created by: Olivia Johnson 11-18-12
    public void displayDonationDetails()
    {
        try
        {
            liabilitySection.Visible = false;   //hides the liability agreement for donation checkouts
            btnPayPal.Enabled = true;
            btnPayPal.CssClass = "buttonMain";

            Decimal cost = Convert.ToDecimal(regInfo["cashValue"]);

            //Table Row 1, cell 0 - sets the Item to Donation
            TableRow tr = new TableRow();
            tblCartItems.Rows.Add(tr);
            TableCell cell1 = new TableCell();
            cell1.Text = "Donation";
            tr.Cells.Add(cell1);

            //Table Row 1, cell 1 - displays the quantity (set to blank for donation)
            TableCell cell2 = new TableCell();
            cell2.Width = 0;
            tr.Cells.Add(cell2);

            //Table Row 1, cell 3 - displays the donation amount
            TableCell cell3 = new TableCell();
            cell3.Text = "$" + cost.ToString("C").Replace("$", "");
            tr.Cells.Add(cell3);

            //Table Row 1, cell 2 - Displays the subtotal
            TableCell cell4 = new TableCell();
            cell4.Text = "$" + cost.ToString("C").Replace("$", "");
            tr.Cells.Add(cell4);

            displayTotalRow(cost.ToString("C").Replace("$", ""));   //Displays the total cost

            tblCartItems.Rows[0].Cells[0].Text = "Item";        //alters the column heading that is normally "Ticket Type"
            tblCartItems.Rows[0].Cells[2].Text = "Amount";      //alters the column heading that is normally "Price"
            tblCartItems.Rows[0].Cells[1].Width = 0;
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Fills in the golf event checkout table to display the number of tickets and the total cost.
    /// </summary>
    /// Created by: Olivia Johnson 11-18-12
    private void displayGolfRegistrantDetails()
    {
        try
        {
            Decimal totalCost = Convert.ToDecimal(regInfo["cost"]);
            Decimal numInParty = Convert.ToDecimal(regInfo["numInParty"]);
            Decimal cost = totalCost / numInParty;


            //Table Row 1, cell 0 - sets the Ticket Type to Golf and displays number of participants
            TableRow tr = new TableRow();
            tblCartItems.Rows.Add(tr);
            TableCell cell1 = new TableCell();
            cell1.Text = "Golf Ticket ";
            tr.Cells.Add(cell1);

            //Table Row 1, cell 1 - displays the quantity
            TableCell cell2 = new TableCell();
            cell2.CssClass = "checkout-col2";
            cell2.Text = "(" + regInfo["numInParty"] + ")";
            tr.Cells.Add(cell2);

            //Table Row 1, cell 2 - displays the price per ticket
            TableCell cell3 = new TableCell();
            cell3.Text = "$" + cost.ToString("C").Replace("$", "");
            tr.Cells.Add(cell3);

            //Table Row 1, cell 3 - Displays the subtotal
            TableCell cell4 = new TableCell();
            cell4.Text = "$" + totalCost.ToString("C").Replace("$", "");
            tr.Cells.Add(cell4);

            displayTotalRow(totalCost.ToString("C").Replace("$", ""));  //Displays the total row
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

    }

    /// <summary>
    /// Determines the number of children and number of adults in the purchase and calls displayRunOrDinner to fill the checkout table.
    /// </summary>
    /// Created by: Olivia Johnson 11-18-12
    private void displayDinnerRegistrantDetails()
    {
        try
        {
            int numOfChildren = Int16.Parse(regInfo["numChildren"]);
            int numOfAdults = Int16.Parse(regInfo["numAdults"]);

            displayRunOrDinner(numOfChildren, numOfAdults);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Fills in the dinner or golf event checkout table to display the number of each type of ticket and the total cost.
    /// </summary>
    /// <param name="numOfChildren">Decimal number to store the number of children</param>
    /// <param name="numOfAdults">Decimal number to store the number of adults</param>
    /// Created by: Olivia Johnson 11-18-12
    private void displayRunOrDinner(Decimal numOfChildren, Decimal numOfAdults)
    {
        try
        {
            Decimal childPrice = 0;
            Decimal adultPrice = 0;

            childPrice = Convert.ToDecimal(getTicketPrice("child"));
            adultPrice = Convert.ToDecimal(getTicketPrice("adult"));

            Decimal totalCost = (numOfAdults * adultPrice) + (numOfChildren * childPrice);

            //Table Row 1, cell 0 - sets the Ticket Type to Adult Ticket and displays number of participants
            TableRow tr = new TableRow();
            tblCartItems.Rows.Add(tr);
            TableCell cell1;
            TableCell cell2;
            TableCell cell3;
            TableCell cell4;

            if (numOfAdults > 0)
            {
                cell1 = new TableCell();
                cell1.Text = "Adult Ticket ";
                tr.Cells.Add(cell1);

                //Table Row 1, cell 1 - displays the quantity
                cell2 = new TableCell();
                cell2.CssClass = "checkout-col2";
                cell2.Text = "(" + numOfAdults + ")";
                tr.Cells.Add(cell2);

                //Table Row 1, cell 2 - displays the adult price per ticket
                cell3 = new TableCell();
                cell3.Text = "$" + adultPrice.ToString("C").Replace("$", "");
                tr.Cells.Add(cell3);

                //Table Row 1, cell 3 - Displays the subtotal
                cell4 = new TableCell();
                cell4.Text = "$" + (numOfAdults * adultPrice).ToString("C").Replace("$", ""); ;
                tr.Cells.Add(cell4);
            }
            if (numOfChildren > 0)
            { 
            //Adds row 2
            TableRow tr2 = new TableRow();
            tblCartItems.Rows.Add(tr2);

            //Table Row 2, cell 0 - sets the Ticket Type to Child Ticket and displays number of participants
            cell1 = new TableCell();
            cell1.Text = "Child Ticket ";
            tr2.Cells.Add(cell1);

            //Table Row 2, cell 1 - displays the quantity
            cell2 = new TableCell();
            cell2.CssClass = "checkout-col2";
            cell2.Text = "(" + numOfChildren + ")";
            tr2.Cells.Add(cell2);

            //Table Row 2, cell 2 - displays the child price per ticket
            cell3 = new TableCell();
            cell3.Text = "$" + childPrice.ToString("C").Replace("$", "");
            tr2.Cells.Add(cell3);

            //Table Row 2, cell 3 - Displays the subtotal
            cell4 = new TableCell();
            cell4.Text = "$" + (numOfChildren * childPrice).ToString("C").Replace("$", ""); ;
            tr2.Cells.Add(cell4);
                }
            displayTotalRow(totalCost.ToString("C").Replace("$", ""));  //Displays the total cost
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Displays the "Total" row in the checkout table based on the total cost passed in
    /// </summary>
    /// <param name="total">Stores the total cost in a string variable</param>
    /// Created by: Olivia Johnson 11-18-12
    private void displayTotalRow(String total)
    {
        try
        {
            TableRow tr3 = new TableRow();
            tr3.CssClass = "rowTotal";
            tblCartItems.Rows.Add(tr3);

            TableCell cell1 = new TableCell();
            cell1.Text = "";
            tr3.Cells.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Text = "";
            tr3.Cells.Add(cell2);

            TableCell cell3 = new TableCell();
            cell3.Text = "   Total:";
            tr3.Cells.Add(cell3);

            TableCell cell4 = new TableCell();
            cell4.Text = "$" + total;
            tr3.Cells.Add(cell4);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Retrieves the ticket price for each ticket type from the database based on the eventID
    /// </summary>
    /// <param name="type">String storing the type of ticket - adult or child</param>
    /// <returns>String storing the price for either an adult ticket or child ticket</returns>
    /// Created by: Olivia Johnson 11-18-12
    private String getTicketPrice(String type)
    {
        BusinessTier bt = new BusinessTier();

        if (eventType == "Dinner")
        {
            if (type == "child")
                return bt.getDinnerDetails(Session["eventID"].ToString()).Tables[1].Rows[0][12].ToString();
            else
                return bt.getDinnerDetails(Session["eventID"].ToString()).Tables[1].Rows[0][13].ToString();
        }
        else
        {
            if (type == "child")
                return bt.getRunDetails(Session["eventID"].ToString()).Tables[1].Rows[0][12].ToString();
            else
                return bt.getRunDetails(Session["eventID"].ToString()).Tables[1].Rows[0][13].ToString();
        }
    }

    /// <summary>
    /// Traverses the List of regInfo Dictionary objects and counts the number of adults and children. Then DisplayRunOrDinner is called to 
    /// fill in the dinner checkout table
    /// </summary>
    /// Created by: Olivia Johnson 11-18-12
    private void displayRunRegistrantDetails()
    {
        try
        {
            BusinessTier bt = new BusinessTier();
            List<Dictionary<String, String>> regInfoList = (List<Dictionary<String, String>>)Session["regInfo"];

            int numOfChildren = 0, numOfAdults = 0;
            string childPrice = bt.getRunDetails(Session["eventID"].ToString()).Tables[1].Rows[0][12].ToString();
            foreach (Dictionary<String, String> registeredRunner in regInfoList)
            {
                if (registeredRunner["cost"] == childPrice)  //if participant is older than 12, count them as an adult.
                    numOfChildren++;
                else
                    numOfAdults++;    //If participant is younger than 12, count them as a child
            }

            displayRunOrDinner(numOfChildren, numOfAdults);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

    }

    /// <summary>
    /// Determines whether the liability checkbox is selected or not
    /// </summary>
    /// Created by: Olivia Johnson 11-28-12
    public bool chkLiabilityStatus()
    {
        if (chkLiability.Checked)
        {
            btnPayPal.CssClass = "buttonMain";
            return true;
        }
        else
        {
            btnPayPal.CssClass = "buttonDisabled";
            return false;
        }
    }

    /// <summary>
    /// Determines which event type it is, inserts the data into the database and proceeds to paypal.
    /// </summary>
    /// Created by: Andrew Heim 11-29-12
    protected void btnPayPal_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnPayPal.CssClass == "buttonDisabled")
                return;
            if (regInfo != null)
                regInfo["eventID"] = Session["eventID"].ToString();

            switch (eventType)
            {
                case "Golf": insertGolfRegistrantDetails();
                    break;
                case "Dinner": insertDinnerRegistrantDetails();
                    break;
                case "Run": insertRunRegistrantDetails();
                    break;
                case "Donation": insertDonationDetails();
                    break;
            }

            savePaypalInfo();
            Response.Redirect("ProcessPaypal.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }

    /// <summary>
    /// Inserts the Donation details and stores transaction id and cost into the session for paypal to use.
    /// </summary>
    /// Created by: Andrew Heim 11-29-12
    private void insertDonationDetails()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = bt.insertDonation((Dictionary<string,string>)Session["regInfo"]);
        Session["transactionID"] = ds.Tables[1].Rows[0][0];
        Session["cost"] = ((Dictionary<string, string>)Session["regInfo"])["cashValue"];
        Session["eventType"] = "Donation";
    }

    /// <summary>
    /// Inserts the Run details and stores transaction id and cost into the session for paypal to use.
    /// </summary>
    /// Created by: Andrew Heim 11-29-12
    private void insertRunRegistrantDetails()
    {
        BusinessTier bt = new BusinessTier();
        List<Dictionary<string,string>> runRegInfo = (List<Dictionary<string, string>>)Session["regInfo"];
        DataSet ds = bt.registerRunGroup(runRegInfo);
        Session["transactionID"] = ds.Tables[1].Rows[0][0];
        double cost = 0;
        foreach(Dictionary<string,string> row in runRegInfo)
        {
            cost += double.Parse(row["cost"]);
        }
        Session["cost"] = cost;
    }

    /// <summary>
    /// Inserts the Dinner details and stores transaction id and cost into the session for paypal to use.
    /// </summary>
    /// Created by: Andrew Heim 11-29-12
    private void insertDinnerRegistrantDetails()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = bt.insertDinnerRegistration((Dictionary<string, string>)Session["regInfo"]);
        Session["transactionID"] = ds.Tables[1].Rows[0][0];
        Session["cost"] = ((Dictionary<string, string>)Session["regInfo"])["cost"];
        Session["eventType"] = "Dinner";
    }

    /// <summary>
    /// Inserts the Golf details and stores transaction id and cost into the session for paypal to use.
    /// </summary>
    /// Created by: Andrew Heim 11-29-12
    private void insertGolfRegistrantDetails()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = bt.insertGolfRegistration((Dictionary<string, string>)Session["regInfo"]);
        Session["transactionID"] = ds.Tables[1].Rows[0][0];
        Session["cost"] = ((Dictionary<string, string>)Session["regInfo"])["cost"];
        Session["eventType"] = "Golf";
    }
}