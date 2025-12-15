using Microsoft.EntityFrameworkCore;

namespace Hotel_Managemnt_System.Models
{
    public class Contextdb : DbContext
    {
        public Contextdb(DbContextOptions<Contextdb> options)
        : base(options)
        {
        }

    }
}
