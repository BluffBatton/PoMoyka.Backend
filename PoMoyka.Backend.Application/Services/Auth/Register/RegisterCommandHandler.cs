using MediatR;
using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Application.Interfaces; // IApplicationDbContext, IJwtService
using PoMoyka.Backend.Application.Services.Auth.Register;
using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Application.Features.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(IApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // 1. Достаём DTO из команды
        var dto = request.UserData;

        // 2. Проверяем, существует ли пользователь
        var userExists = await _context.Users
            .AnyAsync(u => u.Email == dto.Email, cancellationToken);

        if (userExists)
            throw new InvalidOperationException($"Пользователь с email {dto.Email} уже существует");

        // 3. Создаем сущность User (вручную, т.к. есть логика)
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            // 4. Хэшируем пароль из DTO
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = Role.Client // 5. Присваиваем роль по умолчанию
        };

        // 6. Добавляем пользователя в контекст
        await _context.Users.AddAsync(user, cancellationToken);

        // 7. Проверяем и добавляем опциональную машину
        if (dto.Car is not null)
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Name = dto.Car.Name,
                LicensePlate = dto.Car.LicensePlate,
                CarType = dto.Car.CarType,
                User = user // Сразу привязываем к созданному пользователю
            };
            await _context.Cars.AddAsync(car, cancellationToken);
        }

        // 8. Сохраняем ВСЁ ОДИН РАЗ (атомарная транзакция)
        await _context.SaveChangesAsync(cancellationToken);

        // 9. Генерируем и возвращаем JWT
        return _jwtService.GenerateAccessToken(user);
    }
}