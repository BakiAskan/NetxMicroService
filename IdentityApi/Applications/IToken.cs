namespace IdentityApi.Applications
{
    public interface IToken
    {
        public string GenerateToken(string Id);
    }
}
