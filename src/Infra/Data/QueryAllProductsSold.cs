namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        return await db.QueryAsync<EmployeeResponse>(
            @"select
                p.Id,
                p.Name
                count(*) Amount
                from Orders o inner
                join OrderProducts op on o.Id = op.OrdersId
                inner join Products p on p.Id = op.ProductsId
                Group by p.Id, p.Name
                Order By Amount desc");
    }
}
