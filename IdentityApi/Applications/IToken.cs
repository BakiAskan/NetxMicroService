namespace IdentityApi.Applications
{
    public interface IToken
    {
        public string GenerateToken(string country, string Id, string Uid);
    }
}
