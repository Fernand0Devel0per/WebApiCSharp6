using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
    {

        var userClams = new List<Claim>()
        {
            new Claim("Cpf", clientRequest.Cpf),
            new Claim("Name", clientRequest.Name),

        };

        (IdentityResult identity, string userId) result = await userCreator.Create(clientRequest.Email, clientRequest.Password, userClams);

        if (!result.identity.Succeeded) return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/clients/{result.userId}", result.userId);
        
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
