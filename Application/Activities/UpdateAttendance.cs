using Application.Core;
using Application.interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class UpdateAttendance
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(ApplicationDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IUserAccessor _userAccessor = userAccessor;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
            .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (activity == null) return null;

            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == _userAccessor.GetUsername());

            if (user == null) return null;

            var hostUsername = activity.Attendees.FirstOrDefault(attendee => attendee.IsHost)?.AppUser?.UserName;

            var attedance = activity.Attendees.FirstOrDefault(attendee => attendee.AppUser.UserName == user.UserName);

            if (attedance != null && hostUsername == user.UserName)
            {
                activity.IsCanceled = !activity.IsCanceled;
            }

            if (attedance != null && hostUsername != user.UserName)
            {
                activity.Attendees.Remove(attedance);
            }

            if (attedance == null)
            {
                attedance = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };

                activity.Attendees.Add(attedance);
            }

            var result = await _context.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance.");
        }
    }
}