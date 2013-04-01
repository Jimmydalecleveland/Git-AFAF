<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="GolfRegistration.aspx.cs" Inherits="_GolfRegistration" %>

<%@ Register src="Controls/SocialMediaSharingControl.ascx" tagname="SocialMediaSharingControl" tagprefix="uc1" %>
<%@ Register src="Controls/GoogleMap.ascx" tagname="GoogleMap" tagprefix="uc2" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" Runat="Server">
    <title>Anything For A Friend :: Golf Registration</title>
    <script src="Controls/SocialMediaSharingScript.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" Runat="Server">
    <div id="title">
        <p>Golf Registration</p>
    </div>

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
                        <asp:Label ID="lblEventTitle" runat="server"></asp:Label><br />
                        <asp:Label ID="lblEventTime" runat="server"></asp:Label><br />
                        <asp:Label ID="lblRegistrationClosingDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="description">
                        <asp:Label ID="lblDescription" runat="server"></asp:Label><br /><br />
                        <asp:Label ID="lblEventDetails" runat="server"></asp:Label>
                    </td>                
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td id="admission" colspan="2"><asp:Label ID="lblAdmission" runat="server"></asp:Label></td>
                </tr>
            </table>

            <!-- Event Form -->
            <table id="form-info">
                <tr id="first-name">
                    <td class="registration-col1">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="last-name">
                    <td class="registration-col1">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="suffix">
                    <td class="registration-col1">
                        <asp:Label ID="lblSuffix" runat="server" Text="Suffix"></asp:Label>
                        <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtSuffix" runat="server" Width="40px"></asp:TextBox></td>
                </tr>
                <tr id="phone">
                    <td class="registration-col1">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">( <asp:TextBox ID="txtAreaCode" runat="server" 
                            MaxLength="3" Width="25px"></asp:TextBox> ) 
                          <asp:TextBox ID="txtPrefix" runat="server" MaxLength="3" Width="25px"></asp:TextBox> - 
                          <asp:TextBox ID="txtLastFour" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="email">
                    <td class="registration-col1">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="confirm-email">
                    <td class="registration-col1">
                        <asp:Label ID="lblConfirmEmail" runat="server" Text="Confirm Email"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtConfirmEmail" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="party-count">
                    <td class="registration-col1">
                        <asp:Label ID="lblNumInParty" runat="server" Text="Number In Party"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2"><asp:TextBox ID="txtNumInParty" runat="server" 
                            MaxLength="3" Width="25px"></asp:TextBox></td>
                </tr>
                <tr id="error">
                    <td class="registration-col1">&nbsp;</td>
                    <td class="registration-col2">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="registration-col1">&nbsp;</td>
                    <td id="checkout" class="registration-col2">    
                        <asp:Button ID="btnCheckout" class="buttonMain" runat="server" Text="" 
                            onclick="btnCheckout_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <!-- Right Column Content -->
        <div id="right-col">
            <div id="event-location">
                <asp:Label ID="lblLocation" runat="server"></asp:Label>
            </div>
            <div id="map">
                <uc2:GoogleMap ID="GoogleMap" runat="server" />
            </div>
            <div id="share">
                <h3>Share with your friends!</h3>
                <uc1:SocialMediaSharingControl ID="SocialMediaSharingControl" runat="server" />
            </div>
            <div id="remaining">
                <asp:Label ID="lblMaxParticipants" runat="server"></asp:Label><br />
                <asp:Label ID="lblSpotsLeft" runat="server"></asp:Label>
            </div>
        </div>

        <!-- Event Contacts Content -->
        <div id="event-footer">
            <table id="event-footer-table">
                <tr>
                    <td class="right-align">
                        <asp:Label ID="lblRepName" runat="server" Text="EVENT REP :"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblRepNameDisplay" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="right-align">
                        <asp:Label ID="lblRepEmail" runat="server" Text="EMAIL :"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblRepEmailDisplay" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="right-align">
                        <asp:Label ID="lblRepPhone" runat="server" Text="PHONE :"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblRepPhoneDisplay" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

