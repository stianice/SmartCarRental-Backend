using CarRental.Common;
using CarRental.Common.Constant;
using CarRental.Repository;
using CarRental.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental.WebApi.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly CarRentalContext _db;

        // GET: api/<FileController>

        private readonly IWebHostEnvironment _environment;

        public FilesController(CarRentalContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        // POST api/<FileController>
        [CheckPermission(PermissionEnum.CarManagement)]
        [HttpPost("cars/{car}/images")]
        public async Task<AppResult> PostAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                // 生成随机的文件名
                string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                // 获取静态文件夹的物理路径
                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "staticfiles");

                // 拼接文件的完整路径
                string filePath = Path.Combine(uploadsFolder, fileName);

                // 将文件保存到静态文件夹
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return AppResult.Status200OK(
                    "上传成功",
                    "http://localhost:3000/staticfiles/" + fileName
                );
            }

            throw AppResultException.Status404NotFound();
        }

        [HttpDelete("deleteimage")]
        public AppResult Delete()
        {
            List<string> list = _db.Cars.Select(c => c.Image).ToList();

            List<string> newlist = [];
            list.ForEach(s =>
            {
                string[] str = s.Split('/');
                string fileName = str[^1];
                newlist.Add(fileName);
            });
            string uploadsFolder = Path.Combine(_environment.ContentRootPath, "staticfiles");

            List<string> files = Directory.GetFiles(uploadsFolder).ToList();

            files.ForEach(s =>
            {
                string fileName = s.Split('\\')[^1];
                if (!newlist.Contains(fileName))
                {
                    System.IO.File.Delete(s);
                }
            });

            return AppResult.Status200OK("删除成功");
        }
    }
}
