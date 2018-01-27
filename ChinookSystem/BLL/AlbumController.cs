using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Extensions
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using System.Data.Entity;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        /// <summary>
        /// Retrieves all albums as a usable list
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_ToList()
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Albums.OrderBy(x => x.Title).ToList();
            }
        }

        /// <summary>
        /// Finds an album with a specified ID
        /// </summary>
        /// <param name="_albumID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Get(int _albumID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Albums.Find(_albumID);
            }
        }

        /// <summary>
        /// Adds an album to the corresponding table in the database
        /// </summary>
        /// <param name="_album"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Album_Add(Album _album)
        {
            using (ChinookContext context = new ChinookContext())
            {
                // Staged for physical insertion into the database
                context.Albums.Add(_album);

                // Commit the changes to the database
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specific album in the database
        /// </summary>
        /// <param name="_album"></param>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Album_Update(Album _album)
        {
            using (ChinookContext context = new ChinookContext())
            {
                _album.ReleaseLabel = string.IsNullOrEmpty(_album.ReleaseLabel) ? null : _album.ReleaseLabel;

                // Flag record as being modified
                context.Entry(_album).State = EntityState.Modified;

                // Commit the changes to the database
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a specific album from the database
        /// </summary>
        /// <param name="_album"></param>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Album_Delete(Album _album)
        {
            using (ChinookContext context = new ChinookContext())
            {
                Albums_Delete(_album.AlbumId);
            }
        }

        public void Albums_Delete(int _albumID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                var existingAlbum = context.Albums.Find(_albumID);

                // Ensure provided record exists in the database
                if (existingAlbum == null) {
                    throw new Exception("Provided album does not exist on file");
                }

                context.Albums.Remove(existingAlbum);
                context.SaveChanges();
            }
        }
    }
}
