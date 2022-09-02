using Dapper;
using IWantApp.Endpoints.Employee;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        return await db.QueryAsync<EmployeeResponse>(
            @"select Email, ClaimValue as Name
                from AspNetUsers u inner
                join AspNetUserClaims c
                on u.id = c.UserId and claimtype = 'Name'
                Order by name
                OFFSET (@page -1) * @rows Rows Fetch Next @rows Rows Only", new { page, rows }
            );
    }
}
