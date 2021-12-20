namespace MinimalWebAPI.Repositories
{
    public interface IUserRepository
    {
        public string GetToken(UserInfo userInfo);
    }
}
