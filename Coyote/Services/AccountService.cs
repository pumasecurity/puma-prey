using Coyote.Controllers.Authentication.Model;
using Coyote.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coyote.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<PumaUser> _userManager;
        private readonly RabbitDBContext _dbContext;

        public AccountService(UserManager<PumaUser> userManager,
            RabbitDBContext rabbitdbContext)
        {
            _userManager = userManager;
            _dbContext = rabbitdbContext;
        }

        public IdentityResult CreateUser(AccountRequest model)
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

            IdentityResult result = _userManager.CreateAsync(pumaUser, model.Password).GetAwaiter().GetResult();
            return result;
        }

        public bool DoesUserExist(string email)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            return user != null;

        }
        public User ShowUsers(string email)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();

            var user1 = new User
            {
                Email = user.Email,
                CreditCardNumber = user.CreditCardNumber,
                CreditCardExpiration = user.CreditCardExpiration,
                PhoneNumber = user.PhoneNumber,
                BillingAddress1 = user.BillingAddress1,
                BillingAddress2 = user.BillingAddress2,
                BillingCity = user.BillingCity,
                BillingState = user.BillingState,
                BillingZip = user.BillingZip,

            };
            return user1;
        }

        public IdentityResult UpdateUser(AccountUpdate model)
        {
            var users = _userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();

            users.PhoneNumber = model.PhoneNumber;
            users.CreditCardNumber = model.CreditCardNumber;
            users.CreditCardExpiration = model.CreditCardExpiration;
            users.BillingAddress1 = model.BillingAddress1;
            users.BillingAddress2 = model.BillingAddress2;
            users.BillingCity = model.BillingCity;
            users.BillingState = model.BillingState;
            users.BillingZip = model.BillingZip;


            IdentityResult result = _userManager.UpdateAsync(users).GetAwaiter().GetResult();
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
