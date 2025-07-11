var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDomainLayer();
builder.Services.ConfigureMeditaRLayer();
builder.Services.ConfigureInfrastructureLayer(builder);
builder.Services.ConfigureApplicationLayer(builder);
builder.Services.ConfigureAPILayer(builder);

// Builde application.
var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseExceptionHandler(options => { });
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
