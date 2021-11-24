using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using zeroHunger.Model;
using ZeroHunger.Data;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace zeroHunger.Pages.ProductInNeedList
{
    
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CreateModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public ProductInNeed product { get; set; }
        
        
        public async Task OnGet()
        {
            

            /*string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "sel ect top 1 product_id from ProductInNeed Order By product_id desc";
            SqlCommand cmd = new SqlCommand(sql, con);
            newId=cmd.*/

            /*newId = _db.ProductInNeed.FromSqlRaw("select product_id from dbo.ProductInNeed").OrderBy(b => b.product_id).toList;
            newId = _db.ProductInNeed.CreateDbCommand("select top 1 product_id from ProductInNeed Order By product_id desc");*/
            

           /*string connection=_db.
            string sql = "select top 1 product_id from ProductInNeed Order By product_id desc;";
            SqlCommand command = new SqlCommand(sql);
            newId = _db.ProductInNeed.FindAsync()*/

        }
        /*public async Task<IActionResult> OnPost(ProductInNeed p)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename, extension, path;
                filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                extension = Path.GetExtension(product.ImageFile.FileName);
                product.image_path = filename + DateTime.Now.ToString("yymmssfff") + extension;
                path = Path.Combine(wwwRootPath + "/images/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }
                await _db.ProductInNeed.AddAsync(p);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");


            }
            else
            {
                return Page();
            }
        }*/
    }
}
