using CMSGTechnical.Domain.Models;
using CMSGTechnical.Mediator.Basket;
using CMSGTechnical.Mediator.Dtos;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CMSGTechnical.Code
{
    public class BasketChangedEventArgs : EventArgs
    {
        public BasketDto Basket { get; set; }
    }

    public class BasketService
    {
        public event EventHandler<BasketChangedEventArgs> OnChange;

        public BasketDto Basket { get; }

        public BasketService(BasketDto basket)
        {
            Basket = basket;
        }

        public async Task Add(MenuItemDto item)
        {
            var existingItem = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                item.Quantity = 1;
                Basket.MenuItems.Add(item);
            }
            OnChange?.Invoke(this, new BasketChangedEventArgs() { Basket = Basket });
        }

        public async Task Remove(MenuItemDto item)
        {
            var existingItem = Basket.MenuItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity--;
                if (existingItem.Quantity <= 0)
                {
                    Basket.MenuItems.Remove(existingItem);
                }
            }
            OnChange?.Invoke(this, new BasketChangedEventArgs() { Basket = Basket });
        }

        public decimal GetTotal()
        {
            decimal subtotal = Basket.MenuItems.Sum(i => i.Price * i.Quantity);
            decimal deliveryFee = 2.00m; // Fixed delivery fee
            return subtotal + deliveryFee;
        }
    }
}

