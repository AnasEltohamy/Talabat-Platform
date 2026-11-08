using DomainLayer.Models.IdentityModuls;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDTO> LoginAsync(LoginDTO login);
        Task<UserDTO> RegisterAsync(RegisterDTO register);
        Task<UserDTO> GetCurrentUserAsync(string email);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDTO> UpdateCurrentUserAddresAsync(string email, AddressDTO addressDTO);
        Task<AddressDTO> GetCurrentUserAddressAsync(string email);
    }
}
