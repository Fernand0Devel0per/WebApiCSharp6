using Microsoft.EntityFrameworkCore;

namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders{id}";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize]
    public async static Task<IResult> Action([FromRoute]Guid? id, ApplicationDbContext context, HttpContext http, UserManager<IdentityUser> userManager)
    {

        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeCode = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode");
        
        var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
        if (order.ClientId != clientId.Value && employeeCode == null) return Results.Forbid();

        var client = await userManager.FindByIdAsync(order.ClientId);

        var productResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name));

        var orderResponse = new OrderResponse(order.Id, client.Email, productResponse, order.DeliveryAddress);
        
        return Results.Ok(orderResponse);
    }
    
}
