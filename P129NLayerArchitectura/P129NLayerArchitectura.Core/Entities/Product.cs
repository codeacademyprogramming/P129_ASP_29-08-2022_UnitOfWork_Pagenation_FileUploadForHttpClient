using System;
using System.Collections.Generic;
using System.Text;

namespace P129NLayerArchitectura.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
