namespace MessageConsumer.Utils.Interfaces
{
    public interface IPasswordUtil
    {
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
