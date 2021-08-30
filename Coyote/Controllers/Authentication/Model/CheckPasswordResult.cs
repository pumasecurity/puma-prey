using Microsoft.AspNetCore.Identity;
using Puma.Prey.Rabbit.Models;

namespace Coyote.Models
{
    public class CheckPasswordResult
    {
        public CheckPasswordResult(SignInResult result, PumaUser user)
        {
            Result = result;
            User = user;
        }

        public SignInResult Result { get; }
        public PumaUser User { get; }

        public string ErrorMessage
        {
            get
            {
                if (Result == SignInResult.Success)
                {
                    return "";
                }
                if (Result == SignInResult.Failed)
                {
                    return "Invalid password.";
                }
                if (Result == SignInResult.LockedOut)
                {
                    return $"Account is locked out.";
                }
                if (Result == SignInResult.NotAllowed)
                {
                    return $"Invalid username.";
                }
                return "";
            }
        }
    }
}
