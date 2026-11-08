using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModuls;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Identity
{
    internal class AuthenticationService
        (
            UserManager<AppUser> _userManager,
            IMapper _mapper ,
            IConfiguration _configuration
        ) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO login)
        {
            var User = await _userManager.FindByEmailAsync(login.Email);
            if (User is null)
            {
                throw new UserNotFoundException($"Email Not Found {login.Email}");
            }
            var Flag = await _userManager.CheckPasswordAsync(User, login.Password);
            if (Flag)
            {
                return new UserDTO()
                {
                    DisplayName = User.DisplayName,
                    Email = login.Email,
                    Token =await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnauthorizedUserException("Password OR Email Is Not Valed  :( ");
            }

        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO register)
        {
            var User = new AppUser()
            {
                DisplayName= register.DisplayName,
                UserName = register.UserName,
                PhoneNumber = register.PhoneNumber,
                Email = register.Email,
            };

            var Result = await _userManager.CreateAsync(User,register.Password);
            if (Result.Succeeded)
            {
                return new UserDTO()
                {
                    DisplayName = register.DisplayName,
                    Email= register.Email,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(E=>E.Description).ToList();
                throw new BadrequestExcption(Errors);
            }
        }


        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }


        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var normailzeEmail = _userManager.NormalizeEmail(email);
            var user =await _userManager.FindByEmailAsync(normailzeEmail);
            return new UserDTO() 
            {
                DisplayName =user.DisplayName,
                Email=user.Email!,
                Token = await CreateTokenAsync(user)
            };

        }


        public async Task<AddressDTO> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email == email);
            if (user is null)
            {
                   throw new UserNotFoundException($"Email : {email} Not Found ");
            }
            if (user.Address is null)
            {
                   throw new AddressNotFoundException($"Address Of {user.DisplayName} Not Found");
            }
            return _mapper.Map<Address,AddressDTO>(user.Address);    
        }


        public async Task<AddressDTO> UpdateCurrentUserAddresAsync(string email , AddressDTO addressDTO)
        {
            var normalizedEmail = _userManager.NormalizeEmail(email);
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U=>U.NormalizedEmail == normalizedEmail);
            if (user is null)
            {
                throw new UserNotFoundException($"{ email } is not registered.");
            }
            if (user.Address is null)
            {
                user.Address = _mapper.Map<AddressDTO, Address>(addressDTO);
            }
            _mapper.Map(addressDTO ,user.Address);

            var reuselt = await _userManager.UpdateAsync(user);
            if (!reuselt.Succeeded)
            {
                throw new BadrequestExcption(reuselt.Errors.Select(E=>E.Description).ToList());
            }
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }


        private async Task<string> CreateTokenAsync(AppUser appUser)
        {

            //01 Handel Claimes
            var Claims = new List<Claim>()
            {
                
                new(ClaimTypes.NameIdentifier, appUser.Id),
                new(ClaimTypes.Name, appUser.DisplayName!),
                new(ClaimTypes.Email, appUser.Email!),
            };

            var UserRouls =await _userManager.GetRolesAsync(appUser);
            foreach (var Roul in UserRouls) 
            {
                Claims.Add(new Claim(ClaimTypes.Role , Roul));
            };

            //02 Handel Key 
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));
            var Creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);

            // 03 Create Token
            var Token = new JwtSecurityToken
                (
                    issuer : _configuration.GetSection("JWTOptions")["issuer"],
                    audience : _configuration.GetSection("JWTOptions")["audience"],
                    claims: Claims,
                    expires : DateTime.Now.AddHours(1),
                    signingCredentials: Creds
                );

            //04 Write Token 
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}
