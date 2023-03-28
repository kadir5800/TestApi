using Business.DTO.BaseObjects;

namespace Business.IMeneger
{
    public interface ITokenControl
    {
        ClientObject getToken(string token);
    }
}
