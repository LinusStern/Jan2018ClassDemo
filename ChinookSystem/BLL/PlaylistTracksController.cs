using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
               
                //code to go here

                return null;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            // List of all business rule exceptions
            List<string> Exceptions = new List<string>();

            using (var context = new ChinookContext())
            {
                // Attempt to find an existing playlist
                Playlist existingPlaylist = context.Playlists
                    .Where(x => 
                        x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase) 
                        && 
                        x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                    .Select(x => x)
                    .FirstOrDefault();

                PlaylistTrack playlistTrack;
                int trackNumber;

                if (existingPlaylist == null)
                {
                    // In the event there is no existing playlist create a new instance
                    existingPlaylist = new Playlist();

                    // Set playlist attributes
                    existingPlaylist.Name = playlistname;
                    existingPlaylist.UserName = username;

                    // Add playlist to database
                    existingPlaylist = context.Playlists.Add(existingPlaylist);
                    trackNumber = 1;
                }
                else // Playlist exists
                {
                    trackNumber = existingPlaylist.PlaylistTracks
                        .Count() + 1;

                    playlistTrack = existingPlaylist.PlaylistTracks
                        .SingleOrDefault(x => x.TrackId == trackid);

                    // Note: A track can only appear once per playlist
                    if (playlistTrack == null)
                    {
                        if (Exceptions.Count() > 0)
                        {
                            throw new BusinessRuleException("Add playlist track", Exceptions);
                        }
                        else
                        {

                        }

                        // Create a new track instance
                        playlistTrack = new PlaylistTrack();

                        playlistTrack.TrackNumber = trackNumber;
                        playlistTrack.TrackId = trackid;

                        // Note: Parent entity (Playlist) has a HashSet...
                        existingPlaylist.PlaylistTracks.Add(playlistTrack);

                        // Commit changes made
                        context.SaveChanges();
                    }
                    else // The track already exists on the playlist
                    {
                        Exceptions.Add("Playlist track already exists on the current playlist");
                    }
                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
