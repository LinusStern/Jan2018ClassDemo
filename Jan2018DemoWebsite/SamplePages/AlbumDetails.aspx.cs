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

        protected void AlbumTracks_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // e parameter contains the value that was attached to the link (button) on the list row
            // Value must be converted to a string
            CommandArgID.Text = e.CommandArgument.ToString();

            // Extract value from a column on the selected list row (e.Item)
            ColumnID.Text = (e.Item.FindControl("TrackIDLabel") as Label).Text;
        }

        protected void Totals_Click(object sender, EventArgs e)
        {
            double time = 0;
            double size = 0;

            foreach (ListViewItem item in AlbumTracks.Items)
            {
                time += double.Parse(
                    (item.FindControl("MillisecondsLabel") as Label)
                    .Text);

                size += double.Parse(
                    (item.FindControl("BytesLabel") as Label)
                    .Text);

                TracksTime.Text = time.ToString();
                TracksSize.Text = size.ToString();
            }
        }
    }
}