using CatalogLibrary.Entity;
using Fabrikafa.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Business;

public class UserBusiness
{
    public UserBusiness()
    {
    }

    public User GetByUsername(string Username)
    {
        User foundItem = getDummyUser();
        return foundItem;
    }

    /// <summary>
    /// Calls AuthenticateUser with additional API logic
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="Password">API Key For User</param>
    /// <param name="AuthenticatedUser"></param>
    /// <returns></returns>
    public bool AuthenticateUserAPI(string Username, string Password, out User? AuthenticatedUser)
    {
        bool authenticated = false;
        AuthenticatedUser = new User();

        // is there an user registered with this username?
        User foundItem = this.GetByUsername(Username);

        // now check if the pass is matching with API Key
        if (foundItem.ApiKey == Password)
        {
            authenticated = true;
            AuthenticatedUser = foundItem;
        }

        //additional logic for API access
        if (AuthenticatedUser != null && authenticated)
        {
            //add claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, AuthenticatedUser.Username),
                new Claim(JwtRegisteredClaimNames.UniqueName, AuthenticatedUser.Username)
            };

            IList<string> roles = AuthenticatedUser.Roles;

            foreach (var roleName in roles)
            {
                Claim roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }

            // authentication successful so generate jwt token
            var superDuperKeyValue = "some-secret";
            var somesuperDuperKeyByteArray = Encoding.UTF8.GetBytes(superDuperKeyValue);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(somesuperDuperKeyByteArray), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "some-issuer",
                Audience = "some-audience"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            AuthenticatedUser.Token = tokenString;
        }

        return authenticated;
    }
    private User getDummyUser()
    {
        string salt = Salt.Create();
        string hashedPassword = Hash.Create("123", salt);
        string email = "someemail";

        User user = new User
        {
            Id = 1,
            Name = "Dummy User",
            Email = email,
            Username = email,
            Password = hashedPassword,
            Salt = salt,
            Roles = new List<string>
                { "standart" },
        };

        return user;
    }


}