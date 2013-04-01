<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true"
    CodeFile="RunRegistration.aspx.cs" Inherits="_RunRegistration" %>

<%@ Register Src="~/Controls/SocialMediaSharingControl.ascx" TagPrefix="uc1" TagName="SocialMediaSharingControl" %>
<%@ Register Src="Controls/GoogleMap.ascx" TagName="GoogleMap" TagPrefix="uc2" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="Server">
    <title>Anything For A Friend :: Run Registration</title>
    <link href="ParticipantsBoxStyles.css" rel="stylesheet" type="text/css" />
    <link href="jquery.mCustomScrollbar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Controls/SocialMediaSharingScript.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <script type="text/javascript" src="jquery.mousewheel.min.js"></script>
    <script type="text/javascript" src="jquery.mCustomScrollbar.js"></script>
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                $(".participantsList").mCustomScrollbar();   // Applies the scroll bars to the elements specified
            });
        })(jQuery);
    </script>
</asp:Content>
<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" runat="Server">
    <div id="title">
        <p>
            Run Registration</p>
    </div>
    <!-- Start of Page Content -->
    <div id="column-wrapper">
        <!-- Left Column Content -->
        <div id="left-col">
            <!-- Event Info -->
            <table id="event">
                <asp:HiddenField ID="hdnEventId" runat="server" />
                <asp:HiddenField ID="hdnRecipientId" runat="server" />
                <asp:HiddenField ID="hdnNameId" runat="server" Value="-1" />
                <asp:HiddenField ID="hdnEditingRunner" runat="server" Value="no" />
                <tr>
                    <td rowspan="3" id="recipient-photo">
                        <asp:Image ID="imgRecipientPhoto" runat="server" />
                    </td>
                    <td id="event-info">
                        <asp:Label ID="lblEventTitle" runat="server" Text="lblEventTitle"></asp:Label><br />
                        <asp:Label ID="lblEventDate" runat="server" Text="lblEventDate"></asp:Label>
                        <asp:Label ID="lblEventTime" runat="server" Text="lblEventTime"></asp:Label><br />
                        <asp:Label ID="lblRegistrationClosingDate" runat="server"></asp:Label><br />
                        <asp:Label ID="lblEventDetails" runat="server" Text="lblEventDetails"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td id="description">
                        <asp:Label ID="lblEventDescription" runat="server" Text="lblEventDescription"></asp:Label> <br /> <br />
                        <span style="color: #FF0000">
                            <asp:Label ID="lblAdult" runat="server" Text="Adult ticket price: $"></asp:Label>
                            <asp:Label ID="lblAdultPrice" runat="server"></asp:Label><br />
                            <asp:Label ID="lblChild" runat="server" Text="Child ticket price: $"></asp:Label>
                            <asp:Label ID="lblChildPrice" runat="server"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td id="running-total">
                        Your runners so far:
                        <div id="divRunnersSoFar" runat="server">
                        </div>
                        <br />
                        Total:
                        <asp:Label ID="lblTotalCost" runat="server" Text="$0"></asp:Label>
                        (click a name to edit)<br />
                        <br />
                        <asp:Label ID="lblFormSuccess" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <!-- Event Form -->
            <table id="form-info">
                <tr id="first-name">
                    <td class="registration-col1">
                        <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="last-name">
                    <td class="registration-col1">
                        <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="Tr1">
                    <td class="registration-col1">
                        <asp:Label ID="Label13" runat="server" Text="Suffix"></asp:Label>
                        <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtNameSuffix" runat="server" Width="40px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="anonymous">
                    <td class="registration-col1">
                        &nbsp;
                    </td>
                    <td class="registration-col2">
                        <asp:CheckBox ID="chkAnonymous" runat="server" />
                        <asp:Label runat="server" Text="Anonymous"></asp:Label><br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label CssClass="whatsThis" runat="server" Text="What's This?" ToolTip="Your donation will be made anonymously to Anything for a Friend. The information you provide will be used for accounting purposes only."></asp:Label>
                    </td>
                </tr>
                <tr id="phone">
                    <td class="registration-col1">
                        <asp:Label ID="Label4" runat="server" Text="Phone"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        (&nbsp<asp:TextBox ID="txtPhoneAreaCode" runat="server" MaxLength="3" Width="25px"></asp:TextBox>&nbsp)
                        <asp:TextBox ID="txtPhonePrefix" runat="server" MaxLength="3" Width="25px"></asp:TextBox>-
                        <asp:TextBox ID="txtPhoneSuffix" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="email">
                    <td class="registration-col1">
                        <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="confirm-email">
                    <td class="registration-col1">
                        <asp:Label ID="Label6" runat="server" Text="Confirm Email"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtConfirmEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="shirt-size">
                    <td class="registration-col1">
                        <asp:Label ID="Label7" runat="server" Text="T-Shirt Size"></asp:Label>
                    </td>
                    <td class="registration-col2">
                        <asp:DropDownList ID="ddlTshirtSizes" runat="server">
                            <asp:ListItem Value="N/A"></asp:ListItem>
                            <asp:ListItem Value="S"></asp:ListItem>
                            <asp:ListItem Value="M"></asp:ListItem>
                            <asp:ListItem Value="L"></asp:ListItem>
                            <asp:ListItem Value="XL"></asp:ListItem>
                            <asp:ListItem Value="XXL"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="ticket-type">
                    <td class="registration-col1">
                        <asp:Label ID="Label8" runat="server" Text="Ticket Type"></asp:Label>
                    </td>
                    <td class="registration-col2">
                        <asp:DropDownList ID="ddlTicketTypes" runat="server">
                            <asp:ListItem Text="Adult"></asp:ListItem>
                            <asp:ListItem Text="Child"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="emergency-contact-name">
                    <td class="registration-col1">
                        <asp:Label ID="Label9" runat="server" Text="Emergency Contact"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        <asp:TextBox ID="txtEmergContactName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="emergency-contact-phone">
                    <td class="registration-col1">
                        <asp:Label ID="Label10" runat="server" Text="Contact's Phone"></asp:Label>
                        <span style="color: #FF0000">&nbsp;*&nbsp;</span>
                    </td>
                    <td class="registration-col2">
                        (&nbsp<asp:TextBox ID="txtEmergContactPhoneAreaCode" runat="server" MaxLength="3"
                            Width="25px"></asp:TextBox>&nbsp)
                        <asp:TextBox ID="txtEmergContactPhonePrefix" runat="server" MaxLength="3" Width="25px"></asp:TextBox>-
                        <asp:TextBox ID="txtEmergContactPhoneSuffix" runat="server" MaxLength="4" Width="30px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="error">
                    <td class="registration-col1">
                        &nbsp;
                    </td>
                    <td class="registration-col2">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="add-runner">
                    <td class="registration-col1">
                        &nbsp;
                    </td>
                    <td class="registration-col2">
                        <asp:Button ID="btnAddAnotherRunner" class="buttonMain" runat="server" Text="" OnClick="btnAddAnotherRunner_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="registration-col1">
                        &nbsp;
                    </td>
                    <td id="checkout" class="registration-col2">
                        <asp:Button ID="btnCheckout" runat="server" class="buttonMain" Text="" OnClick="btnProceedToCheckout_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- Right Column Content -->
        <div id="right-col">
            <div id="event-location">
                <asp:Label ID="lblEventAddress" runat="server" Text="lblEventAddress"></asp:Label>
            </div>
            <div id="map">
                <uc2:GoogleMap ID="GoogleMap" runat="server" />
            </div>
            <div id="share">
                <h3>
                    Share with your friends!</h3>
                <uc1:SocialMediaSharingControl runat="server" ID="SocialMediaSharingControl" />
            </div>
            <div id="participants">
                <div class="bubble">
                    <div style="width: 50px">
                        <asp:Label ID="lblNumOfRunners" runat="server" Text=""></asp:Label></div>
                </div>
                <h2 class="center-align">
                    Participants</h2>
                <div id="participantsList" class="participantsList" runat="server">
                </div>
            </div>
        </div>
        <!-- Event Contacts Content -->
        <div id="event-footer">
            <table id="event-footer-table">
                <tr>
                    <td class="right-align">
                        <asp:Label ID="Label11" runat="server" Text="EVENT REP:"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblEventRep" runat="server" Text="lblEventRep"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="right-align">
                        <asp:Label ID="Label12" runat="server" Text="EMAIL:"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblEventRepEmail" runat="server" Text="lblEventRepEmail"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="right-align">
                        <asp:Label ID="Label14" runat="server" Text="PHONE:"></asp:Label>
                    </td>
                    <td class="left-align">
                        <asp:Label ID="lblEventRepPhone" runat="server" Text="lblEventRepPhone"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
