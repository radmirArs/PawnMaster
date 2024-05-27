using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public class Product
    {

        public Product()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DateBuy { get; set; }
        public string PriceBuy { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsSale { get; set; } = false;
        public string PriceSale { get; set; }
        public string EmployeeName { get; set; }
        public string DateSale { get; set; }

    }
}

