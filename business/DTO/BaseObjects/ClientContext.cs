namespace Business.DTO.BaseObjects
{
    public interface IClientContext
    {
        string Token { get; set; }
        string UserId { get; }


        void SetToken(string token);
        void SetUserId(string userId);

    }

    public class ClientContext : IClientContext
    {
        public string Token { get; set; }
        public string UserId { get; private set; }

        public ClientContext()
        {
            Token = default(string);
            UserId = default(string);
        }

        public void SetToken(string token)
        {
            Token = token;
        }

        public void SetUserId(string userId)
        {
            UserId = userId;
        }

    }
}
