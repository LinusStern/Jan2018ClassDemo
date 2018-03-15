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
                    throw new Exception("Playlist not found on file.");
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
                        PlaylistTrack otherTrack;
                        
                        // Track movement
                        if (direction.Equals("up"))
                        {
                            // Ensure track is not already the first track
                            if (movedTrack.TrackNumber == 1)
                            {
                                throw new Exception("Playlist track already at the top.");
                            }
                            else
                            {
                                // Find the track being switched as part of the move
                                otherTrack = (
                                    from x in existingTrack.PlaylistTracks
                                    where x.TrackNumber == movedTrack.TrackNumber - 1 // Track below current
                                    select x)
                                    .FirstOrDefault();

                                if (otherTrack == null)
                                {
                                    throw new Exception("Track to be switched during the move operation does not exist.");
                                }
                                else // Tracks moved
                                {
                                    movedTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            // Ensure track is not already the final track
                            if (movedTrack.TrackNumber >= existingTrack.PlaylistTracks.Count)
                            {
                                throw new Exception("Playlist track already at the bottom.");
                            }
                            else
                            {
                                // Find the track being switched as part of the move
                                otherTrack = (
                                    from x in existingTrack.PlaylistTracks
                                    where x.TrackNumber == movedTrack.TrackNumber + 1 // Track above current
                                    select x)
                                    .FirstOrDefault();

                                if (otherTrack == null)
                                {
                                    throw new Exception("Track to be switched during the move operation does not exist.");
                                }
                                else // Tracks moved
                                {
                                    movedTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }

                        // Save changes to the data (two entities)
                        context.Entry(movedTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
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
                    throw new Exception("Playlist not found on file.");
                }
                else
                {
                    // Track physical order may not adhere to the logical order (track numbering)
                    var tracksToKeep =
                        existingTrack.PlaylistTracks
                        .Where(t => !trackstodelete.Any(
                            d => d == t.TrackId)) // Look for an item in list A that is also in list B
                        .OrderBy(t => t.TrackNumber) // Maintains current track order
                        .Select(t => t);

                    // Delete tracks
                    PlaylistTrack item = null;
                    foreach (var trackid in trackstodelete)
                    {
                        item =
                            existingTrack.PlaylistTracks
                            .Where(t => t.TrackId == trackid)
                            .FirstOrDefault();

                        if (item != null)
                        {
                            existingTrack.PlaylistTracks.Remove(item);
                        }
                    }

                    // Renumber tracks (if any remain)
                    int number = 1;
                    foreach (var track in tracksToKeep)
                    {
                        track.TrackNumber = number;
                        number++;

                        context.Entry(track).Property(y => y.TrackNumber).IsModified = true;
                    }

                    // Commit changes
                    context.SaveChanges();
                }
            }
        }//eom
    }
}
