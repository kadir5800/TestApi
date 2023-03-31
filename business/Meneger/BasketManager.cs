using Business.DTO.BaseObjects;
using Business.DTO.Basket;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class BasketManager : ZServerService, IBasketManager
    {
        private readonly IBasketDataAccess _basketDataAccess;
        private readonly IShoesDataAccess _shoesDataAccess;
        private readonly IBrandDataAccess _brandDataAccess;
        private readonly IModelDataAccess _modelDataAccess;
        private readonly IColorDataAccess _colorDataAccess;
        public BasketManager(IServiceProvider serviceProvider, IBasketDataAccess basketDataAccess, IShoesDataAccess shoesDataAccess, IBrandDataAccess brandDataAccess, IModelDataAccess modelDataAccess, IColorDataAccess colorDataAccess) : base(serviceProvider)
        {
            _basketDataAccess = basketDataAccess;
            _shoesDataAccess = shoesDataAccess;
            _brandDataAccess = brandDataAccess;
            _modelDataAccess = modelDataAccess;
            _colorDataAccess = colorDataAccess;
        }

        public ClientResult addBasket(addBasketRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");
            if (string.IsNullOrEmpty(request.CustomerId) || request.Price == 0 || string.IsNullOrEmpty(request.ShoesId))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var addresponse = _basketDataAccess.InsertOne(new Basket()
            {
                CreationDate = DateTime.Now,
                CustomerId = request.CustomerId,
                Price = request.Price,
                IsDeleted = false,
                ShoesId = request.ShoesId
            });
            if (!addresponse.Success)
            {
                return Error(message: addresponse.Message);
            }
            return Success();
        }

        public ClientResult deleteBasket(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");
            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var existingBasket = _basketDataAccess.DeleteById(request.Id);
            if (!existingBasket.Success)
            {
                return Error(message: existingBasket.Message);
            }
            return Success();
        }

        public ClientResult<getAllBasketResponse> getAllBasket(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var basketResponse = _basketDataAccess.GetAll();
            if (!basketResponse.Success)
            {
                return Error<getAllBasketResponse>(message: basketResponse.Message);
            }
            var basketList = new List<getOneBasketResponse>();
            foreach (var item in basketResponse.Result)
            {
                var result = getOneBasket(new getOneRequest() { Id = item.Id.ToString() });
                basketList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                basketList = basketList.Where(x => x.Brand.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Color.ToString().Contains(request.SearchValue.ToLower())
                                               || x.Name.ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Description.ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Gender.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Number.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Price.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.Model.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = basketList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "Brand":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Brand).ToList() : basketList.OrderByDescending(c => c.Brand).ToList();
                    break;
                case "Color":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Color).ToList() : basketList.OrderByDescending(c => c.Color).ToList();
                    break;
                case "Name":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Name).ToList() : basketList.OrderByDescending(c => c.Name).ToList();
                    break;
                case "Description":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Description).ToList() : basketList.OrderByDescending(c => c.Description).ToList();
                    break;
                case "Gender":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Gender).ToList() : basketList.OrderByDescending(c => c.Gender).ToList();
                    break;
                case "Number":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Number).ToList() : basketList.OrderByDescending(c => c.Number).ToList();
                    break;
                case "Price":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Price).ToList() : basketList.OrderByDescending(c => c.Price).ToList();
                    break;
                case "Model":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Model).ToList() : basketList.OrderByDescending(c => c.Model).ToList();
                    break;
                 case "CreationDate":
                    basketList = sortDirection == "asc" ? basketList.OrderBy(c => c.Model).ToList() : basketList.OrderByDescending(c => c.Model).ToList();
                    break;
            }
            basketList = basketList.Skip(skip).Take(takeA).ToList();
            var response = new getAllBasketResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = basketList
            };
            return Success<getAllBasketResponse>(data: response);
        }

        public ClientResult<getCustomerBasketsResponse> getCustomerBaskets(getOneRequest request)
        {
            if (request == null)
                return Error<getCustomerBasketsResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getCustomerBasketsResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var existingCustomerBasket=_basketDataAccess.FilterBy(f=> f.CustomerId == request.Id);
            if (!existingCustomerBasket.Success)
            {
                return Error<getCustomerBasketsResponse>(message: existingCustomerBasket.Message);
            }
            var customerBasketList =new getCustomerBasketsResponse();
            foreach (var item in existingCustomerBasket.Result)
            {
                var result = getOneBasket(new getOneRequest() { Id = item.Id.ToString() });
                customerBasketList.data.Add(result.Data);
            }
            return Success<getCustomerBasketsResponse>(data:customerBasketList);
        }

        public ClientResult<getOneBasketResponse> getOneBasket(getOneRequest request)
        {
            if (request == null)
                return Error<getOneBasketResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneBasketResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var response = new getOneBasketResponse();
            var existingBasket = _basketDataAccess.GetById(request.Id);
            if (!existingBasket.Success)
            {
                return Error<getOneBasketResponse>(message: existingBasket.Message);
            }
            response.Id = request.Id;
            response.Price = existingBasket.Entity.Price;
            response.CreationDate = existingBasket.Entity.CreationDate;
            response.CustomerId = existingBasket.Entity.CustomerId;
            response.ShoesId = existingBasket.Entity.ShoesId;
            var existingShoes = _shoesDataAccess.GetById(response.ShoesId);
            if (!existingShoes.Success)
            {
                return Error<getOneBasketResponse>(message: existingShoes.Message);
            }
            response.Description = existingShoes.Entity.Description;
            response.Number = existingShoes.Entity.Number;
            response.Name = existingShoes.Entity.Name;
            response.Gender = existingShoes.Entity.Gender;
            var existingBrand = _brandDataAccess.GetById(existingShoes.Entity.BrandsId);
            if (!existingBrand.Success)
            {
                return Error<getOneBasketResponse>(message: existingBrand.Message);
            }
            response.Brand = existingBrand.Entity.Name;
            var existingModel = _modelDataAccess.GetById(existingShoes.Entity.ModelId);
            if (!existingModel.Success)
            {
                return Error<getOneBasketResponse>(message: existingModel.Message);
            }
            response.Model = existingModel.Entity.Name;
            var existingColor = _colorDataAccess.GetById(existingShoes.Entity.ColorsId);
            if (!existingColor.Success)
            {
                return Error<getOneBasketResponse>(message: existingColor.Message);
            }
            response.Color = existingColor.Entity.Name;
            return Success<getOneBasketResponse>(response);
        }


    }
}
