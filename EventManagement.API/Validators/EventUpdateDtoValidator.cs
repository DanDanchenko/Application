using FluentValidation;
using EventManagement.API.DTOs.Events;
using System;

namespace EventManagement.API.Validators
{
    public class EventUpdateDtoValidator : AbstractValidator<EventUpdateDto>
    {
        public EventUpdateDtoValidator()
        {
            When(x => x.Title != null, () =>
            {
                    RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.").MaximumLength(200);
            });

           When(x => x.Location != null, () =>
            {
                RuleFor(x => x.Location).NotEmpty().WithMessage("Location cannot be empty.").MaximumLength(250);
            });

            When(x => x.StartDate.HasValue, () =>
            {
                RuleFor(x => x.StartDate.Value).GreaterThan(DateTime.UtcNow.AddMinutes(-1)).WithMessage("Start date must be in the future");
            });

            When(x => x.EndDate.HasValue && x.StartDate.HasValue, () =>
            {
                RuleFor(x => x.EndDate.Value).GreaterThan(x => x.StartDate.Value).WithMessage("End date must be after the start date.");
            });

            When(x => x.Capacity.HasValue, () =>
            {
                RuleFor(x => x.Capacity.Value).GreaterThan(0).WithMessage("Capacity must be greater than 0 when specified.");
            });

        }
    }
}
