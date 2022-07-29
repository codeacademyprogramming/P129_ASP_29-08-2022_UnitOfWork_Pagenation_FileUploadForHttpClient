using P129NLayerArchitectura.Core.Entities;
using P129NLayerArchitectura.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P129NLayerArchitectura.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
    }
}
