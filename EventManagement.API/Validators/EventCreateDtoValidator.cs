using FluentValidation;
using EventManagement.API.DTOs.Events;
using System;

namespace EventManagement.API.Validators
{
    public class EventCreateDtoValidator : AbstractValidator<EventCreateDto>
    {
        public EventCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").MaximumLength(200);

            RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required.").MaximumLength(250);

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("start date is required.").Must(dt => dt.Kind == DateTimeKind.Utc || dt.Kind == DateTimeKind.Local)
                .WithMessage("Start date must be a valid date.").GreaterThan(DateTime.UtcNow.AddMinutes(-1))
                .WithMessage("Event start date must be in the future.");

            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue).WithMessage("End date must be after the start date");

            RuleFor(x => x.Capacity).GreaterThan(0).When(x => x.Capacity.HasValue).WithMessage("Capacity must be greater than 0 when specified.");


        }
    }
}
