using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PresentationHelpers
/// </summary>
public class PresentationHelpers
{
    /// <summary>
    /// Focuses the page on a certain control. Got code from http://www.codeproject.com/Articles/9731/How-to-scroll-an-ASP-NET-control-into-view-after-p
    /// </summary>
    /// <param name="ClientID">The ID given to the control</param>
    /// <param name="page">The page the control is on.</param>
    /// Andrew Heim  - 12/3/12
    public static void FocusControlOnPageLoad(string ClientID,
                                            System.Web.UI.Page page)
        {

            page.RegisterClientScriptBlock("CtrlFocus",

            @"<script> 

      function ScrollView()

      {
         var el = document.getElementById('" + ClientID + @"')
         if (el != null)
         {        
            el.scrollIntoView();
            el.focus();
         }
      }

      window.onload = ScrollView;

      </script>");

        }
}