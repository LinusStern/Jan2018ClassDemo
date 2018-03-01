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

        /// <summary>
        /// Retrieves a list of artist names and an artist ID for display in a dropdown list
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> List_ArtistNames()
        {
            using (var context = new ChinookContext())
            {
                var results =
                    from x in context.Artists
                    orderby x.Name
                    select new SelectionList
                    {
                        IDValueField = x.ArtistId,
                        DisplayText = x.Name
                    };

                return results.ToList();
            }
        }
    }
}
