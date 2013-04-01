<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true"
    CodeFile="DinnerRegistration.aspx.cs" Inherits="_DinnerRegistration" %>

<%@ Register Src="Controls/SocialMediaSharingControl.ascx" TagName="SocialMediaSharingControl" TagPrefix="uc1" %>
<%@ Register Src="Controls/GoogleMap.ascx" TagName="GoogleMap" TagPrefix="uc2" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="Server">
    <title>Anything For A Friend :: Dinner Registration</title>
    <script src="Controls/SocialMediaSharingScript.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" runat="Server">
    <div id="title">
        <p>Dinner Registration</p>
    </div>

    <!-- Dinner Registration Page  -->
    <!-- AC: Aaron Copeland - 11/19/2012 -->
    <!-- Tested by Aaron Copeland - 11/28/2012 -->
    <!-- Start of Page Content -->
    <div id="column-wrapper">

        <!-- Left Column Content -->
        <div id="left-col">

            <!-- Event Info -->
            <table id="event">
                <tr>
                    <td id="recipient-photo" rowspan="2">
                        <asp:Image ID="recipientImage" runat="server" />
                    </td>
                    <td id="event-info">
                        <asp:Label ID="lblEventTitle" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblEventDate" runat="server"></asp:Label><br />
                        <asp:Label ID="lblRegistrationClosingDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="description">
                        <asp:Label ID="lblEventDescription" runat="server"></asp:Label><br /><br />
                        <asp:Label ID="lblEventDetails" runat="server"></asp:Label><br /><br />
                        <span style="color: #FF0000">
                            <asp:Label ID="lblAdult" runat="server" Text="Adult ticket price: $"></asp:Label>
                            <asp:Label ID="lblAdultPrice" runat="server"></asp:Label><br />
                            <asp:Label ID="lblChild" runat="server" Text="Child ticket price: $"></asp:Label>
                            <asp:Label ID="lblChildPrice" runat="server"></asp:Label>
                        </span>
                    </td>
                </tr>
            </table>

            <!-- Event Form -->
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
                <tr id="adult-count">
                    <td class="registration-col1">
                        <asp:Label ID="lblNumAdults" runat="server" Text="Number of adults"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtNumAdults" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="child-count">
                    <td class="registration-col1">
                        <asp:Label ID="lblNumChildren" runat="server" Text="Number of children"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtNumChildren" runat="server" MaxLength="3" Width="25px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="error">
                    <td class="registration-col1">&nbsp;</td>
                    <td class="registration-col2">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="registration-col1">
                        &nbsp;
                    </td>
                    <td id="checkout" class="registration-col2">
                        <asp:Button ID="btnCheckout" runat="server" class="buttonMain" Text="" OnClick="btnCheckout_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <!-- Right Column Content -->
        <div id="right-col">
            <div id="event-location">                
                <asp:Label ID="lblEventLocation" runat="server"></asp:Label>
            </div>
            <div id="map">
                <uc2:GoogleMap ID="GoogleMap" runat="server" />
            </div>
            <div id="share">
                <h3>Share with your friends!</h3>
                <uc1:SocialMediaSharingControl ID="SocialMediaSharingControl" runat="server" />
            </div>
            <div id="participant-limit">
                <asp:Label ID="lblMaxParticipants" runat="server"></asp:Label><br />
                <asp:Label ID="lblSpotsLeft" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Event Contacts Content -->
    <div id="event-footer">
        <table id="event-footer-table">
            <tr>
                <td class="right-align">
                    <asp:Label ID="lblRepName" runat="server" Text="EVENT REP: "></asp:Label>
                </td>
                <td class="left-align">
                    <asp:Label ID="lblRepNameValue" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="right-align">
                    <asp:Label ID="lblRepEmail" runat="server" Text="EMAIL: "></asp:Label>
                </td>
                <td class="left-align">
                    <asp:Label ID="lblRepEmailValue" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="right-align">
                    <asp:Label ID="lblRepPhone" runat="server" Text="PHONE: "></asp:Label>
                </td>
                <td class="left-align">
                    <asp:Label ID="lblRepPhoneValue" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
