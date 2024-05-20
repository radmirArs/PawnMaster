using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelForPawnMaster
{
    internal class Product
    { 
        public int ProductId { get; set;}
        public string ProductName { get; set;}
        public string ProductDescription { get; set;}
        public string ProductDateBuy { get; set;}
        public byte[] ProductImageData { get; set;}
    }
}
