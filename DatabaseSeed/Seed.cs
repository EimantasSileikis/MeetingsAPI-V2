using MeetingsAPI_V2.Data;
using MeetingsAPI_V2.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI_V2.DatabaseSeed
{
    public static class Seed
    {
        public static void PrepSeed(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void SeedData(DataContext context)
        {

            context.Database.Migrate();

            if (!context.Meetings.Any())
            {
                context.Meetings.AddRange(
                    new Meeting()
                    {
                        Name = "Calculus Exam",
                        Users = "{\"Id\":74638,\"Surname\":\"Dirk\",\"Name\":\"Mike\",\"Number\":\" + 37064787734\",\"Email\":\"mikedirk@mail.com\"},{\"Id\":11234,\"Surname\":\"Mer\",\"Name\":\"Eva\",\"Number\":\" + 37064787737\",\"Email\":\"EvaMer@mail.com\"}"
                    },
                    new Meeting()
                    {
                        Name = "Algebra First Meeting",
                        Users = "{\"Id\":87014,\"Surname\":\"Davis\",\"Name\":\"Luke\",\"Number\":\" + 37064787735\",\"Email\":\"davisluke@mail.com\"}"
                    },
                    new Meeting()
                    {
                        Name = "Web programming Meeting"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
