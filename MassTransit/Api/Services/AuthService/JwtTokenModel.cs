namespace Api.Services.AuthService
{
    public class JwtTokenModel
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
