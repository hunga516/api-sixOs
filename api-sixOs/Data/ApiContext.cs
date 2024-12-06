using Microsoft.EntityFrameworkCore;
using api_sixOs.Models;

namespace api_sixOs.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<ChairBookings> ChairBookings { get; set; }
        
        public DbSet<Blogs> Blogs { get; set; }
        
        public DbSet<Service> Service { get; set; }
        
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        { 
             
        }
       

        
    }
}
