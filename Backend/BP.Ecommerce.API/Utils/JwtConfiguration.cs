namespace BP.Ecommerce.API.Utils
{
    public class JwtConfiguration
    {
        public string Key { get; set; }
        public string Issuser { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expires { get; set; } = TimeSpan.FromMinutes(10);
    }
}