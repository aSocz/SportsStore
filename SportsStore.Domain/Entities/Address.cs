namespace SportsStore.Domain.Entities
{
    public class Address
    {
        public Address()
        {
        }

        public Address(
            string line1,
            string city,
            string state,
            string zip,
            string country,
            string line2 = null,
            string line3 = null)
        {
            Line1 = line1;
            Line2 = line2;
            Line3 = line3;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
        }

        public string Line1 { get; protected set; }
        public string Line2 { get; protected set; }
        public string Line3 { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string Zip { get; protected set; }
        public string Country { get; protected set; }
    }
}