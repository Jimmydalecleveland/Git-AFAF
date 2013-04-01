<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="DonationRegistration.aspx.cs" Inherits="_DonationRegistration" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" Runat="Server">
    <title>Anything For A Friend :: Make A Donation</title>
</asp:Content>
<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" Runat="Server">
    <div id="title">
        <p>Make A Donation</p>
    </div>

    <asp:HiddenField ID="hdnReciepantsID" runat="server" />
    <!-- Donation Page  -->
    <!-- AC: Aaron Copeland - 11/27/2012 -->
    <!-- Tested by Aaron Copeland - 11/28/2012 -->
    <!-- Start of Page Content -->
    <div id="column-wrapper" style="padding-top: 19px;">
        <table id="event">
            <tr>
                <td id="recipient-photo">
                    <asp:Image ID="recipientImage" runat="server" />
                </td>
                <td id="description" >
                    <asp:Label ID="lblEventTitle" runat="server"></asp:Label><br /><br />
                    <asp:Label ID="lblEventDescription" runat="server"></asp:Label><br /><br />
                    <asp:Label ID="lblEventDetails" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="form-info">
            <tr id="first-name">
                <td class="registration-col1">
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="last-name">
                <td class="registration-col1">
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="suffix">
                <td class="registration-col1">
                    <asp:Label ID="lblSuffix" runat="server" Text="Suffix"></asp:Label>
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </td>
                <td class="registration-col2">
                    <asp:TextBox ID="txtSuffix" runat="server" Width="40px"></asp:TextBox>
                </td>
            </tr>
            <tr id="anonymous">
                <td class="registration-col1">
                    &nbsp;
                </td>
                <td class="registration-col2">
                    <asp:CheckBox ID="chkAnonymous" runat="server" Text="Anonymous"></asp:CheckBox><br />
                    <asp:Label CssClass="whatsThis"  runat="server" Text="What's This?" ToolTip="Your donation will be made anonymously to Anything for a Friend"></asp:Label>                                        
                </td>
            </tr>
            <tr id="phone">
                <td class="registration-col1">
                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    (
                    <asp:TextBox ID="txtAreaCode" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    )
                    <asp:TextBox ID="txtPrefix" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    -
                    <asp:TextBox ID="txtLineNumber" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                </td>
            </tr>
            <tr id="email">
                <td class="registration-col1">
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr id="confirm-email">
                <td class="registration-col1">
                    <asp:Label ID="lblConfirmEmail" runat="server" Text="Confirm Email"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    <asp:TextBox ID="txtConfirmEmail" runat="server"></asp:TextBox>
                </td>
            </tr>        
            <tr id="donation-amount">
                <td class="registration-col1">
                    <asp:Label ID="lblCashValue" runat="server" Text="Cash Amount"></asp:Label>
                    <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                </td>
                <td class="registration-col2">
                    $&nbsp;<asp:TextBox ID="txtCashValue" runat="server"></asp:TextBox>
                </td>
            </tr>        
            <tr id="error">
                <td class="registration-col1">&nbsp;</td>
                <td class="registration-col2">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="checkout">
                <td class="registration-col1">
                    &nbsp;
                </td>
                <td class="registration-col2">
                    <asp:Button ID="btnCheckout" class="buttonMain" runat="server" Text="" OnClick="btnCheckout_Click" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

