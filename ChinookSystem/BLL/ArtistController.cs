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
    public class ArtistController
    {
        /// <summary>
        /// Retrieves all artists as a usable list
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Artist> Artists_ToList()
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Artists.OrderBy(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Finds an artist with a specified ID
        /// </summary>
        /// <param name="_artistID"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Artist Artist_Get(int _artistID)
        {
            using (ChinookContext context = new ChinookContext())
            {
                return context.Artists.Find(_artistID);
            }
        }
    }
}
