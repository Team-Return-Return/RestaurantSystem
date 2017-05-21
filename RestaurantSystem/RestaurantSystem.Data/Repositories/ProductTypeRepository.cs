﻿namespace RestaurantSystem.Data.Repositories
{
    using RestaurantSystem.Data.Abstraction;
    using RestaurantSystem.Data.Repositories.Abstraction;
    using RestaurantSystem.Models;

    public class ProductTypeRepository : GenericRepository<ProductTypeRepository>, IRepository<ProductTypeRepository>
    {
        public ProductTypeRepository(IRestaurantSystemDbContext context)
            : base(context)
        {
        }
    }
}
