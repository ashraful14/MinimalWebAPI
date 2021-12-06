
using Microsoft.EntityFrameworkCore;
using MinimalWebAPI.Model;
using MinimalWebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


//Endpoints

app.MapGet("api/products/get", async (ProductDbContext dbContext) =>
{
    var products = await dbContext.Products.ToListAsync();
    return Results.Ok(products);
});

app.MapGet("api/products/get/{id}", async (int id,ProductDbContext dbContext) =>
{
    var products = await dbContext.Products.FindAsync(id);
    return Results.Ok(products);
});

app.MapPost("api/products/create", async (Product product, ProductDbContext dbContext) =>
{
    dbContext.Products.Add(product);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("api/products/update", async (Product product, ProductDbContext dbContext) =>
{
    var dbProduct = await dbContext.Products.FindAsync(product.ProdId);
    if (dbProduct == null)
    {
        return Results.NotFound();
    }
    dbProduct.ProdName = product.ProdName;
    dbProduct.Price = product.Price;
   
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/products/delete/{id}", async(int id, ProductDbContext dbContext) =>
{
    var dbProduct = await dbContext.Products.FindAsync(id);
    if (dbProduct == null)
    {
        return Results.NotFound();
    }
    dbContext.Products.Remove(dbProduct);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});


//FromRepository
app.MapGet("productRepo/product", (IProductRepository service) => {
   return service.GetProducts();
});
app.MapGet("productRepo/product/{id}", (int id,IProductRepository service) => 
{
    return service.GetProduct(id);
});
app.MapPost("productRepo/addproduct", (Product product,IProductRepository service) => 
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


app.Run();

