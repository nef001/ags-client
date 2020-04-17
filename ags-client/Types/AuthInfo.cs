namespace ags_client.Types
{
    public class AuthInfo
    {
        public bool isTokenBasedSecurity { get; set; }
        public string tokenServicesUrl { get; set; }
        public int shortLivedTokenValidity { get; set; }

    }
}
