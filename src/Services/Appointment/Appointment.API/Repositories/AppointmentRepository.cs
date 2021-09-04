using Appointment.API.Data;
using Appointment.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Appointment.API.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IAppointmentContext _context;
        private readonly FilterDefinitionBuilder<AppointmentEntity> filterDefinitionBuilder = Builders<AppointmentEntity>.Filter;

        public AppointmentRepository(IAppointmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateAppointment(AppointmentEntity appointment)
        {
            await _context.Appointments.InsertOneAsync(appointment);
        }

        public async Task<bool> DeleteAppointment(string id)
        {
            FilterDefinition<AppointmentEntity> filter = filterDefinitionBuilder.Eq(x => x.Id, id);
            DeleteResult deleteResult = await _context.Appointments.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<AppointmentEntity>> FilterAppointmentByColorAsync(string color)
        {
            FilterDefinition<AppointmentEntity> filter = filterDefinitionBuilder.Regex("Color", new BsonRegularExpression(new Regex(color, RegexOptions.IgnoreCase)));
            var result = await _context.Appointments.Find(filter).ToListAsync();

            return result;
        }

        public async Task<AppointmentEntity> GetAppointment(string id)
        {
            var result = await _context.Appointments.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<AppointmentEntity>> GetAppointments()
        {
            var result = await _context.Appointments.Find(x => true).ToListAsync();
            return result;
        }

        public async Task<bool> IsConflictAppointment(AppointmentEntity appointment)
        {
            var result = await _context.Appointments
                        .Find(filter: x => x.Start <= appointment.Start && x.End >= appointment.End || 
                                           x.Start >= appointment.Start && x.Start < appointment.End ||
                                           x.End > appointment.Start && x.End <= appointment.End)
                        .ToListAsync();
            return result.Count > 0;
        }

        public async Task<bool> IsConflictAppointmentUpdate(AppointmentEntity appointment)
        {
            var result = await _context.Appointments
                        .Find(filter: x => (x.Start <= appointment.Start && x.End >= appointment.End || 
                                            x.Start >= appointment.Start && x.Start < appointment.End ||
                                            x.End > appointment.Start && x.End <= appointment.End) && x.Id != appointment.Id).ToListAsync();
            return result.Count > 0;
        }

        public async Task<bool> UpdateAppointment(AppointmentEntity appointment)
        {
            var updateResult = await _context.Appointments.ReplaceOneAsync(filter: x => x.Id.Equals(appointment.Id), replacement: appointment);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
