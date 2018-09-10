using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Extensions
{
    public static class OrderProcessingExtensions
    {
        public static Address GetAddress(this ShippingInformationViewModel shippingInformation)
        {
            return new Address(
               shippingInformation.Address.Line1,
               shippingInformation.Address.City,
               shippingInformation.Address.State,
               shippingInformation.Address.Zip,
               shippingInformation.Address.Country,
               shippingInformation.Address.Line2,
               shippingInformation.Address.Line3);
        }
    }
}