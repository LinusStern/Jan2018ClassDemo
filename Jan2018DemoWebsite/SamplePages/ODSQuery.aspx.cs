using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ODSQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Albums_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1: Access row selected with the "View" command button from the grid view
            GridViewRow gridViewRow = Albums.Rows[Albums.SelectedIndex];

            // 2: Extract the data from the grid view (template control) as a string
            string albumID = (gridViewRow.FindControl("AlbumID_Label") as Label).Text;

            // Send the extracted data to another page
            // ? denotes a following parameter (in plaintext format)
            // = dentoes the parameter value
            // & seperates multiple parameters
            Response.Redirect("AlbumDetails.aspx?aid=" + albumID);
        }
    }
}