using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Zero_Hunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Delivery> Deliveries { set; get; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async void OnGet()
        {
            //Deliveries = _db.Delivery;
            Deliveries = _db.Delivery.Include(d => d.User);
          
                          /*  .Where(d => d.User.UserType == 2)
                            .AsNoTracking()
                            .ToListAsync();*/
            //var query = _db.Delivery.Include(c => c.User);
            //Deliveries = await query.ToListAsync();
        }
    }
}
