using Microsoft.EntityFrameworkCore;

namespace IWantApp.Endpoints.Products;

public class ProductGetById
{
    public static string Template => "/products/{id:guid}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var product = await context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        
        if (product == null) return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails());

        var results = new ProductResponse(product.Name, product.Category.Name, product.Description, product.HasStock, product.Price, product.Active);
        return Results.Ok(results);
    }


}
