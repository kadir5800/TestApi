namespace ApiCore.Model.service
{
    public class FakeResponse : ddService, IFakeResponse
    {
        public FakeResponse(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public response GetResponse()
        {
            var response = new response()
            {
                Culture = ClientContext.Culture,
                Token = ClientContext.Token,
                UserId = ClientContext.UserId,
            };
            return response;
        }
    }
}
