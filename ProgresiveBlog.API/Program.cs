
using ProgresiveBlog.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterServices(typeof(Program));
var app = builder.Build();
app.RegisterPipelineComponents(typeof(Program));

// Configure the HTTP request pipeline.
