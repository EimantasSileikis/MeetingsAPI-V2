using MeetingsAPI_V2.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI_V2.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Meeting> Meetings { get; set; } = null!;

    }
}
