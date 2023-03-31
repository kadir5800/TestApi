using Business.DTO.BaseObjects;
using Business.DTO.Brand;
using Business.DTO.Campaign;
using Business.IMeneger;
using EntityFramework.Abstract;
using EntityFramework.Concrete;
using Entitys.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Business.Meneger
{
    public class CampaignManager : ZServerService, ICampaignManager
    {
        private readonly ICampaignDataAccess _campaignDataAccess;
        private readonly IHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CampaignManager(IServiceProvider serviceProvider, ICampaignDataAccess campaignDataAccess, IHostEnvironment environment, IHttpContextAccessor httpContextAccessor) : base(serviceProvider)
        {
            _campaignDataAccess = campaignDataAccess;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public ClientResult addCampaign(addUpdateCampaignRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Description) || request.StartDate <= DateTime.MinValue || request.EndDate >= DateTime.MaxValue || request.EndDate <= DateTime.MinValue || request.StartDate >= DateTime.MaxValue)
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var campaign = new Campaign();
            if (string.IsNullOrEmpty(request.Image))
            {

                campaign.CreationDate = DateTime.Now;
                campaign.Description = request.Description;
                campaign.Name = request.Name;
                campaign.EndDate = request.EndDate;
                campaign.IsDeleted = false;
                campaign.Image = "";
                campaign.StartDate = request.StartDate;
               
            }
            else
            {

                byte[] imageBytes = Convert.FromBase64String(request.Image);

                // Rastgele dosya adı oluştur
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                // Dosya yolunu oluştur
                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/images", fileName);

                // Resmi diske kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }
                var host = _httpContextAccessor.HttpContext.Request.Host.Value;
                campaign.CreationDate = DateTime.Now;
                campaign.Description = request.Description;
                campaign.Name = request.Name;
                campaign.EndDate = request.EndDate;
                campaign.IsDeleted = false;
                campaign.Image = $"http://{host}/images/{fileName}";
                campaign.StartDate = request.StartDate;

            }
            
            var addCampaign = _campaignDataAccess.InsertOne(campaign);
            if (!addCampaign.Success) { return Error(message: addCampaign.Message); }
            return Success();
        }

        public ClientResult deleteCampaign(getOneRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var deleteCampaign=_campaignDataAccess.DeleteById(request.Id);
            if (!deleteCampaign.Success) { Error(message: deleteCampaign.Message); }
            if (!string.IsNullOrEmpty(deleteCampaign.Entity.Image))
            {
                var fileName= deleteCampaign.Entity.Image.Substring(deleteCampaign.Entity.Image.LastIndexOf('/') + 1);
                string filePath = Path.Combine(_environment.ContentRootPath, "wwwroot/images", fileName);
                File.Delete(filePath);
            }
            return Success();
        }

        public ClientResult<getAllCampaignResponse> getAllCampaign(dataTableRequest request)
        {
            int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
            int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;
            int recordsTotal = 0;
            var campaignResponse = _campaignDataAccess.GetAll();
            if (!campaignResponse.Success)
            {
                return Error<getAllCampaignResponse>(message: campaignResponse.Message);
            }
            var campaignList = new List<getOneCampaignResponse>();
            foreach (var item in campaignResponse.Result)
            {
                var result = getOneCampaign(new getOneRequest() { Id = item.Id.ToString() });
                campaignList.Add(result.Data);
            }

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                campaignList = campaignList.Where(x => x.Name.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                                || x.StartDate.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                                || x.EndDate.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                                || x.Description.ToString().ToLower().Contains(request.SearchValue.ToLower())
                                               || x.CreationDate.ToString().Contains(request.SearchValue.ToLower()))
                    .ToList();
            }
            recordsTotal = campaignList.Count();
            var takeA = request.Length == "-1" ? recordsTotal : pageSize;
            takeA = takeA == 0 ? 10 : takeA;

            var sortColumn = request.SortColumn;
            var sortDirection = request.SortColumnDir;
            switch (sortColumn)
            {
                case "CreationDate":
                    campaignList = sortDirection == "asc" ? campaignList.OrderBy(c => c.CreationDate).ToList() : campaignList.OrderByDescending(c => c.CreationDate).ToList();
                    break;
                case "Name":
                    campaignList = sortDirection == "asc" ? campaignList.OrderBy(c => c.Name).ToList() : campaignList.OrderByDescending(c => c.Name).ToList();
                    break;
                case "StartDate":
                    campaignList = sortDirection == "asc" ? campaignList.OrderBy(c => c.StartDate).ToList() : campaignList.OrderByDescending(c => c.StartDate).ToList();
                    break;
                case "EndDate":
                    campaignList = sortDirection == "asc" ? campaignList.OrderBy(c => c.EndDate).ToList() : campaignList.OrderByDescending(c => c.EndDate).ToList();
                    break;
                case "Description":
                    campaignList = sortDirection == "asc" ? campaignList.OrderBy(c => c.Description).ToList() : campaignList.OrderByDescending(c => c.Description).ToList();
                    break;
            }
            campaignList = campaignList.Skip(skip).Take(takeA).ToList();
            var response = new getAllCampaignResponse
            {
                draw = request.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = campaignList
            };
            return Success<getAllCampaignResponse>(data: response);
        }

        public ClientResult<getOneCampaignResponse> getOneCampaign(getOneRequest request)
        {
            if (request == null)
                return Error<getOneCampaignResponse>(message: "Request Null");

            if (string.IsNullOrEmpty(request.Id))
            {
                return Error<getOneCampaignResponse>(message: "Gerekli Alanları Doldurunuz");
            }
            var campaign=_campaignDataAccess.GetById(request.Id);
            if (!campaign.Success) { Error<getOneCampaignResponse>(message: campaign.Message); }
            var response=new getOneCampaignResponse()
            {
                CreationDate = campaign.Entity.CreationDate,
                Description = campaign.Entity.Description,
                EndDate = campaign.Entity.EndDate,
                Id = request.Id,
                Name = campaign.Entity.Name,
                StartDate = campaign.Entity.StartDate
            };
            return Success<getOneCampaignResponse>(data:response);
        }

        public ClientResult updateCampaign(addUpdateCampaignRequest request)
        {
            if (request == null)
                return Error(message: "Request Null");

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Id))
            {
                return Error(message: "Gerekli Alanları Doldurunuz");
            }
            var campaign = _campaignDataAccess.GetById(request.Id);
            if (!campaign.Success)
            { return Error(message: campaign.Message); }
            campaign.Entity.Name = request.Name;
            campaign.Entity.Description = request.Description;
            campaign.Entity.EndDate = request.EndDate;
            campaign.Entity.StartDate = request.StartDate;
               
            var updateBrand = _campaignDataAccess.ReplaceOne(campaign.Entity, campaign.Entity.Id.ToString());
            if (!updateBrand.Success) { return Error(message: updateBrand.Message); }
            return Success();
        }
    }
}
