namespace TrainManagement.Common.Abstract.Services
{
    public interface IHasher
    {
        string GetHash(string str);
        bool Сompare(string hash, string str);
    }
}
