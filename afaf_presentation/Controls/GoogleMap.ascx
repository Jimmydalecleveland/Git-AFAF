<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleMap.ascx.cs" Inherits="Controls_GoogleMap" %>

<script src="//maps.google.com/maps?file=api&amp;v=2.x&amp;key=AIzaSyA__yp10_PjC6KruUPlUT7CJnfBRTQV7I0" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    var map = null;
    var geocoder = null;
    var address = '<%= address %>';

    function initialize() {
        if (GBrowserIsCompatible()) {
            map = new GMap2(document.getElementById("map_canvas"));
            geocoder = new GClientGeocoder();
            showAddress(address);
        }
    }

    function showAddress(address) {
        if (geocoder) {
            geocoder.getLatLng(
          address,
          function (point) {
              if (!point) {
                  alert(address + " not found");
              } else {
                  map.setCenter(point, 13);
                  var marker = new GMarker(point);
                  map.addOverlay(marker);
                  map.setUIToDefault();
                  // As this is user-generated content, we display it as
                  // text rather than HTML to reduce XSS vulnerabilities.
                  //marker.openInfoWindow(document.createTextNode(address));
              }
          }
        );
        }
  }

    function addLoadEventHandler(func) {
        var previous_handler = window.onload;
        if (typeof window.onload != "function") window.onload = func;
        else window.onload = function () {
            previous_handler();
            func();
        }
    }

    //add this control's onLoad handler to the document's onload, without
    //replacing whatever handler may be there already
    addLoadEventHandler(initialize);
 
</script>
<div id="map_canvas"></div>
