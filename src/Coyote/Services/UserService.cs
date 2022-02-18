using Coyote.Models.User;
using Coyote.Models.Authentication;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Puma.Prey.Rabbit.Context;
using System.Linq;

namespace Coyote.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<PumaUser> _userManager;
        private readonly RabbitDBContext _dbContext;

        public UserService(UserManager<PumaUser> userManager, RabbitDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IdentityResult> CreateUser(UserRequest model)
        {
            var pumaUser = new PumaUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreditCardNumber = model.CreditCardNumber,
                CreditCardExpiration = model.CreditCardExpiration,
                BillingAddress1 = model.BillingAddress1,
                BillingAddress2 = model.BillingAddress2,
                BillingCity = model.BillingCity,
                BillingState = model.BillingState,
                BillingZip = model.BillingZip
            };

            return await _userManager.CreateAsync(pumaUser, model.Password);
        }

        public async Task<bool> DoesUserExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public User GetUserByMemberId(int id)
        {
            var response = _dbContext.Users.Where(i => i.MemberId == id).FirstOrDefault();

            if (response == null)
                return null;

            var user = new User
            {
                Email = response.Email,
                CreditCardNumber = response.CreditCardNumber,
                CreditCardExpiration = response.CreditCardExpiration,
                PhoneNumber = response.PhoneNumber,
                BillingAddress1 = response.BillingAddress1,
                BillingAddress2 = response.BillingAddress2,
                BillingCity = response.BillingCity,
                BillingState = response.BillingState,
                BillingZip = response.BillingZip,
            };
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var response = await _userManager.FindByEmailAsync(email);
            if (response == null)
                return null;

            var user = new User
            {
                Email = response.Email,
                CreditCardNumber = response.CreditCardNumber,
                CreditCardExpiration = response.CreditCardExpiration,
                PhoneNumber = response.PhoneNumber,
                BillingAddress1 = response.BillingAddress1,
                BillingAddress2 = response.BillingAddress2,
                BillingCity = response.BillingCity,
                BillingState = response.BillingState,
                BillingZip = response.BillingZip,
            };
            return user;
        }

        public async Task<IdentityResult> UpdateUser(UserUpdate model)
        {
            var users = await _userManager.FindByEmailAsync(model.Email);

            users.PhoneNumber = model.PhoneNumber;
            users.CreditCardNumber = model.CreditCardNumber;
            users.CreditCardExpiration = model.CreditCardExpiration;
            users.BillingAddress1 = model.BillingAddress1;
            users.BillingAddress2 = model.BillingAddress2;
            users.BillingCity = model.BillingCity;
            users.BillingState = model.BillingState;
            users.BillingZip = model.BillingZip;

            var result = await _userManager.UpdateAsync(users);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordRequest request, PumaUser user)
        {
            return await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        }

        public async Task<PumaUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }
    }
}