<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="_Default" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" Runat="Server">
    <title>Anything For A Friend :: Confirm</title>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" Runat="Server">
    <div id="receipt-wrapper">
        <div id="receipt-header">
            <h1 class="center-align">Thank You!</h1>
            <hr />
            <p class="center-align">A copy of this receipt has been sent to your email address.</p>
        </div>
        <div id="transaction-info">
            <table>
                <tr><td>Transaction ID:</td><td><asp:Label runat="server" ID="lblTransaction" /></td></tr>
                <tr><td>Order Date:</td><td><asp:Label runat="server" ID="lblOrderDate" /></td></tr>
            </table>
        </div>
        <div id="order-summary">
            <p>Order Summary:</p><br />
            <div id="receipt-items-wrapper">
                <hr />
                <table id="receipt-items">
                    <!-- Each item should be added to the innerHTML of the table 
                    'receipt-items' following a format similar to these ones -->
                    <!-- Possible alternatives could be a gridview to handle this -->
                    <tr>
                        <td class="item-name"><asp:Label ID ="lblticketType1" runat="server"></asp:Label></td>
                        <td class="item-price"><asp:Label ID ="lblprice1" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="item-name"> <asp:Label ID ="lblticketType2" runat="server"></asp:Label></td>
                        <td class="item-price"> <asp:Label ID ="lblprice2" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td class="item-name right-align"><strong>Total: </strong></td>
                        <!-- Total the items and put them as the price here (possibly use label) -->
                        <td class="item-price"><asp:Label ID ="lbltotalCost" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="printable-copy">
            <p>You may want to print a copy of this receipt for your records.</p>
        </div>
        <div id="receipt-footer">
            <p>If you have any questions or comments please contact us with your transaction ID.</p>
            <br />
            <p>someemailaddress@mail.com</p>
            <p>555-555-5555
                </p>
            <p id="receipt-finish" class="center-align">
                <!--<asp:ImageButton ID="imgBtnFinish" runat="server" PostBackUrl="http://www.anythingforafriend.com"
                    ImageUrl="~/images/button.png" />-->
                <asp:Button ID="btnFinish" runat="server" Text="" onclick="btnFinish_Click"/>
            </p>
        </div>
        <div></div>
    </div>
</asp:Content>

