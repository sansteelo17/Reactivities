using Application.Core;
using Application.interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(a => a.Activity).SetValidator(new ActivityValidator());
        }
    }

    public class Handler(ApplicationDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IUserAccessor _userAccessor = userAccessor;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                user => user.UserName == _userAccessor.GetUsername());

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };

            request.Activity.Attendees.Add(attendee);

            _context.Activities.Add(request.Activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to create activity.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}