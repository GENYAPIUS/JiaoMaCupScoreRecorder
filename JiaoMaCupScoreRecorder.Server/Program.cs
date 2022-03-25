var builder = WebApplication.CreateBuilder(args);
builder.Environment.IsDevelopment();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
