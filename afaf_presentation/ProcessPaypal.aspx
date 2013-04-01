<%@ Page Title="" Language="C#" MasterPageFile="~/EventPage.master" AutoEventWireup="true" CodeFile="ProcessPaypal.aspx.cs" Inherits="ProcessPaypal" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" Runat="Server">
    <title>Anything For A Friend :: Process Paypal</title>
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="content_main" Runat="Server">
    <div style="position:absolute; z-index:-1; visibility:hidden;">
        <asp:PlaceHolder ID="phPaypalCart"  runat="server"></asp:PlaceHolder>
    </div>
    <div style="text-align: center; margin-top: 100px;"><h2>Sending Order to Paypal<h2></div>
    <div id="loaderImage"></div>
    <div>
    </div>
    <script type="text/javascript">
        var cSpeed = 12;
        var cWidth = 128;
        var cHeight = 128;
        var cTotalFrames = 8;
        var cFrameWidth = 128;
        var cImageSrc = 'images/sprites.png';

        var cImageTimeout = false;
        var cIndex = 0;
        var cXpos = 0;
        var SECONDS_BETWEEN_FRAMES = 0;

        function startAnimation() {

            document.getElementById('loaderImage').style.backgroundImage = 'url(' + cImageSrc + ')';
            document.getElementById('loaderImage').style.width = cWidth + 'px';
            document.getElementById('loaderImage').style.height = cHeight + 'px';

            //FPS = Math.round(100/(maxSpeed+2-speed));
            FPS = Math.round(100 / cSpeed);
            SECONDS_BETWEEN_FRAMES = 1 / FPS;

            setTimeout('continueAnimation()', SECONDS_BETWEEN_FRAMES / 1000);

        }

        function continueAnimation() {

            cXpos += cFrameWidth;
            //increase the index so we know which frame of our animation we are currently on
            cIndex += 1;

            //if our cIndex is higher than our total number of frames, we're at the end and should restart
            if (cIndex >= cTotalFrames) {
                cXpos = 0;
                cIndex = 0;
            }

            document.getElementById('loaderImage').style.backgroundPosition = (-cXpos) + 'px 0';

            setTimeout('continueAnimation()', SECONDS_BETWEEN_FRAMES * 1000);
        }

        function imageLoader(s, fun)//Pre-loads the sprites image
        {
            clearTimeout(cImageTimeout);
            cImageTimeout = 0;
            genImage = new Image();
            genImage.onload = function () { cImageTimeout = setTimeout(fun, 0) };
            genImage.onerror = new Function('alert(\'Could not load the image\')');
            genImage.src = s;
        }

        //The following code starts the animation
        new imageLoader(cImageSrc, 'startAnimation()');

        window.onload = function () {
            document.getElementById('frmMain').submit();
        };
    </script>
</asp:Content>

