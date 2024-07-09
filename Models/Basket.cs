using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSGTechnical.Domain.Interfaces;

namespace CMSGTechnical.Domain.Models
{
    public class Basket : IEntity
    {
        public int Id { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public int UserId { get; set; }

        public decimal DeliveryFee { get; set; } = 2.00m; // Default delivery fee

           // Method to calculate the total
        public decimal GetTotal()

        {

            decimal itemsTotal = MenuItems.Sum(item => item.Price); // Sum of all menu item prices
            return itemsTotal + DeliveryFee; // Total including delivery fee

        }
    }    
}
