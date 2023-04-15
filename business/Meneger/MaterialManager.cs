using Business.DTO.BaseObjects;
using Business.DTO.Material;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class MaterialManager : ZServerService, IMaterialManager
    {
        private readonly IMaterialDataAccess _materialDataAccess;
        public MaterialManager(IServiceProvider serviceProvider, IMaterialDataAccess materialDataAccess) : base(serviceProvider)
        {
            _materialDataAccess = materialDataAccess;
        }
        public ClientResult addMaterial(addUpdateMaterialRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var Material = new Material()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingMaterial = _materialDataAccess.InsertOne(Material);
            if (!existingMaterial.Success)
            {
                return Error(message: existingMaterial.Message);
            }
            return Success();
        }

        public ClientResult deleteMaterial(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteMaterial = _materialDataAccess.DeleteById(request.Id);
            if (!deleteMaterial.Success) { Error(message: deleteMaterial.Message); }
            return Success();
        }

        public ClientResult<getAllMaterialResponse> getAllMaterial(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var MaterialResponse = _materialDataAccess.GetAll();
            if (!MaterialResponse.Success)
            {
                return Error<getAllMaterialResponse>(message: MaterialResponse.Message);
            }
            var MaterialList = new List<getOneMaterialResponse>();
            foreach (var item in MaterialResponse.Result)
            {
                var result = getOneMaterial(new getOneRequest() { Id = item.Id.ToString() });
                MaterialList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                MaterialList = MaterialList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = MaterialList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    MaterialList = sortDirection == "asc" ? MaterialList.OrderBy(c => c.CreationDate).ToList() : MaterialList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    MaterialList = sortDirection == "asc" ? MaterialList.OrderBy(c => c.Name).ToList() : MaterialList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            MaterialList = MaterialList.Skip(skip).Take(takeA).ToList();
            var response = new getAllMaterialResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = MaterialList
            };
            return Success<getAllMaterialResponse>(data: response);
        }

        public ClientResult<getOneMaterialResponse> getOneMaterial(getOneRequest request)
        {
            if (request == null)
                return Error<getOneMaterialResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneMaterialResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var Materials = _materialDataAccess.GetById(request.Id);
            if (!Materials.Success) { Error(Materials.Message); }
            var response = new getOneMaterialResponse() { Id = Materials.Entity.Id.ToString(), Name = Materials.Entity.Name, CreationDate = Materials.Entity.CreationDate };

            return Success<getOneMaterialResponse>(data: response);
        }

        public ClientResult updateMaterial(addUpdateMaterialRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var Material = _materialDataAccess.GetById(request.Id);
            if (!Material.Success)
            { return Error(message: Material.Message); }
            Material.Entity.Name = request.Name;
            var updateMaterial = _materialDataAccess.ReplaceOne(Material.Entity, Material.Entity.Id.ToString());
            if (!updateMaterial.Success) { return Error(message: updateMaterial.Message); }
            return Success();
        }
    }
}
