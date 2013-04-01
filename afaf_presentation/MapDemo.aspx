<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="MapDemo.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="googleMap" TagName="menu" Src="~/Controls/GoogleMap.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content_main" Runat="Server">
    <googleMap:menu runat="server" ID="map" />
</asp:Content>

