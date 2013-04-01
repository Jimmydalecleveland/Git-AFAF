<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SocialMediaSharingControl.ascx.cs" Inherits="SocialMediaSharingControl" %>

<div id="fb-root"></div>

<table>
    <tr>
        <td class="socialMediaTableCol1">
            <!--  this is the code for the facebook like button on the page -->
            <div style="height:21px;" id="fb" class="fb-like" data-href="http://www.anythingforafriend.com" 
                data-send="false" data-layout="button_count" data-width="90" data-show-faces="true"></div>
            <!-- Javascript code to genterate a dynamic URL for facebook -->
            <script type="text/javascript">
                var sUrl = window.location;
                document.getElementById('fb').setAttribute('href', sUrl);
            </script>
        </td>
        <td class="socialMediaTableCol2" >
            <!--  this is the code for the twitter button on the page -->	
            <a href="https://twitter.com/share" class="twitter-share-button">Tweet</a>
        </td>
        <td class="socialMediaTableCol3">
            <!--  this is the code for the google+ button on the page -->
            <div class="g-plusone" data-size="medium"></div>
        </td>   
    </tr>
</table>


