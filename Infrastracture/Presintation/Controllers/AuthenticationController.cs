using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presintation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IServiceManager _serviceManager ) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            return Ok(await _serviceManager.authenticationService.LoginAsync(loginDTO)); 
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            return Ok(await _serviceManager.authenticationService.RegisterAsync(registerDTO));
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return Ok( await _serviceManager.authenticationService.CheckEmailAsync(email));
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user =await _serviceManager.authenticationService.GetCurrentUserAsync(User.FindFirstValue(ClaimTypes.Email)!);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var userAddress = await _serviceManager.authenticationService.GetCurrentUserAddressAsync(User.FindFirstValue(ClaimTypes.Email)!);
            return Ok(userAddress);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>> UpdateCurrentUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result =await _serviceManager.authenticationService.UpdateCurrentUserAddresAsync(email!, addressDTO);
            return Ok(result);
        
        }

    }
}
