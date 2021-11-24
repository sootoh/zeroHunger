using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace ZeroHunger.Controllers
{
    [Route("api/ProductInNeed")]
    [ApiController]
    public class ProductInNeedController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductInNeedController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        public ActionResult Index()
        {
            return View("~/Views/Image/Add.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.ProductInNeed.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var pidFromDb = await _db.ProductInNeed.FirstOrDefaultAsync(u => u.product_id == id);
            if (pidFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.ProductInNeed.Remove(pidFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_id,product_name,product_description,amount,visibility,image_path")]ProductInNeed pin)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename, extension, path;
                filename = Path.GetFileNameWithoutExtension(pin.ImageFile.FileName);
                extension = Path.GetExtension(pin.ImageFile.FileName);
                pin.image_path = filename + DateTime.Now.ToString("yymmssfff") + extension;
                path = Path.Combine(wwwRootPath + "/images/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await pin.ImageFile.CopyToAsync(fileStream);
                }
                await _db.ProductInNeed.AddAsync(pin);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");


            }
            else
            {
                return View();
            }

        }*/
    }
}
