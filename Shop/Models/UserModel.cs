using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Shop.Models
{
    public class UserModel : IdentityUser
    {
        public List<CartModel> CartList { get; set; }

        public UserModel()
        {
            CartList = new List<CartModel>();
        }
    }
}
