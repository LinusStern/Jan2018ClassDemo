using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class AlbumDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only run on first page load
            if (!Page.IsPostBack)
            {
                // Obtain the parameter value held in the page url
                string albumID = Request.QueryString["aid"];

                // Process paramter value
                if (string.IsNullOrEmpty(albumID))
                {
                    Response.Redirect("ODSQuery.aspx");
                }
                else
                {
                    AlbumID.Text = albumID;
                }
            }
        }
    }
}