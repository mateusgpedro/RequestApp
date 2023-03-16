using Dapper;
using Microsoft.Data.SqlClient;
using RequestApp.Infra.Data;

namespace RequestApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle = Action;

    public static IResult Action(int? page, int? rows, QueryAllUserWithClaimName query)
    {
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
