using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Domain.Entities
{
    public class Order
    {
        private const string SummaryDelimiter = "---";
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public bool GiftWrap { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<CartLine> CartLines { get; set; }

        public string GetOrderSummary()
        {
            var cart = new Cart(CartLines);
            var summary = new StringBuilder("Nowe zamówienie").AppendLine(SummaryDelimiter).AppendLine("Produkty: ");
            cart.Items.ToList().ForEach(
                line => summary.AppendLine(
                    $"{line.Quantity} x {line.Product.Price} (wartość: {line.GetSubtotal():C})"));

            summary.AppendLine($"Wartość całkowita: {cart.ComputeTotal():C}")
                   .AppendLine(SummaryDelimiter)
                   .AppendLine("Wysyłka dla: ")
                   .AppendLine(ClientName)
                   .AppendLine(Address.Line1)
                   .AppendLine(Address.Line2 ?? string.Empty)
                   .AppendLine(Address.Line3 ?? string.Empty)
                   .AppendLine(Address.City)
                   .AppendLine(Address.State)
                   .AppendLine(Address.Country)
                   .AppendLine(Address.Zip)
                   .AppendLine(SummaryDelimiter)
                   .AppendLine($"Pakowanie prezentów: {(GiftWrap ? "Tak" : "Nie")}");

            return summary.ToString();
        }
    }
}