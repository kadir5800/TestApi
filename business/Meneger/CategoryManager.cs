using Business.DTO.BaseObjects;
using Business.DTO.Brand;
using Business.DTO.Category;
using Business.IMeneger;
using EntityFramework.Abstract;
using EntityFramework.Concrete;
using Entitys.Abstract;

namespace Business.Meneger
{
    public class CategoryManager : ZServerService, ICategoryManager
    {
        private readonly ICategoryDataAccess _categoryDataAccess;
        public CategoryManager(IServiceProvider serviceProvider, ICategoryDataAccess categoryDataAccess) : base(serviceProvider)
        {
            _categoryDataAccess = categoryDataAccess;
        }

        public ClientResult addCategory(addCategoryRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var category = new Category()
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                IsDeleted = false,
            };
            var existingcategory = _categoryDataAccess.InsertOne(category);
            if (!existingcategory.Success)
            {
                return Error(message: existingcategory.Message);
            }
            return Success();
        }

        public ClientResult deleteCategory(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deletecategory = _categoryDataAccess.DeleteById(request.Id);
            if (!deletecategory.Success) { Error(message: deletecategory.Message); }
            return Success();
        }

        public ClientResult<getAllCategoryResponse> getAllCategory(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var categoryResponse = _categoryDataAccess.GetAll();
            if (!categoryResponse.Success)
            {
                return Error<getAllCategoryResponse>(message: categoryResponse.Message);
            }
            var categoryList = new List<getOneCategoryResponse>();
            foreach (var item in categoryResponse.Result)
            {
                var result = getOneCategory(new getOneRequest() { Id = item.Id.ToString() });
                categoryList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                categoryList = categoryList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower())
                                               )
                    .ToList();
            }
            recordsTotal = categoryList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    categoryList = sortDirection == "asc" ? categoryList.OrderBy(c => c.CreationDate).ToList() : categoryList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    categoryList = sortDirection == "asc" ? categoryList.OrderBy(c => c.Name).ToList() : categoryList.OrderByDescending(c => c.Name).ToList();
                    break;
            }
            categoryList = categoryList.Skip(skip).Take(takeA).ToList();
            var response = new getAllCategoryResponse()
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = categoryList
            };
            return Success<getAllCategoryResponse>(data: response);
        }

        public ClientResult<getOneCategoryResponse> getOneCategory(getOneRequest request)
        {
            if (request == null)
                return Error<getOneCategoryResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneCategoryResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var category = _categoryDataAccess.GetById(request.Id);
            if (!category.Success) { Error(message: category.Message); }
            var response = new getOneCategoryResponse()
            {
                Id = request.Id,
                Name = category.Entity.Name,
                CreationDate=category.Entity.CreationDate,
            };
            return Success(data: response);
        }

        public ClientResult updateCategory(addCategoryRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var category = _categoryDataAccess.GetById(request.Id);
            if (!category.Success) { Error(message: category.Message); }
            category.Entity.Name = request.Name;
            var updateCategory = _categoryDataAccess.ReplaceOne(category.Entity, request.Id);
            if (!updateCategory.Success) { Error(message: updateCategory.Message); }

            return Success();
        }
    }
}
