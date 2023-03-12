using Microsoft.EntityFrameworkCore;

namespace noteproject.Models
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Note> AddedNotes { get; set; }
        public DbSet<Note> DeletedNotes { get; set; }

    }
}
