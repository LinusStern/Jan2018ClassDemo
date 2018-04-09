using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region More Namespaces
using Chinook.Data.Entities;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        /// <summary>
        /// Retrieves all tracks as a usable list
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_ToList()
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Tracks
                    .OrderBy(x => x.Name)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds a track with a specified ID
        /// </summary>
        /// <param name="_trackID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Get(int _trackID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Tracks.Find(_trackID);
            }
        }

        /// <summary>
        /// Finds all tracks of a specified album
        /// </summary>
        /// <param name="albumID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_GetByAlbumID(int _albumID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Tracks
                    .Where(x => x.AlbumId == _albumID)
                    .Select(x => x)
                    .ToList();
            }
        }

        /// <summary>
        /// Finds all tracks by a specified argument (artist, media, genre)
        /// </summary>
        /// <param name="tracksby"></param>
        /// <param name="argid"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, int argid)
        {
            using (var context = new ChinookContext())
            {
                var results =
                    from x in context.Tracks
                    where tracksby.Equals("Artist") // By artist
                        ? x.Album.ArtistId == argid
                        : tracksby.Equals("MediaType") // By media type
                            ? x.MediaTypeId == argid
                            : tracksby.Equals("Genre") // By genre
                                ? x.GenreId == argid
                                : x.AlbumId == argid
                    orderby x.Name
                    select new TrackList
                    {
                        TrackID = x.TrackId,
                        Name = x.Name,
                        Title = x.Album.Title,
                        MediaName = x.MediaType.Name,
                        GenreName = x.Genre.Name,
                        Composer = x.Composer,
                        Milliseconds = x.Milliseconds,
                        Bytes = x.Bytes,
                        UnitPrice = x.UnitPrice
                    };

                return results.ToList();
            }
        }

        /// <summary>
        /// Creates a flat POCO data set for reports
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GenreAlbumReport> GenreAlbumreport_Get()
        {
            using (var context = new ChinookContext())
            {
                // Sorting done on the report level
                var results =
                    from x in context.Tracks
                    select new GenreAlbumReport
                    {
                        GenreName = x.Genre.Name,
                        AlbumTitle = x.Album.Title,
                        TrackName = x.Name,
                        Milliseconds = x.Milliseconds,
                        Bytes = x.Bytes,
                        UnitPrice = x.UnitPrice
                    };

                return results.ToList();
            }
        }
    }
}
