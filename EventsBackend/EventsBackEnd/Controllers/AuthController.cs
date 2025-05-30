using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventsBackEnd.Models;
namespace EventsBackEnd.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Email, request.Password, false, false);

            if (!result.Succeeded)
                return BadRequest("Invalid login attempt");

            var user = await _userManager.FindByEmailAsync(request.Email);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Email,
                user.Role,
                Token = "JWT_TOKEN_PLACEHOLDER" // Simplified for challenge
            });
        }
    }
}
