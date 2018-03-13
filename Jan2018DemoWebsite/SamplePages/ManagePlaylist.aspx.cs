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
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Playlist Name", "A playlist name is required to receive tracks.");
            }
            else
            {
                string username = "HansenB";
                string playlist = PlaylistName.Text;

                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> Results = sysmgr.List_TracksForPlaylist(playlist, username);

                    if (Results.Count() == 0) {
                        MessageUserControl.ShowInfo("Check playlist name.");
                    }

                    PlayList.DataSource = Results;
                    PlayList.DataBind();
                }, 
                "", 
                "");
            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "No playlist is selected.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Warning", "No playlist name.");
                    // Optional: Empty playlist grid view
                }
                else
                {
                    // Ensure a (single) playlist track has been selected - then extract the data from the grid view row
                    int trackID = 0;
                    int trackNumber = 0;
                    int rowSelections = 0;
                    CheckBox playlistRow = null;

                    // Access each control on the playlist grid
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        // Pointer to the control
                        playlistRow = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        if (playlistRow.Checked)
                        {
                            rowSelections++;

                            // Pointers to the controls
                            trackID = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            trackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    // Process row selections
                    if (rowSelections != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select only one track.");
                    }
                    else
                    {
                        if (trackNumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track unable to be moved down.");
                        }
                        else
                        {
                            MoveTrack(trackID, trackNumber, "down");
                        }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "No playlist is selected.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Warning", "No playlist name.");
                    // Optional: Empty playlist grid view
                }
                else
                {
                    // Ensure a (single) playlist track has been selected - then extract the data from the grid view row
                    int trackID = 0;
                    int trackNumber = 0;
                    int rowSelections = 0;
                    CheckBox playlistRow = null;

                    // Access each control on the playlist grid
                    for (int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        // Pointer to the control
                        playlistRow = PlayList.Rows[i].FindControl("Selected") as CheckBox;

                        if (playlistRow.Checked)
                        {
                            rowSelections++;

                            // Pointers to the controls
                            trackID = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                            trackNumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    // Process row selections
                    if (rowSelections != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select only one track.");
                    }
                    else
                    {
                        if (trackNumber == 1)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track unable to be moved up.");
                        }
                        else
                        {
                            MoveTrack(trackID, trackNumber, "up");
                        }
                    }
                }
            }
        }

        protected void MoveTrack(int trackID, int trackNumber, string direction)
        {
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("HansenB", PlaylistName.Text, trackID, trackNumber, direction);

                // Refresh playlist track listing
                List<UserPlaylistTrack> Info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                PlayList.DataSource = Info;
                PlayList.DataBind();
            },
            "Track Moved",
            "Track successfully moved " + direction);
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
                    List<UserPlaylistTrack> Info = sysmgr.List_TracksForPlaylist(playlist, username);
                    PlayList.DataSource = Info;
                    PlayList.DataBind();
                }, 
                "Track Added", 
                "Track successfully added to your playlist!");
            }
        }
    }
}