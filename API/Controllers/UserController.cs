using API.Attributes;
using API.Errors;
using API.Extensions;
using Core.Dtos.User;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
        }

        [CustomAuthorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById([FromRoute] string userId)
        {
            var user = await this.userManager
                .FindByEmailFromClaimsPrincipalAsync(this.User);

            if (user.Id != userId)
            {
                return this.BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, Constants.CANNOT_FETCH_OTHER_USER_INFO));
            }

            if (!await this.userService.ExistsAsync(userId))
            {
                return this.NotFound(new ApiResponse(StatusCodes.Status404NotFound, Constants.USER_NOT_FOUND));
            }

            UserDto userDto = await this.userService
                .GetByIdAsync(userId);

            return this.Ok(userDto);
        }
    }
}
