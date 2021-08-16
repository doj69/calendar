using Appointment.API.Entities;
using MongoDB.Driver;

namespace Appointment.API.Data
{
    public interface IAppointmentContext
    {
        IMongoCollection<AppointmentEntity> Appointments { get; }
    }
}
