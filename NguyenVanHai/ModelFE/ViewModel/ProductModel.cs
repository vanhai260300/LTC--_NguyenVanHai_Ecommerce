using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelFE.ViewModel
{
    public class ProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public decimal? UniCost { get; set; }
    }
}
