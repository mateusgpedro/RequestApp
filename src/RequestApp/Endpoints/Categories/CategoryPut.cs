using Microsoft.AspNetCore.Mvc;
using RequestApp.Infra.Data;

namespace RequestApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle = Action;

    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, ApplicationDBContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
