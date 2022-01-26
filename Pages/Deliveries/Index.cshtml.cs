using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;

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
        public void OnGet()
        {
            Deliveries = _db.Delivery.Include(d => d.Volunteer).Include(d=>d.Receiver);
            ArrayList message = new ArrayList();
            foreach (var delivery in Deliveries)
            {
                if (delivery.DeliveryStatus.Equals(3))
                {
                    var id = delivery.DeliveryID.ToString();
                    message.Add("Delivery with ID " + id + " is rejected. Please find a new volunteer.");
                }
            }
            foreach (var delivery in Deliveries)
            {
                if (_db.DeliveryItem.Find(delivery.DeliveryID) == null)
                {
                    var delid = delivery.DeliveryID.ToString();
                    message.Add("Delivery with ID " + delid + " has no delivery item. Please add delivery item now.");
                }
            }
            ViewData["Message"] = message;
        }
    }
}
