using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RequestApp.Infra.Data;

namespace RequestApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle = Action;

    public static IResult Action([FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, ApplicationDBContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        
        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
