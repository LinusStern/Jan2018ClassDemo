using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Extensions
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace Chinook.Data.Entities
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }

        [StringLength(240, ErrorMessage = "Max length of an artist name is 240 characters")]
        public string Name { get; set; } // Nullable

        // Navigational properties (class relationships)
        public virtual ICollection<Album> Albums { get; set; }
    }
}
