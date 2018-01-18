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
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int AlbumID { get; set; }

        [Required(ErrorMessage = "Album title is a required field")]
        [StringLength(320, ErrorMessage = "Max length of an album title is 320 characters")]
        public string Title { get; set; }

        public int ArtistID { get; set; }
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "Max length of a release label is 50 characters")]
        public string ReleaseLabel { get; set; } // Nullable

        // Navigational properties (class relationships)
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
