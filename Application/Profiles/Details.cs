using Application.Core;
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

    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<Profile>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.ProjectTo<Profile>(
                _mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Username == request.Username);

            return Result<Profile>.Success(user);
        }
    }
}