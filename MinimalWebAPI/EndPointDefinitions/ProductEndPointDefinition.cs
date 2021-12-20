using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MinimalWebAPI.EndPointDefinitions
{
    public class ProductEndPointDefinition : IEndPointDefinition
    {
        public void DefineEndPoints(WebApplication app)
        {
            #region Test
            //    app.MapGet("api/products/get", async (ProductDbContext dbContext) =>
            //    {
            //        var products = await dbContext.Products.ToListAsync();
            //        return Results.Ok(products);
            //    });

            //    app.MapGet("api/products/get/{id}", async (int id, ProductDbContext dbContext) =>
            //    {
            //        var products = await dbContext.Products.FindAsync(id);
            //        return Results.Ok(products);
            //    });

            //    app.MapPost("api/products/create", async (Product product, ProductDbContext dbContext) =>
            //    {
            //        dbContext.Products.Add(product);
            //        await dbContext.SaveChangesAsync();
            //        return Results.Ok();
            //    });

            //    app.MapPut("api/products/update", async (Product product, ProductDbContext dbContext) =>
            //    {
            //        var dbProduct = await dbContext.Products.FindAsync(product.ProdId);
            //        if (dbProduct == null)
            //        {
            //            return Results.NotFound();
            //        }
            //        dbProduct.ProdName = product.ProdName;
            //        dbProduct.Price = product.Price;

            //        await dbContext.SaveChangesAsync();
            //        return Results.NoContent();
            //    });

            //    app.MapDelete("api/products/delete/{id}", async (int id, ProductDbContext dbContext) =>
            //    {
            //        var dbProduct = await dbContext.Products.FindAsync(id);
            //        if (dbProduct == null)
            //        {
            //            return Results.NotFound();
            //        }
            //        dbContext.Products.Remove(dbProduct);
            //        await dbContext.SaveChangesAsync();
            //        return Results.Ok();
            //    });
            //}
            #endregion

            
            app.MapGet("productRepo/product",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] (IProductRepository service) =>
            {
                return service.GetProducts();
            });
            app.MapGet("productRepo/product/{id}", (int id, IProductRepository service) =>
            {
                return service.GetProduct(id);
            });
            app.MapPost("productRepo/addproduct", (Product product, IProductRepository service) =>
            {
                service.AddProduct(product);
            });
            app.MapPut("productRepo/update", (Product product, IProductRepository service) =>
            {
                service.UpdateProduct(product);
            });
            app.MapDelete("productRepo/delete/{id}", (int id, IProductRepository service) =>
            {
                service.DeleteProduct(id);
            });

        }
        public void DefineServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductsRepository>();
        }
    }
}
