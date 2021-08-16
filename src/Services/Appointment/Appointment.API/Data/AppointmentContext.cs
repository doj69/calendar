using Appointment.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Appointment.API.Data
{
    public class AppointmentContext : IAppointmentContext
    {
        public AppointmentContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Appointments = database.GetCollection<AppointmentEntity>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            AppointmentContextSeed.SeedData(Appointments);
        }
        public IMongoCollection<AppointmentEntity> Appointments { get; }
    }
}
