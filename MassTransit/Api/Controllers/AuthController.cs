using Api.Services.AuthService;
using Contract.Authentication;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IRequestClient<AuthenticationRequest> _requestClient;
        private readonly AuthService _authService;

        public AuthController(IRequestClient<AuthenticationRequest> requestClient, AuthService authService)
        {
            _requestClient = requestClient;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest model, CancellationToken cancellationToken = default)
        {
            var result = await _requestClient.GetResponse<AuthenticationResponse>(model, cancellationToken);
            var message = result.Message;

            if (message.IsAuthenticated)
            {
                JwtTokenModel jwt = new JwtTokenModel
                {
                    Username = message.Username,
                    Roles = new List<string>
                    {
                        "user",
                        "admin"
                    }
                };

                var token = _authService.GenerateJwtToken(jwt);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

    }
}
