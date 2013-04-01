using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_GoogleMap : System.Web.UI.UserControl
{
    /// <summary>
    /// Holds the Address that the map is to show.
    /// </summary>
    /// Andrew Heim - 11/28/2012
    public string address;

    /// <summary>
    /// Loads the address into the google map widget
    /// </summary>
    /// <param name="address">The address to load</param>
    /// Andrew Heim - 11/28/2012
    public void LoadAddress(string address)
    {
        this.address = address;
    }
}