using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;
#endregion

namespace Jan2018ClassDemo.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                TracksBy.Text = "Artist";
                SearchArgID.Text = ArtistDDL.SelectedValue;

                TracksSelectionList.DataBind();
            }, 
            "Tracks by Artist", 
            "Add a track to your playlist by clicking (+)");
        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
           
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        /// <summary>
        /// Executes on user intiated add track event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo(
                    "Playlist Name", 
                    "Playlist name is required when adding a track.");
            }
            else
            {
                string username = "HansenB"; // (!) Find via security
                string playlist = PlaylistName.Text;

                // Note: TrackID is imbedded on each list row (with the CommandArgument parameter)
                int trackID = int.Parse(e.CommandArgument.ToString());

                // Send data to the BLL
                MessageUserControl.TryRun(() =>
                {
                    // BLL connection
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlist, username, trackID);

                    // Refresh playlist track listing
                }, 
                "Track Added", 
                "Track successfully added to your playlist!");
            }
        }
    }
}