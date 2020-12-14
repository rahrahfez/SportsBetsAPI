namespace SportsBetsServer.Contracts.Repository
{
    public interface IRepositoryWrapper 
    {
         IUserRepository User { get; }
         IWagerRepository Wager { get; }
    }
}