using Dapper;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employee;

public class EmployeeGetAll
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        if (page < 1 || rows < 1) return Results.BadRequest();
        {

        }
        return await Results.Ok(query.Execute(page.Value, rows.Value));
    
    }

    /* Consulta sem performance
     public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.Skip((page-1)*rows).Take(rows).ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }

        return Results.Ok(employees);
    }
     */

}
