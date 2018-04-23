using System.Collections.Generic;

namespace SportsStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public int? CategoryId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}