using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    String eventType;
    String eventID;
    String transactionID;
    Dictionary<String, String> customerInfo;

    /// <summary>
    /// When the page loads, the transaction date and information are displayed.
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    protected void Page_Load(object sender, EventArgs e)
    {
        //read in txn token from querystring
        String txToken = Request.QueryString.Get("tx");
        String IPNResponce = getIPN(txToken);

        //Create dictonary with Paypal info
        Dictionary<String, String> payPalInfo = getPayPalStuff(IPNResponce);

        // Parse the eventID and transactionID from the 'option' field
        // Example: eventID_transactionID (112314_31231231231)
        String customField = payPalInfo["custom"].ToString();
        eventID = customField.Substring(0, customField.LastIndexOf("_"));
        transactionID = customField.Substring(customField.IndexOf("_") + 1, customField.Length - customField.LastIndexOf("_") - 1);
        customerInfo = new Dictionary<string, string>();
        customerInfo["transactionID"] = transactionID;
        customerInfo["eventID"] = eventID;
        customerInfo["firstName"] = payPalInfo["first_name"];
        customerInfo["lastName"] = payPalInfo["last_name"];
        customerInfo["suffix"] = "";
        customerInfo["phone"] = "";
        if(Session["email"] == null)
            customerInfo["email"] = payPalInfo["payer_email"].Replace("%40", "@");
        else
            customerInfo["email"] = Session["email"].ToString();
        customerInfo["address"] = payPalInfo["address_street"];
        customerInfo["city"] = payPalInfo["address_city"];
        customerInfo["state"] = payPalInfo["address_state"];
        customerInfo["zip"] = payPalInfo["address_zip"];


        lblOrderDate.Text = DateTime.Now.ToString("MM/dd/yyyy");    //Display Date
        lblTransaction.Text = transactionID;

        BusinessTier bt = new BusinessTier();
        eventType = bt.getEventType(eventID).Tables[1].Rows[0][0].ToString();

        switch (eventType)
        {
            case "Golf": golfConfirmation();
                break;
            case "Dinner": dinnerConfirmation();
                break;
            case "Run": runConfirmation();
                break;
            case "Donation": donateConfirmation();
                break;
        }
    }

    /// <summary>
    /// Sends POST request to Paypal and returns a IPN string
    /// </summary>
    /// <param name="txToken"></param>
    /// <returns>String</returns>
    /// 11/28/12 Nathan Schroader
    private String getIPN(String txToken)
    {
        String authToken, query;
        String strResponse = "";
        authToken = "M66t6nVHeEQ2NuO4BZLV_oto4-Ij8a_P7EUqu7Jz7WCvlEyC4cvBVnHlS5q";
        query = "cmd=_notify-synch&tx=" + txToken +
                    "&at=" + authToken;
        try
        {
            // Create the request back
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

        return strResponse;
    }

    /// <summary>
    /// Parses through IPN information and creates a dictonary object
    /// </summary>
    /// <param name="strResponse"></param>
    /// <returns>dictionary object</returns>
    /// 11/28/12 Nathan Schroader
    private Dictionary<String, String> getPayPalStuff(String strResponse)
    {
        Dictionary<String, String> IPNInfo = new Dictionary<String, String>();
        String sKey, sValue;

        try
        {
            if (strResponse.Substring(0, 4) == "FAIL")
            {
                ErrorLog.logError(new System.Exception("IPNInfo returned FAIL"), strResponse);
            }
            // If response was SUCCESS, parse response string and
            //output details
            else if (strResponse.Substring(0, 7) == "SUCCESS")
            {
                //split response into string array using whitespace
                //as delimiter
                String[] StringArray = strResponse.Split();

                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');

                    sKey = StringArray1[0];
                    sValue = StringArray1[1];

                    IPNInfo.Add(sKey, sValue);
                }
            }
            else
            {
                ErrorLog.logError(new System.Exception("IPNInfo returned unknown error"), strResponse);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

        return IPNInfo;
    }

    /// <summary>
    /// Confirms the transaction in the database and displays the transaction info
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    private void donateConfirmation()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = new DataSet();
        Decimal donationAmount = 0;


        try
        {
            ds = bt.confirmDonation(customerInfo);
            donationAmount = decimal.Parse(ds.Tables[1].Rows[0][10].ToString());
            lblticketType1.Text = " Donation";
            lblprice1.Text = donationAmount.ToString("C");
            lbltotalCost.Text = donationAmount.ToString("C");
         
            donationAmount = Convert.ToDecimal(ds.Tables[1].Rows[0][9].ToString());

            sendConfirmationEmail("Donation", ds.Tables[1].Rows[0][0].ToString(), ds.Tables[1].Rows[0][5].ToString());
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

        
    }

    /// <summary>
    /// Confirms the transaction in the database and displays the transaction info
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    private void golfConfirmation()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = new DataSet();
        Decimal cost = 0;
        Decimal numOfGolfers = 0;

        try
        {
            ds = bt.confirmGolfPayment(customerInfo);

            lblticketType1.Text = "Golf Event Ticket x" + ds.Tables[1].Rows[0][5].ToString();
            lblprice1.Text = cost.ToString("C");
            lbltotalCost.Text = (cost * numOfGolfers).ToString("C");
           
            cost = Convert.ToDecimal(ds.Tables[1].Rows[0][7].ToString());
            numOfGolfers = Convert.ToDecimal(ds.Tables[1].Rows[0][5].ToString());

            sendConfirmationEmail("Golf Event", ds.Tables[1].Rows[0][0].ToString(), ds.Tables[1].Rows[0][3].ToString());

        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }

        
    }

    /// <summary>
    /// Confirms the transaction in the database and displays the transaction info
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    private void dinnerConfirmation()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = new DataSet();
        Decimal childCost = 0;
        Decimal adultCost = 0;
        int numOfAdults = 0;
        int numOfChildren = 0;

        try
        {
            ds = bt.confirmDinnerPayment(customerInfo);

            numOfChildren = Convert.ToInt16(ds.Tables[1].Rows[0][5].ToString());
            numOfAdults = Convert.ToInt16(ds.Tables[1].Rows[0][6].ToString());
            childCost = Convert.ToDecimal(bt.getDinnerDetails(eventID).Tables[1].Rows[0][12].ToString());
            adultCost = Convert.ToDecimal(bt.getDinnerDetails(eventID).Tables[1].Rows[0][13].ToString());

            lblticketType1.Text = "Child Dinner Ticket x" + numOfChildren;
            lblprice1.Text = (numOfChildren * childCost).ToString("C");
            lblticketType2.Text = "Adult Dinner Ticket x" + numOfAdults;
            lblprice2.Text = (numOfAdults * adultCost).ToString("C");
            lbltotalCost.Text = ((numOfAdults * adultCost) + (numOfChildren * childCost)).ToString("C");
            
         
            sendConfirmationEmail("Dinner Event", ds.Tables[1].Rows[0][0].ToString(), ds.Tables[1].Rows[0][3].ToString());
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }      
    }


    /// <summary>
    /// Confirms the transaction in the database and displays the transaction info
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    private void runConfirmation()
    {
        BusinessTier bt = new BusinessTier();
        DataSet ds = new DataSet();
        Decimal totalCost = 0;
        int numOfRunners = 0;
        Decimal childCost = 0;
        Decimal adultCost = 0;
        int numOfAdults = 0;
        int numOfChildren = 0;
        string childPrice = bt.getRunDetails(Session["eventID"].ToString()).Tables[1].Rows[0][12].ToString();

        try
        {
            ds = bt.confirmRunPayment(customerInfo);

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                totalCost += Convert.ToDecimal(dr[12].ToString());
                numOfRunners++;
                if (dr["cost"] == childPrice)  //if participant is older than 12, count them as an adult.
                    numOfChildren++;
                else
                    numOfAdults++;    //If participant is younger than 12, count them as a child

                if (dr[7].ToString() != "") //if there is a confirm email
                    sendConfirmationEmail("5k Run Event", dr[1].ToString(), dr[7].ToString());
            }

            lblticketType1.Text = "5k Child Ticket x" + numOfChildren;
            lblprice1.Text = (childCost * numOfChildren).ToString("C");
            lblticketType2.Text = "5k Adult Ticket x" + numOfAdults;
            lblprice2.Text = (adultCost * numOfAdults).ToString("C");
            lbltotalCost.Text = totalCost.ToString("C");

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                if (dr[7].ToString() != "") //if there is a confirm email
                    sendConfirmationEmail("5k Run Event", dr[1].ToString(), dr[7].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    
    }

    /// <summary>
    /// Returns the user to the anythingforafriend.com main web page
    /// </summary>
    /// Creator: Olivia Johnson 11-28-12
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.anythingforafriend.com");
    }

    /// <summary>
    /// Emails the confirmation reciept to the user
    /// </summary>
    /// <param name="eventDescription"> a short description of the type of event</param>
    /// <param name="Participant">The first name of the participant</param>
    /// <param name="emailAddr">The confirmation email</param>
    /// Creator: Olivia Johnson - 11/29/12
    protected void sendConfirmationEmail(String eventDescription, String Participant, String emailAddr)
    {
        String emailEventHelp = "Please show this receipt at the event to ensure a smooth check-in.</p><br/>";
        if (eventType == "Donate")
            emailEventHelp = "";    //if this is a donation confirmation, don't request they bring the confirmation to the event
        MailMessage Msg = new MailMessage();
        MailAddress fromMail = new MailAddress("cs4790.afaf.test@gmail.com");
        // Sender e-mail address.
        Msg.From = fromMail;
        // Recipient e-mail address.
        Msg.To.Add(new MailAddress(emailAddr));
        // Subject of e-mail
        Msg.Subject = "Anything For a Friend Event Confirmation";
        Msg.Body += "<body><hr /><p><strong>This is an automated confirmation message from Anything For a Friend Organization. " +
         "Please do not reply. This is for your records. To contact us, please refer to the contact information below</strong></p>" +
        "<hr /><p>Dear&nbsp;" + Participant + ", <br /><br/> Thank you for your support and generosity. Here is a confirmation of your transaction. " +
         emailEventHelp +
        "<p>Order Date: " + lblOrderDate.Text + "</p>" +
        "<p>Transaction ID: " + transactionID + "</p><br/>" +
    "<h3>Order Summary</h3>" +

    "<table style=\"border: 1px ridge; border-color: #7a1232;\" >" +
        "<tr style=\"font-weight: bold\">" +
           " <td style=\"width:150px\">Item</td>" +
            "<td>Price</td>" +
        "</tr>" +
        "<tr>" +
           " <td>" + lblticketType1.Text + "</td>" +
            "<td>" + lblprice1.Text + "</td>" +
        "</tr>" +
        "<tr>" +
           " <td>" + lblticketType2.Text + "</td>" +
            "<td>" + lblprice2.Text + "</td>" +
       " </tr>" +
       " <tr style=\"font-weight: bold\">" +
            "<td>Total</td>" +
           " <td>" + lbltotalCost.Text + "</td>" +
       " </tr>" +
       " </table><br /> " +
   " <hr />" +
   " <p><strong>You can contact us at -email- or by calling 222-222-2222. Remember to check <a href=\"http://Anythingforafriend.com\" style=\"color:#7a1232\">www.AnythingForAFriend.com</a>" +
       " for more opportunities to get involved and donate." +
      " </strong></p>" +
   " <hr />" +
"</body>";

        Msg.IsBodyHtml = true;

        SmtpClient a = new SmtpClient();
        a.Host = "smtp.gmail.com";   // We use gmail as our smtp client
        a.Port = 587;
        a.UseDefaultCredentials = true;
        a.Credentials = new System.Net.NetworkCredential("afaftest4790@gmail.com", "demodemo");  //this is a real address        
        a.EnableSsl = true;
        a.Send(Msg);
    }
}