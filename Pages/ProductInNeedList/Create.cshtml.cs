using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ProductInNeedList
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
        public void OnGet()
        {
        }
    }
}
