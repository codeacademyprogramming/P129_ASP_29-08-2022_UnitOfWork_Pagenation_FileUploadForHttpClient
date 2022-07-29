using P129NLayerArchitectura.Core;
using P129NLayerArchitectura.Core.Repositories;
using P129NLayerArchitectura.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P129NLayerArchitectura.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository != null ? _categoryRepository : new CategoryRepository(_context);

        public IProductRepository ProductRepository => _productRepository != null ? _productRepository : new ProductRepository(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
