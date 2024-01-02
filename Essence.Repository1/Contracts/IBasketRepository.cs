using Essence.Data.DTO.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IBasketRepository
    {
        public Task<(string,string)> AddToCart(int sizeId, int colorId);
        public (string,string) RemoveFromCart(int id);
        public List<BasketDTO> GetBaskets();
    }
}
