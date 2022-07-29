using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using P129NLayerArchitectura.Core.Repositories;

namespace P129NLayerArchitectura.Core
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
