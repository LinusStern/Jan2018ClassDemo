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
        public List<UserPlaylistTrack> List_TracksForPlaylist(string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                // Attempt to find an existing playlist (given passed parameters)
                var results = (
                    from x in context.Playlists
                    where
                        x.UserName.Equals(username)
                        &&
                        x.Name.Equals(playlistname)
                    select x)
                    .FirstOrDefault();

                // Validate playlist (if exists)
                if (results == null)
                {
                    return null;
                }
                else
                {
                    // Find tracks of the found playlist
                    var tracks =
                        from x in context.PlaylistTracks
                        where x.PlaylistId.Equals(results.PlaylistId)
                        orderby x.TrackNumber
                        select new UserPlaylistTrack
                        {
                            // On playlist entity
                            TrackID = x.TrackId,
                            TrackNumber = x.TrackNumber,

                            // On track entity
                            TrackName = x.Track.Name,
                            Milliseconds = x.Track.Milliseconds,
                            UnitPrice = x.Track.UnitPrice
                        };

                    return tracks.ToList();
                }
            }
        }//eom

        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            // List of all business rule exceptions
            List<string> Exceptions = new List<string>();

            using (var context = new ChinookContext())
            {
                // Attempt to find an existing playlist (given passed parameters)
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
                var existingTrack = (
                    from x in context.Playlists
                    where
                        x.Name.Equals(playlistname)
                        &&
                        x.UserName.Equals(username)
                    select x)
                    .FirstOrDefault();

                if (existingTrack == null)
                {
                    throw new Exception("Playlist removed not found on file.");
                }
                else
                {
                    PlaylistTrack movedTrack = (
                        from x in existingTrack.PlaylistTracks
                        where x.TrackId == trackid
                        select x)
                        .FirstOrDefault();

                    if (movedTrack == null)
                    {
                        throw new Exception("Playlist track not found on file.");
                    }
                    else
                    {
                        // Track movement
                        if (direction.Equals("up"))
                        {

                        }
                        else
                        {

                        }
                    }
                }
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
