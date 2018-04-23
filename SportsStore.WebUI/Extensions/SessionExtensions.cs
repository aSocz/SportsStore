using System.Web;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Extensions
{
    public static class SessionExtensions
    {
        private const string CartSessionKey = "Cart";

        public static Cart GetCart(this HttpSessionStateBase session)
        {
            var cart = (Cart)session[CartSessionKey];
            if (cart == null)
            {
                cart = new Cart();
                session[CartSessionKey] = cart;
            }

            return cart;
        }

        public static void ClearCart(this HttpSessionStateBase session)
        {
            session[CartSessionKey] = null;
        }
    }
}