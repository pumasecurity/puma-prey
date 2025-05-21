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
                FirstName = model.FirstName,
                LastName = model.LastName,
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
                FirstName = response.FirstName,
                LastName = response.LastName,
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
                MemberId = response.MemberId,
                FirstName = response.FirstName,
                LastName = response.LastName,
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
            var user = await _userManager.FindByEmailAsync(model.Email);

            user.PhoneNumber = model.PhoneNumber;
            user.CreditCardNumber = model.CreditCardNumber;
            user.CreditCardExpiration = model.CreditCardExpiration;
            user.BillingAddress1 = model.BillingAddress1;
            user.BillingAddress2 = model.BillingAddress2;
            user.BillingCity = model.BillingCity;
            user.BillingState = model.BillingState;
            user.BillingZip = model.BillingZip;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(user);
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