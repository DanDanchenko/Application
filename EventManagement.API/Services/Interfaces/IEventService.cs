using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.API.DTOs.Events;

namespace EventManagement.API.Services.Guiderfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetPublicEventsAsync();
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task<EventDto> CreateEventAsync(EventCreateDto dto, Guid organizerId);
        Task<bool> UpdateEventAsync(Guid id, EventUpdateDto dto, Guid userId);
        Task<bool> DeleteEventAsync(Guid id, Guid userId);
        Task<bool> JoinEventAsync(Guid eventId, Guid userId);
        Task<bool> LeaveEventAsync(Guid eventId, Guid userId);
        Task<IEnumerable<EventDto>> GetUserEventsAsync(Guid userId);
        Task<Guid> GetUserIdByEmail(string email);
    }
}
