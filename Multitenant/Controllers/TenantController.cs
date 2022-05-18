using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Models;
using Multitenant.Repository;
using Newtonsoft.Json;


namespace Multitenant.Controllers
{
    //[Area("Tenant")]
    //[Authorize(Roles = (""))]
    public class TenantController : Controller
    {
        private readonly ITenantDataService _tenantDataService;
        private readonly TenantDataService _tenantDataServiceSP;
        IWebHostEnvironment _webHostEnvironment;

        public TenantController(ITenantDataService _tenantDataService, IWebHostEnvironment webHostEnvironment)
        {
            this._tenantDataService = _tenantDataService;
            _webHostEnvironment = webHostEnvironment;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            TenantDataViewModel model = new TenantDataViewModel
            {
                fileDDL = await _tenantDataService.GetFileTypes(),
                TenantDDL = await _tenantDataService.GetTenants(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatabaseAndFileTables(TenantDataViewModel model)
        {
            //var result = "error";

            if (!ModelState.IsValid)
            {

                if (model.inputFile != null)
                {

                    string folder = "Files";
                    folder += Guid.NewGuid().ToString() + "_" + model.inputFile.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.inputFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    decimal FileSize = new System.IO.FileInfo(serverFolder).Length;
                    var AllocatedSize = (decimal) Helper.BytesToGigabytes((int)FileSize);

                    var DbName = await _tenantDataService.GetDbName(model.TenantId);
                    if ((DbName == null) || (DbName.Length == 0))
                    {
                        throw new Exception("Database Name not found");
                    }
                    else
                    {
                        _tenantDataService.CreateFile(model.FileTypeId, FileSize, model.TenantId, DbName,AllocatedSize,false);
                    }
                }

                //string jsonString = JsonConvert.SerializeObject(_tenantDataServiceSP.CreateDatabaseAndFileTables("Part1", "DB"));
                //return Ok(jsonString);
                //return Json(result);
            }

            return Ok();
        }
    }
}
