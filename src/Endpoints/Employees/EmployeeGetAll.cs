using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employee;

public class EmployeeGetAll
{
    public static string Template => "/employees";

    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var employees = db.Query<EmployeeResponse>(
            @"select Email, ClaimValue as Name
                from AspNetUsers u inner
                join AspNetUserClaims c
                on u.id = c.UserId and claimtype = 'Name'
                Order by name
                OFFSET (@page -1) * @rows Rows Fetch Next @rows Rows Only", new {page, rows }
            );
        return Results.Ok(employees);
    
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
