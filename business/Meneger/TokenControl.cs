using Business.DTO.BaseObjects;
using Business.IMeneger;
using EntityFramework.Context;

namespace Business.Meneger
{
    public class TokenControl : ITokenControl
    {
        private readonly ZDbContext _dbContext;
        public TokenControl(ZDbContext dbContext = null)
        {
            _dbContext=dbContext;
        }
        public ClientObject getToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new ClientObject() { Id=0, token="", Status=false };
            }

            var isCustomer = _dbContext.Users.FirstOrDefault(f => f.Token==token);
            if (isCustomer==null)
            {
                return new ClientObject() { Id=0, token="", Status=false };
            }
            else
            {
                return new ClientObject() { Id=isCustomer.Id, token=isCustomer.Token, Status=true };
            }
        }
    }
}
