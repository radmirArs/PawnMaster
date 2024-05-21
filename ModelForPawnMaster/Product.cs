using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelForPawnMaster
{
    public class Product
    {
        public string ProductName { get; set;}
        public string ProductDescription { get; set;}
        public string ProductDateBuy { get; set;}
        public string ProductPriceBuy { get; set; }
        public byte[] ProductImageData { get; set;}
    }
}
