// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using CoWorkingProject.Server.Data;
using CoWorkingProject.Server.Services;
using CoWorkingProject.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(
			new JsonStringEnumConverter());
	});


builder.Services.AddDbContext<CoWorkingContext>(
	opt =>
	{
        _ = opt.UseNpgsql(builder.Configuration.GetConnectionString("CoWorkingDbContext"));
	}, ServiceLifetime.Transient);

builder.Services.AddCors(options =>
{
	options.AddPolicy("localhost:6732", policy =>
	{
		policy.WithOrigins("https://localhost:6732")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("localhost:6732");

app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

await app.RunAsync();
