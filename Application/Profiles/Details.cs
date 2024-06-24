using Application.Core;
using Application.interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles;

public class Details
{
    public class Query : IRequest<Result<Profile>>
    {
        public string Username { get; set; }
    }

    public class Handler(
        ApplicationDbContext context,
        IMapper mapper,
        IUserAccessor userAccessor) : IRequestHandler<Query, Result<Profile>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IUserAccessor _userAccessor = userAccessor;

        public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.ProjectTo<Profile>(
                _mapper.ConfigurationProvider,
                new { currentUsername = _userAccessor.GetUsername() })
                .SingleOrDefaultAsync(x => x.Username == request.Username);

            return Result<Profile>.Success(user);
        }
    }
}