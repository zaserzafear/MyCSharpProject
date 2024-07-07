using Contract.Authentication;
using MassTransit;

namespace Authentication.Consumers
{
    public class AuthenticationConsume : IConsumer<AuthenticationRequest>
    {
        public async Task Consume(ConsumeContext<AuthenticationRequest> context)
        {
            var request = context.Message;

            bool isAuthenticated = AuthenticateUser(request.Username, request.Password);
            var response = new AuthenticationResponse
            {
                IsAuthenticated = isAuthenticated,
                Username = request.Username,
            };

            await context.RespondAsync(response);
        }

        private bool AuthenticateUser(string username, string password)
        {
            if (username == "username" && password == "password")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
