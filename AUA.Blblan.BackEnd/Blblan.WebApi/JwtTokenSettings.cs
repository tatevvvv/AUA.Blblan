namespace Blblan.WebApi
{
    public class JwtTokenSettings
    {
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public string SymmetricSecurityKey { get; set; }

        public string JwtRegisteredClaimNamesSub { get; set; }
    }
}