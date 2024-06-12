using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class List
{
    public class Query : IRequest<Result<List<ActivityDto>>> { }

    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<ActivityDto>>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await _context.Activities
            .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return Result<List<ActivityDto>>.Success(activities);
        }
    }
}