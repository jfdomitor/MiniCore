using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
//Lägg till stöd för snabb omladdning endast vid debug
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
#else
builder.Services.AddRazorPages();
#endif

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();