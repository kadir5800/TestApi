using Business.DTO.BaseObjects;
using Business.DTO.Brand;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class BrandManager : ZServerService, IBrandManager
    {
        private readonly IBrandDataAccess _brandDataAccess;
        public BrandManager(IServiceProvider serviceProvider, IBrandDataAccess brandDataAccess) : base(serviceProvider)
        {
            _brandDataAccess = brandDataAccess;
        }

        public ClientResult addBrand(addUpdateBrandRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var brand = new Brand()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingBrand = _brandDataAccess.InsertOne(brand);
            if (!existingBrand.Success)
            {
                return Error(message: existingBrand.Message);
            }
            return Success();
        }

        public ClientResult deleteBrand(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteBrand = _brandDataAccess.DeleteById(request.Id);
            if (!deleteBrand.Success) { Error(message: deleteBrand.Message); }
            return Success();
        }

        public ClientResult<getAllBrandResponse> getAllBrand(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var brandResponse = _brandDataAccess.GetAll();
            if (!brandResponse.Success)
            {
                return Error<getAllBrandResponse>(message: brandResponse.Message);
            }
            var brandList = new List<getOneBrandResponse>();
            foreach (var item in brandResponse.Result)
            {
                var result = getOneBrand(new getOneRequest() { Id = item.Id.ToString() });
                brandList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                brandList = brandList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = brandList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    brandList = sortDirection == "asc" ? brandList.OrderBy(c => c.CreationDate).ToList() : brandList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    brandList = sortDirection == "asc" ? brandList.OrderBy(c => c.Name).ToList() : brandList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            brandList = brandList.Skip(skip).Take(takeA).ToList();
            var response = new getAllBrandResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = brandList
            };
            return Success<getAllBrandResponse>(data: response);
        }

        public ClientResult<getOneBrandResponse> getOneBrand(getOneRequest request)
        {
            if (request == null)
                return Error<getOneBrandResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneBrandResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var brands = _brandDataAccess.GetById(request.Id);
            if (!brands.Success) { Error(brands.Message); }
            var response = new getOneBrandResponse() { Id = brands.Entity.Id.ToString(), Name = brands.Entity.Name, CreationDate = brands.Entity.CreationDate };

            return Success<getOneBrandResponse>(data: response);
        }

        public ClientResult updateBrand(addUpdateBrandRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var brand = _brandDataAccess.GetById(request.Id);
            if (!brand.Success)
            { return Error(message: brand.Message); }
            brand.Entity.Name = request.Name;
            var updateBrand = _brandDataAccess.ReplaceOne(brand.Entity, brand.Entity.Id.ToString());
            if (!updateBrand.Success) { return Error(message: updateBrand.Message); }
            return Success();
        }
    }
}
