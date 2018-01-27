using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Extensions
using Chinook.Data.POCOs;
#endregion

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

        protected void CountAlbums_Click(object sender, EventArgs e)
        {
            // Note: Only available records are the instances on the grids current page
            List<ArtistAlbumCounts> ArtistList = new List<ArtistAlbumCounts>();

            // Reusable pointer to the instance of the class
            ArtistAlbumCounts item = null;

            // Iterate through each grid row
            int artistID = 0;
            foreach (GridViewRow row in Albums.Rows)
            {
                // Current rows artist ID
                artistID = int.Parse((row.FindControl("Artists") as DropDownList).SelectedValue);

                // Check if the current item is already accounted for
                item = ArtistList.Find(x => x.ArtistId == artistID);

                // Result of check
                if (item == null)
                {
                    // Create instance and add it to the list
                    item = new ArtistAlbumCounts();
                    item.ArtistId = artistID;
                    item.AlbumCount = 1;
                    ArtistList.Add(item);
                }
                else
                {
                    item.AlbumCount++;
                }
            }

            // Attach the list to the display control
            ArtistAlbumCountList.DataSource = ArtistList;
            ArtistAlbumCountList.DataBind();
        }
    }
}