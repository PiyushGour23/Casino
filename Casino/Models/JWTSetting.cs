namespace Casino.Models
{
    public class JWTSetting
    {
        public string key { get; set; } = null!;
        public string issuer { get; set; } = null!;
        public string audience { get; set; } = null!;
        public string subject { get; set; } = null!;

    }
}
