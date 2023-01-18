using CatalogLibrary.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CatalogWebAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private UserBusiness userBusiness;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            userBusiness = new UserBusiness();
        }

        #region Request & Response

        public class LoginRequest
        {
            public LoginRequest()
            {
                this.UserName = String.Empty;
                this.Password = String.Empty;
            }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponse
        {
            public LoginResponse()
            {
                this.Token = String.Empty;
                this.User = new UserResponse();
            }

            public string Token { get; set; }

            public UserResponse User { get; set; }
        }

        public class UserResponse
        {
            public UserResponse()
            {
                this.Id = String.Empty;
                this.Email = String.Empty;
                this.Roles = new List<string>();
            }

            public string Id { get; set; }

            public string Email { get; set; }

            public IList<string> Roles { get; set; }
        }

        #endregion

        /// <summary>
        /// Checks user credentials
        /// </summary>
        /// <param name="User"></param>
        /// <returns>Status code</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/login
        ///     {
        ///        "userName": "string",
        ///        "password": "string"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if valid login</response>
        /// <response code="400">If User is null</response>
        /// <response code="401">If invalid login attempt</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Login", Name = "Login")]
        public IActionResult Post([FromBody] LoginRequest User)
        {
            if (User == null)
            {
                return BadRequest("Invalid client request");
            }

            // TODO: Implement auth logic
            bool authenticated = false;
            CatalogLibrary.Entity.User? authenticatedUser;
            authenticated = userBusiness.AuthenticateUserAPI(User.UserName, User.Password, out authenticatedUser);

            if (authenticatedUser !=  null && authenticated)
            {
                // TODO: automapper
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.Token = authenticatedUser.Token;
                loginResponse.User = new UserResponse();
                loginResponse.User.Email = authenticatedUser.Email;
                loginResponse.User.Id = authenticatedUser.Id.ToString();
                loginResponse.User.Roles = authenticatedUser.Roles;

                return Ok(new { Token = loginResponse.Token, User = loginResponse.User });
            }

            return Unauthorized();
        }
    }
}
