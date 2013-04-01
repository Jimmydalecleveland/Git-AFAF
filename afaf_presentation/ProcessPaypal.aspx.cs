using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessPaypal : System.Web.UI.Page
{
    /// <summary>
    /// Executes on load and creates a form to send to paypal.
    /// The page is auto redirected via javascript
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>  
    /// 11/28/12 Nathan Schroader
    protected void Page_Load(object sender, EventArgs e)
    {
        //Assign paypal link to the form
        Form.Attributes.Add("action", "https://www.sandbox.paypal.com/cgi-bin/webscr");
        assemblePaypal();
    }

    /// <summary>
    /// Assembles a cart input form to be sent to Paypal
    /// </summary>
    /// Creator: Nathan Schroader 11/28/12
    private void assemblePaypal()
    {
        try
        {
            String sellerEmail = "Seller_1354037983_biz@gmail.com";
            String returnAddress = "http://anythingforafriend.org/EventRegistration/Confirm.aspx";
            String eventID = Session["eventID"].ToString();
            String transactionID = Session["transactionID"].ToString();
            String cost = Session["cost"].ToString();
            String eventType = Session["eventType"].ToString();
            Table checkoutTable = (Table)Session["checkoutTable"];


            Literal cmd = new Literal();
            cmd.Text = "<input type='hidden' name='cmd' value='_cart'>";
            phPaypalCart.Controls.Add(cmd);

            Literal upload = new Literal();
            upload.Text = "<input type='hidden' name='upload' value='1'>";
            phPaypalCart.Controls.Add(upload);

            Literal business = new Literal();
            business.Text = "<input type='hidden' name='business' value='" + sellerEmail + "'>";
            phPaypalCart.Controls.Add(business);

            Literal returnValue = new Literal();
            returnValue.Text = "<input type='hidden' name='return'" +
                " value='" + returnAddress + "'>";
            phPaypalCart.Controls.Add(returnValue);

            for (int i = 1; i < checkoutTable.Rows.Count - 1; i++)
            {
                Literal item = new Literal();
                item.Text = "<input type='hidden' name='item_name_" + i + "' value='" + checkoutTable.Rows[i].Cells[0].Text.ToString() + "'>";
                phPaypalCart.Controls.Add(item);

                String itemAmount = checkoutTable.Rows[i].Cells[2].Text.ToString();
                itemAmount.Replace("$", "");

                Literal amount = new Literal();
                amount.Text = "<input type='hidden' name='amount_" + i + "' value='" + itemAmount + "'>";
                phPaypalCart.Controls.Add(amount);


                String itemQuanity = checkoutTable.Rows[i].Cells[1].Text.ToString();
                itemQuanity = itemQuanity.Replace("(", "");
                itemQuanity = itemQuanity.Replace(")", "");

                Literal quanity = new Literal();
                quanity.Text = "<input type='hidden' name='quantity_" + i + "' value='" + itemQuanity + "'>";
                phPaypalCart.Controls.Add(quanity);
            }

            Literal eventNumber = new Literal();
            eventNumber.Text = "<input type='hidden' name='custom' value='" + eventID + "_" + transactionID + "'>";
            phPaypalCart.Controls.Add(eventNumber);
        }
        catch (Exception ex)
        {
            ErrorLog.logError(ex);
            Response.Redirect("Oops.aspx");
        }
    }
}