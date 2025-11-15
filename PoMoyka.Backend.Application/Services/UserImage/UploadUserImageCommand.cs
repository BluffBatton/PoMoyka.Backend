using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Application.Services.UserImage
{
    public class UploadUserImageCommand : IRequest<Guid>
    {
        public IFormFile Image { get; set; }
    }

    public class UploadProfileImageCommandHandler : IRequestHandler<UploadUserImageCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContextService;

        public UploadProfileImageCommandHandler(IApplicationDbContext context, IFileService fileService, IUserContextService userContextService)
        {
            _context = context;
            _fileService = fileService;
            _userContextService = userContextService;
        }

        public async Task<Guid> Handle(UploadUserImageCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            var user = await _context.Users
                .Include(p => p.UserImage)
                .FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"Profile with ID {userId} not found");

            // Delete existing image if any
            if (user.UserImage != null)
            {
                await _fileService.DeleteFileAsync(user.UserImage.ImageUrl);
                _context.UserImages.Remove(user.UserImage);
            }

            // Save new image
            var imageUrl = await _fileService.SaveFileAsync(request.Image, "user-images");

            var userImage = new PoMoyka.Backend.Domain.Entities.UserImage
            {
                Name = request.Image.FileName,
                ImageUrl = imageUrl,
                User = user,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.UserImages.AddAsync(userImage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return userImage.Id;
        }
    }
}
