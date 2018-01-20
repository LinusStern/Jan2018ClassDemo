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
                return context.Tracks.OrderBy(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Finds a track with a specified ID
        /// </summary>
        /// <param name="trackID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Get(int trackID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Tracks.Find(trackID);
            }
        }
    }
}
