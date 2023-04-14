using CRUDWithRepository.Core;
using CRUDWithRepository.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWithRepository.Infrastructure.Implemantations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyAppDbContext _context;

        public ProductRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Add(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Product model)
        {
            var product = await _context.Products.FindAsync(model.Id);
            if(product != null)
            {
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.Update(product);
                await _context.SaveChangesAsync();

            }
        }
    }
}
