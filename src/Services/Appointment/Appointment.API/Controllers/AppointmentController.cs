using Appointment.API.Entities;
using Appointment.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Appointment.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repository;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentRepository repository, ILogger<AppointmentController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppointmentEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAppointmentsAsync()
        {
            var result = await _repository.GetAppointments();

            return Ok(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetAppointment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AppointmentEntity>> GetAppointmentByIdAsync(string id)
        {
            var appointment = await _repository.GetAppointment(id);
            if (appointment == null)
            {
                _logger.LogError($"Appointment with id: {id}, not found.");
                return NotFound();
            }
            return Ok(appointment);
        }

        [Route("check-conflict", Name = "CheckConflictAppointment")]
        [HttpGet]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckConflictAppointmentAsync([FromBody] AppointmentEntity appointment)
        {
            var isConflict = await _repository.IsConflictAppointment(appointment);

            if (isConflict)
            {
                _logger.LogError($"Appointment have a conflict with other appointment");
                return BadRequest($"Appointment have a conflict with other appointment");
            }

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AppointmentEntity>> CreateAppointmentAsync([FromBody] AppointmentEntity appointment)
        {
            var isConflict = await _repository.IsConflictAppointment(appointment);

            if (isConflict)
            {
                _logger.LogError($"Appointment have a conflict with other appointment");
                return BadRequest($"Appointment have a conflict with other appointment");
            }

            await _repository.CreateAppointment(appointment);

            return CreatedAtRoute("GetAppointment", new { id = appointment.Id }, appointment);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAppointmentAsync([FromBody] AppointmentEntity appointment)
        {
            var isConflict = await _repository.IsConflictAppointmentUpdate(appointment);

            if (isConflict)
            {
                _logger.LogError($"Appointment have a conflict with other appointment");
                return BadRequest($"Appointment have a conflict with other appointment");
            }

            return Ok(await _repository.UpdateAppointment(appointment));
        }

        [HttpPatch]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PatchAppointmentAsync([FromBody] AppointmentEntity appointment)
        {
            return Ok(await _repository.UpdateAppointment(appointment));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteAppointment")]
        [ProducesResponseType(typeof(AppointmentEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAppointmentAsync(string id)
        {
            return Ok(await _repository.DeleteAppointment(id));
        }
    }
}
