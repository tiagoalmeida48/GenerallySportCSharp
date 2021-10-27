namespace GenerallySport.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken(string email, string senha);
    }
}
