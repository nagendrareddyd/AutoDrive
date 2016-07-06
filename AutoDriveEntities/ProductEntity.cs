using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveEntities
{
    public class ProductEntity
    {
        public string Id { get; set; }        
        public int ProductId { get; set; }        
        public string ProductName { get; set; }        
        public int Price { get; set; }        
        public string Category { get; set; }
    }
}
