using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Extensions
{
    public static class OrderProcessingExtensions
    {
        public static Address GetAddress(this ShippingInformationViewModel shippingInformation)
        {
            return new Address(
               shippingInformation.Line1,
               shippingInformation.City,
               shippingInformation.State,
               shippingInformation.Zip,
               shippingInformation.Country,
               shippingInformation.Line2,
               shippingInformation.Line3);
        }
    }
}