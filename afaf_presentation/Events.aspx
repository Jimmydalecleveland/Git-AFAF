<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="Events.aspx.cs" Inherits="_Events" %>
<%@ Register TagPrefix="uc" TagName="EventHolder" 
    Src="~/Controls/EventHolder.ascx" %>  
<asp:Content ID="contentHead" ContentPlaceHolderID="head" Runat="Server">
    <title>Anything For A Friend :: Events</title>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" Runat="Server">
    <h1 class="dark-red">Event Listings</h1>
    <div id="events" runat="server">

    </div>
</asp:Content>

