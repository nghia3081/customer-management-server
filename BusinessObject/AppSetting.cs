namespace BusinessObject
{
    public class AppSetting
    {
        public string HashSalt { get; set; } = null!;
        public Jwt Jwt { get; set; } = null!;
        public HostMail HostMail { get; set; } = null!;
    }
    public class Jwt
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public int ValidTime { get; set; }
    }
    public class HostMail
    {
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}
