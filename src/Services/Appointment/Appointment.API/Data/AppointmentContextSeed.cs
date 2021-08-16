using Appointment.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appointment.API.Data
{
    public static class AppointmentContextSeed
    {
        public static void SeedData(IMongoCollection<AppointmentEntity> appointmentCollection)
        {
            bool existAppointment = appointmentCollection.Find(p => true).Any();
            if (!existAppointment)
            {
                var list = GetPreconfiguredEvents();
                appointmentCollection.InsertManyAsync(list);
            }
        }

        private static IEnumerable<AppointmentEntity> GetPreconfiguredEvents()
        {
            return new List<AppointmentEntity>()
            {

                new AppointmentEntity()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "Meeting",
                    Description = "Meeting with bussiness owner",
                    Start =  DateTime.Parse("2021-08-13T12:00:00"),
                    End = DateTime.Parse("2021-08-13T13:59:59"),
                    Color = "#B8AB14FF",
                    Timed =  true,
                },
                new AppointmentEntity()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "Holiday",
                    Description = "Office outing",
                    Start =  DateTime.Parse("2021-08-14T12:00:00"),
                    End = DateTime.Parse("2021-08-14T13:59:59"),
                    Color = "#346FA8FF",
                    Timed =  true,
                }
            };
        }
    }
}
