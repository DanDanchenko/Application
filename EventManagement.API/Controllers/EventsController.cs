using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EventManagement.API.DTOs.Events;
using EventManagement.API.Services;
using EventManagement.API.Services.Guiderfaces;


namespace EventManagement.API.Controllers
{
    [Route("events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public IEventService _EService;

        public EventsController(IEventService eService)
        {
            _EService = eService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublicEvents()
        {
            var events = await _EService.GetPublicEventsAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var ev = await _EService.GetEventByIdAsync(id);

            if (ev == null) return NotFound("Error occured! This event is not found");

            return Ok(ev);
        }

      //  [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
        {

            var organizerId = await _EService.GetUserIdByEmail(dto.OrganizerEmail);

            var newEvent = await _EService.CreateEventAsync(dto, organizerId);

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

      //  [Authorize]
        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventUpdateDto dto)
        {

            var userId = await _EService.GetUserIdByEmail(dto.OrganizerEmail);

            bool feedback = await _EService.UpdateEventAsync(id, dto, userId);

            if (!feedback) return BadRequest("Something gone wrong");

            return Ok("Edited event successfully");
        }

       // [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
             var userId = Guid.Parse(User.FindFirst("id").Value);

            bool feedback = await _EService.DeleteEventAsync(id, userId);

            if (!feedback) return BadRequest("Something gone wrong");

            return Ok("Deleted event successfully");
        }

       // [Authorize]
        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinEvent(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);

            bool feedback = await _EService.JoinEventAsync(id, userId);

            if (!feedback) return BadRequest("Something gone wrong");

            return Ok("Joined successfully");

        }

      //  [Authorize]
        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveEvent(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);

            bool feedback = await _EService.LeaveEventAsync(id, userId);

            if (!feedback) return BadRequest("Something gone wrong");

            return Ok("Left event successfully");
        }

       // [Authorize]
        [HttpGet("/users/me/events")]
        public async Task<IActionResult> GetUserEvents()
        {
            var userId = Guid.Parse(User.FindFirst("id").Value);

            var events = await _EService.GetUserEventsAsync(userId);

            return Ok(events);
        }



    }
}
