using Core.Dtos.User;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext dbContext;

        public UserService(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await this.dbContext.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<UserDto> GetByIdAsync(string userId)
        {
            UserDto userDto = await this.dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FullName = u.FullName,
                })
                .FirstOrDefaultAsync();

            return userDto;
        }
    }
}
