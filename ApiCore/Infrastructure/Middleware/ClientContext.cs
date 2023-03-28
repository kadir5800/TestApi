namespace ApiCore.Infrastructure.Middleware
{
    public interface IClientContext
    {
        string Token { get; set; }
        long UserId { get; }


        void SetToken(string token);
        void SetUserId(long userId);

    }

    public class ClientContext : IClientContext
    {
        public string Token { get; set; }
        public long UserId { get; private set; }

        public ClientContext()
        {
            Token = default(string);
            UserId = 0;
        }

        public void SetToken(string token)
        {
            Token = token;
        }

        public void SetUserId(long userId)
        {
            UserId = userId;
        }

    }
}
