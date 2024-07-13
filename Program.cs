using minimalTodo.Data;
using minimalTodo.Models;
using minimalTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);

//vai gerenciar minhas conexões, garante 1 conexão por requisiao.
builder.Services.AddDbContext<AppDbContext>();

//adicionado Servico do swagger documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("v1/todos", (AppDbContext context) =>
{
    var todo = context.Todos.ToList();
    return Results.Ok(todo);
}).Produces<Todo>();


app.MapPost("v1/todos", (
    AppDbContext context,
    CreateTodoViewModel model) =>
{
    var todo = model.MapTo();

    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

app.MapDelete("v1/todos/{id}", async (AppDbContext context, string id) =>
{
    //var todoItem = await context.Todos.FindAsync(id);
    var todoItemId = Guid.Parse(id);
    var todoItem = await context.Todos.FindAsync(todoItemId);

    if (todoItem == null)
        return Results.NotFound();

    context.Todos.Remove(todoItem);
    await context.SaveChangesAsync();

    return Results.NoContent();
});



app.Run();
