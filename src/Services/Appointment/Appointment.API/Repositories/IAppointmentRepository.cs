using Appointment.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointment.API.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentEntity>> GetAppointments();
        Task<AppointmentEntity> GetAppointment(string id);        
        Task CreateAppointment(AppointmentEntity appointment);
        Task<bool> UpdateAppointment(AppointmentEntity appointment);
        Task<bool> DeleteAppointment(string id);
        Task<bool> IsConflictAppointment(AppointmentEntity appointment);
        Task<bool> IsConflictAppointmentUpdate(AppointmentEntity appointment);
        Task<IEnumerable<AppointmentEntity>> FilterAppointmentByColorAsync(string color);
    }
}
