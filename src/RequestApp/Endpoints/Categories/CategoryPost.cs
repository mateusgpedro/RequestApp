using Microsoft.AspNetCore.Mvc;
using RequestApp.Domain.Products;
using RequestApp.Infra.Data;

namespace RequestApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle = Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDBContext context)
    {
        var category = new Category(categoryRequest.Name, "Test", "Test");

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
