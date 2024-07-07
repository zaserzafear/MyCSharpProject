namespace Api.Settings
{
    public class JwtSetting
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long ExpiresMinute { get; set; }
        public string SignKey { get; set; }
    }
}
