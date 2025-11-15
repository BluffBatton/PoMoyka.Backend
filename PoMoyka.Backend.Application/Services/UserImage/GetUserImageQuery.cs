using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Application.Services.UserImage
{
    public class GetUserImageQuery : IRequest<string>
    {
        public GetUserImageQuery() { }
    }

    public class GetUserImageQueryHandler : IRequestHandler<GetUserImageQuery, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContextService;

        public GetUserImageQueryHandler (IApplicationDbContext context, IFileService fileService, IUserContextService userContextService)
        {
            _context = context;
            _fileService = fileService;
            _userContextService = userContextService;
        }

        public async Task<string> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            var user = await _context.Users
                .Include(p => p.UserImage)
                .FirstOrDefaultAsync(p => p.Id == userId.Value, cancellationToken);

            if (user == null || user.UserImage == null || string.IsNullOrEmpty(user.UserImage.ImageUrl))
                throw new KeyNotFoundException($"Profile or profile image not found for profileId {userId}");

            var signedUrl = await _fileService.GetFileUrlAsync(user.UserImage.ImageUrl);
            return signedUrl;
        }
    }
}
