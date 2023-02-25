using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoryInterfaces;
using BP.Ecommerce.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BP.Ecommerce.Infraestructure.RepositoriesImplementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Product>> GetQueryable(string? search, int limit, int offset, string sort, string order)
        {
            var query = _context.Products.Where(b => b.State == Status.Vigente.ToString());

            if (!string.IsNullOrEmpty(search))
            {
                query = from product in query
                        where product.Name.Contains(search) || product.Description.Contains(search) || product.ProductTypeId.ToString().Equals(search)
                        select product;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToUpper())
                {
                    case "NAME":
                        query = order.ToUpper() == "ASC"
                            ? query.OrderBy(b => b.Name)
                            : order.ToUpper() == "DESC"
                                ? query.OrderByDescending(b => b.Name)
                                : throw new ArgumentException($"Argumento: {order} no valido");
                        break;
                    case "PRICE":
                        query = order.ToUpper() == "ASC"
                            ? query.OrderBy(b => b.Price)
                            : order.ToUpper() == "DESC"
                                ? query.OrderByDescending(b => b.Price)
                                : throw new ArgumentException($"Argumento: {order} no valido");
                        break;
                    default:
                        throw new ArgumentException($"Argumento: {sort} no valido");
                }
            }
            if(limit != 0)
            {
                query = query.Skip(offset).Take(limit);
            }

            return query;
        }

        public async Task<IQueryable<Product>> GetQueryableByIdAsync(Guid id)
        {
            var query = _context.Products.Where(b => b.Id == id && b.State == Status.Vigente.ToString());
            Product productExist = await query.SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"No existe producto con id: {id}");
            }
            return query;
        }

        public async Task<IQueryable<Product>> PostAsync(Product product)
        {
            var query = _context.Products.Where(b => b.Name == product.Name);
            if (product.Stock < 0)
                throw new ArgumentException("El stock debe ser mayor igual a 0");

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<IQueryable<Product>> PutAsync(Product product)
        {
            var query = _context.Products.Where(b => b.Id == product.Id);
            bool productExist = _context.Products.Any(b => b.Id == product.Id && b.State == Status.Vigente.ToString());
            if (!productExist)
                throw new ArgumentException($"No existe producto con id: {product.Id}");

            if (product.Stock < 0)
                throw new ArgumentException("El stock debe ser mayor igual a 0");

            product.DateModification = DateTime.Now;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return query;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            Product productExist = await _context.Products.Where(b => b.Id == id && b.State == Status.Vigente.ToString()).SingleOrDefaultAsync();
            if (productExist == null)
            {
                throw new Exception($"No existe producto con id: {id}");
            }
            productExist.DateDeleted = DateTime.Now;
            productExist.State = Status.Eliminado.ToString();
            _context.Products.Update(productExist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
