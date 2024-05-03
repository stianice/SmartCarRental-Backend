using CarRental.Common;
using CarRental.Services;
using CarRental.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Checks")]
    public class CheckController : ControllerBase
    {
        private readonly CheckService _checkService;

        public CheckController(CheckService checkService)
        {
            _checkService = checkService;
        }

        [HttpGet]
        public AppResult List()
        {
            var list = _checkService.GetAllCheck();
            return AppResult.Status200OKWithData(list);
        }

        [HttpPost]
        public AppResult Create(PostCheckReq req)
        {
            _checkService.AddCheck(req);
            return AppResult.Status200OKWithMessage("车辆入库成功");
        }

        [HttpPatch("deletebyids")]
        public AppResult DeleteByids(long[] ids)
        {
            var row = _checkService.DeleteByIds(ids);
            return AppResult.Status200OKWithMessage($"成功删除{row}行数据");
        }

        [HttpPost("search")]
        public AppResult GetCheckByCondiction(CheckSearchReq req)
        {
            var list = _checkService.GetByCondiction(req);
            return AppResult.Status200OKWithData(list);
        }
    }
}
