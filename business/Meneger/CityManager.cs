using Business.DTO.BaseObjects;
using Business.DTO.City;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class CityManager : ZServerService, ICityManager
    {
        private readonly ICityDataAccess _CityDataAccess;
        public CityManager(IServiceProvider serviceProvider, ICityDataAccess CityDataAccess) : base(serviceProvider)
        {
            _CityDataAccess = CityDataAccess;
        }

        public ClientResult addCity(addUpdateCityRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var City = new City()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingCity = _CityDataAccess.InsertOne(City);
            if (!existingCity.Success)
            {
                return Error(message: existingCity.Message);
            }
            return Success();
        }

        public ClientResult deleteCity(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteCity = _CityDataAccess.DeleteById(request.Id);
            if (!deleteCity.Success) { Error(message: deleteCity.Message); }
            return Success();
        }

        public ClientResult<getAllCityResponse> getAllCity(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var CityResponse = _CityDataAccess.GetAll();
            if (!CityResponse.Success)
            {
                return Error<getAllCityResponse>(message: CityResponse.Message);
            }
            var CityList = new List<getOneCityResponse>();
            foreach (var item in CityResponse.Result)
            {
                var result = getOneCity(new getOneRequest() { Id = item.Id.ToString() });
                CityList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                CityList = CityList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = CityList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    CityList = sortDirection == "asc" ? CityList.OrderBy(c => c.CreationDate).ToList() : CityList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    CityList = sortDirection == "asc" ? CityList.OrderBy(c => c.Name).ToList() : CityList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            CityList = CityList.Skip(skip).Take(takeA).ToList();
            var response = new getAllCityResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = CityList
            };
            return Success<getAllCityResponse>(data: response);
        }

        public ClientResult<getOneCityResponse> getOneCity(getOneRequest request)
        {
            if (request == null)
                return Error<getOneCityResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneCityResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var Citys = _CityDataAccess.GetById(request.Id);
            if (!Citys.Success) { Error(Citys.Message); }
            var response = new getOneCityResponse() { Id = Citys.Entity.Id.ToString(), Name = Citys.Entity.Name, CreationDate = Citys.Entity.CreationDate };

            return Success<getOneCityResponse>(data: response);
        }

        public ClientResult updateCity(addUpdateCityRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var City = _CityDataAccess.GetById(request.Id);
            if (!City.Success)
            { return Error(message: City.Message); }
            City.Entity.Name = request.Name;
            var updateCity = _CityDataAccess.ReplaceOne(City.Entity, City.Entity.Id.ToString());
            if (!updateCity.Success) { return Error(message: updateCity.Message); }
            return Success();
        }
    }
}
