using IWantApp.Domain.Orders;

namespace IWantApp.Endpoints.Clients;

public class OrderPost
{
    public static string Template => "/orders";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims.First(c => c.Type == "Name").Value;

        var products = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();

        var order = new Order(clientId, clientName, products, orderRequest.DeliveryAddress);
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();


        return Results.Created($"/orders/{order.Id}", order.Id);
        
    }
    
}

//var newUser= new IdentityUser {UserName = clientRequest.Email, Email = clientRequest.Email };
//var result = await userManager.CreateAsync(newUser, clientRequest.Password);

//var userClams = new List<Claim>()
//{
//    new Claim("Cpf", clientRequest.Cpf),
//    new Claim("Name", clientRequest.Name),

//};

//if (!result.Succeeded) return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

//var clamResult = await userManager.AddClaimsAsync(newUser, userClams);
