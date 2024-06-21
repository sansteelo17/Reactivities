using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments;

public class List
{
    public class Query : IRequest<Result<List<CommentDto>>>
    {
        public Guid ActivityId { get; set; }
    }

    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<CommentDto>>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments.Where(
                x => x.Activity.Id == request.ActivityId
            ).OrderByDescending(
                x => x.CreatedAt
            ).ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

            return Result<List<CommentDto>>.Success(comments);
        }
    }
}