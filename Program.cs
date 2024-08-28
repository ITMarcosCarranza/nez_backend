
using Microsoft.Data.Sqlite;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options=>{
    options.AddPolicy(name:"localHostPolicy",policy=>{
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var connection=new SqliteConnection("Data Source= Nezter.db");
connection.Open();
connection.Close();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("localHostPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
