using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.User;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper mapper;

        public UserService(
            ApplicationContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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
                .ProjectTo<UserDto>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return userDto;
        }
    }
}
