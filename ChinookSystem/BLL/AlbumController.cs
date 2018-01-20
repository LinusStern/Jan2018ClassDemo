using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Extensions
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
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
        /// <param name="albumID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Get(int albumID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Albums.Find(albumID);
            }
        }
    }
}
