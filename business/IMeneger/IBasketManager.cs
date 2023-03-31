using Business.DTO.BaseObjects;
using Business.DTO.Basket;

namespace Business.IMeneger
{
    public interface IBasketManager
    {
        ClientResult<getOneBasketResponse> getOneBasket(getOneRequest request);
        ClientResult addBasket(addBasketRequest request);
        ClientResult deleteBasket(getOneRequest request);
        ClientResult<getAllBasketResponse> getAllBasket(dataTableRequest request);
        ClientResult<getCustomerBasketsResponse> getCustomerBaskets(getOneRequest request);
        
    }
}
