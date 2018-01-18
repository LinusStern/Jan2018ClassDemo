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
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackID { get; set; }

        [Required(ErrorMessage = "Track name is a required field")]
        [StringLength(400, ErrorMessage = "Max length of a track name is 400 characters")]
        public string Name { get; set; }

        public int? AlbumID { get; set; } // Nullable
        public int MediaTypeID { get; set; }
        public int? GenreID { get; set; } // Nullable

        [StringLength(440, ErrorMessage = "Max length of a track composer is 440 characters")]
        public string Composer { get; set; } // Nullable

        public int Milliseconds { get; set; }
        public int? Bytes { get; set; } // Nullable
        public decimal UnitPrice { get; set; }

        // Navigational properties (class relationships)
        public virtual Album Album { get; set; }
    }
}
