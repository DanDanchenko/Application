using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.API.Data;
using EventManagement.API.DTOs.Events;
using EventManagement.API.Models;
using EventManagement.API.Services.Guiderfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Services
{
    public class EventService : IEventService
    {
        public EventsManagementContext _context;

        public EventService(EventsManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDto>> GetPublicEventsAsync()
        {
            var publicEvents = await _context.Events.Where(e => e.IsPublic).Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Capacity = e.Capacity,
                NumberOfParticipants = e.Participants.Count
            }).ToListAsync();

            return publicEvents;

        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var ev = await _context.Events.Include(e => e.Participants).ThenInclude(p => p.User).FirstOrDefaultAsync(e  => e.Id == id);

            if (ev == null) return null;

            return new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                Location = ev.Location,
                Capacity = ev.Capacity,
                NumberOfParticipants = ev.Participants.Count,
                Participants = ev.Participants.Select(p => p.User.FullName).ToList(),
                OrganizerId = ev.OrganizerId,
            };
        }

        public async Task<EventDto> CreateEventAsync(EventCreateDto dto, Guid organizerId)
        {

            if (dto.StartDate < DateTime.UtcNow) throw new Exception("You cannot create events in the past.");

            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Capacity = dto.Capacity,
                IsPublic = dto.IsPublic,
                OrganizerId = organizerId
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                StartDate = ev.StartDate,
                EndDate = ev.EndDate,
                Location = ev.Location,
                Capacity = ev.Capacity,
                IsPublic = ev.IsPublic,
                OrganizerId = ev.OrganizerId
            };
        }

        public async Task<bool> UpdateEventAsync(Guid id, EventUpdateDto dto, Guid userId)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null) { throw new Exception("Error occured! Event is not found"); return false; }
            if (ev.OrganizerId != userId) { throw new Exception("Error occured! You are not an authorized user to edit this event."); return false; }
            if (dto.StartDate < DateTime.UtcNow) { throw new Exception("Error occured! You cannot set the event time in the past"); return false; }

            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.StartDate = dto.StartDate;
            ev.EndDate = dto.EndDate;
            ev.Location = dto.Location;
            ev.Capacity = dto.Capacity;

            await _context.SaveChangesAsync();

            return true;
          
        }

        public async Task<bool> DeleteEventAsync(Guid id, Guid userId)
        {
            var ev = await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null) { throw new Exception("Error occured! Event is not found"); return false; }
            if (ev.OrganizerId != userId) { throw new Exception("Error occured! You are not an authorized user to delete this event."); return false; }

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> JoinEventAsync(Guid eventId, Guid userId)
        {
            var ev = await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == eventId);

            if (ev == null) { throw new Exception("Error occured! Event is not found"); return false; }
            if (ev.Participants.Any(p => p.UserId == userId)) { throw new Exception("You have already joined this event"); return false; }
            if (ev.Capacity.HasValue && ev.Participants.Count >= ev.Capacity.Value) { throw new Exception("This event is full"); return false; }

            var participant = new Participant { EventId = eventId, UserId = userId };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return true;
           
        }

        public async Task<bool> LeaveEventAsync(Guid eventId, Guid userId)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId);

            if (participant == null) throw new Exception("You are not a participant of this event.");

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<EventDto>> GetUserEventsAsync(Guid userId)
        {
            var events = await _context.Events.Include(e => e.Participants).Where(e => e.OrganizerId == userId || e.Participants.Any(p => p.UserId == userId)).ToListAsync();

            return events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Capacity = e.Capacity,
                NumberOfParticipants = e.Participants.Count,
                OrganizerId = e.OrganizerId,
            });
            


        }

        public async Task<Guid> GetUserIdByEmail(string email)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user.Id;
        }
    }
}
