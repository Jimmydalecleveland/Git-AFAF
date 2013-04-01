<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Checkout.aspx.cs" Inherits="_Checkout" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="Server">
    <title>Anything For A Friend :: Checkout</title>
    <script language="javascript" type="text/javascript">
        var count = '<%= PostBackCount %>';
</script>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" runat="Server">

    <div id="title">
        <p><asp:Label ID="lblEventType" Text="" runat="server" /></p>
    </div>

    <div id="registration-header">
        <asp:Table ID="tblCartItems" runat="server">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="colTicType"  cssclass="checkout-col1" runat="server">Ticket Type</asp:TableCell>
                <asp:TableCell ID="colQuantity"  cssclass="checkout-col2" runat="server">&nbsp;</asp:TableCell>
                <asp:TableCell ID="colPrice" cssclass="checkout-col3" runat="server">Price</asp:TableCell>
                <asp:TableCell ID="colSubTotal" cssclass="checkout-col4" runat="server">SubTotal</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div id="liabilitySection" runat="server">
        <div id="disclaimer-header">
            Liability Disclaimer
        </div>
        <div id="disclaimer">
            <p>
                I understand that my consent to these provisions is given in consideration of the
            acceptance of this registration and for being permitted to participate in this event.
            I am a voluntary participant in this event, and am in good physical condition. I
            know that this event is a potentially hazardous activity and I hereby assume full
            and complete responsibility for any injury or accident which may occur during my
            participation in this event or while on the premises of this event, and I hereby
            release and hold harmless and covenant not to file suit against Anything for a Friend
            and any affiliated organizations/individuals and Walk sponsors and their agents
            and employees, and all other persons or entities associated with this event (the
            “releasees”) from any loss, liability or claims I may have arising out of my participation
            in this event, including personal injury or damage suffered by me or others, whether
            same be caused by falls, contact with participants, conditions of the course, negligence
            of the releasees or otherwise. If I do not follow all of the rules of this event,
            I understand that I may be removed from participation. I give my full permission
            to Anything for a Friend and its sponsors to use my photographs, videotapes or other
            recordings of me that are made during the course of this event.
            </p>
        </div>
        <div id="disclaimer-agreement">
            <asp:CheckBox ID="chkLiability" runat="server" AutoPostBack="True"  Text="I have read and agree to the terms above" />
        </div>
    </div>

    <div id="buttons">
        <asp:Button ID="btnBack" Text="" cssclass="buttonMain" runat="server"  OnClientClick="JavaScript: window.history.go(-count); return false;"/>
        <asp:Button ID="btnPayPal" runat="server" Text="" OnClick ="btnPayPal_Click"/>
    </div>

    </asp:Content>

