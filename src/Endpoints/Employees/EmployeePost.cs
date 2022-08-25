using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employee;

public class EmployeePost
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser {UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result =  userManager.CreateAsync(user, employeeRequest.Password).Result;
        
        if (!result.Succeeded) return Results.BadRequest(result.Errors.First());

        var clamResult = userManager.AddClaimAsync(user, new Claim("EmployeeCode", employeeRequest.EmployeeCode)).Result;
        
        if (!result.Succeeded) return Results.BadRequest(clamResult.Errors.First());

        clamResult = userManager.AddClaimAsync(user, new Claim("Name", employeeRequest.Name)).Result;

        if (!result.Succeeded) return Results.BadRequest(clamResult.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);
        
    }
    
}
