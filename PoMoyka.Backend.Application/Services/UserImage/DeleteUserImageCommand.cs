using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Application.Services.UserImage
{
    public class DeleteUserImageCommand : IRequest
    {
        public DeleteUserImageCommand() { }
    }

    public class DeleteProfileImageCommandHandler : IRequestHandler<DeleteUserImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContextService;

        public DeleteProfileImageCommandHandler(IApplicationDbContext context, IFileService fileService, IUserContextService userContextService)
        {
            _context = context;
            _fileService = fileService;
            _userContextService = userContextService;
        }

        public async Task Handle(DeleteUserImageCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            var user = await _context.Users
                .Include(p => p.UserImage)
                .FirstOrDefaultAsync(p => p.Id == userId.Value, cancellationToken);

            if (user == null)
                throw new KeyNotFoundException($"Profile with ID {userId} not found");

            if (user.UserImage == null)
                throw new InvalidOperationException("Profile does not have an image to delete");

            // Delete the file from storage
            await _fileService.DeleteFileAsync(user.UserImage.ImageUrl);

            // Remove the image record from database
            _context.UserImages.Remove(user.UserImage);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
