using Newtonsoft.Json;
using Claudia;
using DotNetEnv;
using Npgsql;
using Supabase;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;              
using System.Text;
using System;
using FirstBackend_AI;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      
                      policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());

});

Env.Load();




builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var url = System.Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = System.Environment.GetEnvironmentVariable("SUPABASE_KEY");







builder.Services.Configure<SupabaseConfig>(builder.Configuration.GetSection("Supabase"));

var app = builder.Build();


app.MapGet("/", () => "Supabase is initialised and runnning successfully");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthorization();

app.MapControllers();

app.Run();


