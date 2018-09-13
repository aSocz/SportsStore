using System.Collections.Generic;

namespace SportsStore.Domain.Entities
{
    public class UserInformation
    {
        public UserInformation()
        {
        }

        public UserInformation(Address address)
        {
            Address = address;
        }

        public int UserInformationId { get; set; }
        public Address Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public void Update(UserInformation updatedUserInformation)
        {
            Address = updatedUserInformation.Address;
        }
    }
}