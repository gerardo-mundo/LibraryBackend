using LibraryBackend.context;
using Microsoft.EntityFrameworkCore;

 
namespace LibraryNetCoreAPI.Models
{
    public static class PrepareDB
    {
        public static void Population(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()){
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDBContext>()!);
            }
        }
 
        public static void SeedData(ApplicationDBContext context){
            Console.WriteLine("Applying initial migration..."); //para informarnos desde la consola
            context.Database.Migrate();
            Console.WriteLine("Initial migration (database) done!");
        }
    }
}