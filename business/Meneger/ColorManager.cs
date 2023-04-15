using Business.DTO.BaseObjects;
using Business.DTO.Color;
using Business.IMeneger;
using EntityFramework.Abstract;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class ColorManager : ZServerService, IColorManager
    {
        private readonly IColorDataAccess _colorDataAccess;
        public ColorManager(IServiceProvider serviceProvider, IColorDataAccess colorDataAccess) : base(serviceProvider)
        {
            _colorDataAccess = colorDataAccess;
        }
        public ClientResult addColor(addUpdateColorRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var Color = new Color()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingColor = _colorDataAccess.InsertOne(Color);
            if (!existingColor.Success)
            {
                return Error(message: existingColor.Message);
            }
            return Success();
        }

        public ClientResult deleteColor(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteColor = _colorDataAccess.DeleteById(request.Id);
            if (!deleteColor.Success) { Error(message: deleteColor.Message); }
            return Success();
        }

        public ClientResult<getAllColorResponse> getAllColor(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var ColorResponse = _colorDataAccess.GetAll();
            if (!ColorResponse.Success)
            {
                return Error<getAllColorResponse>(message: ColorResponse.Message);
            }
            var ColorList = new List<getOneColorResponse>();
            foreach (var item in ColorResponse.Result)
            {
                var result = getOneColor(new getOneRequest() { Id = item.Id.ToString() });
                ColorList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                ColorList = ColorList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = ColorList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    ColorList = sortDirection == "asc" ? ColorList.OrderBy(c => c.CreationDate).ToList() : ColorList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    ColorList = sortDirection == "asc" ? ColorList.OrderBy(c => c.Name).ToList() : ColorList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            ColorList = ColorList.Skip(skip).Take(takeA).ToList();
            var response = new getAllColorResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = ColorList
            };
            return Success<getAllColorResponse>(data: response);
        }

        public ClientResult<getOneColorResponse> getOneColor(getOneRequest request)
        {
            if (request == null)
                return Error<getOneColorResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneColorResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var Colors = _colorDataAccess.GetById(request.Id);
            if (!Colors.Success) { Error(Colors.Message); }
            var response = new getOneColorResponse() { Id = Colors.Entity.Id.ToString(), Name = Colors.Entity.Name, CreationDate = Colors.Entity.CreationDate };

            return Success<getOneColorResponse>(data: response);
        }

        public ClientResult updateColor(addUpdateColorRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var Color = _colorDataAccess.GetById(request.Id);
            if (!Color.Success)
            { return Error(message: Color.Message); }
            Color.Entity.Name = request.Name;
            var updateColor = _colorDataAccess.ReplaceOne(Color.Entity, Color.Entity.Id.ToString());
            if (!updateColor.Success) { return Error(message: updateColor.Message); }
            return Success();
        }
    }
}
