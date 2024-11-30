using Core.Dtos.User;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistsAsync(string userId);

        Task<UserDto> GetByIdAsync(string userId);
    }
}
