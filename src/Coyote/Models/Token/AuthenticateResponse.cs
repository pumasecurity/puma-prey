namespace Coyote.Models.Token
{
    using Puma.Prey.Rabbit.Models;
    using System.Text.Json.Serialization;

    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse() { }
        
        public AuthenticateResponse(string id, string firstName, string lastName, string userName, string email, string jwtToken, string refreshToken)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            Email = email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }

        public AuthenticateResponse(PumaUser user, string jwtToken, string refreshToken) : this(user.Id, user.FirstName, user.LastName, user.UserName, user.Email, jwtToken, refreshToken) 
        { 
        }
    }
}
