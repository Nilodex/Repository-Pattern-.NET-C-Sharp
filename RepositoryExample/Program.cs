using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryExample.Interfaces;
using RepositoryExample.Models;
using RepositoryExample.Repository;
using RepositoryExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryPatternContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMvc();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //dependency injection

builder.Services.AddEndpointsApiExplorer(); //swagger
builder.Services.AddSwaggerGen(); //swagger

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API");
});

app.UseHttpsRedirection();

app.MapGet("/User/todos", ([FromServices] IRepository<User> user) =>
{
    UserService service = new UserService(user);

    return Results.Ok(service.GetAllUsers());
});


app.MapGet("/User/{id}", ([FromRoute] int id, [FromServices] IRepository<User> user) =>
{
    UserService service = new UserService(user);
    if (service.GetUserById(id) is null) return Results.NotFound("Usuário inexistente");

    return Results.Ok(service.GetUserById(id));
});

app.MapPost("/User", ([FromBody] User model, [FromServices] IRepository<User> user) =>
{
    UserService service = new UserService(user);
    if (model.Id != 0) return Results.BadRequest("Impossivel criar usuário colocando o ID manualmente, deixe o id : 0 ou remova o id do body e tente novamente");
    service.AddUser(model);

    return Results.Created($"/users/{model.Id}", model.Id);
});

app.MapDelete("/User/{id}", ([FromRoute] int id, [FromServices] IRepository<User> user) =>
{
    UserService service = new UserService(user);
    if (service.GetUserById(id) is null) return Results.BadRequest("Id inválido ou usuário inexistente");
    
    service.DeleteUser(service.GetUserById(id));
    return Results.Ok($"O usuário de id {id} foi removido");
});

app.MapPut("/User", async ([FromBody] User model, [FromServices] IRepository<User> user) =>
{
    UserService service = new UserService(user);
    service.UpdateUser(model);

    return Results.Ok($"O usuário de id {model.Id} foi alterado.");
});


app.Run();