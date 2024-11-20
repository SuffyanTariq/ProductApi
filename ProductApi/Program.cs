
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var myAllowSpecification = "_myAllowSpecification";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors(x =>
//{
//    x.AddPolicy(name: myAllowSpecification,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://example.com");
//                      });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecification, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DevConnection");
builder.Services.AddDbContext<ProductDBContext>(x =>
x.UseSqlServer(connectionString));

builder.Services.AddCors();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: myAllowSpecification,
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:4200/")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(myAllowSpecification);
app.MapControllers();
app.Run();
