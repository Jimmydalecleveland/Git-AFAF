<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventHolder.ascx.cs" Inherits="Controls_EventHolder" %>
<div id="event-holder">
    <asp:HiddenField ID="hdnEventId" runat="server" />
    <asp:HiddenField ID="hdnEventType" runat="server" />
    <div id ="recipient-photo">
        <asp:Image ID="imgRecipientPhoto" runat="server" />
    </div>
    <div id ="event-info">
        <div id ="event-name">
            <asp:Label ID="lblEventTitle" runat="server" Text="lblEventTitle" />
        </div>
        <div id ="event-description">
            <asp:Label ID="lblEventDetails" runat="server" Text="lblEventDetails" />
        </div>
    </div>
    <div id ="event-info2">
        <div id ="event-time">
            <asp:Label ID="lblEventTime" runat="server" Text="lblEventTime" />
        </div>
        <div id ="sign-up">
            <asp:Button ID="btnSignup" runat="server" class="buttonMain" Text="" OnClick="btnSignUp_Click" />
        </div>
    </div>
</div>