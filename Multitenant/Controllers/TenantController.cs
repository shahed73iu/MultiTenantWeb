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

        public TenantController(ITenantDataService _tenantDataService)
        {
            this._tenantDataService = _tenantDataService;
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
            var result = "error";

            if (!ModelState.IsValid)
            {
                string jsonString = JsonConvert.SerializeObject(_tenantDataServiceSP.CreateDatabaseAndFileTables("Part1", "DB"));
                return Ok(jsonString);
                return Json(result);
            }

            

            return View(result);
        }
    }
}
