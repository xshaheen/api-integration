using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(
    c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }); }
);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

var api = app.MapGroup("restaurants");

api.MapGet(
    "by-postcode/{postcode}",
    (string postcode) =>
    {
        var response = new
        {
            postcode,
            restaurants = new[]
            {
                new
                {
                    name = "Spicy Restaurant",
                    ratingStars = "4.77",
                    cuisineTypes = new[]
                    {
                        new { name = "any name" },
                    },
                },
            },
        };

        return TypedResults.Ok(response);
    });

app.Run();