using Business.DTO.BaseObjects;
using Business.DTO.District;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class DistrictManager : ZServerService, IDistrictManager
    {
        private readonly IDistrictDataAccess _districtDataAccess;

        public DistrictManager(IServiceProvider serviceProvider, IDistrictDataAccess districtDataAccess) : base(serviceProvider)
        {
            _districtDataAccess = districtDataAccess;
        }
        public ClientResult addDistrict(addUpdateDistrictRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var District = new District()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingDistrict = _districtDataAccess.InsertOne(District);
            if (!existingDistrict.Success)
            {
                return Error(message: existingDistrict.Message);
            }
            return Success();
        }

        public ClientResult deleteDistrict(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteDistrict = _districtDataAccess.DeleteById(request.Id);
            if (!deleteDistrict.Success) { Error(message: deleteDistrict.Message); }
            return Success();
        }

        public ClientResult<getAllDistrictResponse> getAllDistrict(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var DistrictResponse = _districtDataAccess.GetAll();
            if (!DistrictResponse.Success)
            {
                return Error<getAllDistrictResponse>(message: DistrictResponse.Message);
            }
            var DistrictList = new List<getOneDistrictResponse>();
            foreach (var item in DistrictResponse.Result)
            {
                var result = getOneDistrict(new getOneRequest() { Id = item.Id.ToString() });
                DistrictList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                DistrictList = DistrictList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = DistrictList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    DistrictList = sortDirection == "asc" ? DistrictList.OrderBy(c => c.CreationDate).ToList() : DistrictList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    DistrictList = sortDirection == "asc" ? DistrictList.OrderBy(c => c.Name).ToList() : DistrictList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            DistrictList = DistrictList.Skip(skip).Take(takeA).ToList();
            var response = new getAllDistrictResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = DistrictList
            };
            return Success<getAllDistrictResponse>(data: response);
        }

        public ClientResult<getOneDistrictResponse> getOneDistrict(getOneRequest request)
        {
            if (request == null)
                return Error<getOneDistrictResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneDistrictResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var Districts = _districtDataAccess.GetById(request.Id);
            if (!Districts.Success) { Error(Districts.Message); }
            var response = new getOneDistrictResponse() { Id = Districts.Entity.Id.ToString(), Name = Districts.Entity.Name, CreationDate = Districts.Entity.CreationDate };

            return Success<getOneDistrictResponse>(data: response);
        }

        public ClientResult updateDistrict(addUpdateDistrictRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var District = _districtDataAccess.GetById(request.Id);
            if (!District.Success)
            { return Error(message: District.Message); }
            District.Entity.Name = request.Name;
            var updateDistrict = _districtDataAccess.ReplaceOne(District.Entity, District.Entity.Id.ToString());
            if (!updateDistrict.Success) { return Error(message: updateDistrict.Message); }
            return Success();
        }
    }
}
