using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Extensions
using Chinook.Data.Entities;
using System.Data.Entity; // Inherits DbContext
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext : DbContext
    {
        public ChinookContext() : base("name=ChinookDB") // Name links to web connection string specified in WebConnection.config file
        {

        }

        // Represents the tables of the database
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
    }
}
